using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical2") > 0.5 || Input.GetAxis("Horizontal2") > 0.5 || Input.GetAxis("Vertical2") < -0.5 || Input.GetAxis("Horizontal2") < -0.5)
        {
            float horizontal = Input.GetAxis("Horizontal2");
            float vertical = Input.GetAxis("Vertical2");

            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "DamageObject")
        {
            collision.gameObject.GetComponent<StandardProjectile>().canKillEnemy = true;
            Debug.Log("Projectile armed");
        }
    }
}
