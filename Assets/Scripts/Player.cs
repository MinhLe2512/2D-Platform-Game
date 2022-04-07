using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Collider2D playerCol;
    private Vector3 moveDelta;
    private enum State {idle, running, jumping, falling, hurt}
    private static State state = State.idle;

    public Rigidbody2D playerRb;
    public Animator anim;
    //Platform ground
    [SerializeField] private LayerMask ground;
    [SerializeField] public float speed = 5.0f;
    [SerializeField] public float jumpForce = 7.0f;
    [SerializeField] public float bounceForce = 5.0f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;

    private void Start()
    {
        //Get Component
        boxCollider = GetComponent<BoxCollider2D>();
        playerCol = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (state != State.hurt)
            movementInput();
        handleState();
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            cherries++;
            cherryText.text = cherries.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.Defeat();
                Jump();
            }
            else
            {
                state = State.hurt;
                //Moving right -> bounce left
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    playerRb.velocity = new Vector2(-bounceForce, 0);
                }
                //Moving left -> bounce right
                else
                {
                    playerRb.velocity = new Vector2(bounceForce, 0);
                }
            }
        }
    }
    private void movementInput()
    {
        //Input A, D or < >
        float deltaX = Input.GetAxis("Horizontal");
        float deltaJump = Input.GetAxisRaw("Jump");
        /*//Input W, S or ^ v nhưng không cần trong project này
        float deltaY = Input.GetAxisRaw("Vertical");*/

        //New Vector 3
        moveDelta = new Vector3(deltaX, 0, 0);

        //Turn to input direction
        if (moveDelta.x > 0)
        {
            //Turn right
            transform.localScale = Vector2.one;
            //Move right
            playerRb.velocity = new Vector2(speed, playerRb.velocity.y);
        }
        else if (moveDelta.x < 0)
        {
            //Turn left
            transform.localScale = new Vector2(-1, 1);
            playerRb.velocity = new Vector2(-speed, playerRb.velocity.y);
        }

        //transform.position += Time.deltaTime * moveDelta * 5.0f;
        if (Input.GetButtonDown("Jump") && playerCol.IsTouchingLayers(ground))
        {
            Jump();  
        }
    }

    private void Jump()
    {
        playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        state = State.jumping;
    }
    private void handleState()
    {
        if (state == State.jumping)
        {
            if (playerRb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (playerCol.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(playerRb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (state != State.hurt && Mathf.Abs(playerRb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
            state = State.idle;
        
    }
}
