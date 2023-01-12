using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using TagHolder;

public class PlayerMovement : MonoBehaviour
{
    
    // Start is called before the first frame update
    private CharacterController character_controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Veloctiy;

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL),0f,
                                     Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;


        ApplyGravity();
        character_controller.Move(move_Direction);
    }// move apply

    void ApplyGravity()
    {
        vertical_Veloctiy -= gravity * Time.deltaTime;

        //jump
        PlayerJump();

        move_Direction.y = vertical_Veloctiy * Time.deltaTime;
        //move_Direction.y = vertical_Veloctiy;

    }// apply Gravity 

    void PlayerJump()
    {
        if (character_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //move_Direction.y = vertical_Veloctiy * Time.deltaTime;
            vertical_Veloctiy = jump_Force;
        }
    }

    public bool IsMoving()
    {
        float move = Input.GetAxis(Axis.VERTICAL);
        if (move == Input.GetAxis(Axis.VERTICAL))
            return true;
        else
            return false;

    } // IsMoving
}
