using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
   
    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Move();

        if (anim.GetBool("Jumping"))
        {
            //Switch from jumping to falling
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        //Switch from falling to idling
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }
    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftWaypoint)
            {
                //Sprite is facing right -> make it face left
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
                facingLeft = false;
        }
        else
        {
            if (transform.position.x < rightWaypoint)
            {
                //Sprite is facing right -> make it face left
                if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
                facingLeft = true;
        }
    }

}