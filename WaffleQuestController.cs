using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleQuestController : MonoBehaviour
{
    private static List<Quest> startedQuests;
    private static int questButtonYPosition = 310;

    private static GameObject buttonPrefab;
    private static GameObject questPanel;
    private static Canvas questDescriptionUI;
    private static Text questDescriptionText;
    private static Text questDescriptionTitleText;

    void Start()
    {
        buttonPrefab = (GameObject)Resources.Load("prefabs/QuestButtonPrefab", typeof(GameObject));
        questPanel = GameObject.Find("QuestPanel");

        GameObject tempQuestDescriptionUI = GameObject.Find("QuestDescriptionUI");
        questDescriptionUI = tempQuestDescriptionUI.GetComponent<Canvas>();
        questDescriptionUI.enabled = false;

        questDescriptionText = GameObject.Find("QuestDescriptionText").GetComponent<Text>();
        questDescriptionTitleText = GameObject.Find("QuestDescriptionTitleText").GetComponent<Text>();

        startedQuests = new List<Quest>();
    }

    public static void addQuestToList(Quest newQuest)
    {
        startedQuests.Add(newQuest);

        GameObject tempQuestButton = (GameObject)Instantiate(buttonPrefab);
        tempQuestButton.transform.SetParent(questPanel.transform);
        tempQuestButton.transform.localPosition = new Vector3(0, questButtonYPosition, 0);
        tempQuestButton.transform.GetChild(0).GetComponent<Text>().text = newQuest.getNPC().name + ": " + newQuest.questName;

        Button questButton = tempQuestButton.GetComponent<Button>();
        questButton.onClick.AddListener(() => QuestButtonClicked(newQuest));

        questButtonYPosition -= 40;
        foreach(GameObject inventoryItem in WaffleInventoryManager.getInventoryItemList())
        {
            checkIfInventoryContainsItemNeededForCompletion(inventoryItem, newQuest);
        }
        foreach(GameObject permenentItem in WaffleInventoryManager.getPermanentItemList())
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

    private static void QuestButtonClicked(Quest quest)
    {
        questDescriptionUI.enabled = true;
        questDescriptionText.text = quest.questDescription;
        questDescriptionTitleText.text = quest.getNPC().name + ": " + quest.questName;
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
    }
}
