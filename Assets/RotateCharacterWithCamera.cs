using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// https://answers.unity.com/questions/1401917/third-person-character-rotation-with-mouse.html
// Test
public class RotateCharacterWithCamera : MonoBehaviour
{
    public float mouseSpeed = 3;
    public Transform player;
    public Camera camera;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float X = Input.GetAxis("Mouse X") * mouseSpeed;
        float Y = Input.GetAxis("Mouse Y") * mouseSpeed;
        //this.rotation = new Vector3(0, -X, 0);
        //this.transform.Rotate(this.rotation);

        player.Rotate(0, 0, -X);
        if (camera.transform.eulerAngles.x + (-Y) > 80 &&
            camera.transform.eulerAngles.x + (-Y) < 280)
        {

        }
        else
        {
            camera.transform.RotateAround(player.position, camera.transform.right, -Y);
        }
    }
}
