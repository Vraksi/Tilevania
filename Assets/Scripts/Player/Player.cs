using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float ladderClimbingSpeed = 3f;
    [SerializeField] float health = 10f;
    [SerializeField] bool isAlive = true;

    Animator anim;
    Rigidbody2D rb2d;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    float controlThrow;

    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        death();
    }

    private void Run()
    {
        controlThrow = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(controlThrow * movementSpeed, rb2d.velocity.y);        
    }

    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        anim.SetBool("isClimbing", myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")));
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb2d.gravityScale = 1f;
            return;
        }
        rb2d.gravityScale = 0f;
        float control = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2(rb2d.velocity.x, control * movementSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            anim.SetBool("isRunning", true);
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), transform.localScale.y);          
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    private void death()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            
            isAlive = false;
            anim.SetTrigger("isDying");
            Debug.Log("dwaeath"); 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
