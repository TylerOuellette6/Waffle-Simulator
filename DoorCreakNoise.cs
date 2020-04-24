using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCreakNoise : MonoBehaviour
{
    public GameObject door;
    public AudioSource creak;
    private bool playOnce;

    // Update is called once per frame
    void Update()
    {
        if(door.GetComponent<Rigidbody>().velocity.magnitude >= 6.5 && !creak.isPlaying && !playOnce)
        {
            creak.Play();
            playOnce = true;
        }

        if (playOnce)
        {
            StartCoroutine(waitFiveSeconds());
        }

        IEnumerator waitFiveSeconds()
        {
            yield return new WaitForSeconds(5);
            playOnce = false;
        }
    }
}
