using System;
using UnityEngine;

public class transporter_controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public SpriteRenderer SR;
    public bplayer_script player;
    public bool charged;
    void Start()
    {
        SR.color = Color.gray7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out bplayer_script play))
        {
            if (!player.charged && charged)
            {
                charged = false;
                SR.color = Color.gray7;
                play.tranport();
            }
            else
            {
                SR.color = Color.yellow;
                charged = true;
                play.tranport();
            }
        }
    }
}
