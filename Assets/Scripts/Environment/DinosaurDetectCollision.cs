using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurDetectCollision : MonoBehaviour
{
    [SerializeField] private DinosaurYeet dinoYeet;

    private void OnCollisionEnter(Collision collision)
    {
        if (dinoYeet == null) { return; }
        if (dinoYeet.HasYeeted()) { return; }
        if (collision.rigidbody.velocity.magnitude < 1f) { return; }
        if (collision.rigidbody.mass < 5) { return; }
        dinoYeet.YeetBones();
    }
}
