using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingDoor : Interactable
{
    [SerializeField]
    private Transform _shardParent;

    [SerializeField]
    private Transform _wholeDoor;

    [SerializeField]
    private float _explodeForce;

    [SerializeField]
    private float _explodeRadius;

    [SerializeField]
    private MuseumRoom _room;

    public override bool IsInteractable => _isInteractable;

    private bool _isInteractable = true;

    public override void Interact(Vector3 fromPos)
    {
        _wholeDoor.gameObject.SetActive(false);
        _shardParent.gameObject.SetActive(true);

        foreach (Transform t in _shardParent)
        {
            t.GetComponent<Rigidbody>().AddExplosionForce(_explodeForce, fromPos, _explodeRadius);
        }

        Destroy(this);
        Destroy(GetComponent<Collider>());
        _isInteractable = false;

        if (_room != null)
        {
            _room.gameObject.SetActive(true);
        }
    }
}
