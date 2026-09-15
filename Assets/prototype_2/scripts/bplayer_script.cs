using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class bplayer_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed;
    public bool charged;
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    private Vector2 velocity;
    void Start()
    {
        charged = false;
        SR.color = Color.gray7;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = Vector2.zero;
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            velocity.x = speed;
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            velocity.x = -speed;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            velocity.y = speed;
        }

        if (Keyboard.current.downArrowKey.isPressed)
        {
            velocity.y = -speed;
        }

        RB.linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "charger")
        {
            charged = true;
            SR.color = Color.yellow;
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
    
    public void tranport()
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
