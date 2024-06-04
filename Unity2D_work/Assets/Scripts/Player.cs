using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float groundCheckRadius;
    public KeyCode jumpKey;
    public AudioClip jumpSound;

    public Transform groundCheck;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private Vector2 move;
    private bool isJumpButtonPressed;
    private bool isGrounded;
    private bool isLadder;
    private AudioSource audioSource;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();   
    }   
    
    void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal"); // -1 0 1
        move.y = Input.GetAxisRaw("Vertical");


        if (Input.GetKeyDown(jumpKey))
        {
            isJumpButtonPressed = true;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();

        rb.velocity = new Vector2(speed * move.x,rb.velocity.y);

        if(isLadder) 
        {
        rb.velocity = new Vector2(rb.velocity.x, move.y*speed);
        }


        if (move.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * move.x, transform.localScale.y, transform.localScale.z); // Flip
        }

        if (isJumpButtonPressed && isGrounded)
        {
            rb.AddForce(new Vector2(0,jumpForce));
            PlayJumpSound();        
            
        }

        isJumpButtonPressed=false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundMask);
        //Debug.DrawLine(groundCheck.position,groundCheck.position+new Vector3(0,-groundCheckRadius,0),Color.red);
    }

    public void SetLadder(bool value) {
        isLadder = value;
        rb.gravityScale = value? 0 : 1;
    }

    private void PlayJumpSound()
    {
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

}
