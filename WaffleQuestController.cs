using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleQuestController : MonoBehaviour
{
    private static List<Quest> startedQuests;
    private static List<Quest> finishedQuests;
    private static Canvas questDescriptionUI;

    void Start()
    {
        GameObject tempQuestDescriptionUI = GameObject.Find("QuestDescriptionUI");
        questDescriptionUI = tempQuestDescriptionUI.GetComponent<Canvas>();

        startedQuests = new List<Quest>();
        finishedQuests = new List<Quest>();
    }

    public static void addQuestToList(Quest newQuest)
    {
        startedQuests.Add(newQuest);

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
        if(completedQuest.getObjectNeededForCompletion() != null)
        {
            WaffleInventoryManager.removeInventoryItemAfterQuest(completedQuest.getObjectNeededForCompletion().name);
        }
        NPCQuestManager.moveNPCAfterQuest(completedQuest);
        finishedQuests.Add(completedQuest);
        startedQuests.Remove(completedQuest);
        IncompleteQuestUIManager.updateScrollList(startedQuests);
        CompleteQuestUIManager.updateScrollList(finishedQuests);
    }
}
