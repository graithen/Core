using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidControl : MonoBehaviour
{
    public bool LightVoid;

    public GameObject[] LightStateObjects;
    public GameObject[] DarkStateObjects;
    public GameObject Spawner;

    public GameObject Projectile;
    private GameObject projectile;

    public GameObject ParentController;
    PlayerController controllerScript;

    private bool darkState;

    Vector2 target;

    public float MovementSpeed = 0.05f;
    private float StoredMovementSpeed;

    private bool doOnce;
    public bool firing;

    // Start is called before the first frame update
    void Start()
    {
        StoredMovementSpeed = MovementSpeed; //store movement speed to recover later

        controllerScript = ParentController.GetComponent<PlayerController>();
        darkState = controllerScript.DarkState;

        StartCoroutine(Firing());
    }

    // Update is called once per frame
    void Update()
    {
        DarkChange();
        Movement();
    }

    private void FixedUpdate()
    {
        Control();
    }

    IEnumerator Firing ()
    {
        while (true)
        {
            Debug.Log("Starting to fire");
            yield return new WaitForSeconds(5);
            projectile = Instantiate(Projectile, Spawner.transform.position, transform.rotation);
            StandardProjectile projectileScript = projectile.GetComponent<StandardProjectile>();
            projectileScript.ParentSpawner = Spawner.transform;
            projectileScript.ParentEnemy = this.gameObject;
            projectileScript.Player = ParentController;
        }
    }

    void Movement()
    {
        target = ParentController.transform.localPosition;
        float distance = Vector2.Distance(transform.position, target);
        float step = MovementSpeed;

        //---Movement speed changes if LightVoid selected---
        if (darkState && LightVoid && doOnce)
        {
            MovementSpeed = MovementSpeed / 15;
            doOnce = false;
        }
        if (!darkState && LightVoid && doOnce)
        {
            MovementSpeed = StoredMovementSpeed;
            doOnce = false;
        }
        //---Movement speed changes if DarkVoid selected---
        if (!darkState && !LightVoid && doOnce)
        {
            MovementSpeed = MovementSpeed / 15;
            doOnce = false;
        }
        if (darkState && !LightVoid && doOnce)
        {
            MovementSpeed = StoredMovementSpeed;
            doOnce = false;
        }
        //----

        if (distance >= 6)
            step = MovementSpeed;
        if (distance < 4)
            step = -MovementSpeed;
        else if (distance > 4 && distance < 6)
            step = 0;

        if(!firing || step < 0)
            transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    void DarkChange()
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
            darkState = controllerScript.DarkState;
            doOnce = true;
        }
    }

    void Control ()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, 100) == ParentController)
            Debug.Log("Can see player");
    }
}
