using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public float RotationSpeed = 5;
    public GameObject Player;
    public GameObject DeathParticles;

    Vector2 target;

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
        {
            target = Player.transform.position;
            Vector2 position = gameObject.transform.position;
            Vector2 direction = target - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DamageObject")
            if (collision.gameObject.GetComponent<StandardProjectile>().canKillEnemy)
            {
                Instantiate(DeathParticles, transform.position, transform.rotation);
                GameObject.FindWithTag("MainCamera").GetComponent<RestartLevel>().score++; //Hack to push score!!! BAD CODE!!!
                Destroy(transform.parent.gameObject);
            }
    }
}
