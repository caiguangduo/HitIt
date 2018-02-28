using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForWei_cgd : MonoBehaviour {

    public GameObject nextTuAn_cgd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "NextOne_all")
        {
            if (nextTuAn_cgd != null)
            {
                nextTuAn_cgd.SetActive(true);
            }
            else
            {
                InstantiateTuAn_cgd.instance_cgd.Call_StartInstantiateTuAn();//如果第三个图案离开刮枪，则开始随机实例化图案
            }
        }
    }

}
