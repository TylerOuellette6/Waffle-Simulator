using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseQuestController : MonoBehaviour
{
    public NPCQuestManager mouseQuestManager;
    public GameObject mouseNPC;
    private bool grown = false;
    private bool hasPowerupBeenGiven = false;

    void Update()
    {
        Quest currentQuest = mouseQuestManager.getTempCurrentQuest();
        if (currentQuest != null)
        {
            if (!currentQuest.getCompleted())
            {
                grown = false;
            }
            if (currentQuest.getCompleted())
            {
                growMouseSize();
            }
            if(currentQuest.questName.Equals("Desire for Donut") && currentQuest.getCompleted())
            {
                PowerupController.showSuperSpeedPowerup();
            }
        }
    }

    private void growMouseSize()
    {
        if(!grown)
        {
            mouseNPC.transform.localScale += new Vector3(50, 0, 50);
        }
        grown = true;
    }
}
