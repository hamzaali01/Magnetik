using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    private bool detectPlayer = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            detectPlayer = true;
        }
    }
    // void OnTriggerStay(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         detectPlayer = true;
    //     }
    // }


    public bool playerDetected()
    {
        return detectPlayer;
    }

    public void setPlayerDetection(bool flag)
    {
        detectPlayer = flag;
    }
}
