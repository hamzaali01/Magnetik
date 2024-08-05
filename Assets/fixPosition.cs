using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class fixPosition : MonoBehaviour
{
    public GameObject hinge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(SceneManager.GetActiveScene().name == "Level8" && collisionInfo.gameObject.tag=="Magnetic"){
            collisionInfo.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if(hinge != null){
                Object.Destroy(hinge.GetComponent<Rigidbody>());
            }
        }
               if(SceneManager.GetActiveScene().name == "Level8" && collisionInfo.gameObject.tag=="StoppingObject"){
                gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
    
        }

      

    }
    void OnTriggerEnter(Collider other)
    {
          if(SceneManager.GetActiveScene().name == "Level4" && other.gameObject.tag=="StoppingObject" ){
              this.gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationX;
                this.gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;

        }
    }
}
