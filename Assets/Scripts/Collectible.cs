using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    private SpriteRenderer mySpriteRenderer;
    public float animationDuration = 1;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        mySpriteRenderer.flipY = Physics2D.gravity.y < 0;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

}
