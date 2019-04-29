using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Dictionary<string, Item> owned_items;
    // Start is called before the first frame update
    void Start()
    {
        owned_items = new Dictionary<string, Item>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickUpItem(Item item, Sprite itemSprite)
    {
        if (item.count > 0)
        {
            if (owned_items.ContainsKey(item.name))
            {
                owned_items[item.name].count += item.count;
                GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().UpdateItemCount(item, owned_items[item.name].count);
            }
            else
            {
                owned_items.Add(item.name, item);
                GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().AddItem(item, itemSprite);
            }
            
        }

        item.OnItemPickedUp(gameObject);
    }

    public bool TryResurrect()
    {
        if (owned_items.ContainsKey("Book of Resurrection"))
        {
            Item book = owned_items["Book of Resurrection"];
            if (book.count > 0)
            {
                book.count--;
                return true;
            }
        }

        return false;
    }
}
