using UnityEngine;

public class water_controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bplayer_script player;
    public Collider2D col;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.charged == false)
        {
            col.isTrigger = true;
        }
        else
        {
            col.isTrigger = false;
        }
    }
    
}
