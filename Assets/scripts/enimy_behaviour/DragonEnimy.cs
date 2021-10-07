using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnimy : Enimy
{
    public float speed;
    public float fieldOfImpact;

    private Transform flame;

    private Quaternion targetRotation = new Quaternion(0, 0, 0, 0);

    protected override Transform GetTrackTransform()
    {
        return GameObject.FindGameObjectWithTag("air_track").transform;
    }

    protected override void OnCalculateValues() {
        flame = transform.GetChild(1);
    }

    protected override void MoveNext(Vector3 nextPoint)
    {
        var time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var altitudeOffset = Math.Sin(time);
        var altitudedPoint = nextPoint;
        altitudedPoint.y = (float)(altitudedPoint.y + altitudeOffset);

        transform.position = Vector3.MoveTowards(transform.position, altitudedPoint, speed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 10 * Time.deltaTime);
    }

    protected override void OnNextPoint(Transform point)
    {
        targetRotation = point.transform.rotation;
    }

    protected override void OnUpdate() {

        Collider[] objects = Physics.OverlapSphere(flame.position, fieldOfImpact);
        foreach (Collider obj in objects)
        {
            var flameable = obj.GetComponent<Flameable>();
            if (flameable != null)
                flameable.burn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.GetChild(1).transform.position, fieldOfImpact);
    }

}
