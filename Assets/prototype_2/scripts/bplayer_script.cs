using System;
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
    
    //public InputAction action;
    
    public bool charged;
    
    //components
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    private Vector2 velocity;
    void Start()
    {
        charged = false;
        SR.color = Color.gray7;
        RB.gravityScale = baseGravity;
    }

    // Update is called once per frame
    void Update()
    {
        //velocity.x = 0;
        
        /*
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            velocity.x = speed;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            velocity.x = -speed;
        }
        */

/*
        if (Keyboard.current.spaceKey.isPressed && canJump())
        {
            velocity.y = jumpForce;
        }
        */
        
        //this would have been the else
            velocity.y = RB.linearVelocity.y;
            gravityCounter -= Time.deltaTime;
            if (gravityCounter < 0)
            {
                RB.gravityScale = baseGravity;
            }
           
/*
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && RB.linearVelocity.y > 0)
        {
            velocity.y = 0.5f;
            RB.gravityScale = baseGravity - 0.9f;
            gravityCounter = 0.17f;
            
        }
*/
        //Debug.Log(velocity.y);
        //RB.linearVelocity = velocity;
    }

    public void FixedUpdate()
    {
        //Debug.Log(velocity.y);
        RB.linearVelocity = velocity;
    }

    public void OnMove(InputValue value)
    {
        velocity.x = value.Get<Vector2>().x * speed;
        //RB.linearVelocity = velocity;
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("jump");
        if (value.isPressed && canJump())
        {
            velocity.y = jumpForce;
            //gravityCounter = jumpForce;
        }
        else if (!value.isPressed && velocity.y > 0)
        {
            Debug.Log("release");
            velocity.y = 0.5f;
            RB.gravityScale = baseGravity - 1;
            gravityCounter = 0.2f;
        }
        RB.linearVelocityY = velocity.y;
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
        else if (other.gameObject.TryGetComponent(out transmitter_controller transmitter))
        {
            if (charged)
            {
                transmitter.becomePowered();
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
