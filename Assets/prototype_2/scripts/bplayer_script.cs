using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class bplayer_script : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public float jumpForce;
    public float baseGravity;
    public float gravityCounter;
    public bool isJumping;
    
    //public InputAction action;
    
    public bool charged;
    
    //components
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    private Vector2 velocity;
    void Start()
    {
        SetState(PlayerState.unpowered);
        RB.gravityScale = baseGravity;
    }

    // Update is called once per frame
    void Update()
    {
            //velocity.y = RB.linearVelocity.y;
    }

    public void FixedUpdate()
    {
        //This functions as an if statement. If the player is jumping then gravity is active (velocity.y = RB.LinearVelocityY)
        velocity.y = isJumping ? velocity.y : RB.linearVelocityY;
        RB.linearVelocity = velocity;
        
        //This subtracts until gravity counter is equal to 0
        gravityCounter = Mathf.Max(gravityCounter - Time.fixedDeltaTime, 0);
        
        isJumping = false;
        /*
        if (gravityCounter < 0.01)
        {
            RB.gravityScale = baseGravity;
            isJumping = false;
        }
        */
        
        //Debug.Log(velocity.y);
    }

    public void OnMove(InputValue value)
    {
        velocity.x = value.Get<Vector2>().x * speed;
    }

    //Controls jump
    public void OnJump(InputValue value)
    {
        //Debug.Log(value.isPressed);
        if (value.isPressed && canJump())
        {
            Debug.Log("jump");
            velocity.y = jumpForce;
            isJumping = true;
        }
        else if (!value.isPressed && velocity.y > 0)
        {
            //this controls hang time
            velocity.y = 0;
            //RB.gravityScale = baseGravity - 1;
            gravityCounter = 0.2f;
            Debug.Log("hang time");
            //isJumping = false;
        }
        else
        {
            isJumping = false;
        }
        //RB.linearVelocityY = velocity.y;
    }

    public void SetState(PlayerState nextState)
    {
        switch (nextState)
        {
            case PlayerState.unpowered:
                SR.color = Color.gray7;
                charged = false;
                break;
            case PlayerState.powered:
                SR.color = Color.yellow;
                charged = true;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("charger"))
        {
            SetState(PlayerState.powered);
        }
        else if (other.gameObject.TryGetComponent(out reciever_controller reciever))
        {
            if (charged)
            {
                reciever.becomePowered();
            }
            charged = false;
            SR.color = Color.gray7;
        }
    }

    public bool canJump()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);
        //Debug.DrawRay(transform.position, Vector2.down * 0.6f, Color.red, 0.1f);
        if (hit.collider != null)
        {
            if  (hit.collider.CompareTag("Floor"))
                return true;
        }
        return false;
    }
    
    public void transport()
    {
        if (charged)
        {
            charged = false;
            SR.color = Color.gray7;
        }
        else
        {
            charged = true;
            SR.color = Color.yellow;
        }
    }
}

public enum PlayerState
{
    unpowered = 0,
    powered = 1,
}
