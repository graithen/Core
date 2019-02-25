using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour
{
    public int score;
    public Text[] scoreText;
    public AudioClip[] Music;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = Music[Random.Range(0, 1)];
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        scoreText[0].text = "" + score;
        scoreText[1].text = "" + score;
    }
}
