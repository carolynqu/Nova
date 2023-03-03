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

    IEnumerator CollectAnimation()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up * 1.5f;
        float startRotation = 0;
        float endRotation = 1500;
        Color startColor = Color.white;
        Color endColor = Color.white;
        endColor.a = 0;
        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 endScale = new Vector3(1.5f, 1.5f, 1.5f);
        float startTime = Time.time;
        float endTime = Time.time + animationDuration;
        while (Time.time < endTime)
        {
            float t = 1 - ((endTime - Time.time) / animationDuration);
            mySpriteRenderer.color = Color.Lerp(startColor, endColor, t);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(startRotation, endRotation, t), Vector3.up);
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        Destroy(gameObject);
    }
}
