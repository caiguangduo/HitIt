using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForMove_cgd : MonoBehaviour {

    public static float speed_cgd;
    Vector3 originPosition_cgd;
    public ForShou_cgd shou_cgd;

    List<Transform> tuAnChildList_cgd;
    int childCount_cgd;

    void Awake()
    {
        originPosition_cgd = transform.position;
        childCount_cgd = transform.childCount;
        tuAnChildList_cgd = new List<Transform>(childCount_cgd);
        for(int i = 0; i < childCount_cgd; i++)
        {
            tuAnChildList_cgd.Add(transform.GetChild(i));
        }
    }

    void Start()
    {
        speed_cgd = 0.15f;
        GetComponent<BoxCollider>().enabled = false;
    }

	void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed_cgd, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shoubing")
        {
            GameManager_cgd.instance_cgd.SetNormalOrSmile();
            GameManager_cgd.instance_cgd.AddPlayerScore(true);
            GameObject hitParticle = Instantiate(GameManager_cgd.instance_cgd.hitParticleWhite_cgd, transform.position, Quaternion.identity);
            Destroy(hitParticle, 2.0f);
            init();
        }
    }

    public void init()
    {
        #region 弃用，三个整体图案的速度初始化问题也要放在重新开始游戏的位置，因为在第三个图案被销毁后，三个整体图案的速度也是在不断增加的
        //if (gameObject.name == "TuAn_003")
        //{
        //    speed_cgd = 0.15f;
        //}
        #endregion
        transform.position = originPosition_cgd;
        gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        shou_cgd.isCanUpWards_cgd = false;
        for (int i = 0; i < childCount_cgd; i++)
        {
            tuAnChildList_cgd[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
