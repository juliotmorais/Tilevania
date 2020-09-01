﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Player : MonoBehaviour
{
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    Collider2D myCollider2D;
    bool isAlive = true;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed =5f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
        Climb();
        //RunAndJump();
        FlipSprite();
        
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow *  runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);

        //var deltaX = Input.GetAxis("Horizontal");
        //var newXPos = transform.position.x + deltaX * Time.deltaTime * runSpeed;
        //var newYPos = transform.position.y * Time.deltaTime;
        //transform.position = new Vector2(newXPos, newYPos);

    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x)>Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x),1f);
        }
    }

    private void Jump()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return; //prevent multijump
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody2D.velocity += jumpVelocity;
        }

    }

    private void Climb()
    {
        if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("IsClimbing", false);
            myRigidBody2D.gravityScale = 1;
            return; 
        }

        myRigidBody2D.gravityScale = 0;
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * climbSpeed);
        myRigidBody2D.velocity = climbVelocity;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);


    }


}