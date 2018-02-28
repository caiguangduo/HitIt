using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class UIInteraction_cgd : MonoBehaviour {

    #region 弃用
    //public GameObject[] uiGameObject_cgd;
    #endregion

    public static UIInteraction_cgd instance_cgd;

    public int _3secs_cgd = 3;
    public int _90secs_cgd = 90;
    public GameObject guaQiang_cgd;
    Vector3 originPosition_cgd;
    Vector3 originEuler_cgd;
    Vector3 originScale_cgd;
    public GameObject guaQiangIntro_cgd;
    public GameObject gameIntro_cgd;
    public GameObject count_3secs_cgd;
    public GameObject count_90secs_cgd;
    public GameObject tvScore_cgd;
    public GameObject tvRank_cgd;
    public GameObject tvLaughOrNormal_cgd;
    public GameObject tvButton_cgd;
    public Sprite[] zeroToTenTime_cgd = new Sprite[10];
    public Sprite[] zeroToTenScore_cgd = new Sprite[10];
    public Sprite[] countDown_3secs_cgd = new Sprite[4];
    public Image imageCountDown3secs_cgd;
    public Image imageCountDown90secsFirst_cgd;
    public Image imageCountDown90secsSecond_cgd;

    [SerializeField]
    private Vector3 positionNearGuaQiang_cgd;
    [SerializeField]
    private Vector3 positionPlayGame_cgd;
    [SerializeField]
    private Transform cameraRig_cgd;

    public GameObject guaQiangHighLight_cgd;

    public Image guaQiangIntro10secs_cgd;
    int _10secs_cgd = 10;//这个数据也是需要初始化的

    public GameObject tuAn_001;

    public GameObject leftControllerModel_cgd;
    public GameObject rightControllerModel_cgd;
    public GameObject leftSphere_cgd;
    public GameObject rightSphere_cgd;
    public GameObject leftHammer_cgd;
    public GameObject rightHammer_cgd;

    GameObject leftMoveCurve_cgd;
    GameObject rightChooseLine_cgd;
    //public ArcTeleporter leftMoveScript_cgd;
    //public VRTK_SimplePointer rightSimplePointer_cgd; 
    public GameObject floor_cgd;

    void Awake()
    {
        instance_cgd = this;
        originPosition_cgd = guaQiang_cgd.transform.position;
        originEuler_cgd = guaQiang_cgd.transform.eulerAngles;
        originScale_cgd = guaQiang_cgd.transform.localScale;
        cameraRig_cgd.position = positionNearGuaQiang_cgd;
        cameraRig_cgd.eulerAngles = new Vector3(0.0f,270.0f,0.0f);
    }

    void Start()
    {
        StartCoroutine(FindLeftCurve());
        StartCoroutine(FindRightChooseLine());
        floor_cgd.SetActive(false);
    }

    IEnumerator FindLeftCurve()
    {
        while (leftMoveCurve_cgd==null)
        {
            leftMoveCurve_cgd = GameObject.Find("ArcTeleporter");
            yield return new WaitForSeconds(0.02f);
        }
        //Debug.Log(leftMoveCurve_cgd.name);
        leftMoveCurve_cgd.SetActive(false);
    }

    IEnumerator FindRightChooseLine()
    {
        while (rightChooseLine_cgd == null)
        {
            rightChooseLine_cgd = GameObject.Find("[Controller (right)]WorldPointer_SimplePointer_Holder");
            yield return new WaitForSeconds(0.02f);
        }
        rightChooseLine_cgd.SetActive(false);
        //Debug.Log(rightChooseLine_cgd.name);
    }

    public void GuaQiangIntroCountDown10secs()
    {
        //Debug.Log("001cgd");
        InvokeRepeating("CountDown10secs", 0.2f, 1.0f);
    }

    void CountDown10secs()
    {
        //Debug.Log("002cgd");
        if (_10secs_cgd > 0)
        {
            //Debug.Log("003cgd");   
            guaQiangIntro10secs_cgd.sprite = zeroToTenScore_cgd[_10secs_cgd - 1];
        }
        else
        {
            //Debug.Log("004cgd");
            Button_ZhiDaoLe();
            CancelInvoke("CountDown10secs");
        }
        _10secs_cgd -= 1;
    }

    public void Button_ZhiDaoLe()
    {
        RecoverGuaQiang();
        guaQiang_cgd.GetComponent<BoxCollider>().enabled = false;
        guaQiangIntro_cgd.SetActive(false);
        //gameIntro_cgd.SetActive(true);
        //cameraRig_cgd.position = positionPlayGame_cgd;
        Button_GameStart();
    }
	public void Button_GameStart()
    {
        #region 弃用
        //GameManager_cgd.instance_cgd.PlayLongMusic("duringGame");
        //for(int i = 0; i < 3; i++)
        //{
        //    uiGameObject_cgd[i].SetActive(false);
        //}
        //-------------------------需要添加的代码-----------------
        //更改玩家的位置至最佳游戏位置
        //解除左手柄的移动功能
        //解除右手柄的指针功能
        //-------------------------需要添加的代码-----------------
        #endregion

        leftControllerModel_cgd.SetActive(false);
        rightControllerModel_cgd.SetActive(false);
        leftSphere_cgd.SetActive(false);
        rightSphere_cgd.SetActive(false);
        leftHammer_cgd.SetActive(true);
        rightHammer_cgd.SetActive(true);


        //leftMoveCurve_cgd.SetActive(false);
        //rightChooseLine_cgd.SetActive(false);
        //floor_cgd.SetActive(false);
        //leftMoveScript_cgd.enabled = false;
        //rightSimplePointer_cgd.enabled = false;

        GameManager_cgd.instance_cgd.is_90secsEnd_cgd = false;
        TuAnMovement_cgd.speed_cgd = 0.15f;
        InstantiateTuAn_cgd.instance_cgd.waitTime_cgd = 4.6f;
        ForMove_cgd.speed_cgd = 0.15f;
        _3secs_cgd = 3;
        _90secs_cgd = 90;
        cameraRig_cgd.position = positionPlayGame_cgd;
        cameraRig_cgd.eulerAngles = new Vector3(0, 180, 0);
        //guaQiangHighLight_cgd.SetActive(false);
        //guaQiang_cgd.GetComponent<BoxCollider>().enabled = false;
        //gameIntro_cgd.SetActive(false);
        tvScore_cgd.SetActive(true);
        tvLaughOrNormal_cgd.SetActive(true);
        GameManager_cgd.instance_cgd.InitPlayerScore();
        InvokeRepeating("CountDownTime", 0.2f, 1.0f);
    }
    public void Button_GameReStart()
    {
        #region 弃用
        //------------重新开始要做的事---------
        //更改玩家的位置至靠近刮枪
        //开启左手柄的移动功能
        //开启右手柄的指针功能
        #endregion
        leftMoveCurve_cgd.SetActive(false);
        rightChooseLine_cgd.SetActive(false);
        floor_cgd.SetActive(false);

        _10secs_cgd = 10;
        guaQiang_cgd.GetComponent<ForGuaQiangGrab_cgd>().isGrab_cgd = false;

        guaQiangHighLight_cgd.SetActive(true);
        guaQiang_cgd.GetComponent<BoxCollider>().enabled = true;
        cameraRig_cgd.position = positionNearGuaQiang_cgd;
        tvButton_cgd.SetActive(false);
        tvScore_cgd.SetActive(false);
        tvRank_cgd.SetActive(false);
        gameIntro_cgd.SetActive(true);
    }
    public void Button_GameQuit()
    {
        Application.Quit();
    }
    void CountDownTime()
    {
        if (_3secs_cgd >= 0)
        {
            DoDuringCountDown3Secs();
        }
        else if(_90secs_cgd>=0)
        {
            DoDuringCountDown90Secs();
        }else if (_90secs_cgd<0)
        {
            DoWhenCountDown90End();
        }
    }
    void DoDuringCountDown3Secs()
    {
        #region 弃用
        //if (uiGameObject_cgd[3].activeInHierarchy == false)
        //{
        //    uiGameObject_cgd[3].SetActive(true);
        //}
        //uiGameObject_cgd[3].GetComponent<Text>().text = _3secs_cgd.ToString();
        #endregion
        if (count_3secs_cgd.activeInHierarchy==false)
        {
            count_3secs_cgd.SetActive(true);
        }
        Change3secsImage();
        _3secs_cgd--;
    }
    void DoDuringCountDown90Secs()
    {
        #region 弃用
        //if (uiGameObject_cgd[5].activeInHierarchy == false)
        //{
        //    Debug.Log("InstantiateTuAn_cgd.instance_cgd.Call_StartInstantiateTuAn();调用了几次");
        //    InstantiateTuAn_cgd.instance_cgd.Call_StartInstantiateTuAn();
        //    uiGameObject_cgd[3].SetActive(false);
        //    for (int i = 4; i < 9; i++)
        //    {
        //        uiGameObject_cgd[i].SetActive(true);
        //    }
        //}
        //uiGameObject_cgd[5].GetComponent<Text>().text = _90secs_cgd.ToString();
        #endregion
        if (count_90secs_cgd.activeInHierarchy == false)
        {
            count_3secs_cgd.SetActive(false);
            count_90secs_cgd.SetActive(true);
            //InstantiateTuAn_cgd.instance_cgd.Call_StartInstantiateTuAn();
            //这里应该先激活第一个完整的图案，然后接着出第二个完整图案、第三个完整图案，等到第三个完整图案走出刮枪，开始实例化随机图案
            tuAn_001.SetActive(true);
        }
        Change90secsImage();
        //增加游戏难度的代码
        #region 暂时弃用
        //if (_90secs_cgd == 45)
        //{
        //    Debug.Log("_90secs_cgd == 45");
        //    TuAnMovement_cgd.speed_cgd = 0.13f;
        //    InstantiateTuAn_cgd.instance_cgd.waitTime_cgd = 6.6f;
        //}
        #endregion
        TuAnMovement_cgd.speed_cgd += 0.005f;
        ForMove_cgd.speed_cgd += 0.005f;
        InstantiateTuAn_cgd.instance_cgd.waitTime_cgd -= 0.005f;
        //增加游戏难度的代码
        _90secs_cgd--;
        if (_90secs_cgd == 9)
        {
            InstantiateTuAn_cgd.instance_cgd.Call_StopInstantiateTuAn();//停止实例化图案
        }
    }
    void DoWhenCountDown90End()
    {
        #region 弃用
        //for (int i = 4; i < 9; i++)
        //{
        //    uiGameObject_cgd[i].SetActive(false);
        //}
        //uiGameObject_cgd[9].SetActive(true);//显示游戏结束提示文字
        //uiGameObject_cgd[10].SetActive(true);//显示玩家的最终得分
        #endregion
        //90秒记时结束，瞬间增大场景中所有现存图案的前进速度，让场景中所有现存图案快速消失
        TuAnMovement_cgd.speed_cgd += 0.3f;

        GameManager_cgd.instance_cgd.indexAudioClip = 0;

        count_90secs_cgd.SetActive(false);
        tvLaughOrNormal_cgd.SetActive(false);
        GameManager_cgd.instance_cgd.SetRank();
        tvRank_cgd.SetActive(true);
        tvButton_cgd.SetActive(true);
        //InstantiateTuAn_cgd.instance_cgd.Call_StopInstantiateTuAn();//停止实例化图案
        GameManager_cgd.instance_cgd.is_90secsEnd_cgd = true;
        ForRGBLight_cgd.instance_cgd.InitLightColor();

        leftHammer_cgd.SetActive(false);
        rightHammer_cgd.SetActive(false);
        leftSphere_cgd.SetActive(true);
        rightSphere_cgd.SetActive(true);

        leftControllerModel_cgd.SetActive(true);
        rightControllerModel_cgd.SetActive(true);

        leftMoveCurve_cgd.SetActive(true);
        rightChooseLine_cgd.SetActive(true);
        floor_cgd.SetActive(true);
        //leftMoveScript_cgd.enabled = true;
        //rightSimplePointer_cgd.enabled = true;

        CancelInvoke("CountDownTime");
    }
    void RecoverGuaQiang()//该方法用于将手柄抓到的刮枪还原回去
    {
        guaQiang_cgd.transform.SetParent(null, false);
        guaQiang_cgd.transform.position = originPosition_cgd;
        guaQiang_cgd.transform.eulerAngles = originEuler_cgd;
        guaQiang_cgd.transform.localScale = originScale_cgd;
    }
    void Change3secsImage()
    {
        imageCountDown3secs_cgd.sprite = countDown_3secs_cgd[_3secs_cgd];
    }
    void Change90secsImage()
    {
        int i_01 = (_90secs_cgd - _90secs_cgd % 10) / 10;
        int i_02 = _90secs_cgd%10;
        imageCountDown90secsFirst_cgd.sprite = zeroToTenTime_cgd[i_01];
        imageCountDown90secsSecond_cgd.sprite = zeroToTenTime_cgd[i_02];
    }
}
