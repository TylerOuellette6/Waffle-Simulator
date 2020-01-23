using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Canvas questUI;
    public Canvas inventoryUI;

    //Rigidbody rigidbody;
    CharacterController characterController;
    public float moveSpeed = 10.0f;
    public float jumpHeight = 3.0f;

    private bool questUIVisible;
    private bool inventoryUIVisible;

    private Vector3 velocity;

    private int mouseSpeed = 3;



    public float walkSpeed = 10;
    public float runSpeed = 100;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    Transform cameraT;






    // Start is called before the first frame update
    void Start()
    {
        cameraT = Camera.main.transform;

        questUI.enabled = false;
        questUIVisible = false;

        inventoryUI.enabled = false;
        inventoryUIVisible = false;

        //rigidbody = GetComponent<Rigidbody>();
        //rigidbody.freezeRotation = true;

        characterController = GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float pushPower = 8.0f;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);


        //if (characterController.isGrounded && velocity.y < 0)
        //    velocity.y = 0f;
        //if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        //    velocity.y += Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);

        //if (Input.anyKey)
        //{
        //    Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    characterController.Move(move * Time.deltaTime * moveSpeed);
        //    Debug.Log(move);
        //}

        //velocity += Physics.gravity * Time.deltaTime;
        //characterController.Move(velocity * Time.deltaTime);
        //transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Mouse X") * mouseSpeed));

        //if (move != Vector3.zero)
        //    transform.forward = move;
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //rigidbody.AddForce(new Vector3(-moveHorizontal, 0.0f, -moveVertical) * speed);

        //if (Input.GetKey(KeyCode.D))
        //{
        //    //rigidbody.AddForce(Vector3.right);
        //    rigidbody.velocity = transform.right * speed;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    //rigidbody.AddForce(Vector3.left);
        //    rigidbody.velocity = -transform.right * speed;
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //rigidbody.AddForce(Vector3.forward);
        //    rigidbody.velocity = transform.up * speed;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //rigidbody.AddForce(Vector3.back);
        //    rigidbody.velocity = -transform.up * speed;
        //}
        //if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        //{
        //    rigidbody.AddForce(new Vector3(0.0f, 4.0f, 0.0f), ForceMode.Impulse);
        //}

        if (Input.GetKeyDown(KeyCode.Q) && !inventoryUIVisible)
        {
            Cursor.visible = !questUIVisible;
            if (questUIVisible)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            questUI.enabled = !questUIVisible;
            questUIVisible = !questUIVisible;
        }

        if (Input.GetKeyDown(KeyCode.E) && !questUIVisible)
        {
            Cursor.visible = !inventoryUIVisible;
            if (inventoryUIVisible)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
            inventoryUI.enabled = !inventoryUIVisible;
            inventoryUIVisible = !inventoryUIVisible;
        }

    }
}
