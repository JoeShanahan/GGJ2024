using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnOpen();
        }
    }

    public void OnOpen()
    {
        animator.SetBool("canOpen", true);
    }
}
