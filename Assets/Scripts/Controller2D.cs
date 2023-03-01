using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Controller2D : MonoBehaviour
{
   
    protected Rigidbody2D rb2d;
    public GravityControl gravity;

    public float speed = 5;
    public float jumpForce = 5;

    public bool grounded;
    public LayerMask groundMask;

    [HideInInspector] public Vector2 playerVelocity = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        

    }

    // Update is called once per frame
    
    public void Movement()
    {

        Vector2 movement = rb2d.velocity;
        movement.x = Input.GetAxis("Horizontal") * speed;
        playerVelocity = rb2d.velocity;

        rb2d.velocity = movement;

    }

    protected bool UpdateGrounding()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 0.1f, groundMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.red);

        if (hit.collider != null)
        {
            grounded = true;
            return true;
        }
        
        grounded = false;

        return false;
    }

    public void GravityFlip()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.velocity = new Vector2(0, 0);

            if (rb2d.gravityScale > 0)
            {
                rb2d.gravityScale = -5f;
            }
            else
            {
                rb2d.gravityScale = 5f;
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
