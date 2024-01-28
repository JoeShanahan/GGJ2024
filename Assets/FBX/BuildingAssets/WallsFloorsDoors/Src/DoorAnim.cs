using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void OnOpen()
    {
        animator.SetBool("canOpen", true);
    }
}
