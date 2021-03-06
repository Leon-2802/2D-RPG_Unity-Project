﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    public soundManagerScript callSounds;
    // public float enemyDeaths;
    public bool allowTransition;

    void Start() 
    {
        // enemyDeaths = 0;
        allowTransition = false;
        StartCoroutine(nextSceneOk());
    }

    // Update is called once per frame, used for processing inputs
    void Update()
    {
        processInput();
        // if (enemyDeaths >= 2)
        // {
        //     Debug.Log("jo");       
        // }
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        Move();
        // checkForWalk();
    }

    void processInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveX, moveY).normalized; //new Vector2, da der Vector2 oben nur die "Blaupause" für den angewendeten Vector ist

        animator.SetFloat("Horizontal", moveX);
        animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", movement.sqrMagnitude); //Warum movement²?

        safeLastMove();

        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("Block", true); //Block wird ausgelöst
        }
        if(Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Block", false);
        } 

        if(Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Shoot", true); //Shoot ausgelöst
        }

    }
    void safeLastMove()
    {
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", 0);
        }
        else if(Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("LastHorizontal", 0);
        }
    }

    // public void checkForWalk() 
    // {
    //     if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
    //     {
    //         callSounds.Play("PlayerWalk");
    //     }
    //     else 
    //     {
    //         callSounds.Stop("PlayerWalk");
    //     }
    // }

    void Move()
    {
       if(animator.GetBool("Block") == true)
       {
           rb.velocity = Vector2.zero; //Während Block kein Laufen möglich
       }
       else if(animator.GetBool("Shoot") == true)
       {
           rb.velocity = Vector2.zero;
       }
       else if (animator.GetBool("Sign") == true)
       {
           rb.velocity = Vector2.zero;
       }
       else if(animator.GetBool("isDead") == true)
       {
           rb.velocity = Vector2.zero;
       }
        else
       {
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
       }
    }

    void StopShoot()
    {
        if(animator.GetBool("Shoot"))
            animator.SetBool("Shoot", false);
    } 

    public IEnumerator nextSceneOk()
    {
        yield return new WaitForSeconds(15f);
        allowTransition = true;
    }   
}   

