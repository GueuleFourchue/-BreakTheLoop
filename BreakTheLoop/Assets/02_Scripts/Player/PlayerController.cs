using UnityEngine;
using System.Collections;
using DG.Tweening;


public class PlayerController : MonoBehaviour {

    // public vars
    [Header("Mouse Look")]
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;

    [Header("On Ground")]
    public float walkSpeed = 6;
    public float runSpeed = 9;

    [Header("In Air")]
    public float airSpeed = 2f;

    [Header("Jump")]
    public float jumpForce = 220;

    [Header("Gravity")]
    public float gravity;

    [Header("Drag and Drop")]
    public Rigidbody rb;
    public Transform cameraTransform;
    public Transform capsuleTransform;
    public HeadBob headBob;
    public CameraEffects cameraEffects;
    public SoundEffects soundEffects;

    // System vars
    bool grounded = true;
    bool canJump = true;
    bool isRunning;
    float verticalLookRotation;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    Vector3 targetMoveAmount;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraLook();
        Gravity();
        Movement();
        CheckGrounded();
        //CheckRun();

        if (Input.GetButtonDown("Jump") && grounded && canJump)
        {
            StartCoroutine(Jump());
        }
    }

    void Gravity()
    {
        rb.AddForce(-Vector3.up * gravity);
    }

    void CheckGrounded()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1 + .1f))
        {
            if (!grounded)
                grounded = true;
        }
        else if (grounded)
            grounded = false;
    }

    void CameraLook()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivityX);

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -80, 80);
        cameraTransform.localEulerAngles = -Vector3.right * verticalLookRotation;
    }

    
	IEnumerator Jump ()
    {
        canJump = false;
        soundEffects.PlaySoundOnce(soundEffects.jump);
        rb.AddForce(Vector3.up * jumpForce * Time.deltaTime * 1000, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        canJump = true;
	}

    void Movement()
    {
        //Calculate movement
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;

        if (isRunning)
            targetMoveAmount = moveDir * runSpeed;
        else
            targetMoveAmount = moveDir * walkSpeed;

        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);


        //WalkSoundEffect
        if (moveDir != Vector3.zero)
        {
            if (grounded && soundEffects.isWalking == false)
            {
                soundEffects.footSteps_Timer = 0.8f;
                soundEffects.isWalking = true;
            }
            else if (!grounded)
            {
                if (soundEffects.isWalking == true)
                {
                    soundEffects.footSteps_Timer = 0;
                    soundEffects.isWalking = false;
                }
            }
        }
        else if (moveDir == Vector3.zero)
        {
            if (soundEffects.isWalking == true)
            {
                soundEffects.footSteps_Timer = 0;
                soundEffects.isWalking = false;
            }   
        }
    }

    void FixedUpdate()
    {
        //Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + localMove);
    }

    void CheckRun()
    {
        if (Input.GetButtonDown("Run"))
        {
            isRunning = true;
            headBob.isRunning = true;
        }
        if (Input.GetButtonUp("Run"))
        {
            isRunning = false;
            headBob.isRunning = false;
        }
    }
}