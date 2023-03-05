using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    GameObject pickedItem;
    CharController player;
    // Start is called before the first frame update
    private void Start()
    {

        player = GetComponent<CharController>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject other = col.gameObject;
        Collectible item = other.GetComponent<Collectible>();
        if(item != null)
        {
            other.transform.SetParent(transform);
            other.transform.localPosition = new Vector3(0.5f, 0, 0);
            other.GetComponent<Collider2D>().enabled = false;
            pickedItem = other.gameObject;
            player.CollectObject();

            if (pickedItem.CompareTag("Tape"))
            {
                player.holdingTape = true;
            }
            if(pickedItem.CompareTag("Pliers"))
            {
                player.holdingPliers = true;
            }
                

        }
    }
    private void Update()
    {
        if(pickedItem != null)
        {
            pickedItem.transform.localPosition = new Vector3(0.5f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(pickedItem != null)
            {
                Collider2D itemCollider = pickedItem.GetComponent<Collider2D>();
                //itemCollider.isTrigger = true;
                itemCollider.enabled = true;
                //pickedItem.GetComponent<Rigidbody2D>.AddForce(new Vector2(5, 5));
                pickedItem.transform.SetParent(null);
                pickedItem = null;
                player.holdingTape = false;
                player.holdingPliers = false;


            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Reparable item = other.GetComponent<Reparable>();
        if(item != null)
        {
            if (pickedItem != null && other.CompareTag("Steam") && pickedItem.CompareTag("Tape"))
            {
                item.FixIt();
            }
            if (pickedItem != null && other.CompareTag("Wires") && pickedItem.CompareTag("Pliers"))
            {
                item.FixIt();
            }
        }
        
    }

}
