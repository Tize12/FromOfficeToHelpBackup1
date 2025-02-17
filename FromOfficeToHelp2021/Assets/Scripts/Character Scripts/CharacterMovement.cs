﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CREADOR: LUCAS OLIVARES

public class CharacterMovement : MonoBehaviour
{
    CapsuleCollider Playercol;
    float OriginalHeight;
    public float ReducedHeigth;
    public Rigidbody RB;
    public float baseSpeed = 12f;
    public float speed;
    public Vector3 MoveDirection;
    public Animator camaraAnimator;
    public float JumpForce;
    bool isGrounded = false;
     


    private void Start()
    {
        Playercol = GetComponent<CapsuleCollider>();
        OriginalHeight = Playercol.height;
        speed = baseSpeed;
        camaraAnimator.SetBool("ShakeCamera", false);
    }
       
    public void SpeedVariation(float amount) //Pedimos un valor para saber cuanta velocidad tenemos
    {
        if (amount > 0f)
        {
            speed -= baseSpeed * amount;
            camaraAnimator.SetBool("ShakeCamera", true);
        }
        else
        {
            speed = baseSpeed;
            camaraAnimator.SetBool("ShakeCamera", false);
        }
    }

    void FixedUpdate()
    {
        float HorMov = Input.GetAxis("Horizontal");
        float VerMov = Input.GetAxis("Vertical");

        if (HorMov != 0 || VerMov != 0)
        {
            MoveDirection = (transform.forward * VerMov + transform.right * HorMov) * speed;
            MoveDirection.y = RB.velocity.y;
            RB.velocity = MoveDirection;
        }

        GroundCheck();
    }

    private void UpdateJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            RB.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            GoUp();
        }

        UpdateJump();
    }

    void Crouch() //Metodo para agacharse
    {
        Playercol.height = ReducedHeigth;
    }

    void GoUp() //Metodo para levantarse
    {
        Playercol.height = OriginalHeight;
        
    }

    void GroundCheck() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, +3f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}



