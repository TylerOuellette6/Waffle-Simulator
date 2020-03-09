using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // CREDIT TO: https://github.com/SebLague/Blender-to-Unity-Character-Creation
    public Canvas questUI;
    public Canvas inventoryUI;
    public Canvas pauseUI;
    private bool questUIVisible;
    private bool inventoryUIVisible;
    private static bool pauseUIVisible;

    public float walkSpeed = 25;
    public float runSpeed = 50; // SET TO 25 WHEN DONE WITH TESTING
    public float gravity = -12;
    public float jumpHeight = 1;
    [Range(0, 1)]
    public float airControlPercent;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    float velocityY;

    public GameObject camera;

    Transform cameraT;
    CharacterController controller;

    void Start()
    {
        cameraT = Camera.main.transform;
        controller = GetComponent<CharacterController>();

        questUIVisible = false;
        inventoryUIVisible = false;
        pauseUIVisible = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float pushPower = 8.0f;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.AddForce(pushDir * pushPower, ForceMode.Impulse);
        //body.velocity = (pushDir * pushPower);
    }

    void Update()
    {
        // input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        bool running = Input.GetKey(KeyCode.LeftShift);

        Move(inputDir, running);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !inventoryUIVisible)
        {
            Cursor.visible = !questUIVisible;
            if (questUIVisible) { 
                Cursor.lockState = CursorLockMode.Locked;  
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            camera.GetComponent<ThirdPersonCamera>().enabled = questUIVisible;
            this.GetComponent<CharacterController>().enabled = questUIVisible;

            questUI.enabled = !questUIVisible;
            questUIVisible = !questUIVisible;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !pauseUIVisible;
            if (pauseUIVisible)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }

            camera.GetComponent<ThirdPersonCamera>().enabled = pauseUIVisible;
            this.GetComponent<CharacterController>().enabled = pauseUIVisible;

            pauseUI.enabled = !pauseUIVisible;
            pauseUIVisible = !pauseUIVisible;
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

            camera.GetComponent<ThirdPersonCamera>().enabled = inventoryUIVisible;
            this.GetComponent<CharacterController>().enabled = inventoryUIVisible;

            inventoryUI.enabled = !inventoryUIVisible;
            inventoryUIVisible = !inventoryUIVisible;
        }
    }

    void Move(Vector2 inputDir, bool running)
    {
        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
        }

        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        velocityY += Time.deltaTime * gravity;
        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
        currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

        if (controller.isGrounded)
        {
            velocityY = 0;
        }

    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (controller.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }

    public void setRunSpeed(int speed)
    {
        this.runSpeed = speed;
    }

    public static void setPauseUIVisible(bool visible)
    {
        PlayerController.pauseUIVisible = visible;
    }
}