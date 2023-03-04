using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRestart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Controller2D controller = collision.gameObject.GetComponent<Controller2D>();
        if(controller != null)
        {
            Physics2D.gravity = new Vector2(0, 10);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
