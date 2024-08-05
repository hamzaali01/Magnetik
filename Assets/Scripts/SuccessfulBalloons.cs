using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessfulBalloons : MonoBehaviour
{

    public AudioSource AudioSource;
    public AudioClip ShortSuccess;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Balloon")
        {
            other.GetComponent<Fly>().GrayRing.SetActive(false);
            other.GetComponent<Fly>().enabled = false;
            ManageGameState.successfulBalloonsCount += 1;
            AudioSource.PlayOneShot(ShortSuccess);
        }
    }
}
