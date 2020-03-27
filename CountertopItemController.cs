using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountertopItemController : MonoBehaviour
{
    public GameObject countertopObject;
    private bool hasFallen = false;
    private bool callOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countertopObject.transform.position.y < 85)
        {
            SetHasFallen(true, false);
        }
        if(hasFallen & !callOnce)
        {
            callOnce = true;
            AchievementsController.checkIfCountertopsCleaned();
        }
    }

    // Fallen is if the item has fallen off the counter/been removed
    // Pickup is if the item was picked up rather than pushed off counter
    public void SetHasFallen(bool fallen, bool pickup)
    {
        hasFallen = fallen;
        if (pickup)
        {
            callOnce = true;
            AchievementsController.checkIfCountertopsCleaned();
        }
    }
}
