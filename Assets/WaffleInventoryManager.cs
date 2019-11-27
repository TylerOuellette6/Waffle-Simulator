using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaffleInventoryManager : MonoBehaviour
{
    static List<GameObject> inventoryItems;
    // Start is called before the first frame update
    void Start()
    {
        inventoryItems = new List<GameObject>();
    }

    public static void addItemToInventory(GameObject newInventoryItem)
    {
        inventoryItems.Add(newInventoryItem);
        foreach(GameObject inventoryItem in inventoryItems)
        {
            Debug.Log(inventoryItem.name);
        }
    }
}
