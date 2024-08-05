using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleKidMovement : MonoBehaviour
{   
    public float speed = 3; 
    public float leftbound = -5;
    public float rightbound = 10;

    public Animator _animator;

    private Quaternion originalRotation;

    public bool canMove = true;

    public float jumpForce = 5;

    public float jumpDistance = 5;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if(canMove==true){

        // transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        // if(transform.position.x <= leftbound){
        //      transform.rotation = Quaternion.Euler(0, 180, 0);
        //      speed = -speed;
        // }
        //         if(transform.position.x >= rightbound){
        //      transform.rotation = Quaternion.Euler(0, 0, 0);
        //      speed = -speed;
        // }

        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && getHitState()==false){
            other.gameObject.GetComponent<playerMovement>().PlayJumpAnimation(true);
            playHitAnimation();
            StartCoroutine(Lift(other.gameObject));
            StartCoroutine(stopanimation());
        }

    }
    void OnTriggerExit(Collider other)
    {
       // stopHitAnimation();
    }
    IEnumerator stopanimation(){
          yield return new WaitForSeconds(1f);
          stopHitAnimation();
    }

    IEnumerator Lift(GameObject player){
        yield return new WaitForSeconds(0.1f);
       // player.GetComponent<Rigidbody>().AddForce(5,10,5,ForceMode.Impulse);
          Vector3 jumpDirection = transform.up;
            player.GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
             player.GetComponent<playerMovement>().canMove = false;


             if(transform.eulerAngles.y == 0){
                player.transform.rotation = Quaternion.Euler(0, 90, 0);
               player.GetComponent<Rigidbody>().velocity = new Vector3(jumpDistance,0,0);

             }
             else if(transform.eulerAngles.y == 270){
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
               player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,jumpDistance);

             }
            else if(transform.eulerAngles.y == 90){
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
               player.GetComponent<Rigidbody>().velocity = new Vector3(0,0,-jumpDistance);

             }
            else if(transform.eulerAngles.y == 180){
                player.transform.rotation = Quaternion.Euler(0, 270, 0);
               player.GetComponent<Rigidbody>().velocity = new Vector3(-jumpDistance,0,0);

             }


         yield return new WaitForSeconds(0.1f);
         player.GetComponent<playerMovement>().onGround = false;
               // player.GetComponent<Rigidbody>().gravityScale = fallGravityScale;
               //  player.GetComponent<Rigidbody>().gra


      //  stopHitAnimation();
        
    }

    public void playHitAnimation(){
       // canMove = false;
         originalRotation = transform.rotation;
       // transform.rotation = Quaternion.Euler(0, 270, 0);
        _animator.SetBool("Hit", true);
    }
        public void stopHitAnimation(){
        _animator.SetBool("Hit", false);
         transform.rotation = originalRotation;
        //canMove = true;
    }

    public bool getHitState(){
        return _animator.GetBool("Hit");
    }
}
