using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ItemAttributes : MonoBehaviour
{
    public int item_count;
    public string item_name;
    public string item_description;

    public GameObject particle_system;

    public Item Item { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Type thisType = GetType();
        MethodInfo instantiateMethod = thisType.GetMethod(item_name.Split(' ')[0] + "_init");
        Item = (Item)instantiateMethod.Invoke(this, new object[3] { item_count, item_name, item_description });

        particle_system = Instantiate(particle_system, transform);
    }

    public Item Ring_init(int item_count, string item_name, string item_description)
    {
        return new Item_RingOfHealing()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Boots_init(int item_count, string item_name, string item_description)
    {
        return new Item_BootsOfSwiftness()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Bow_init(int item_count, string item_name, string item_description)
    {
        return new Item_BowOfSpeed()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Hat_init(int item_count, string item_name, string item_description)
    {
        return new Item_HatOfCurse()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Shield_init(int item_count, string item_name, string item_description)
    {
        return new Item_ShieldOfBlocking()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Spear_init(int item_count, string item_name, string item_description)
    {
        return new Item_SpearOfBleeding()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Jacket_init(int item_count, string item_name, string item_description)
    {
        return new Item_JacketOfFirepower()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Stone_init(int item_count, string item_name, string item_description)
    {
        return new Item_StoneKey()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Helmet_init(int item_count, string item_name, string item_description)
    {
        return new Item_HelmetOfProtection()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Golden_init(int item_count, string item_name, string item_description)
    {
        return new Item_GoldenKey()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Ruby_init(int item_count, string item_name, string item_description)
    {
        return new Item_RubyOfFire()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Sword_init(int item_count, string item_name, string item_description)
    {
        return new Item_SwordOfRapidStrikes()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Rapier_init(int item_count, string item_name, string item_description)
    {
        return new Item_RapierOfDamage()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Scepter_init(int item_count, string item_name, string item_description)
    {
        return new Item_ScepterOfLava()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Emblem_init(int item_count, string item_name, string item_description)
    {
        return new Item_EmblemOfSprint()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }

    public Item Book_init(int item_count, string item_name, string item_description)
    {
        return new Item_BookOfResurrection()
        {
            count = item_count,
            name = item_name,
            description = item_description
        };
    }
}
