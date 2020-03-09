using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickupHandler : MonoBehaviour
{
    public GameObject collectible;
    public static GameObject waffle;

    void Start()
    {
        waffle = GameObject.Find("Waffle");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider);
    }
}
