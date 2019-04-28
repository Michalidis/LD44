using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using Assets.Scripts.Monsters;
using Assets.Scripts.Projectiles;
using UnityEngine;


public class WeaponProjectileEmitterMonster : MonoBehaviour, IWeaponProjectileEmitter
{
    public GameObject skeleton;
    public float ShotsPerSecond = 0.5f;

    public float shooting_force = 175.0f;

    private AudioSource sound_player;
    public GameObject projectile;
    public float projectile_duration = 3.5f;

    private Transform shoulder;
    public float weapon_length = 0.13f;

    private float shooting_cooldown;

    private Queue<GameObject> projectile_storage;

    private GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        if (this.player == null)
        {
            this.player = GameObject.Find("Character");
        }

        this.shoulder = this.transform.parent.transform;
        this.projectile_storage = new Queue<GameObject>();
        this.sound_player = this.GetComponent<AudioSource>();

        ProjectileAttributes pa = this.projectile.GetComponent<ProjectileAttributes>();
        if (pa)
        {
            this.sound_player.clip = pa.fired_sound;
        }

        this.gameObject.layer = 8;
    }
    
    private void Update()
    {
        if (this.shooting_cooldown > 0.0f)
        {
            this.shooting_cooldown -= Time.deltaTime;
        }

        if (this.shooting_cooldown <= 0.0f)
        {
            this.shooting_cooldown = 1.0f / ShotsPerSecond;
            Vector2 weaponToMouseDir = player.transform.position - this.transform.position;

            GameObject projectile = this.GetProjectileForShooting();
            this.AdjustProjectilePositionAndRotation(projectile);

            weaponToMouseDir.Normalize();
            projectile.GetComponent<Rigidbody2D>().AddForce(weaponToMouseDir * this.shooting_force);
            projectile.GetComponent<ProjectileBehavior>().shotFrom = this;
            this.sound_player.Play();

            this.StartCoroutine(this.HideAndStoreProjectile(projectile));
        }
    }

    private void AdjustProjectilePositionAndRotation(GameObject projectile)
    {
        Vector3 shoulderToMouseDir = player.transform.position - this.shoulder.position;
        shoulderToMouseDir.z = 0;
        projectile.transform.position += (this.weapon_length * shoulderToMouseDir.normalized);

        ProjectileAttributes projectieAttributes = projectile.GetComponent<ProjectileAttributes>();
        projectile.transform.eulerAngles = new Vector3(projectile.transform.eulerAngles.x,
            projectile.transform.eulerAngles.y,
            projectile.transform.eulerAngles.z + (projectieAttributes ? projectieAttributes.rotation_offset : 0.0f));
    }

    private GameObject GetProjectileForShooting()
    {
        GameObject projectile;
        if (this.projectile_storage.Count == 0)
        {
            projectile = Instantiate(this.projectile, this.transform.position, this.transform.rotation);
            projectile.GetComponent<SpriteRenderer>().sortingOrder = 5;
            this.TurnOffCollisionWithEnemies(projectile);
        }
        else
        {
            projectile = this.projectile_storage.Dequeue();
            projectile.SetActive(true);
            projectile.transform.position = this.transform.position;
            projectile.transform.rotation = this.transform.rotation;
        }

        return projectile;
    }

    private IEnumerator HideAndStoreProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(this.projectile_duration);
        this.projectile_storage.Enqueue(projectile);
        projectile.SetActive(false);
    }

    private void TurnOffCollisionWithEnemies(GameObject projectile)
    {
        projectile.layer = 9;
    }

    public int GetDamage()
    {
        return this.skeleton.GetComponent<EnemyStats>().GetDamage();
    }
}
