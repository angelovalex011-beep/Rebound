using UnityEngine;
using UnityEngine.InputSystem;
public class BallMovement : MonoBehaviour
{

public float rollTorque = 10f;
public float moveForce = 5f;
private Rigidbody2D rb;
public float moveInput;
public bool isGrounded;
public float maxSpeed = 8f;

public float CurrentVerticalVelocity;

[Header("Jump Settings")]
public float jumpForce = 3f;
public float holdJumpBoost = 12f;
public float maxJumpTime = 0.25f;
private bool isJumping;
private float jumpTimeCounter;
public Animator BallAnimator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(isGrounded && Keyboard.current != null && Keyboard.current.spaceKey.isPressed)
        {
            isJumping = true;
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }


        if(Keyboard.current.spaceKey.isPressed && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                rb.linearVelocity += new Vector2(0f, holdJumpBoost * Time.deltaTime);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            isJumping = false;
        }



    }


    void FixedUpdate()
    { 
          CurrentVerticalVelocity = rb.linearVelocity.y;
        
        if(Mathf.Abs(rb.linearVelocity.x) < maxSpeed){
        rb.AddTorque(-moveInput * rollTorque);
        rb.AddForce(new Vector2(moveInput * moveForce, 0f));
         }
    }




    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Ground") && CurrentVerticalVelocity < -1f)
        {
            isGrounded = true;
            BallAnimator.Play("Ball_Clip", 0, 0f);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

        
}
