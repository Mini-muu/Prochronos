using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationScript : MonoBehaviour
{
    public Animator anim;
    public bool First;

    // Start is called before the first frame update
    void Start()
    {
        First = true;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && First)
        {
            First = false;
            anim.Play("TitleAnimation");
        }
    }
}
