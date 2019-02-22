using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDarkState : MonoBehaviour
{
    public GameObject [] LightStateObjects;
    public GameObject[] DarkStateObjects;

    public GameObject ParentController;
    PlayerController controllerScript;

    private bool darkState;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = ParentController.GetComponent<PlayerController>();
        darkState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (darkState != controllerScript.DarkState)
        {
            if(controllerScript.DarkState)
            {
                foreach(GameObject darkObject in DarkStateObjects)
                {
                    darkObject.SetActive(true);
                }
                foreach(GameObject lightObject in LightStateObjects)
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
        }

    }
}
