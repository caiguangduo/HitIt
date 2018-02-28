using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColision : MonoBehaviour {

	void OnTriggerEnter(Collider enterCollider)
    {
        Debug.Log(enterCollider.name+"__enter");
    }

    void OnTriggerStay(Collider stayCollider)
    {
        Debug.Log(stayCollider.name + "__stay");
    }

    void OnTriggerExit(Collider exitCollider)
    {
        Debug.Log(exitCollider.name + "__exit");
    }
}
