using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupQuestController : MonoBehaviour
{
    public NPCQuestManager syrupQuestManager;
    private GameObject soapBottle;
    public GameObject syrupBottle;
    void Start()
    {
        soapBottle = syrupBottle.transform.Find("Soap").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Quest currentQuest = syrupQuestManager.getTempCurrentQuest();
        if (currentQuest != null)
        {
            if (currentQuest.questName.Equals("Soap Bottle Secret") && currentQuest.getCompleted())
            {
                soapBottle.SetActive(true);
            }
            if (currentQuest.questName.Equals("Mystery Googly Eyes") && currentQuest.getCompleted())
            {
                Transform eyes = soapBottle.transform.Find("Googly Eyes");
                eyes.gameObject.SetActive(true);
            }
            if (currentQuest.questName.Equals("Fancy Soap") && currentQuest.getCompleted())
            {
                Transform hat = soapBottle.transform.Find("Top Hat");
                hat.gameObject.SetActive(true);
            }
        }
    }
}
