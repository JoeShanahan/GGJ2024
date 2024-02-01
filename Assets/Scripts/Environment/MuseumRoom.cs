using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumRoom : MonoBehaviour
{
    [SerializeField]
    private bool _startsVisible;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(_startsVisible);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
