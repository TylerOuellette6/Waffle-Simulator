using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTVQuestController : MonoBehaviour
{
    public GameObject tvObject;
    public NPCQuestManager pancakeQuestManager;
    public bool doOnce = false;

    void Update()
    {
        Vector3 tvPos = tvObject.transform.position;
        float tvYPos = tvPos.y;

        Quest currentQuest = pancakeQuestManager.getTempCurrentQuest();
        if (currentQuest != null)
        {
            if ((currentQuest.questName.Equals("Missing Piece") && currentQuest.getCompleted()) ||
            (currentQuest.questName.Equals("TV Topple") && !currentQuest.getCompleted()))
            {
                if (tvYPos < 75)
                {
                    currentQuest.setConditionMetForCompletion(true);
                }
            }
            if(currentQuest.questName.Equals("TV Topple") && currentQuest.getCompleted())
            {
                if (!doOnce)
                {
                    doOnce = true;
                    tvObject.GetComponent<DestroyTVQuestController>().enabled = false;
                    AchievementsController.unlockPancakesProjects();
                }
            }
        }
    }
}
