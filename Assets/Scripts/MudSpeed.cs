using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class MudSpeed : MonoBehaviour
{

    public float reduceSpeedBy = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<playerMovement>().speed = other.GetComponent<playerMovement>().speed - reduceSpeedBy;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<playerMovement>()._joystick.Horizontal != 0 || other.GetComponent<playerMovement>()._joystick.Vertical != 0)
            {
                MMVibrationManager.ContinuousHaptic(0.2f, 0.9f, 100f, HapticTypes.None, this);
            }
            else
            {
                MMVibrationManager.StopContinuousHaptic();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<playerMovement>().speed = other.GetComponent<playerMovement>().speed + reduceSpeedBy;
            MMVibrationManager.StopContinuousHaptic();

        }
    }
}
