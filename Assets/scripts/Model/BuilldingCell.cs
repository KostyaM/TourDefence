using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilldingCell: MonoBehaviour, Flameable
{
    public Material activeMaterial;
    public Material disabledMaterial;
    public Material defaultMaterial;
    public Material burnedMaterial;

    public GameObject firePrefab;

    public bool canBuild = true;

    private DefenceObject defenceObject;
    private bool isBurnt = false;


    private void OnMouseOver()
    {
        if (defenceObject == null && canBuild)
        {
            gameObject.GetComponent<Renderer>().material = activeMaterial;
        }
        else {
            gameObject.GetComponent<Renderer>().material = disabledMaterial;
        }
    }

    private void OnMouseExit()
    {
        if(!isBurnt)
            gameObject.GetComponent<Renderer>().material = defaultMaterial;
        else
            gameObject.GetComponent<Renderer>().material = burnedMaterial;
    }

    private void OnMouseDown()
    {
        if (defenceObject == null)
        {
            defenceObject = new DefenceObject(100);
            isBurnt = false;
        }
    }
    public void burn()
    {
        if (isBurnt)
            return;
        gameObject.GetComponent<Renderer>().material = burnedMaterial;
        Destroy(Instantiate(firePrefab, transform.position, transform.rotation), 3f);
        isBurnt = true;
    }
}
