using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Controller2D : MonoBehaviour
{
   
    protected Rigidbody2D rb2d;
    public GravityControl gravity;

    public float speed = 5;

    public bool grounded;
    public LayerMask groundMask;
    public float groundRay = 1.1f;
    public float raySpread = 0.3f;

    [HideInInspector] public Vector2 playerVelocity = new Vector2();

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame

    //public void Movement()
    //{

    //    Vector2 movement = rb2d.velocity;
    //    movement.x = Input.GetAxis("Horizontal") * speed;
    //    playerVelocity = rb2d.velocity;

    //    rb2d.velocity = movement;

    //}

    protected bool UpdateGrounding()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, groundRay, groundMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundRay, Color.red);

        Vector3 rayStartLeft = transform.position + Vector3.up * groundRay + Vector3.left * raySpread;
        Vector3 rayStartRight = transform.position + Vector3.up * groundRay + Vector3.right * raySpread;

        RaycastHit2D hitLeft = Physics2D.Raycast(rayStartLeft, Vector2.down, groundRay * 2, groundMask);
        RaycastHit2D hitRight = Physics2D.Raycast(rayStartRight, Vector2.down, groundRay * 2, groundMask);

        Debug.DrawLine(rayStartLeft, rayStartLeft + Vector3.down * groundRay * 2, Color.red);
        Debug.DrawLine(rayStartRight, rayStartRight + Vector3.down * groundRay * 2, Color.red);

        if (hit.collider != null)
        {
            grounded = true;
            return true;
        }
        else if (hitLeft.collider != null)
        {
            grounded = true;
            return true;
        }
        else if (hitRight.collider != null)
        {
            grounded = true;
            return true;
        }

        grounded = false;
        return false;
        
    }

    public void GravityFlip()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2d.velocity = new Vector2(0, 0);

            if (rb2d.gravityScale > 0)
            {
                Physics2D.gravity = new Vector2(0, -10);
            }
            else
            {
                Physics2D.gravity = new Vector2(0, 10);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dangerous"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
