using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?time_continue=102&v=_nRzoTzeyxU
public class DialogueManager : MonoBehaviour
{
    public Canvas dialogueUI;
    public Button acceptButton;
    public Button declineButton;
    public Button advanceTextButton;
    public Text nameText;
    public Text dialogueText;
    public GameObject camera;

    private NPCQuestManager npcQuestManager;

    private Quest quest;

    private Queue<string> sentences;
    void Start()
    {
        dialogueUI.enabled = false;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Quest quest)
    {
        camera.GetComponent<ThirdPersonCamera>().enabled = false;
        this.quest = quest;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        dialogueUI.enabled = true;
        nameText.text = quest.getNPC().name;

        ButtonVisibilityToggle(false, 1, 0, Color.black, Color.clear);

        sentences.Clear();
        if (!quest.getConditionMetForCompletion())
        {
            foreach (string sentence in quest.getQuestDialogue().sentences)
            {
                sentences.Enqueue(sentence);
            }
        } 

        if(quest.getConditionMetForCompletion()){
            sentences.Clear();
            foreach (string sentence in quest.getQuestCompleteDialogue().sentences)
            {
                sentences.Enqueue(sentence);
            }
            quest.setCompleted(true);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        if(sentences.Count == 1 && !this.quest.getConditionMetForCompletion())
        {
            ButtonVisibilityToggle(true, 0, 1, Color.clear, Color.black);
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(typeSentence(sentence));
    }

    IEnumerator typeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if (this.quest != null && this.quest.getCompleted())
        {
            WaffleQuestController.completeQuest(quest);
            this.quest = null;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialogueUI.enabled = false;
        camera.GetComponent<ThirdPersonCamera>().enabled = true;
    }

    public void AcceptButtonClicked()
    {
        Quest acceptedQuest = npcQuestManager.getTempCurrentQuest();
        acceptedQuest.setAccepted(true);
        WaffleQuestController.addQuestToList(acceptedQuest);
        EndDialogue();
    }

    private void ButtonVisibilityToggle(bool visibility, int advanceTextAlpha, int acceptDeclineAlpha, Color advanceColor, Color acceptDeclineColor)
    {
        advanceTextButton.enabled = !visibility;
        advanceTextButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(advanceTextAlpha);
        advanceTextButton.GetComponentInChildren<Text>().color = advanceColor;

        acceptButton.enabled = visibility;
        acceptButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(acceptDeclineAlpha);
        acceptButton.GetComponentInChildren<Text>().color = acceptDeclineColor;

        declineButton.enabled = visibility;
        declineButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(acceptDeclineAlpha);
        declineButton.GetComponentInChildren<Text>().color = acceptDeclineColor;
    }

    public void setNPCQuestManager(NPCQuestManager npcQuestManager)
    {
        this.npcQuestManager = npcQuestManager;
    }
}
