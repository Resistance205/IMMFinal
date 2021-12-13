using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public GameManager gameManager;
    public float moveSpeed = 10.0f;
    public bool isOnGround = true;
    private Rigidbody playerRb;

    public float jumpForce = 8;

    public bool gameOver;

    public bool hasPowerup = false;
    public GameObject powerupAbility;


    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip winSound;
    private AudioSource playerAudio;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("gameManager").GetComponent<GameManager>();



    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(powerupAbility, transform.position, powerupAbility.transform.rotation);

        }

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
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
            gameManager.GameOver(); // couldn't get this working, followed tutorial but it doesn't work
        }
        else if (collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("You won!");
            gameOver = true;
            playerAudio.PlayOneShot(winSound);
        }
    }
}