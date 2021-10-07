using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnimy : Enimy, Flameable
{
    public float speed;
    public long accelerationDelayMillis;

    private long accelerationTime;

    protected override void MoveNext(Vector3 nextPoint)
    {
        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds() + accelerationDelayMillis;
        var acceleratedSpeed = speed;
        if (now < accelerationTime) {
            acceleratedSpeed += (float)Math.Pow(2, now % 5000);
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPoint, acceleratedSpeed * Time.deltaTime);
    }

    protected override void OnNextPoint(Transform point) {
        gameObject.transform.rotation = point.transform.rotation;
    }

    protected override Transform GetTrackTransform()
    {
        return GameObject.FindGameObjectWithTag("track").transform;
    }

    protected override void OnCalculateValues() {
        accelerationDelayMillis += UnityEngine.Random.Range(0, 1000);
        accelerationTime = DateTimeOffset.Now.ToUnixTimeMilliseconds() + accelerationDelayMillis;
    }

    public void burn()
    {
        Destroy(gameObject);
    }
}
