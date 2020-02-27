using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenchToastOutfitController : MonoBehaviour
{
    public NPCQuestManager frenchToastQuestManager;
    public GameObject frenchToastNPC;
    private bool hasPowerupBeenGiven = false;

    void Update()
    {
        Quest currentQuest = frenchToastQuestManager.getTempCurrentQuest();
        if(currentQuest != null)
        {
            if (currentQuest.questName.Equals("Lost Beret") && currentQuest.getCompleted())
            {
                Transform beret = frenchToastNPC.transform.Find("Beret");
                beret.gameObject.SetActive(true);
            }
            if(currentQuest.questName.Equals("Lost Striped Shirt") && currentQuest.getCompleted())
            {
                Transform stripedShirt = frenchToastNPC.transform.Find("StripedShirt");
                stripedShirt.gameObject.SetActive(true);
            }
            if (currentQuest.questName.Equals("Lost Mustache") && currentQuest.getCompleted())
            {
                Transform mustache = frenchToastNPC.transform.Find("Mustache");
                mustache.gameObject.SetActive(true);
                PowerupController.showSuperJumpPowerup();
            }
        }
    }
}
