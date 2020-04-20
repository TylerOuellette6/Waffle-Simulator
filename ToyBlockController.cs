using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyBlockController : MonoBehaviour
{
    public GameObject block;
    private bool inBox = false;

    // Update is called once per frame
    void Update()
    {
        if ((block.transform.position.x >= -310 && block.transform.position.x <= -275) &&
            (block.transform.position.z <= -125 && block.transform.position.z >= -160) &&
            !inBox)
        {
            AchievementsController.checkIfBlocksCleanedUp();
            inBox = true;
            block.GetComponent<ToyBlockController>().enabled = false;
        }
    }
}
