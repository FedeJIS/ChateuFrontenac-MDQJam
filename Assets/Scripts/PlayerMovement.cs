#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -10f;
    public float jumpHeight = 2f;

    public float sprintEnergy = 10f;
    public float currentEnergy;
    private bool canRun= true;
    public float run = 2f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioClip[] sfxClips;
    
    public AudioSource audioSource;

    public Animator handAnimator;
    public BatteryHandler battery;

    Vector3 velocity;
    bool isGrounded;

#if ENABLE_INPUT_SYSTEM
    InputAction movement;
    InputAction jump;

    void Start()
    {
        movement = new InputAction("PlayerMovement", binding: "<Gamepad>/leftStick");
        movement.AddCompositeBinding("Dpad")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");
        
        jump = new InputAction("PlayerJump", binding: "<Gamepad>/a");
        jump.AddBinding("<Keyboard>/space");

        movement.Enable();
        jump.Enable();
        audioSource = GetComponent<AudioSource>();
    }
#endif

    // Update is called once per frame
    void Update()
    {
        float x;
        float z;

#if ENABLE_INPUT_SYSTEM
        var delta = movement.ReadValue<Vector2>();
        x = delta.x;
        z = delta.y;
#else
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
#endif

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector3 move = transform.right * x + transform.forward * z;
        if(GameManager.currentAlfajores < 12)
        {
            if(move != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 && canRun)
            {
            currentEnergy -= Time.deltaTime*2;
            audioSource.clip = sfxClips[1];
            controller.Move(move * speed * run * Time.deltaTime);
            handAnimator.SetBool("isRunning",true); 
            }else
            {
                if(currentEnergy < sprintEnergy){ currentEnergy += Time.deltaTime/2;}
                audioSource.clip = sfxClips[0];
                controller.Move(move * speed * Time.deltaTime);
                handAnimator.SetBool("isRunning",false); 
            }

            if(currentEnergy <= 0) canRun = false;
            else if(currentEnergy >= sprintEnergy/2) canRun = true;
        }
        else
        {
            audioSource.clip = sfxClips[1];
            controller.Move(move * speed * run * Time.deltaTime);
            handAnimator.SetBool("isRunning",true); 
        }
        if(Input.GetKey(KeyCode.F)) 
        {
            
            battery.SwitchFlashlight(!BatteryHandler.isOn);
        }
        if(audioSource && move != Vector3.zero && !audioSource.isPlaying) { 
            audioSource.pitch = Random.Range(1f,1.4f);
            audioSource.Play();}

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
