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

    //Audio
    private AudioSource aud;
    [SerializeField] AudioClip[] Walk;
    [SerializeField] float stepRate = 10;
    private float stepT = 0;

    //Umbrella Movement
    [SerializeField] Transform playerUmbrella = null;
    [SerializeField] Transform RainArea = null;
    [SerializeField] float scrollSpeed = 5f;
    public float RAMod = 1f;

    [SerializeField]float counter = 0;
    
    
    //Umbrella Particle VFX
    [SerializeField] private ParticleSystem umbrellaParticles;
    [SerializeField] [Range(0, 5)] private float maxYParticleSpd;
    [SerializeField] [Range(0, 5)] private float maxRParticleSpd;
    private bool _spinBurst;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        //I'm setting the controller in awake. Tutorial put it in start.
        controller = GetComponent<CharacterController>(); 
    }
    void Start()
    {
        Time.timeScale = 1f;
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        stepT = stepRate / walkSpeed;
    }

    void Update()
    {
        UpdateStep();
        UpdateMouseLook();
        UpdateMovement();
        UpdateUmbrella();
    }

    void UpdateStep()
    {
    //setting and reseting the timer between step sound effects
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            stepT -= Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            stepT = 0.2f;
        }
        if (stepT <= 0)
        {
            aud.PlayOneShot(Walk[Random.Range(0, Walk.Length - 1)]);
            stepT = stepRate / walkSpeed;
        }
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        
        //Clamped Camera movement to look only at floor and not go above 10 angle
        cameraPitch -= currentMouseDelta.y * mouseSensitivity; //We subtract to invert the delta
        cameraPitch = Mathf.Clamp(cameraPitch, -15.0f, 35.0f); //Clamp camera

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
        Rigidbody umbrellaRigid = playerUmbrella.GetComponent<Rigidbody>();

        umbrellaRigid.AddRelativeTorque(playerUmbrella.up* scrollWheelDir, ForceMode.VelocityChange);
        umbrellaRigid.maxAngularVelocity = 12;
        
        //Stopping the Umbrella when player stops spinning
        var umbrellaParticlesEmission = umbrellaParticles.emission;
        if(scrollWheelDir == 0)
        {
            counter += Time.deltaTime;
        }
        else
        {
            counter = 0;
            if (!_spinBurst)
            {
                _spinBurst = true;
                umbrellaParticlesEmission.SetBurst(0, new ParticleSystem.Burst(0f, 3));
            }
        }

        if(counter >= .35f)
        {
            //Debug.Log("Starting Stop");
            umbrellaRigid.angularVelocity = Vector3.Slerp(umbrellaRigid.angularVelocity, Vector3.zero, .1f);
            _spinBurst = false;
            umbrellaParticlesEmission.SetBurst(0, new ParticleSystem.Burst(0 ,0));
        }


        // Changes particle behavior based on the umbrella's spin speed.
        var proportion = Mathf.Clamp(1 - counter, 0, 1);
        //Debug.Log(proportion);
        var vOL = umbrellaParticles.velocityOverLifetime;
        vOL.radial = new ParticleSystem.MinMaxCurve(0, proportion * maxRParticleSpd);
        vOL.y = new ParticleSystem.MinMaxCurve(proportion * maxYParticleSpd);
        umbrellaParticlesEmission.rateOverTime = new ParticleSystem.MinMaxCurve(Mathf.Lerp(20, 80, proportion));


        //rain area scales off of a base modifier * umbrella rotation speed
        var rotV = playerUmbrella.GetComponent<Rigidbody>().angularVelocity;
        RainArea.GetComponent<Transform>().localScale = new Vector3(RAMod * rotV.magnitude,0.1f, RAMod * rotV.magnitude);

    }
}
