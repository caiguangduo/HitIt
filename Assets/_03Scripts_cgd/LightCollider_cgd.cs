using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider_cgd : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("lightcolliderenter_cgd01   "+other.tag);

        switch (other.tag)
        {
            case "red":
                //Debug.Log("lightcolliderenter_cgd02");
                switch (gameObject.name)
                {
                    case "1":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(0, 0);
                        break;
                    case "6":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(5, 0);
                        break;
                }
                break;
            case "green":
                //Debug.Log("lightcolliderenter_cgd03");
                switch (gameObject.name)
                {
                    case "2":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(1, 1);
                        break;
                    case "3":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(2, 1);
                        break;
                    case "4":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(3, 1);
                        break;
                    case "5":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(4, 1);
                        break;
                }
                break;
            case "blue":
                //Debug.Log("lightcolliderenter_cgd04");
                switch (gameObject.name)
                {
                    case "1":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(6, 2);
                        break;
                    case "2":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(7, 2);
                        break;
                    case "3":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(8, 2);
                        break;
                    case "4":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(9, 2);
                        break;
                    case "5":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(10, 2);
                        break;
                    case "6":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(11, 2);
                        break;
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("lightcolliderexit_cgd01   "+other.tag);

        switch (other.tag)
        {
            case "red":
                //Debug.Log("lightcolliderexit_cgd02");
                switch (gameObject.name)
                {
                    case "1":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(0, 3);
                        break;
                    case "6":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(5, 3);
                        break;
                }
                break;
            case "green":
                //Debug.Log("lightcolliderexit_cgd03");
                switch (gameObject.name)
                {
                    case "2":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(1, 3);
                        break;
                    case "3":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(2, 3);
                        break;
                    case "4":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(3, 3);
                        break;
                    case "5":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(4, 3);
                        break;
                }
                break;
            case "blue":
                //Debug.Log("lightcolliderexit_cgd04");
                switch (gameObject.name)
                {
                    case "1":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(6, 3);
                        break;
                    case "2":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(7, 3);
                        break;
                    case "3":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(8, 3);
                        break;
                    case "4":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(9, 3);
                        break;
                    case "5":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(10, 3);
                        break;
                    case "6":
                        ForRGBLight_cgd.instance_cgd.ChangeLightColor(11, 3);
                        break;
                }
                break;
        }
    }
}
