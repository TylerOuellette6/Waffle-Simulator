using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    // Help from this tutorial: http://gregandaduck.blogspot.com/2015/07/unity-ui-dynamic-buttons-and-scroll-view.html
    private string questName;
    private string questDescription;
    private GameObject npc;
    public Canvas questDescriptionUI;
    public Text questDescriptionText;
    public Text questDescriptionTitleText;

    private void Start()
    {
        questDescriptionUI = GameObject.Find("QuestDescriptionUI").GetComponent<Canvas>();
        questDescriptionText = GameObject.Find("QuestDescriptionText").GetComponent<Text>();
        questDescriptionTitleText = GameObject.Find("QuestDescriptionTitleText").GetComponent<Text>();
    }

    public void setButtonInfo(Quest quest)
    {
        this.questName = quest.questName;
        this.questDescription = quest.questDescription;
        this.npc = quest.getNPC();
    }

    public void questButtonClicked()
    {
        questDescriptionUI.enabled = true;
        questDescriptionText.text = questDescription;
        questDescriptionTitleText.text = npc.name + ": " + questName;
    }
}
