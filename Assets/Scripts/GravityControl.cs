using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public void GravityFlip(Rigidbody2D rb2d)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.velocity = new Vector2(0, 0);

            if (rb2d.gravityScale > 0)
            {
                rb2d.gravityScale = -8f;
            }
            else
            {
                rb2d.gravityScale = 8f;
            }
        }
    }
}
