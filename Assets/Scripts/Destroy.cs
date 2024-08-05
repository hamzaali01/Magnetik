using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float Delay = 2;

    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(this.gameObject, Delay);
    }


}
