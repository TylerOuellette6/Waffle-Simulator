using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleQuestController : MonoBehaviour
{
    private static List<Quest> startedQuests;
    private static List<Quest> finishedQuests;
    private static Canvas questDescriptionUI;
    private static GameObject waffle;

    void Start()
    {
        GameObject tempQuestDescriptionUI = GameObject.Find("QuestDescriptionUI");
        questDescriptionUI = tempQuestDescriptionUI.GetComponent<Canvas>();

        startedQuests = new List<Quest>();
        finishedQuests = new List<Quest>();

        waffle = GameObject.Find("Waffle");
    }

    public static void addQuestToList(Quest newQuest)
    {
        if (!startedQuests.Contains(newQuest))
        {
            startedQuests.Add(newQuest);
        }

        IncompleteQuestUIManager.updateScrollList(startedQuests);

        foreach (GameObject inventoryItem in WaffleInventoryManager.getInventoryItemList())
        {
            checkIfInventoryContainsItemNeededForCompletion(inventoryItem, newQuest);
        }
        foreach (GameObject permenentItem in WaffleInventoryManager.getPermanentItemList())
        {
            checkIfInventoryContainsItemNeededForCompletion(permenentItem, newQuest);
        }
    }

    private static void checkIfInventoryContainsItemNeededForCompletion(GameObject inventoryItem, Quest newQuest)
    {
        if (newQuest.getObjectNeededForCompletion() != null && inventoryItem.name == newQuest.getObjectNeededForCompletion().name)
        {
            foreach (Quest tempQuest in startedQuests)
            {
                if (tempQuest == newQuest)
                {
                    tempQuest.setConditionMetForCompletion(true);
                }
            }
        }
    }

    public void closeQuestDescriptionUI()
    {
        questDescriptionUI.enabled = false;
    }

    public static void checkIfItemCompletesQuest(GameObject newInventoryItem)
    {
        foreach (Quest quest in startedQuests)
        {
            if(quest.getObjectNeededForCompletion().name == newInventoryItem.name)
            {
                quest.setConditionMetForCompletion(true);
            }
        }
    }

    public static void completeQuest(Quest completedQuest)
    {
        WaffleInventoryManager waffleInventoryManager = waffle.GetComponent<WaffleInventoryManager>();
        if(completedQuest.getObjectNeededForCompletion() != null)
        {
            waffleInventoryManager.removeInventoryItemAfterQuest(completedQuest.getObjectNeededForCompletion().name);
        }
        finishedQuests.Add(completedQuest);
        startedQuests.Remove(completedQuest);
        NPCQuestManager.moveNPCAfterQuest(completedQuest);
        IncompleteQuestUIManager.updateScrollList(startedQuests);
        CompleteQuestUIManager.updateScrollList(finishedQuests);
    }

    public List<Quest> getStartedQuests()
    {
        return startedQuests;
    }

    public List<Quest> getFinishedQuests()
    {
        return finishedQuests;
    }

    public void setStartedQuests(List<Quest> quests)
    {
        startedQuests = quests;
    }

    public void setSFinishedQuests(List<Quest> quests)
    {
        finishedQuests = quests;
    }
}
