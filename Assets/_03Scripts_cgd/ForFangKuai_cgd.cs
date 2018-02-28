using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForFangKuai_cgd : MonoBehaviour
{

    MeshRenderer mr_cgd;
    [HideInInspector]
    public bool isChoosed_cgd;//标示该方快是否被选中
    bool isCanHit_cgd;
    Color startColor_cgd;
    Color endColor_cgd;
    Color originColor_cgd;
    float duration_cgd;
    float topY_cgd = 0.13f;//指定方块上升的最大高度
    bool isCanUpWards_cgd = false;
    [HideInInspector]
    public float upWardsSpeed_cgd = 0.5f;//指定方块上升的速度

    Color highLightColor_cgd;
    float alpha_cgd;
    bool isAdd_cgd;

    #region AddByZhiXiang
    [SerializeField]
    private Vector3 startColorValue=new Vector3(0,0,0);
    private Vector3 destinationColorValue;
    private float totalTime=2;

    float currentTime = 0.0f;
    bool isFinishColor = true;
    Color currentColor;
    Color destinationColor;
    Vector3 currentColorValue;
    #endregion

    void Start()
    {
        isCanHit_cgd = false;
        mr_cgd = GetComponent<MeshRenderer>();
        originColor_cgd = mr_cgd.material.color;
        mr_cgd.enabled = false;//新实例化出来的图案，需要将其所有的方块的MeshRenderer组件false掉，在每一排方块碰到显示触发物体后再逐排显示出来

        #region 预热方法1、方法2需要的变量
        duration_cgd = 0.3f;
        startColor_cgd = Color.black;
        endColor_cgd = Color.yellow;

        highLightColor_cgd = Color.yellow;
        alpha_cgd = 0.1f;
        highLightColor_cgd.a = alpha_cgd;
        isAdd_cgd = true;
        #endregion

        #region AddByZhiXiang
        destinationColor = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        destinationColorValue = new Vector3(destinationColor.r, destinationColor.g, destinationColor.b);
        #endregion
    }

    void Update()
    {
        if (isCanUpWards_cgd)
        {
            UpWardsFangKuai();
        }

        #region AddByZhiXiang
        if (!isFinishColor)
        {
            if (currentTime < totalTime)
            {
                currentColorValue = Vector3.Lerp(startColorValue, destinationColorValue, currentTime / totalTime);
                currentColor = new Color(currentColorValue.x, currentColorValue.y, currentColorValue.z);
                GetComponent<Renderer>().material.SetColor("_EmissionColor", currentColor);
                currentTime += Time.deltaTime;
            }
            else
            {
                isFinishColor = true;
            }
        }
        #endregion
    }

    void OnTriggerEnter(Collider enterCollider)
    {
        switch (enterCollider.tag)
        {
            #region 弃用
            //case "rgblight"://这时候应该让灯亮起来，具体灯是什么颜色取决于当前方块的标签
            //    int index = GetComponent<FangKuaiColumnNumber_cgd>().columnNumber_cgd - 1;
            //    ChangeLampsColor(index, thisGameObjectTag_cgd);
            //    break;
            #endregion
            case "activate":
                mr_cgd.enabled = true;
                break;
            case "warmup":
                if (isChoosed_cgd)
                {
                    //StartCoroutine("WarmUp02");
                    isFinishColor = false; //开始改变颜色
                }
                break;
            case "upwards":
                if (isChoosed_cgd)
                {
                    isFinishColor = true;
                    //StopCoroutine("WarmUp02");
                    //mr_cgd.material.color = originColor_cgd;
                    GetComponent<Renderer>().material.SetColor("_EmissionColor", destinationColor);

                    isCanUpWards_cgd = true;
                    isCanHit_cgd = true;//确保只有在被选中的方块开始上升的时候才能被手柄敲击
                }
                break;
            case "destroy":
                #region 弃用，要在图案物体上处理图案的最终销毁
                //if (gameObject.name == "LastOne")
                //{
                //Debug.Log("4_cgd");
                //Destroy(transform.parent, 0.5f);
                //}
                #endregion
                DestroyLeftFangKuai();
                break;
            case "shoubing":
                //如果方块和手柄碰撞在一起，如果该方块是被选中的方块，如果方块此时在可击打范围内，则销毁该方块，并给玩家加分
                if (isChoosed_cgd && isCanHit_cgd)
                {
                    HitByController();
                }
                break;
        }
    }
    #region 弃用
    //更改指示灯颜色的方法
    //void ChangeLampsColor(int index,string fangKuaiTag)
    //{
    //    switch (fangKuaiTag)
    //    {
    //        case "red":
    //            ForRGBLight_cgd.instance_cgd.mrs_cgd[index].GetComponent<MeshRenderer>().material = ForRGBLight_cgd.instance_cgd.lampMaterials_cgd[0];
    //            break;
    //        case "green":
    //            ForRGBLight_cgd.instance_cgd.mrs_cgd[index].GetComponent<MeshRenderer>().material = ForRGBLight_cgd.instance_cgd.lampMaterials_cgd[1];
    //            break;
    //        case "blue":
    //            ForRGBLight_cgd.instance_cgd.mrs_cgd[index].GetComponent<MeshRenderer>().material = ForRGBLight_cgd.instance_cgd.lampMaterials_cgd[2];
    //            break;
    //    }
    //}
    #endregion
    //预热的方法
    IEnumerator WarmUp01()
    {
        while (true)
        {
            duration_cgd = Mathf.PingPong(Time.time, duration_cgd) / duration_cgd;
            mr_cgd.material.color = Color.Lerp(startColor_cgd, endColor_cgd, duration_cgd);
            yield return new WaitForSeconds(0.3f);
        }
    }

    //第二种预热方法
    IEnumerator WarmUp02()
    {
        while (true)
        {
            if (isAdd_cgd)
            {
                alpha_cgd += 0.003f;
                highLightColor_cgd.a = alpha_cgd;
                mr_cgd.material.color = highLightColor_cgd;
                if (alpha_cgd >= 0.8f)
                {
                    isAdd_cgd = false;
                }
            }
            else
            {
                alpha_cgd -= 0.003f;
                highLightColor_cgd.a = alpha_cgd;
                mr_cgd.material.color = highLightColor_cgd;
                if (alpha_cgd <= 0.1f)
                {
                    isAdd_cgd = true;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    //方块上升的方法，在Update方法中调用
    void UpWardsFangKuai()
    {
        transform.Translate(Vector3.up * upWardsSpeed_cgd * Time.deltaTime);
        if (transform.localPosition.y >= topY_cgd)
        {
            isCanUpWards_cgd = false;
        }
    }
    //方块没有被击中，最后被销毁的方法
    void DestroyLeftFangKuai()
    {
        //GameObject destroyParticle = Instantiate(GameManager_cgd.instance_cgd.destroyLeftParticle_cgd, transform.position, Quaternion.identity);
        //Destroy(destroyParticle, 0.15f);
        //GameManager_cgd.instance_cgd.PlayShortMusic("left");
        Destroy(this.gameObject,0.1f);
    }
    //方块被手柄击中
    void HitByController()
    {
        GameObject hitParticleTemp;
        switch (gameObject.tag)
        {
            case "red":
                hitParticleTemp = Instantiate(GameManager_cgd.instance_cgd.hitParticleRed_cgd, transform.position, Quaternion.identity);
                //GameManager_cgd.instance_cgd.PlayShortMusic("red");
                //hitParticleTemp.transform.SetParent(this.transform);
                Destroy(hitParticleTemp, 2.0f);
                break;
            case "green":
                hitParticleTemp = Instantiate(GameManager_cgd.instance_cgd.hitParticleGreen_cgd, transform.position, Quaternion.identity);
                //GameManager_cgd.instance_cgd.PlayShortMusic("green");
                //hitParticleTemp.transform.SetParent(this.transform);
                Destroy(hitParticleTemp, 2.0f);
                break;
            case "blue":
                hitParticleTemp = Instantiate(GameManager_cgd.instance_cgd.hitParticleBlue_cgd, transform.position, Quaternion.identity);
                //GameManager_cgd.instance_cgd.PlayShortMusic("blue");
                //hitParticleTemp.transform.SetParent(this.transform);
                Destroy(hitParticleTemp, 2.0f);
                break;
        }

        GameManager_cgd.instance_cgd.PlayShortMusicSceond();

        if (GameManager_cgd.instance_cgd.is_90secsEnd_cgd == false)
        {
            GameManager_cgd.instance_cgd.SetNormalOrSmile();
            GameManager_cgd.instance_cgd.AddPlayerScore(false);
        }

        Destroy(this.gameObject, 0.05f);
    }
}
