using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TestUI_cgd : MonoBehaviour {

	void Start()
    {
        transform.DOScale(1.0f, 3.0f);
        transform.DOMoveX(1680, 3.0f, true);
        Debug.Log("001");
    }

}
