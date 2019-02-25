using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Track();
    }

    void Track()
    {
        if (transform.position.x > -7.5 && transform.position.x < 7.5)
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        if (transform.position.y > -14.5 && transform.position.y < 14.5)
            transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
        if (Player.transform.position.x > transform.position.x && transform.position.x < -7.5 || Player.transform.position.x < transform.position.x && transform.position.x > 7.5)
            transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        if (Player.transform.position.y > transform.position.y && transform.position.y < -14.5 || Player.transform.position.y < transform.position.y && transform.position.y > 14.5)
            transform.position = new Vector3(transform.position.x, Player.transform.position.y, transform.position.z);
    }
}
