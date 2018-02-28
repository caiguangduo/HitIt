using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test01 : MonoBehaviour {

    

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetString("第三名"));
        
        if (!PlayerPrefs.HasKey("第三名"))
        {
            Debug.Log("111");
        }
        //PlayerPrefs.SetString("第一名", "90");
        //PlayerPrefs.SetString("第二名", "80");
        //Debug.Log(PlayerPrefs.GetString("第一名"));
        //Debug.Log(PlayerPrefs.GetString("第二名"));
    }
}
