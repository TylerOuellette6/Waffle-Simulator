using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeTextureController : MonoBehaviour
{
    public NPCQuestManager pancakeQuestManager;
    public GameObject wholePancakeTexture;
    public GameObject pancakeTextureMissingPiece;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quest currentQuest = pancakeQuestManager.getTempCurrentQuest();
        if(currentQuest != null)
        {
            if ((currentQuest.questName.Equals("(incomprehensible pancake nonsense)") && currentQuest.getCompleted()) ||
            (currentQuest.questName.Equals("Missing Piece") && !currentQuest.getCompleted()))
            {
                wholePancakeTexture.SetActive(false);
                pancakeTextureMissingPiece.SetActive(true);
            }
            else
            {
                wholePancakeTexture.SetActive(true);
                pancakeTextureMissingPiece.SetActive(false);
            }
        }
        
    }
}
