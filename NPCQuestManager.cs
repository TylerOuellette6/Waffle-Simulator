using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuestManager : MonoBehaviour
{
    public List<Quest> quests;
    private Quest tempCurrentQuest;

    public List<Quest> getQuests()
    {
        return quests;
    }

    public void setTempCurrentQuest(Quest tempCurrentQuest)
    {
        this.tempCurrentQuest = tempCurrentQuest;
    }

    public Quest getTempCurrentQuest()
    {
        return tempCurrentQuest;
    }

    public static void moveNPCAfterQuest(Quest quest)
    {
        GameObject npcObject = quest.getNPC();
        // Move based on the x, y, and z constants that are provided by Quest class
        npcObject.transform.position = new Vector3(quest.getNewXPos(), quest.getNewYPos(), quest.getNewZPos());
        // Rotate based on the new y constant that is provided by the Quest class
        npcObject.transform.rotation = Quaternion.Euler(0, quest.getNewYRotation(), 0);
    }
}
