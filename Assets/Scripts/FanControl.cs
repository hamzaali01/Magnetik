using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControl : MonoBehaviour
{
    public GameObject Fan1;
    public AudioSource fanAudio;

    bool turnbackon = true;

    public float fanRunningTime = 3f;
    public float fanResetTime =  2f;

    public float fanForce = 20f;


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Balloon")
        {
            if (Fan1.GetComponent<Animator>().GetBool("StartFan") == true)
            {
                if (this.transform.rotation.eulerAngles.y == 0)
                    other.GetComponent<Rigidbody>().AddForce(fanForce, 0, 0, ForceMode.Acceleration);
                if (this.transform.rotation.eulerAngles.y == 180)
                    other.GetComponent<Rigidbody>().AddForce(-fanForce, 0, 0, ForceMode.Acceleration);

            }
        }
    }

    void FixedUpdate()
    {
        if (turnbackon == true)
        {
           // StartCoroutine(WaitRandom(Random.Range(0,2)));
            turnbackon = false;
            StartCoroutine(TurnFansOn());

        }
    }

    public IEnumerator TurnFansOn()
    {
        yield return new WaitForSeconds(3f);

        Fan1.GetComponent<Animator>().SetBool("StartFan", true);
        fanAudio.Play();

        yield return new WaitForSeconds(fanRunningTime);

        Fan1.GetComponent<Animator>().SetBool("StartFan", false);
        fanAudio.Stop();

        yield return new WaitForSeconds(fanResetTime);
        turnbackon = true;


    }

        public IEnumerator WaitRandom(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
