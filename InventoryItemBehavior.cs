using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBehavior : MonoBehaviour
{
    public Canvas pressFToPickUp;
    public GameObject inventoryObj;
    public GameObject playerObj;

    private bool nearInventoryObj;

    // Start is called before the first frame update
    void Start()
    {
        pressFToPickUp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if((inventoryObj.transform.position - playerObj.transform.position).magnitude < 20.0f && !nearInventoryObj)
        {
            pressFToPickUp.enabled = true;
            nearInventoryObj = true;
        }
        if((inventoryObj.transform.position - playerObj.transform.position).magnitude > 20.0f && nearInventoryObj)
        {
            pressFToPickUp.enabled = false;
            nearInventoryObj = false;
        }
        if(Input.GetKeyDown(KeyCode.F) && nearInventoryObj)
        {
            HandleItemPickup();
        }
    }

    public void HandleItemPickup()
    {
        pressFToPickUp.enabled = false;
        InventoryItem item = inventoryObj.GetComponent<InventoryItem>();
        WaffleInventoryManager waffleInventoryManager= playerObj.GetComponent<WaffleInventoryManager>();
        bool permanent = item.isPermanentItem;
        if (permanent)
        {
            waffleInventoryManager.addPermanentItemToInventory(inventoryObj);
            if (inventoryObj.name.Equals("Super Speed Powerup"))
            {
                PowerupController.receiveSuperSpeedPowerup();
            } 
        }
        else
        {
            waffleInventoryManager.addTempItemToInventory(inventoryObj);
        }
        WaffleQuestController.checkIfItemCompletesQuest(inventoryObj);
    }
}
