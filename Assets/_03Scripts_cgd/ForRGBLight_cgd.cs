using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForRGBLight_cgd : MonoBehaviour {

    //该脚本挂在灯上面，根据碰撞到的方块的标签来确定灯的颜色

    public static ForRGBLight_cgd instance_cgd;
    public GameObject[] mrs_cgd = new GameObject[12];
    public Material[] lampMaterials_cgd = new Material[4];

    void Awake()
    {
        instance_cgd = this;
    }

    void Start()
    {
        InitLightColor();
    }

    public void InitLightColor()
    {
        foreach(GameObject obj in mrs_cgd)
        {
            obj.GetComponent<MeshRenderer>().material = lampMaterials_cgd[3];
        }
    }

    public void ChangeLightColor(int whichLamp,int whichColor)
    {
        mrs_cgd[whichLamp].GetComponent<MeshRenderer>().material = lampMaterials_cgd[whichColor];
    }
}
