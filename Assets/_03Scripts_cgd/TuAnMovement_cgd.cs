using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuAnMovement_cgd : MonoBehaviour {

    //该脚本负责图案的整体移动,并设置该图案的哪一个方块被选中
    public static float speed_cgd;
    int childCount_cgd;
    List<Transform> tuAnChildsList_cgd;

    void Start()
    {
        speed_cgd = 0.15f;

        childCount_cgd = transform.childCount;
        tuAnChildsList_cgd = new List<Transform>(childCount_cgd);
        for (int i = 0; i < childCount_cgd; i++)
        {
            tuAnChildsList_cgd.Add(transform.GetChild(i));
        }
        int choosedNumber = Random.Range(1, 3);
        for(int i = 0; i < childCount_cgd; i++)
        {
            int j = tuAnChildsList_cgd[i].GetComponent<FangKuaiColumnNumber_cgd>().choosedNumber_cgd;
            if (j == choosedNumber)
            {
                tuAnChildsList_cgd[i].GetComponent<ForFangKuai_cgd>().isChoosed_cgd = true;
            }
            else
            {
                tuAnChildsList_cgd[i].GetComponent<ForFangKuai_cgd>().isChoosed_cgd = false;
            }
        }
    }

	void Update()//负责移动
    {
        transform.Translate(Vector3.right*Time.deltaTime*speed_cgd,Space.Self);
        #region 弃用，因为90秒倒计时结束不要立即销毁场景中现存的图案，还是要让图案依次运动到最后再销毁
        //if (isNeedToDestroy_cgd)//当倒计时90秒结束时销毁该图案
        //{
        //    Destroy(this.gameObject);
        //}
        #endregion
    }

    void OnTriggerEnter(Collider other)//负责在最后销毁该图案
    {
        if (other.tag == "destroy")
        {
            Destroy(this.gameObject);
        }
    }

    #region 弃用，随机选择图案中的方块的方法弃用，新方案设计把被选择的方块做死
    //void ChooseFangKuai()
    //{
    //    for (int i = 0; i < choosedNumber_cgd; i++)
    //    {
    //        bool isNeed = true;
    //        while (isNeed)
    //        {
    //            int tempIndex_cgd = Random.Range(0, childCount_cgd);
    //            if (!choosedChildsList_cgd.Contains(tuAnChildsList_cgd[tempIndex_cgd]))
    //            {
    //                choosedChildsList_cgd.Add(tuAnChildsList_cgd[tempIndex_cgd]);
    //                choosedChildsList_cgd[i].GetComponent<ForFangKuai_cgd>().isChoosed_cgd = true;
    //                isNeed = false;
    //            }
    //        }
    //    }
    //}
    #endregion
}
