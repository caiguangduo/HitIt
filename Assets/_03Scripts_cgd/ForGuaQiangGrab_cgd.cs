using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForGuaQiangGrab_cgd : MonoBehaviour {

    //该脚本用于处理用户用左手柄拿起刮枪的交互问题
    [SerializeField]
    private Transform leftController_cgd;
    [SerializeField]
    private Transform rightController_cgd;
    [SerializeField]
    private GameObject guaQiangHighLight_cgd;
    [SerializeField]
    private GameObject guaQiangIntro_cgd;
    [SerializeField]
    private GameObject gameIntro_cgd;

    public bool isGrab_cgd;

    void Start()
    {
        isGrab_cgd = false;
    }

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shoubing")
        {
            switch (other.name)
            {
                case "SphereLeft":
                    transform.SetParent(leftController_cgd, true);
                    break;
                case "SphereRight":
                    transform.SetParent(rightController_cgd, true);
                    break;
            }
            GrabGuaQiang();

            if (!isGrab_cgd)
            {
                DoWhenGrabGuaQiang();
                isGrab_cgd = true;
            }
        }
    }

    void GrabGuaQiang()
    {
        transform.localScale = Vector3.one;
        transform.localEulerAngles = new Vector3(90, 180, -90);
        transform.localPosition = new Vector3(0.0217f, -0.0329f, 0.2047f);
    }

    void DoWhenGrabGuaQiang()
    {
        gameIntro_cgd.SetActive(false);
        guaQiangHighLight_cgd.SetActive(false);
        guaQiangIntro_cgd.SetActive(true);
        UIInteraction_cgd.instance_cgd.GuaQiangIntroCountDown10secs();
    }
}
