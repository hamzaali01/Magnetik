using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour
{


    public GameObject GreenRing;
    public GameObject YellowRing;
    public GameObject RedRing;
    public GameObject Detector;

    public GameObject HitEffect1;
    public GameObject HitEffect2;

    public GameObject GrayRing;
    // Start is called before the first frame update
    void Start()
    {
        GreenRing = transform.GetChild(1).gameObject;
        YellowRing = transform.GetChild(2).gameObject;
        RedRing = transform.GetChild(3).gameObject;
        Detector = transform.GetChild(4).gameObject;
        HitEffect1 = transform.GetChild(5).gameObject;
        HitEffect2 = transform.GetChild(6).gameObject;
        GrayRing = transform.GetChild(7).gameObject;

    }


}
