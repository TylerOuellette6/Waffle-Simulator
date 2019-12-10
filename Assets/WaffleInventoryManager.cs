using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleInventoryManager : MonoBehaviour
{
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

        GameObject tempInventoryItemUI = (GameObject)Instantiate(singleInventoryItemPrefab);
        tempInventoryItemUI.transform.SetParent(inventoryPanel.transform);
        tempInventoryItemUI.transform.localPosition = new Vector3(
            temporaryInventoryItemXPosition, temporaryInventoryItemYPosition, 0);
        //tempInventoryItemUI.transform.GetChild(0).GetComponent<Image>().sprite = newInventoryItem.GetComponent<SpriteMask>().sprite;
        tempInventoryItemUI.transform.GetChild(1).GetComponent<Text>().text = newInventoryItem.name;

        inventoryUIItems.Add(tempInventoryItemUI);

        temporaryInventoryItemXPosition += 175;
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
            GameObject tempInventoryItemUI = (GameObject)Instantiate(singleInventoryItemPrefab);
            tempInventoryItemUI.transform.SetParent(inventoryPanel.transform);
            tempInventoryItemUI.transform.localPosition = new Vector3(
                temporaryInventoryItemXPosition, temporaryInventoryItemYPosition, 0);
            //tempInventoryItemUI.transform.GetChild(0).GetComponent<Image>().sprite = inventoryItem.GetComponent<SpriteMask>().sprite;
            tempInventoryItemUI.transform.GetChild(1).GetComponent<Text>().text = inventoryItem.name;

            temporaryInventoryItemXPosition += xPosMovementConst;
        }
    }

    public static int getNumInventoryItems()
    {
        return numInventoryItems;
    }

    public static void removeInventoryItem(GameObject inventoryItemToRemove)
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
