using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class CharController : MonoBehaviour
{
    protected Rigidbody2D rb2d;

    public float speed = 5;

    public bool grounded;
    public bool holdingTape;
    public bool holdingPliers;
    public LayerMask groundMask;
    public float groundRay = 1.1f;
    public float raySpread = 0.3f;
  
    public float gravity = 10;


    [HideInInspector] public Vector2 playerVelocity = new Vector2();

    private SpriteRenderer mySpriteRenderer;

    public AudioSource Gravitysound;
    public AudioSource Outofboundsound;
    public AudioSource Objectsound;
    public AudioSource Repairsound;
    public AudioSource Steamdeathsound;
    public AudioSource Wiredeathsound;
    public AudioSource Nextlevelsound;

    // Start is called before the first frame update
    public void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGrounding();
        GravityFlip();
        Movement();

    }

    public void Movement()
    { 
        Vector2 movement = rb2d.velocity;
        movement.x = Input.GetAxis("Horizontal") * speed;
        playerVelocity = rb2d.velocity;

        rb2d.velocity = movement;

    }

    private void UpdateGrounding()
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
            return;
        }
        else if (hitLeft.collider != null)
        {
            grounded = true;
            return;
        }
        else if (hitRight.collider != null)
        {
            grounded = true;
            return;
        }

        grounded = false;
        return;

    }

    public void GravityFlip()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2d.velocity = new Vector2(0, 0);
            Gravitysound.Play();

            if (Physics2D.gravity.y > 0)
            {
                Physics2D.gravity = new Vector2(0, -1 * gravity);  
            }
            else
            {
                Physics2D.gravity = new Vector2(0, gravity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //for out of bounds
        if (collision.gameObject.CompareTag("Dangerous"))
        {
            Outofboundsound.Play();
            Physics2D.gravity = new Vector2(0, -1 * gravity);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.gameObject.CompareTag("NextLevel"))
        {
            Nextlevelsound.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //make sure to check isTrigger for obstacles
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Steam") && !holdingTape)
        {
            Steamdeathsound.Play();

            Physics2D.gravity = new Vector2(0, -1 * gravity);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (other.gameObject.CompareTag("Wires") && !holdingPliers)
        {
            Wiredeathsound.Play();
            Physics2D.gravity = new Vector2(0, -1 * gravity);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void CollectObject()
    {
        Objectsound.Play();
    }
}
