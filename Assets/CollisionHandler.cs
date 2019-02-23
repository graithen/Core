using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject PlayerControllerParent;
    public PlayerController controller;

    private void Start()
    {
        controller = PlayerControllerParent.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DamageObject")
        {
            Destroy(collision.gameObject);
            controller.damage++;
        }
    }
}
