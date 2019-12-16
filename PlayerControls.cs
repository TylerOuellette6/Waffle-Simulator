using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Canvas questUI;
    public Canvas inventoryUI;

    Rigidbody rigidbody;
    CharacterController characterController;
    public float speed = 10.0f;
    public float gravity = 20.0f;

    private bool questUIVisible;
    private bool inventoryUIVisible;

    Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        questUI.enabled = false;
        questUIVisible = false;

        inventoryUI.enabled = false;
        inventoryUIVisible = false;

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;

        characterController = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
            body.velocity += hit.controller.velocity;
    }


    // Update is called once per frame
    void Update()
    {
        //if (characterController.isGrounded)
        //{
        //    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        //    moveDirection *= speed;
        //    if (Input.GetButton("Jump"))
        //    {
        //        moveDirection.y = 8.0f;
        //    }
        //}


        //moveDirection.y -= gravity * Time.deltaTime;
        //characterController.Move(moveDirection * Time.deltaTime);
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //rigidbody.AddForce(new Vector3(-moveHorizontal, 0.0f, -moveVertical) * speed);

        if (Input.GetKey(KeyCode.D))
        {
            //rigidbody.AddForce(Vector3.right);
            rigidbody.velocity = transform.right * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rigidbody.AddForce(Vector3.left);
            rigidbody.velocity = -transform.right * speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //rigidbody.AddForce(Vector3.forward);
            rigidbody.velocity = transform.up * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //rigidbody.AddForce(Vector3.back);
            rigidbody.velocity = -transform.up * speed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            rigidbody.AddForce(new Vector3(0.0f, 4.0f, 0.0f), ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Q) && !inventoryUIVisible)
        {
            Cursor.visible = !questUIVisible;
            if (questUIVisible)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
            questUI.enabled = !questUIVisible;
            questUIVisible = !questUIVisible;
        }

        if (Input.GetKeyDown(KeyCode.E) && !questUIVisible)
        {
            Cursor.visible = !inventoryUIVisible;
            if (inventoryUIVisible)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
            inventoryUI.enabled = !inventoryUIVisible;
            inventoryUIVisible = !inventoryUIVisible;
        }

    }
}
