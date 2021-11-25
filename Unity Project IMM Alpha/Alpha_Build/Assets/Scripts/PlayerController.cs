using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hI;
    public float moveSpeed = 10.0f;
    public bool isOnGround = true;
    private Rigidbody playerRb;
    public float jumpForce = 8;
    public bool gameOver;
   // public float gravModifier;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
       // Physics.gravity *= gravModifier;
        
   
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            hI = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * hI * Time.deltaTime * moveSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;

        }else if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("Game Over");
            gameOver = true;
        }
    }
}
