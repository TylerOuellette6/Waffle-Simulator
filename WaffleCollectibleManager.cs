using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleCollectibleManager : MonoBehaviour
{
    private List<GameObject> collectiblesFound;
    public Text collectibleCountText;

    void Start()
    {
        collectiblesFound = new List<GameObject>();
        collectibleCountText.text = collectiblesFound.Count + " / TOTAL";
    }

    public void addCollectible(GameObject newCollectible)
    {
        collectiblesFound.Add(newCollectible);
        // TODO: Add total when done
        collectibleCountText.text = collectiblesFound.Count + " / TOTAL"; 
    }
}
