using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Projectiles;
using UnityEngine;

public class WeaponProjectileEmitter : MonoBehaviour
{
    public PlayerStats player_stats;
    public PlayerController player_controller;

    public float shooting_force;
    public int shooting_damage;

    private AudioSource sound_player;
    public GameObject projectile;
    public float projectile_duration;

    private Transform shoulder;
    public float weapon_length;

    private float shooting_cooldown;
    private enum Buttons
    {
        MOUSE_BUTTON_LEFT = 0,
        MOUSE_BUTTON_RIGHT = 1,
        MOUSE_BUTTON_MIDDLE = 2
    }

    private Queue<GameObject> projectile_storage;

    // Start is called before the first frame update
    void Start()
    {
        shoulder = transform.parent.transform;
        projectile_storage = new Queue<GameObject>();
        sound_player = GetComponent<AudioSource>();

        ProjectileAttributes pa = projectile.GetComponent<ProjectileAttributes>();
        if (pa)
        {
            sound_player.clip = pa.fired_sound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting_cooldown > 0.0f)
        {
            shooting_cooldown -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown((int)Buttons.MOUSE_BUTTON_LEFT) || Input.GetMouseButton((int)Buttons.MOUSE_BUTTON_LEFT))
        {
            if (shooting_cooldown <= 0.0f)
            {
                shooting_cooldown = 1.0f / player_stats.ProjectilesPerSecond;
                Vector2 weaponToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

                GameObject _projectile = GetProjectileForShooting();
                AdjustProjectilePositionAndRotation(_projectile);

                weaponToMouseDir.Normalize();
                _projectile.GetComponent<Rigidbody2D>().AddForce(weaponToMouseDir * shooting_force);
                _projectile.GetComponent<ProjectileBehavior>().shotFrom = this;
                sound_player.Play();
                player_controller.StopRunning();

                StartCoroutine(HideAndStoreProjectile(_projectile));
            }
        }
    }

    void AdjustProjectilePositionAndRotation(GameObject projectile)
    {
        Vector3 shoulderToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shoulder.position;
        shoulderToMouseDir.z = 0;
        projectile.transform.position += (weapon_length * shoulderToMouseDir.normalized);

        ProjectileAttributes rotation_offset = projectile.GetComponent<ProjectileAttributes>();
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x,
            projectile.transform.eulerAngles.y,
            projectile.transform.eulerAngles.z + (rotation_offset ? rotation_offset.rotation_offset : 0.0f));
    }

    GameObject GetProjectileForShooting()
    {
        GameObject _projectile;
        if (projectile_storage.Count == 0)
        {
            _projectile = Instantiate(projectile, transform.position, transform.rotation);
            _projectile.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        else
        {
            _projectile = projectile_storage.Dequeue();
            _projectile.SetActive(true);
            _projectile.transform.position = transform.position;
            _projectile.transform.rotation = transform.rotation;
        }

        return _projectile;
    }

    IEnumerator HideAndStoreProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(projectile_duration);
        projectile_storage.Enqueue(projectile);
        projectile.SetActive(false);
    }
}
