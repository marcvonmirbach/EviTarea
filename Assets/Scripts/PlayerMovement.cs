using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour
{

    // Variables
    [SerializeField] private float walkSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;
    private Vector3 moveArmDirection;

    GameObject player;
    /*
    private Text score_player1;
    private Text score_player2;
    */

    [SerializeField] private bool isGrounded; // is the player on the ground?
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] GameObject ball_prefab;
    [SerializeField] private float jumpHeight;

    // SoundFX
    [SerializeField] private AudioSource audioMovement;
    [SerializeField] private AudioSource audioSword;

    // References to Unity Editor
    private CharacterController controller;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        if (GameObject.Find("Ball(Clone)") == null)
        {
            GameObject sphere = (GameObject)Instantiate(ball_prefab, new Vector3(0, 1, 0), transform.rotation);
            NetworkServer.Spawn(sphere);
            ClientScene.RegisterPrefab(sphere);
        }



        // Change player name  & colors

        if (GameObject.Find("Player1") == null)
        {
            this.gameObject.name = "Player1";

            Transform[] children = transform.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name == "Torso")
                {
                    child.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_Color", Color.green);
                }
            }            
        
        }
        else
        {
            this.gameObject.name = "Player2";

            Transform[] children = transform.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.name == "Torso")
                {
                    child.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("_Color", Color.yellow);
                }
            }
        }


    }


    // Update is called once per frame
    void Update()
    {

        if (this.isLocalPlayer)
        {
            anim.SetBool("isHitting", false);
            Movement();
        }
    }

    // Character movement
    private void Movement()
    {
        // Creates a small sphere at the bottom of the character and checks if it's touching the ground layer
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask); // returns true | false

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5f; // Stops applying gravity
        }

        float moveZ = Input.GetAxis("Vertical"); // 1 when 'W' is pressed, -1 when 'S' is pressed
        float moveX = Input.GetAxis("Horizontal"); // 1 when 'A' is pressed, -1 when 'D' is pressed

        // TODO: Slow down movement when jumping
        moveDirection = new Vector3(moveX, 0, moveZ);

        if (isGrounded)
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if(moveDirection !=  Vector3.zero)
            {
                anim.SetBool("isRunning", true);
                audioMovement.mute = false;
            } else {
                anim.SetBool("isRunning", false);
                audioMovement.mute = true;
            }

        }
        else
        {
            audioMovement.mute = true;
        }

        if (Input.GetMouseButton(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_01"))
        {
            anim.SetBool("isHitting", true);
            audioSword.Play();
        }


        // Basic movement

        moveDirection *= walkSpeed;
        controller.Move(moveDirection * Time.deltaTime); // Move() is a Unity function

        velocity.y += gravity * Time.deltaTime; // Calculates gravity
        controller.Move(velocity * Time.deltaTime); // Apply gravity to our character


        // Rotation movement
        
        if(moveDirection !=  Vector3.zero) { 
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up); // Quaternion is a special datatype for rotation
            controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, toRotation, 720f * Time.deltaTime); // 720 is rotationSpeed
        }

    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Weird formula to calculate jumping distance =)
    }

    
}
