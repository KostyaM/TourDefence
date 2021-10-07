using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public SpawnedObject[] enimies;
    public GameObject spawnerEffect;

    private long lastSpawnMillis = 0;
    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Instantiate(spawnerEffect, gameObject.transform.position, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (step == enimies.Length) {
            DestroyImmediate(spawnerEffect, true);
            Destroy(gameObject);
            return;
        }
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var enimy = enimies[step];

        if (lastSpawnMillis + enimy.delayMillis < now) {
            lastSpawnMillis = now;
            Instantiate(enimy.enimyPrefab, transform.position, transform.rotation);
            step++;
        }
    }
}
