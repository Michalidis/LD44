using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributes : MonoBehaviour
{
    public int item_count;
    public string item_name;
    public string item_description;

    public Item Item { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Item = new Item()
        {
            name = item_name,
            count = item_count,
            description = item_description
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
