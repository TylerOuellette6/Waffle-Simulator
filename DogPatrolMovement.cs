using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogPatrolMovement : MonoBehaviour
{
    public Transform[] points;
    public int[] rotations;
    private int destPoint = 0;
    public GameObject dog;
    private bool heWalkin = true;

    private void Start()
    {
        dog.transform.position = points[0].position;
        dog.transform.rotation = Quaternion.Euler(0, rotations[0], 0);
        StartCoroutine(MoveOverSeconds(dog, points[destPoint + 1].position, 5f));
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        while (heWalkin)
        {
            float elapsedTime = 0;
            Vector3 startingPos = points[destPoint].position;
            while (elapsedTime < seconds)
            {
                objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToMove.transform.position = end;
            Debug.Log(dog.GetComponent<NPCQuestManager>().getTempCurrentQuest());
            destPoint = (destPoint >= 6 ? 0 : destPoint + 1) % points.Length;
            dog.transform.rotation = Quaternion.Euler(0, rotations[destPoint >= 5 ? rotations.Length - 1 : destPoint], 0);
            end = points[destPoint + 1].position;
        }
    }

    public void setHeWalkin(bool heWalkin)
    {
        this.heWalkin = heWalkin;
    }
}
