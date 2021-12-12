using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float moveSpeed = 10.0f;
    public bool isOnGround = true;
    private Rigidbody playerRb;
    public float jumpForce = 8;
    public bool gameOver;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip winSound;
    private AudioSource playerAudio;
    public float jumpSpeed = 30.0F;
    public float gravity = 1.0F;
    private Vector3 moveDirection = Vector3.zero;





    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            CharacterController controller = GetComponent<CharacterController>();
            Vector3 forward = transform.TransformDirection(Vector3.right);
            float curSpeed = moveSpeed * Input.GetAxis("Horizontal");
            controller.SimpleMove(forward * curSpeed);

            if (controller.isGrounded && Input.GetButton("Jump"))
            {
                moveDirection.y = 20;
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        } // referenced from https://docs.unity3d.com/ScriptReference/CharacterController.Move.html


    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

        }
        else if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            playerAudio.PlayOneShot(deathSound);
        }
        else if (collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("You won!");
            gameOver = true;
            playerAudio.PlayOneShot(winSound);
        }
    }
}