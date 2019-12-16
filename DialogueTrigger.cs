using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private static Canvas pressFToTalkUI;
    public GameObject npcObj;
    public GameObject playerObj;
    public NPCQuestManager npcQuestManager;
    public DialogueManager dialogueManager;

    private bool nearNPC;

    private void Start()
    {
        GameObject tempCanvas = GameObject.Find("PressButtonToTalkUI");
        if(tempCanvas != null)
        {
            pressFToTalkUI = tempCanvas.GetComponent<Canvas>();
            pressFToTalkUI.enabled = false;
        }
        this.nearNPC = false;
    }

    private void Update()
    {
        if ((npcObj.transform.position - playerObj.transform.position).magnitude < 5.0f && !nearNPC)
        {
            pressFToTalkUI.enabled = true;
            nearNPC = true;
        }
        if((npcObj.transform.position - playerObj.transform.position).magnitude > 5.0f && nearNPC)
        {
            pressFToTalkUI.enabled = false;
            nearNPC = false;
            EndDialogue();
        }
        if (Input.GetKeyDown(KeyCode.F) && nearNPC)
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue()
    {
        dialogueManager.setNPCQuestManager(npcQuestManager);
        pressFToTalkUI.enabled = false;
        List<Quest> quests = npcQuestManager.getQuests();
        Quest tempQuest = null;
        foreach(Quest quest in quests)
        {
            if (!quest.getCompleted())
            {
                tempQuest = quest;
                npcQuestManager.setTempCurrentQuest(tempQuest);
                break;
            }
        }
        if((tempQuest != null && !tempQuest.getAccepted()) || tempQuest.getConditionMetForCompletion())
        {
            FindObjectOfType<DialogueManager>().StartDialogue(tempQuest);
        }
    }

    private void EndDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<DialogueManager>().EndDialogue();
    }
}
