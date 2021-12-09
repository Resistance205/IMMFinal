using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    public float moveSpeed = 30.0f;
    public bool isOnGround = true;
    private Rigidbody playerRb;
    public float jumpForce = 12.4f;
    public bool gameOver;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip winSound;
    private AudioSource playerAudio;

    public float gravModifier = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        characterController = gameObject.AddComponent<CharacterController>();
        Physics.gravity = new Vector3(0.0f, -gravModifier, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        characterController.Move(Vector3.right * horizontalInput * Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);

        }
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