using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : Controller2D
{
    private SpriteRenderer mySpriteRenderer;
    private float inputX;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = rb2d.velocity;
        movement.x = inputX;
        playerVelocity = movement;

        UpdateGrounding();
        GravityFlip();


        rb2d.velocity = movement;
    }
}
