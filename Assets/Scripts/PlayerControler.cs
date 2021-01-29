using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    [SerializeField] Transform playerCamera = null;

    [SerializeField] float mouseSensitivty = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -5.0f;
    [SerializeField] float jumpHeight = 3.0f;

    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;
    [SerializeField] bool isOnHeavyPlanet = false;
    [SerializeField] bool isOnNormalPlanet = false;
    [SerializeField] bool isOnLightPlanet = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;

    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
    }

    void FixedUpdate()
    {
        UpdateMove();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivty;

        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivty);
    }

    void UpdateMove()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            if(isOnLightPlanet)
            {
                velocityY = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            }
            else if (isOnNormalPlanet)
            {
                gravity = -20.0f;
                velocityY = Mathf.Sqrt(jumpHeight * -0.9f * gravity);
            }
            else if (isOnHeavyPlanet)
            {
                gravity = -60.0f;
                velocityY = Mathf.Sqrt(jumpHeight * -0.8f * gravity);
            }
        }

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }
}
