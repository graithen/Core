using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool DarkState = true;
    private bool changeState;

    public float moveSpeed = 5;

    public GameObject[] LightStateObjects;
    public GameObject[] DarkStateObjects;

    private Rigidbody2D rb;
    Vector2 movementVector;

    //--DAMAGE STATE
    public int damage = 0;

    public GameObject[] Damage1;
    public GameObject[] Damage2;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = Vector2.zero;

        changeState = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("ChangeButton"))
        {
            DarkState = !DarkState;
            ChangeState();
        }

        DamageState();

        changeState = true;
    }

    void FixedUpdate()
    {
        Control();
    }

    void Control ()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        rb.AddForce(movementVector * moveSpeed * Time.deltaTime);
    }

    void ChangeState ()
    {
        if (changeState)
        {
            if (DarkState)
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
            if (!DarkState)
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

            changeState = false;
        }
    }

    void DamageState()
    {
        if (damage == 0)
        {
            foreach(GameObject crack in Damage1)
            {
                crack.SetActive(false);
            }
            foreach (GameObject crack in Damage2)
            {
                crack.SetActive(false);
            }
        }
        if (DarkState && damage == 1)
        {
            Damage1[0].SetActive(true);
            Damage1[1].SetActive(false);
        }
        if (!DarkState && damage == 1)
        {
            Damage1[1].SetActive(true);
            Damage1[0].SetActive(false);
        }
        if (DarkState && damage == 2)
        {
            Damage2[0].SetActive(true);
            Damage2[1].SetActive(false);
        }
        if (!DarkState && damage == 2)
        {
            Damage2[1].SetActive(true);
            Damage2[0].SetActive(false);
        }
        
    }
}
