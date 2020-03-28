using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PancakeTextureController : MonoBehaviour
{
    public NPCQuestManager pancakeQuestManager;
    public GameObject wholePancakeTexture;
    public GameObject pancakeTextureMissingPiece;
    public GameObject missingPancakePiece;

    private bool hasPieceBeenShown = false;

    // Update is called once per frame
    void Update()
    {
        Quest currentQuest = pancakeQuestManager.getTempCurrentQuest();
        if(currentQuest != null)
        {
            if ((currentQuest.questName.Equals("(Incomprehensible pancake nonsense)") && currentQuest.getCompleted()) ||
            (currentQuest.questName.Equals("Missing Piece") && !currentQuest.getCompleted()))
            {
                wholePancakeTexture.SetActive(false);
                showPancakePiece();
            }
            else
            {
                wholePancakeTexture.SetActive(true);
                pancakeTextureMissingPiece.SetActive(false);
            }
        }
        
    }

    private void showPancakePiece()
    {
        if (!hasPieceBeenShown)
        {
            pancakeTextureMissingPiece.SetActive(true);
            hasPieceBeenShown = true;
            missingPancakePiece.SetActive(true);
        }
    }
}
