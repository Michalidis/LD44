using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupManager : MonoBehaviour
{
    bool isPickedUp;
    // Start is called before the first frame update
    void Start()
    {
        isPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPickedUp)
        {
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            ItemAttributes itemAttributes = GetComponent<ItemAttributes>();
            if (itemAttributes)
            {
                collision.gameObject.GetComponent<ItemManager>().PickUpItem(itemAttributes.Item);
                StartCoroutine(PlanDestroy());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isPickedUp)
        {
            return;
        }

        foreach (var contact in collision.contacts)
        {
            if (contact.collider.tag == "Player")
            {
                ItemAttributes itemAttributes = GetComponent<ItemAttributes>();
                if (itemAttributes)
                {
                    contact.collider.GetComponent<ItemManager>().PickUpItem(itemAttributes.Item);
                    StartCoroutine(PlanDestroy());
                }
            }
        }
    }

    IEnumerator PlanDestroy()
    {
        isPickedUp = true;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0.0f, 0.5f);
        GetComponent<ItemAttributes>().particle_system.GetComponent<ParticleSystem>().Stop();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
