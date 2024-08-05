using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class PlayerSlip : MonoBehaviour
{

    public AudioSource source;

    private bool slipped = false;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<playerMovement>().isSlipping == false && other.gameObject.GetComponent<playerMovement>().isHitting == false && slipped == false)
            {
                slipped = true;
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                other.gameObject.GetComponent<playerMovement>().PlaySlipAnimation(true);
                // this.gameObject.GetComponent<Rigidbody>().AddForce(0, 500, 0, ForceMode.Impulse);
                StartCoroutine(CanMoveAgain(other.gameObject));
            }
        }
    }

    IEnumerator CanMoveAgain(GameObject other)
    {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.GetComponent<Rigidbody>().AddForce(0, 1200, 0, ForceMode.Impulse);
        yield return new WaitForSeconds(0.4f);
        other.GetComponent<TraumaInducer>().enabled = true;
        source.Play();
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

        yield return new WaitForSeconds(2f);
        other.GetComponent<playerMovement>().PlaySlipAnimation(false);
        other.GetComponent<TraumaInducer>().enabled = false;
        Destroy(this.gameObject, 1f);
    }
}
