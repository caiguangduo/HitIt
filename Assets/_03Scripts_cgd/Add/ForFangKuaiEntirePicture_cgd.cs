using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForFangKuaiEntirePicture_cgd : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "activate")
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
	
}
