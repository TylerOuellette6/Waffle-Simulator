using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleInventoryManager : MonoBehaviour
{
    private static GameObject playerObj;

    private static List<GameObject> inventoryItems;
    private static List<GameObject> inventoryUIItems;

    private static int permanentInventoryItemXPosition = -300;
    private static int permanentInventoryItemYPosition = 115;
    private static int temporaryInventoryItemXPosition = -300;
    private static int temporaryInventoryItemYPosition = -115;

    private static int xPosMovementConst = 175;

    private static GameObject singleInventoryItemPrefab;
    private static GameObject inventoryPanel;

    private static int numInventoryItems = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Waffle");
        inventoryItems = new List<GameObject>();
        inventoryUIItems = new List<GameObject>();

        singleInventoryItemPrefab = (GameObject)Resources.Load(
            "prefabs/SingleInventoryItemUIPrefab", typeof(GameObject));
        inventoryPanel = GameObject.Find("InventoryPanel"); 
    }

    public static void addItemToInventory(GameObject newInventoryItem)
    {
        numInventoryItems++;

        inventoryItems.Add(newInventoryItem);
        //updateInventoryUI();

        createInventoryItemPrefab(newInventoryItem);
    }

    private static void updateInventoryUI()
    {
        foreach(GameObject inventoryUIItem in inventoryUIItems)
        {
            Destroy(inventoryUIItem);
        }
        inventoryUIItems.Clear();
        foreach(GameObject inventoryItem in inventoryItems)
        {
            createInventoryItemPrefab(inventoryItem);
        }
    }

    private static void createInventoryItemPrefab(GameObject inventoryItem)
    {
        GameObject tempInventoryItemUI = (GameObject)Instantiate(singleInventoryItemPrefab);
        tempInventoryItemUI.transform.SetParent(inventoryPanel.transform);
        tempInventoryItemUI.transform.localPosition = new Vector3(
            temporaryInventoryItemXPosition, temporaryInventoryItemYPosition, 0);
        //tempInventoryItemUI.transform.GetChild(0).GetComponent<Image>().sprite = inventoryItem.GetComponent<SpriteMask>().sprite;
        tempInventoryItemUI.transform.GetChild(1).GetComponent<Text>().text = inventoryItem.name;
        Button removeInventoryItemBtn = tempInventoryItemUI.GetComponentInChildren<Button>();
        removeInventoryItemBtn.onClick.AddListener(delegate { removeInventoryItem(tempInventoryItemUI);});

        inventoryUIItems.Add(tempInventoryItemUI);

        temporaryInventoryItemXPosition += xPosMovementConst;
    }

    public static int getNumInventoryItems()
    {
        return numInventoryItems;
    }

    private static void removeInventoryItem(GameObject inventoryUIItemToRemove)
    {
        string itemName = inventoryUIItemToRemove.GetComponentInChildren<Text>().text;
        int indexToRemove = -1;
        for(int i = 0; i < inventoryUIItems.Count; i++)
        {
            if(inventoryUIItems[i] == inventoryUIItemToRemove)
            {
                indexToRemove = i;
            }
        }

        Vector3 playerPosition = playerObj.transform.position;

        GameObject itemToRemove = inventoryItems[indexToRemove];
        itemToRemove.transform.position = playerPosition + new Vector3 (5, 0, 5);
        itemToRemove.SetActive(true);

        inventoryItems.Remove(itemToRemove);
        temporaryInventoryItemXPosition = -300;
        updateInventoryUI();

        Debug.Log(indexToRemove);

    }

    public static void removeInventoryItemAfterQuest(GameObject inventoryItemToRemove)
    {
        if (inventoryItems.Contains(inventoryItemToRemove))
        {
            inventoryItems.Remove(inventoryItemToRemove);
            Destroy(inventoryItemToRemove);
            temporaryInventoryItemXPosition = -300;
            updateInventoryUI();
        }
    }
}
