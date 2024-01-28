using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurRB : MonoBehaviour
{
    private Component[] rbList;

    private void Start()
    {
        rbList = GetComponentsInChildren<Rigidbody>();
        FreezeRB();
    }

    private void FreezeRB() { foreach(Rigidbody r in rbList) { r.constraints = RigidbodyConstraints.FreezeAll; } }

    public void UnfreezeRB() { foreach(Rigidbody r in rbList) { r.constraints = RigidbodyConstraints.None; } }
}
