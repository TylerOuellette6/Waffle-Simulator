using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickupHandler : MonoBehaviour
{
    public GameObject collectible;
    public static GameObject waffle;
    private WaffleCollectibleManager waffleCollectibleManager;

    void Start()
    {
        waffle = GameObject.Find("Waffle");
        waffleCollectibleManager = waffle.GetComponent<WaffleCollectibleManager>();
    }

    void Update()
    {
        if ((collectible.transform.position - waffle.transform.position).magnitude < 3.0f)
        {
            collectible.SetActive(false);
            waffleCollectibleManager.addCollectible(collectible);
        }
    }
}
