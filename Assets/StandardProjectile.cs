using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool DarkBullet;

    public GameObject Player;
    public float MovementSpeed = 10;
    public float GrowthRate = 0.02f;
    private float storedGrowthRate;
    private Vector2 storedMovementVelocity;


    public bool CanMove;
    public Transform ParentSpawner;
    public GameObject ParentEnemy;
    private VoidControl parentScript;
    private CircleCollider2D collider;

    private bool initialized;
    private bool doOnce = true;

    PlayerController controllerScript;

    public GameObject[] LightStateObjects;
    public GameObject[] DarkStateObjects;
    public bool darkState;

    bool addForce = true;

    public bool canKillEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();
        storedGrowthRate = GrowthRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ParentSpawner != null) //check to see if reference to parent spawner has been added yet
        {
            if (!initialized)
            {
                Initialize();
            }
            ChangeDarkState();
            Movement();
            CollisionControl();
        }

        if (ParentEnemy == null) //catches when a projectile is left parentless before firing, and deletes
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        controllerScript = Player.GetComponent<PlayerController>(); //get the player script
        parentScript = ParentEnemy.GetComponent<VoidControl>(); //get the parents script
        parentScript.firing = true; //deactivate parent enemies ability to move

        if (DarkBullet)
            darkState = true;
        if (!DarkBullet)
            darkState = false;
        ChangeDarkState();

        initialized = true;
    }

    void Movement()
    {
        //---Movement speed changes if LightVoid selected---
        if (darkState && !DarkBullet && doOnce)
        {
            rb.velocity = rb.velocity / 15;
            GrowthRate = GrowthRate / 15;
            doOnce = false;
        }
        if (!darkState && !DarkBullet && doOnce)
        {
            rb.velocity = storedMovementVelocity;
            GrowthRate = storedGrowthRate;
            doOnce = false;
        }
        //---Movement speed changes if DarkVoid selected---
        if (!darkState && DarkBullet && doOnce)
        {
            rb.velocity = rb.velocity / 15;
            GrowthRate = GrowthRate / 15;
            doOnce = false;
        }
        if (darkState && DarkBullet && doOnce)
        {
            rb.velocity = storedMovementVelocity;
            GrowthRate = storedGrowthRate;
            doOnce = false;
        }
        //----


        if (CanMove && addForce)
        {
            rb.velocity = transform.right * MovementSpeed;
            addForce = false;
        }
        if (!CanMove)
        {
            transform.position = ParentSpawner.position; //track the spawner until grown
            transform.localScale += new Vector3(GrowthRate, GrowthRate, 0); //grows the orb at position
            if (transform.localScale.x > 0.22f)
            {
                RotateTowardsPlayer();
                CanMove = true; //reactivate parent enemies ability to move
                parentScript.firing = false;
            }
        }
    }

    void RotateTowardsPlayer()
    {
        Vector2 target;
        target = Player.transform.position;
        Vector2 position = gameObject.transform.position;
        Vector2 direction = target - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
    }

    void ChangeDarkState()
    {
        if (darkState != controllerScript.DarkState)
        {
            if (controllerScript.DarkState)
            {
                foreach (GameObject darkObject in DarkStateObjects)
                {
                    darkObject.SetActive(true);
                }
                foreach (GameObject lightObject in LightStateObjects)
                {
                    lightObject.SetActive(false);
                }
            }
            if (!controllerScript.DarkState)
            {
                foreach (GameObject lightObject in LightStateObjects)
                {
                    lightObject.SetActive(true);
                }
                foreach (GameObject darkObject in DarkStateObjects)
                {
                    darkObject.SetActive(false);
                }
            }

            if (DarkBullet && darkState)
                storedMovementVelocity = rb.velocity; //store movement velocity before change

            if (!DarkBullet && !darkState)
                storedMovementVelocity = rb.velocity; //store movement velocity before change

            darkState = controllerScript.DarkState;
            doOnce = true;
        }
    }

    void CollisionControl()
    {
        if (CanMove)
        {
            if (darkState && DarkBullet)
                collider.enabled = true;
            if (!darkState && DarkBullet)
                collider.enabled = false;
            if (!darkState && !DarkBullet)
                collider.enabled = true;
            if (darkState && !DarkBullet)
                collider.enabled = false;
        }
        if (!CanMove)
        {
            collider.enabled = false;
        }
    }
}
