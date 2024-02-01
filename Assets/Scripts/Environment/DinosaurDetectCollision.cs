using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurDetectCollision : MonoBehaviour
{
    [SerializeField] private DinosaurYeet dinoYeet;

    private void OnCollisionEnter(Collision collision)
    {
        if (dinoYeet.HasYeeted()) { return; }
        if (collision.rigidbody.velocity.magnitude < 1f) { return; }
        Debug.Log(collision.rigidbody.velocity.magnitude);
        dinoYeet.YeetBones();
    }
}
