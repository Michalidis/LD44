using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public bool EnsureLoot;
    public GameObject[] LootTable;
    public float[] ChanceTable;
    // Start is called before the first frame update

    public GameObject GetLootItem()
    {
        if (EnsureLoot)
        {
            List<GameObject> passedRngTest = new List<GameObject>();
            for (int i = 0; i < LootTable.Length; i++)
            {
                if (UnityEngine.Random.Range(0, 100) <= ChanceTable[i])
                {
                    passedRngTest.Add(LootTable[i]);
                }
            }

            if (passedRngTest.Count == 0)
            {
                return LootTable[Mathf.RoundToInt(UnityEngine.Random.Range(0, LootTable.Length - 1))];
            }
            else
            {
                return passedRngTest[Mathf.RoundToInt(UnityEngine.Random.Range(0, passedRngTest.Count - 1))];
            }
        }
        else
        {
            for (int i = 0; i < LootTable.Length; i++)
            {
                if (UnityEngine.Random.Range(0, 100) <= ChanceTable[i])
                {
                    return LootTable[i];
                }
            }
        }

        return null;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
