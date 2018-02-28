using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;

public class InitCanvas_cgd : MonoBehaviour {

    bool isDestroy_cgd;

	void Start()
    {
        isDestroy_cgd = false;
        StartCoroutine(DestroyComponent());
    }

    IEnumerator DestroyComponent()
    {
        while (!isDestroy_cgd)
        {
            if (GetComponent<VRTK_UIGraphicRaycaster>() && GetComponent<BoxCollider>() && GetComponent<Image>())
            {
                Destroy(this.GetComponent<VRTK_UIGraphicRaycaster>());
                Destroy(this.GetComponent<BoxCollider>());
                Destroy(this.GetComponent<Image>());
                Destroy(this.GetComponent<InitCanvas_cgd>());
                isDestroy_cgd = true;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}
