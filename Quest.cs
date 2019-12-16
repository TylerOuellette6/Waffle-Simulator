using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public Dialogue questDialogue;
    public Dialogue questCompleteDialogue;
    public GameObject npc;
    public GameObject objectNeededForCompletion;
    public string questName;
    private bool completed;
    private bool accepted;
    private bool conditionMetForCompletion = false;

    [TextArea(3, 10)]
    public string questDescription;

    public Dialogue getQuestDialogue()
    {
        return questDialogue;
    }

    public Dialogue getQuestCompleteDialogue()
    {
        return questCompleteDialogue;
    }

    public bool getCompleted()
    {
        return completed;
    }

    public GameObject getNPC()
    {
        return this.npc;
    }

    public void setCompleted(bool completed)
    {
        this.completed = completed;
    }

    public bool getAccepted()
    {
        return accepted;
    }

    public void setAccepted(bool accepted)
    {
        this.accepted = accepted;
    }

    public bool getConditionMetForCompletion()
    {
        return conditionMetForCompletion;
    }

    public void setConditionMetForCompletion(bool conditionMetForCompletion)
    {
        this.conditionMetForCompletion = conditionMetForCompletion;
    }

    public GameObject getObjectNeededForCompletion()
    {
        return this.objectNeededForCompletion;
    }
}
