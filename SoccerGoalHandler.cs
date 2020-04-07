using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerGoalHandler : MonoBehaviour
{
    public Collider soccerBallCollider;
    public Collider goalCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        goalCollider.enabled = false;
        if (collision.collider == soccerBallCollider)
        {
            AchievementsController.unlockGoal();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        goalCollider.enabled = true;
    }
}
