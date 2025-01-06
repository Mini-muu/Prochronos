using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BootsAnimatorStart : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string animName;

    private bool firstTimeAnimation;

    void Start()
    {
        firstTimeAnimation = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() == null) return;

        if(firstTimeAnimation)
        {
            anim.SetBool(animName, true);
            firstTimeAnimation = false;
        }
    }
}