using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTuAn_cgd : MonoBehaviour {

    //该脚本用于每隔一定的时间随机实例化图案
    public static InstantiateTuAn_cgd instance_cgd;

    public GameObject[] tuAn_cgd = new GameObject[3];
    int index_cgd;
    [HideInInspector]
    public float waitTime_cgd;

    void Awake()
    {
        instance_cgd = this;
        waitTime_cgd = 4.6f;
    }

    public void Call_StartInstantiateTuAn()//外部方法通过调用该方法开始实例化图案
    {
        StartCoroutine("InstantiateTuAn");
    }

    public void Call_StopInstantiateTuAn()
    {
        StopCoroutine("InstantiateTuAn");
    }

    IEnumerator InstantiateTuAn()
    {
        while (true)
        {
            index_cgd = Random.Range(0, 3);
            Instantiate(tuAn_cgd[index_cgd], transform.position, transform.rotation);
            //Debug.Log("Instantiate执行了");
            yield return new WaitForSeconds(waitTime_cgd);
        }
    }
}
