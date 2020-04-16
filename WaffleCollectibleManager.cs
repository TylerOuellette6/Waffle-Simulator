using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaffleCollectibleManager : MonoBehaviour
{
    private List<GameObject> collectiblesFound;
    public Text collectibleCountText;
    public AudioSource collectibleSound;

    void Start()
    {
        collectiblesFound = new List<GameObject>();
        collectibleCountText.text = collectiblesFound.Count + " / TOTAL";
    }

    public void addCollectible(GameObject newCollectible)
    {
        collectibleSound.Play();
        collectiblesFound.Add(newCollectible);
        // TODO: Add total when done
        collectibleCountText.text = collectiblesFound.Count + " / TOTAL"; 
        if(collectiblesFound.Count == 5)
        {
            AchievementsController.unlockSomeButter();
        }
        if(collectiblesFound.Count == 10)
        {
            AchievementsController.unlockSomeMoreButter();
        }
        if(collectiblesFound.Count == 15)
        {
            AchievementsController.unlockSomeMoreMoreButter();
        }
        // TODO: Add how many final butters
    }
}
