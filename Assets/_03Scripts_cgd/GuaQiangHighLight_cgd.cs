using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuaQiangHighLight_cgd : MonoBehaviour {

    Material thisMaterial_cgd;
    float alpha_cgd;
    Color newColor_cgd;
    bool isAdd_cgd = true;
    [SerializeField]
    float speed_cgd = 0.003f;

    void Start()
    {
        thisMaterial_cgd = GetComponent<MeshRenderer>().material;
        alpha_cgd = 0.1f;
        newColor_cgd = thisMaterial_cgd.color;
    }

	void OnEnable()
    {
        alpha_cgd = 0.1f;
    }

    void Update()
    {
        if (isAdd_cgd)
        {
            alpha_cgd += speed_cgd;
            newColor_cgd.a = alpha_cgd;
            thisMaterial_cgd.color = newColor_cgd;
            if (alpha_cgd >= 0.784f)
            {
                isAdd_cgd = false;
            }
        }
        else
        {
            alpha_cgd -= speed_cgd;
            newColor_cgd.a = alpha_cgd;
            thisMaterial_cgd.color = newColor_cgd;
            if (alpha_cgd <= 0.1f)
            {
                isAdd_cgd = true;
            }
        }
    }
}
