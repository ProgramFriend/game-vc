using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public bool respawn;
    public float spawnDelay;
    private float curTime;
    private bool spawning;

    void Start()
    {
        Spawn();
        curTime = spawnDelay;
    }

    void Update()
    {
        if (spawning)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0)
                Spawn();
        }

    }

    public void Respawn()
    {
        spawning = true;
        curTime = spawnDelay;
    }

    void Spawn()
    {
        IEnemy instance = Instantiate(enemy, transform.position, Quaternion.identity, this.transform).GetComponent<IEnemy>();

        instance.spawner = this;
        spawning = false;
    }
}
