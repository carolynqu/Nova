using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : Controller2D
{
    private SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    { 
        Movement();
        GravityFlip();
        UpdateGrounding();
    }
}
