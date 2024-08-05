using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(-17.3999996f,49.9000015f,-26.7000008f);
   

     public Vector3 startoffset = new Vector3(-5.30000019f,17.3799992f,-9.60000038f);

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    public float transitionDuration = 2f; // in seconds

    private bool follow = false;

    private float timer = 0f;

    void Start()
    {

        transform.position = player.transform.position + startoffset;
        timer = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        initialPosition = player.transform.position + startoffset;
        finalPosition = player.transform.position + offset;

        float t = Mathf.Clamp01(timer / transitionDuration);
        t = Mathf.SmoothStep(0f, 1f, t);
        transform.position = Vector3.Lerp(initialPosition, finalPosition, t);

        //}
        


        if(follow == false && transform.position ==  player.transform.position + offset){
                follow = true;
        }

        if(follow == true){
            transform.position = player.transform.position + offset;//new Vector3(0, 10, -15)
        }


      //  transform.position = player.transform.position + offset;//new Vector3(0, 10, -15);
       
    }


}
