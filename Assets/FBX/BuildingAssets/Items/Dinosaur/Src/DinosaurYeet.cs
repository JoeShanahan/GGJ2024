using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurYeet : MonoBehaviour
{
    [SerializeField] List<GameObject> bones;
    private bool hasYeeted = false;

    public void YeetBones()
    {
        hasYeeted = true;
        if (bones.Count == 0) { return; }
        foreach (GameObject g in bones) { g.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; }
        foreach (GameObject g in bones) { g.GetComponent<Rigidbody>().AddForce(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f), ForceMode.Impulse); }
    }

    public bool HasYeeted() { return hasYeeted; }
}
