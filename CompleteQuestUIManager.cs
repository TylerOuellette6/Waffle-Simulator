using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteQuestUIManager : MonoBehaviour
{
    public static GameObject questButtonPrefab;
    public static GameObject scrollContent;

    private void Start()
    {
        questButtonPrefab = (GameObject)Resources.Load("prefabs/QuestButtonPrefab", typeof(GameObject));
        scrollContent = GameObject.Find("CompleteQuestsScrollContent");
        Debug.Log(scrollContent);
    }

    public static void updateScrollList(List<Quest> quests)
    {
        foreach (Transform child in scrollContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Quest quest in quests)
        {
            GameObject button = Instantiate(questButtonPrefab) as GameObject;
            button.SetActive(true);
            button.transform.GetChild(0).GetComponent<Text>().text = quest.questName;
            QuestButton questButton = button.GetComponent<QuestButton>();
            questButton.setButtonInfo(quest);
            questButton.transform.SetParent(scrollContent.transform);
        }
    }
}
