using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllfawQuestController : MonoBehaviour
{
    public NPCQuestManager ellfawQuestManager;
    public GameObject ellfawNPC;

    void Update()
    {
        Quest currentQuest = ellfawQuestManager.getTempCurrentQuest();
        if(currentQuest != null)
        {
            if (currentQuest.questName.Equals("Mystery Location #1") && !currentQuest.getCompleted() && currentQuest.getAccepted())
            {
                ellfawNPC.transform.position = new Vector3(-318.4f, 6, -491.9f);
                ellfawNPC.transform.rotation = Quaternion.Euler(0, 45, 0);
                currentQuest.setConditionMetForCompletion(true);
            }
            if (currentQuest.questName.Equals("Mystery Location #2") && !currentQuest.getCompleted() && currentQuest.getAccepted())
            {
                ellfawNPC.transform.position = new Vector3(-909f, 6, 297.6f);
                ellfawNPC.transform.rotation = Quaternion.Euler(0, -45, 0);
                currentQuest.setConditionMetForCompletion(true);
            }
            if (currentQuest.questName.Equals("Mystery Location #3") && !currentQuest.getCompleted() && currentQuest.getAccepted())
            {
                ellfawNPC.transform.position = new Vector3(-666, 18.6f, 310.2f);
                currentQuest.setConditionMetForCompletion(true);
            }
            if (currentQuest.questName.Equals("Mystery Location #3") && currentQuest.getCompleted())
            {
                AchievementsController.unlockEllfawExpeditions();
                PowerupController.showSuperMiniPowerup();
            }
        }
    }
}
