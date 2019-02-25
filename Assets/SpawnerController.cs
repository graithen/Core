using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] Spawners;
    public GameObject[] Enemies;

    public float TimeBetweenSpawns;

    int damage;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().damage; //hack fix to stop spawners crashing game trying to get ref to player
        if (damage > 2)
            StopAllCoroutines();
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(Enemies[Random.Range(0, 2)], Spawners[Random.Range(0, 7)].transform.position, transform.rotation);
            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
    }
}
