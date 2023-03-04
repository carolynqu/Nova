using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimator : MonoBehaviour
{
   

    public float animationFPS;
    public Sprite[] activeAnimation;

    private SpriteRenderer mySpriteRenderer;


    private float frameTimer = 0;
    private int frameIndex = 0;
 
   
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            frameIndex %= activeAnimation.Length;
            mySpriteRenderer.sprite = activeAnimation[frameIndex];
            frameIndex++;
        }
    }
}
