using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Core")
        {
            collision.transform.parent.gameObject.GetComponent<PlayerController>().damage++;
            Vector3 velocity = collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity;
            velocity = new Vector3(-velocity.x, -velocity.y, velocity.z);
            collision.transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
            Debug.Log("Hit");
        }
    }
}
