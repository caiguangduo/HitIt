using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForShou_cgd : MonoBehaviour {

    Transform thisParent_cgd;

    public bool isCanUpWards_cgd;//每次图案走完一个过程需要对其初始化
    float topY_cgd=0.74f;
    float upwardSpeed_cgd = 0.08f;

    private void Start()
    {
        thisParent_cgd = transform.parent;
        isCanUpWards_cgd = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "UpWard_all")
        {
            thisParent_cgd.GetComponent<BoxCollider>().enabled = true;
            isCanUpWards_cgd = true;
        }
    }

    private void Update()
    {
        if (isCanUpWards_cgd)
        {
            //Debug.Log("00000cgd");
            thisParent_cgd.Translate(Vector3.up * upwardSpeed_cgd * Time.deltaTime);
            if (thisParent_cgd.position.y >= topY_cgd)
            {
                isCanUpWards_cgd = false;//如果还没有上升到最高点就已经被用户敲击到，则需要注意该参数的初始化问题
            }
        }
    }
}
