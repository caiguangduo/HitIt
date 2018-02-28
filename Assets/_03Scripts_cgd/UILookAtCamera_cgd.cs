using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtCamera_cgd : MonoBehaviour {

    private Transform headCamera_cgd;

    void Start()
    {
        StartCoroutine("FindHeadCamera");//开启一个协程，用于找到头盔的摄像机
    }

    IEnumerator FindHeadCamera()
    {
        while (headCamera_cgd == null)
        {
            headCamera_cgd = GameObject.Find("Camera (eye)").transform;
            yield return new WaitForSeconds(0.02f);
        }
        StopCoroutine("FindHeadCamera");
    }

    void Update()
    {
        if (headCamera_cgd != null)//当找到头盔的摄像机之后，使信息面板始终朝向头盔的摄像机
        {
            Vector3 pos = headCamera_cgd.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }
    }

}
