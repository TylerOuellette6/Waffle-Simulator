using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WaffleInventoryManager : MonoBehaviour
{
    private GameObject playerObj;

    private static List<GameObject> inventoryItems;
    private List<GameObject> inventoryUIItems;

    private static List<GameObject> permanentItems;
    private List<GameObject> permanentUIItems;

    private int permanentInventoryItemXPosition = -300;
    private int permanentInventoryItemYPosition = 115;
    private int temporaryInventoryItemXPosition = -300;
    private int temporaryInventoryItemYPosition = -115;

    private int xPosMovementConst = 175;

    private GameObject singleInventoryItemPrefab;
    private GameObject singlePermanentInventoryItemPrefab;
    private GameObject superJumpInventoryUIPrefab;
    private GameObject superMiniInventoryUIPrefab;

    private GameObject inventoryPanel;
    private Canvas inventoryFullErrorUI;

    private bool superJumpEnabled = false;
    private Text superJumpText;

    private bool superMiniEnabled = false;
    private Text superMiniText;


    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Waffle");
        inventoryItems = new List<GameObject>();
        inventoryUIItems = new List<GameObject>();

        permanentItems = new List<GameObject>();
        permanentUIItems = new List<GameObject>();

        singleInventoryItemPrefab = (GameObject)Resources.Load(
            "prefabs/SingleInventoryItemUIPrefab", typeof(GameObject));
        singlePermanentInventoryItemPrefab = (GameObject)Resources.Load(
            "prefabs/SinglePermanentInventoryItemUIPrefab", typeof(GameObject));
        superJumpInventoryUIPrefab = (GameObject)Resources.Load(
            "prefabs/SuperJumpInventoryUIPrefab", typeof(GameObject));
        superMiniInventoryUIPrefab = (GameObject)Resources.Load(
            "prefabs/SuperMiniInventoryUIPrefab", typeof(GameObject));

        inventoryPanel = GameObject.Find("InventoryPanel");
        inventoryFullErrorUI = GameObject.Find("InventoryFullErrorUI").GetComponent<Canvas>();
        inventoryFullErrorUI.enabled = false;

    }

    public async void addTempItemToInventory(GameObject newInventoryItem)
    {
        if (inventoryItems.Count >= 8)
        {
            inventoryFullErrorUI.enabled = true;
            await Task.Delay(TimeSpan.FromSeconds(2));
            inventoryFullErrorUI.enabled = false;
        } else
        {
            inventoryItems.Add(newInventoryItem);
            newInventoryItem.SetActive(false);
            updateInventoryUI(false);
        }
    }

    private void checkSoupCount()
    {
        int soupCounter = 0;
        foreach(GameObject inventoryItem in inventoryItems){
            if (inventoryItem.name.Contains("Soup"))
            {
                soupCounter++;
            }
        }
        if(soupCounter >= 8)
        {
            AchievementsController.unlockSoup();
        }
    }

    public void addPermanentItemToInventory(GameObject newInventoryItem)
    {
        permanentItems.Add(newInventoryItem);
        newInventoryItem.SetActive(false);
        updateInventoryUI(true);
    }

    private void updateInventoryUI(bool permanent)
    {
        if (!permanent)
        {
            foreach (GameObject inventoryUIItem in inventoryUIItems)
            {
                Destroy(inventoryUIItem);
            }
            inventoryUIItems.Clear();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (i >= 4)
                {
                    temporaryInventoryItemYPosition = -285;
                }
                else
                {
                    temporaryInventoryItemYPosition = -115;
                }
                if (i == 4 || i == 0)
                {
                    temporaryInventoryItemXPosition = -300;
                }
                createInventoryItemPrefab(inventoryItems[i], permanent);
            }
        }
        else
        {
            permanentInventoryItemXPosition = -300;
            foreach (GameObject permanentUIItem in permanentUIItems)
            {
                Destroy(permanentUIItem);
            }
            inventoryUIItems.Clear();
            for (int i = 0; i < permanentItems.Count; i++)
            {
                createInventoryItemPrefab(permanentItems[i], permanent);
            }
        }
        
    }

    private void createInventoryItemPrefab(GameObject inventoryItem, bool permanent)
    {
        if (!permanent)
        {
            GameObject tempInventoryItemUI = inventoryUIBuilderHelper(singleInventoryItemPrefab, inventoryItem, permanent);
            Button removeInventoryItemBtn = tempInventoryItemUI.GetComponentInChildren<Button>();
            removeInventoryItemBtn.onClick.AddListener(delegate { removeInventoryItem(tempInventoryItemUI); });

            inventoryUIItems.Add(tempInventoryItemUI);

            temporaryInventoryItemXPosition += xPosMovementConst;
        }
        else
        {
            GameObject permanentItemUI;
            if (inventoryItem.name.Equals("Super Jump Powerup"))
            {
                permanentItemUI = inventoryUIBuilderHelper(superJumpInventoryUIPrefab, inventoryItem, permanent);

                Button superJumpToggleBtn = permanentItemUI.GetComponentInChildren<Button>();
                superJumpToggleBtn.onClick.AddListener(delegate { superJumpToggled(superJumpToggleBtn); });
                Image buttonColor = superJumpToggleBtn.GetComponent<Image>();
                buttonColor.color = Color.red;
                superJumpText = superJumpToggleBtn.GetComponentInChildren<Text>();
            }
            else if (inventoryItem.name.Equals("Super Mini Powerup"))
            {
                permanentItemUI = inventoryUIBuilderHelper(superMiniInventoryUIPrefab, inventoryItem, permanent);

                Button superMiniToggleBtn = permanentItemUI.GetComponentInChildren<Button>();
                superMiniToggleBtn.onClick.AddListener(delegate { superMiniToggled(superMiniToggleBtn); });
                Image buttonColor = superMiniToggleBtn.GetComponent<Image>();
                buttonColor.color = Color.red;
                superMiniText = superMiniToggleBtn.GetComponentInChildren<Text>();
            } 
            else
            {
                permanentItemUI = inventoryUIBuilderHelper(singlePermanentInventoryItemPrefab, inventoryItem, permanent);
            }
            permanentUIItems.Add(permanentItemUI);
            permanentInventoryItemXPosition += xPosMovementConst;
        }

    }

    private GameObject inventoryUIBuilderHelper(GameObject inventoryUIPrefab, GameObject inventoryItem, bool permanent)
    {
        GameObject itemUI = (GameObject)Instantiate(inventoryUIPrefab);
        itemUI.transform.SetParent(inventoryPanel.transform);
        if (permanent)
        {
            itemUI.transform.localPosition = new Vector3(permanentInventoryItemXPosition, permanentInventoryItemYPosition, 0);
        }
        else
        {
            itemUI.transform.localPosition = new Vector3(temporaryInventoryItemXPosition, temporaryInventoryItemYPosition, 0);
        }
        itemUI.transform.GetChild(0).GetComponent<Image>().sprite = inventoryItem.GetComponent<InventoryItem>().image;
        itemUI.transform.GetChild(1).GetComponent<Text>().text = inventoryItem.name;
        return itemUI;
    }

    private void superMiniToggled(Button superMiniToggleBtn)
    {
        Image buttonColor = superMiniToggleBtn.GetComponent<Image>();
        superMiniEnabled = !superMiniEnabled;
        PowerupController.toggleSuperMiniPowerup(superMiniEnabled);
        if (superMiniEnabled)
        {
            superMiniText.text = "ON";
            buttonColor.color = Color.green;
        }
        else
        {
            superMiniText.text = "OFF";
            buttonColor.color = Color.red;
        }
    }

    private void superJumpToggled(Button superJumpToggleBtn)
    {
        Image buttonColor = superJumpToggleBtn.GetComponent<Image>();
        superJumpEnabled = !superJumpEnabled;
        PowerupController.toggleSuperJumpPowerup(superJumpEnabled);
        if (superJumpEnabled)
        {
            superJumpText.text = "ON";
            buttonColor.color = Color.green;
        }
        else
        {
            superJumpText.text = "OFF";
            buttonColor.color = Color.red;
        }
    }

    private void removeInventoryItem(GameObject inventoryUIItemToRemove)
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
        InventoryItem item = itemToRemove.GetComponent<InventoryItem>();
        bool permanent = item.isPermanentItem;
        updateInventoryUI(permanent);
    }

    public void removeInventoryItemAfterQuest(String inventoryItemToRemoveName)
    {
        foreach(GameObject inventoryItem in inventoryItems)
        {
            if (inventoryItem.name.Equals(inventoryItemToRemoveName))
            {
                inventoryItems.Remove(inventoryItem);
                Destroy(inventoryItem);
                temporaryInventoryItemXPosition = -300;
                InventoryItem item = inventoryItem.GetComponent<InventoryItem>();
                bool permanent = item.isPermanentItem;
                updateInventoryUI(permanent);
                return;
            }
        }
    }

    public static List<GameObject> getInventoryItemList()
    {
        return WaffleInventoryManager.inventoryItems;
    }

    public static List<GameObject> getPermanentItemList()
    {
        return WaffleInventoryManager.permanentItems;
    }
}
