using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    AsyncOperation async;
    public GameObject StateCircle;

    public GameObject[] LightStateObjects;
    public GameObject[] DarkStateObjects;
    public AudioClip[] ChangeSound;
    public GameObject Audio1;
    public GameObject Audio2;

    public bool DarkState;

    // Start is called before the first frame update
    void Start()
    {
        async = SceneManager.LoadSceneAsync("Main");
        async.allowSceneActivation = false;
    }

    // Update is called once per frameq
    void Update()
    {
        if (Input.GetButtonDown("ChangeButton") || Input.GetKeyDown(KeyCode.Q))
        {
            StateCircle.transform.localRotation *= Quaternion.Euler(0, 0, 180);
            DarkState = !DarkState;

            StateCircle.GetComponent<AudioSource>().clip = ChangeSound[Random.Range(0, 2)];

            ChangeState();
        }
    }

    void ChangeState()
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
                Audio1.GetComponent<AudioSource>().mute = false;
                Audio2.GetComponent<AudioSource>().mute = true;
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
                Audio2.GetComponent<AudioSource>().mute = false;
                Audio1.GetComponent<AudioSource>().mute = true;
            }
    }

    public void NewGame()
    {
        async.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
