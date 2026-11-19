using UnityEngine;

public class reciever_controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool powered;
    public SpriteRenderer SR;
    void Start()
    {
        SR.color = Color.darkGray;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void becomePowered()
    {
        powered = true;
        SR.color = Color.green;
    }
}
