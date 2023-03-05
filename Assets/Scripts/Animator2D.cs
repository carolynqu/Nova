using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator2D : MonoBehaviour
{
    // Start is called before the first frame update
    public enum AnimationState
    {
        Idle,
        Walk,
        Gravity
    }
    public float animationFPS;
    public Sprite[] walkAnimation;
    public Sprite[] gravityAnimation;
    public Sprite[] idleAnimation;

    private CharController controller;
    private SpriteRenderer mySpriteRenderer;


    private float frameTimer = 0;
    private int frameIndex = 0;
    private AnimationState state = AnimationState.Idle;
    private Dictionary<AnimationState, Sprite[]> animationAtlas;
    

    void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>();
        animationAtlas.Add(AnimationState.Idle, idleAnimation);
        animationAtlas.Add(AnimationState.Walk, walkAnimation);
        animationAtlas.Add(AnimationState.Gravity, gravityAnimation);

        
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharController>();
    }

    // Update is called once per frame

    void Update()
    {
        AnimationState newState = GetAnimationState();
        if (state != newState)
        {
            TransitionToState(newState);
        }

        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;
            Sprite[] anim = animationAtlas[state];
            frameIndex %= anim.Length;
            mySpriteRenderer.sprite = anim[frameIndex];
            frameIndex++;
        }

        if (controller.playerVelocity.x < -0.01f)
        {
            mySpriteRenderer.flipX = true;
        }

        if (controller.playerVelocity.x > 0.01f)
        {
            mySpriteRenderer.flipX = false;
        }

        if (controller.playerVelocity.y < -0.01f)
        {
            mySpriteRenderer.flipY = false;
        }

        if (controller.playerVelocity.y > 0.01f)
        {
            mySpriteRenderer.flipY = true;
        }
    }

    void TransitionToState(AnimationState newState)
    {
        frameTimer = 0.0f;
        frameIndex = 0;
        state = newState;
    }

    AnimationState GetAnimationState()
    {
        if (!controller.grounded && (controller.playerVelocity.y > 0.01f))
        {
            return AnimationState.Gravity;
        }
        if (Mathf.Abs(controller.playerVelocity.x) > 0.1f)
        {
            return AnimationState.Walk;
        }
        return AnimationState.Idle;
    }
}
