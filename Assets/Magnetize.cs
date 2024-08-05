using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class Magnetize : MonoBehaviour
{
      public int count = 0;
    Color SouthColor = Color.blue;
    Color NorthColor = Color.red;
    Color DefaultColor;

    // public Material defaultMat;
    // public Material redMat;
    // public Material blueMat;
    Renderer rend;
    // SkinnedMeshRenderer skinrend;
    public char polarity = 'd'; // d for default, n for north, s for south
    // Start is called before the first frame update

    public float magneticFieldStrength = 10f;

     public float overlapPreventionDistance = 0.0f;

    public AudioSource AudioSource1;
    public AudioClip magnetSfx;

    public AudioClip ClickSfx;

   // public bool giveParentPolarity = false;

public float maxDistance = 5f;
    void Start()
    {
        if(this.gameObject.tag=="Player"){
            rend = gameObject.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        }
        else{
            rend = GetComponent<Renderer>();
        }
        DefaultColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
            if (count % 3 == 0)
        {
           // rend.material = defaultMat;
            rend.material.color = DefaultColor;
            polarity = 'd';
          //  AssignPolarity();

        }
        else if (count % 3 == 1)
        {
           // rend.material = redMat;
           rend.material.color = NorthColor;
            polarity = 'n';
            //AssignPolarity();
        }
        else if (count % 3 == 2)
        {
           // rend.material = blueMat;
           rend.material.color = SouthColor;
            polarity = 's';
           // AssignPolarity();
        }




    // if(giveParentPolarity==true){
    //     this.count =  transform.parent.GetComponent<Magnetize>().count;

    // }
    // else{
                    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                count++;
                MMVibrationManager.Haptic(HapticTypes.Selection);
            }
        }
    }
   // }

    }

    void FixedUpdate()
    {
        if(polarity !='d'){
            Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance);
            foreach (Collider collider in colliders)
            {
        
                if (collider.attachedRigidbody != null && collider.tag =="Magnetic" && collider.GetComponent<Magnetize>().polarity != 'd')
                {
                    Vector3 direction = transform.position - collider.transform.position;
                    float distance = direction.magnitude;
                    if(distance >0){
                    direction.Normalize();
                    float forceMagnitude = magneticFieldStrength * collider.attachedRigidbody.mass / Mathf.Pow(distance, 2f);
                    Vector3 force = direction * forceMagnitude;
                   // Debug.Log(force);
                    if(polarity=='n' && collider.GetComponent<Magnetize>().polarity=='s'|| polarity=='s' && collider.GetComponent<Magnetize>().polarity=='n'){
                    //collider.attachedRigidbody.AddForce(force);
                    collider.GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
                    }
                    else if(polarity=='n' && collider.GetComponent<Magnetize>().polarity=='n'|| polarity=='s' && collider.GetComponent<Magnetize>().polarity=='s'){
                   // collider.attachedRigidbody.AddForce(-force);
                    collider.GetComponent<Rigidbody>().AddForce(-force, ForceMode.Acceleration);
                    }
                    }   
                }
            }
        }


    //    RaycastHit hitInfo;
    //     bool hit = Physics.Raycast(transform.position, transform.forward, out hitInfo, overlapPreventionDistance);

    //   //  Ray ray = new Ray(transform.position, transform.forward);

    //     if (hit && hitInfo.collider.gameObject != gameObject)
    //     {
    //          Debug.DrawRay(transform.position, transform.forward * overlapPreventionDistance, Color.red);

    //         // Move the object away from the hit point to prevent overlapping
    //         Vector3 moveDirection = transform.position - hitInfo.point;
    //         transform.position += moveDirection.normalized * (overlapPreventionDistance - moveDirection.magnitude);
    //     }
    

    }

        public char getPolarity()
    {
        return polarity;
    }

    void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag=="Player"){
            if(other.tag=="RMagnetSphere" )
                count=1;
            else if(other.tag=="BMagnetSphere" )
                count=2;
            else if(other.tag=="DMagnetSphere" )
                count=0;
        Object.Destroy(other.gameObject,0.2f);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
            if(collisionInfo.gameObject.tag == "Magnetic" || collisionInfo.gameObject.tag =="StoppingObject"){
                // AudioSource1.time = AudioSource1.clip.length * 0.1f;
                AudioSource1.pitch = 1.1f;
                  AudioSource1.PlayOneShot(magnetSfx);

        }
    }


    // void OnCollisionStay(Collision collisionInfo)
    // {
    //     if (collisionInfo.gameObject.CompareTag("Player"))
    //     {
    //         // if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().name == "Level3")
    //         // {
    //         // //collisionInfo.gameObject.transform.SetParent(transform);
    //         // collisionInfo.gameObject.transform.position = new Vector3(collisionInfo.gameObject.transform.position.x ,collisionInfo.gameObject.transform.position.y+7 ,collisionInfo.gameObject.transform.position.z);
    //         // if(collisionInfo.gameObject.transform.position.z >){

    //         // }
    //         // if(collisionInfo.gameObject.transform.position.z <){

    //         // }
    //         // }
    //         // Attach the player to the cube
    //        // collisionInfo.gameObject.transform.SetParent(transform);
    //       // collisionInfo.gameObject.transform.position = new Vector3(collisionInfo.gameObject.transform.position.x ,collisionInfo.gameObject.transform.position.y ,gameObject.transform.position.z);
    //     }
    // }


    // void OnCollisionExit(Collision collisionInfo)
    // {
    //                    if (collisionInfo.gameObject.CompareTag("Player"))
    //     {
    //                     if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().name == "Level3")
    //         {
    //              collisionInfo.gameObject.transform.SetParent(null);
    //         }
    //         // Attach the player to the cube

    //     }
    // }
}
