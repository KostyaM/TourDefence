using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enimy : MonoBehaviour
{

    private Transform trackTransform;
    private Vector3 nextPoint;

    private Transform[] track;
    private int step = 0;
    private Game gameCallback;

    // Start is called before the first frame update
    protected void Start()
    {   
        trackTransform = GetTrackTransform();
        track = new Transform[trackTransform.childCount];
        for (int i = 0; i < track.Length; i++)
        {
            track[i] = trackTransform.GetChild(i);
        }
        gameCallback = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        nextPoint = track[0].position;
        OnCalculateValues();
    }

    // Update is called once per frame
    void Update()
    {
        var position = gameObject.transform.position;

        if (nextPoint.x == position.x && nextPoint.z == position.z)
        {
            if (step == track.Length)
            {
                gameCallback.onEnimyPass();
                Destroy(gameObject);
                return;
            }

            nextPoint = track[step].position;
            OnNextPoint(track[step]);
            step++;
        }
        MoveNext(nextPoint);
        OnUpdate();
    }
    protected abstract void MoveNext(Vector3 nextPoint);
    protected abstract Transform GetTrackTransform();

    protected virtual void OnNextPoint(Transform point) { }
    protected virtual void OnCalculateValues() { }
    protected virtual void OnUpdate() { }
}
