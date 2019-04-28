using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationMonster : MonoBehaviour
{
    private Transform shoulder;
    private bool is_mirrored;
    private float rotation_offset_z;

    public float armLength = 0.125f;

    private GameObject player;

    void Start()
    {
        if (this.player == null)
        {
            this.player = GameObject.Find("Character");
        }

        shoulder = transform.parent.transform;
        is_mirrored = false;
        rotation_offset_z = -transform.rotation.z;
    }

    void Update()
    {
        Vector3 shoulderToMouseDir = player.transform.position - shoulder.position;
        shoulderToMouseDir.z = 0;
        transform.position = shoulder.position + (armLength * shoulderToMouseDir.normalized);

        Vector3 mouse_pos = Camera.main.WorldToScreenPoint(player.transform.position);
        mouse_pos.z = 5.23f;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        if (!is_mirrored)
        {
            if (angle < -90 || angle > 90)
            {
                transform.localScale -= new Vector3(0, 2 * transform.localScale.y, 0);
                rotation_offset_z *= -1;
                is_mirrored = true;
            }
        }
        else
        {
            if (angle > -90 && angle < 90)
            {
                transform.localScale -= new Vector3(0, 2 * transform.localScale.y, 0);
                rotation_offset_z *= -1;
                is_mirrored = false;
            }
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - rotation_offset_z));
    }
}
