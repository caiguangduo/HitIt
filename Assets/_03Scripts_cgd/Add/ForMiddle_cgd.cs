using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMiddle_cgd : MonoBehaviour {

    public ForMove_cgd thisParentForMove_cgd;

    void OnTriggerEnter(Collider other)
    {
        if(other.name== "Destroy_all")
        {
            thisParentForMove_cgd.init();
        }
    }

}
