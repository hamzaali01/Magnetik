using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGem : MonoBehaviour
{
    public AudioSource gemSFX;
    void OnTriggerEnter(Collider other)
    {
        gemSFX.Play();
        ManageGameState.gemCount++;
        this.gameObject.SetActive(false);
    }
}
