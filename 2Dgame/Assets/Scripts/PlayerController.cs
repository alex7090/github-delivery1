using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    public float speed;
    private float moveInput;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float JumpForce;
    private float jumpTimeCounter;
    private float attackTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            animator.SetFloat("Direction", Input.GetAxis("Horizontal"));
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
            if (Input.GetAxis("Horizontal") < 0 )
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.position = transform.position + direction * Time.deltaTime;
        } else {
            animator.SetFloat("Direction", 0.0f);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("test: ");
            animator.SetBool("Jump", true);
            jumpTimeCounter = jumpTime;
            isJumping = true;
            rb.velocity = Vector2.up * JumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * JumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Jump", false);
            isJumping = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttacking = true;
            attackTimeCounter = 0;
            animator.SetBool("Attack", true);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("Attack", false);
        }
        if (isAttacking)
        {
            Debug.Log(isAttacking);
            attackTimeCounter += Time.deltaTime;
        }
        if (attackTimeCounter >= 0.583f)
        {
            isAttacking = false;
        }
    }
}
