using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    //Stores for our movement
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    //Stores for mouse input
    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    //Umbrella Movement
    [SerializeField] Transform playerUmbrella = null;
    [SerializeField] Transform RainArea = null;
    float scrollSpeed = 5f;
    public float RAMod = 1f;

    private void Awake()
    {
        //I'm setting the controller in awake. Tutorial put it in start.
        controller = GetComponent<CharacterController>(); 
    }
    void Start()
    {
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        UpdateUmbrella();
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        
        //Clamped Camera movement to look only at floor and not go above 10 angle
        cameraPitch -= currentMouseDelta.y * mouseSensitivity; //We subtract to invert the delta
        cameraPitch = Mathf.Clamp(cameraPitch, -10.0f, 90.0f); //Clamp camera

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        //This is the vector we are trying to get to, without any smoothing
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize(); //Keeps all movement directions to 1, even if moving NE, SW, etc.

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed
            + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);
    }

    //This is the code that will spin the umbrella
    void UpdateUmbrella()
    {
        float scrollWheelDir = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        playerUmbrella.GetComponent<Rigidbody>().AddRelativeTorque(playerUmbrella.up* scrollWheelDir, ForceMode.VelocityChange);

        //rain area scales off of a base modifier * umbrella rotation speed
        var rotV = playerUmbrella.GetComponent<Rigidbody>().angularVelocity;
        RainArea.GetComponent<Transform>().localScale = new Vector3(RAMod * rotV.magnitude,0.1f, RAMod * rotV.magnitude);
        
    }
}
