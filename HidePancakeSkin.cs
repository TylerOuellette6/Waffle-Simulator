using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePancakeSkin : MonoBehaviour
{
    public GameObject wholePancakeTexture;
    public GameObject pancakeTextureMissingPiece;

    void Start()
    {
        wholePancakeTexture.SetActive(true);
        pancakeTextureMissingPiece.SetActive(false);
    }
}
