using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal_control : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetTrigger(){
        anim.ResetTrigger("GlowUp");
    }
}
