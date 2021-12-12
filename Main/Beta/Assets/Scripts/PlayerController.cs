using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();



    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
            Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            playerRb.MovePosition(transform.position + m_Input * Time.deltaTime * moveSpeed);
        }
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