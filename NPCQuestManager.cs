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
}
