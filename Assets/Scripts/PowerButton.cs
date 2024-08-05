using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{

    Animator _animator;

    public bool ButtonIsBeingPressed = false;

    public bool Initiliaze = false;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Initiliaze == true)
        {
            Invoke("ResetInitialize", 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger("GoDown");
            ButtonIsBeingPressed = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetTrigger("GoUp");
            ButtonIsBeingPressed = false;
            gameObject.GetComponent<AudioSource>().Stop();

            Initiliaze = true;
        }
    }

    void ResetInitialize()
    {
        Initiliaze = false;

    }
}
