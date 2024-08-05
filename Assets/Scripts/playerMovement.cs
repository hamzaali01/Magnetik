using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private Animator[] _animator;
    private CharacterAnimationController _animationController;



    public float speed = 6.0F;

    public bool canMove = false;

    //Animation hitAnimation;

    public AudioSource AudioSource1;
    public AudioClip RunSfx;
    private bool RunSfxHasPlayed = false;

    Rigidbody rb;

    //public float rotateSpeed = 3.0F;
    public FloatingJoystick _joystick;

    public bool PowerActivated = false;

    public bool isSlipping = false;
    public bool isHitting = false;

    //below is testing
    public float slidingForce = 10.0f;
    public float slidingDamping = 0.1f;
     private Vector3 _slidingDirection;
    public  bool IsOnSlipperySurface = false;

    //
    public bool isJumping = false;
    //       public float gravityScale = 1.0f;
  
    // public  float globalGravity = -9.81f;

    public bool onGround = true;

    public float horzOffset = 0;
    public float vertOffset = 0;

    Vector3 playerInitialPos;



    void Start()
    {
        if (ManageGameState.selectedCharacter == 0)
            _animationController = new CharacterAnimationController(_animator[0]);
        else if (ManageGameState.selectedCharacter == 1)
            _animationController = new CharacterAnimationController(_animator[1]);
        else if (ManageGameState.selectedCharacter == 2)
            _animationController = new CharacterAnimationController(_animator[2]);
        rb = GetComponent<Rigidbody>();

        transform.GetChild(ManageGameState.selectedCharacter).gameObject.SetActive(true);

        canMove = false;
        StartCoroutine(LetMove());

        playerInitialPos =  transform.position;

      //  rb.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ManageGameState.gameIsPaused == false)
        {

                             
            // Vector3 gravity = globalGravity * gravityScale * Vector3.up;
            // Vector3 gravity =  gravityScale * Vector3.up;
            // rb.AddForce(gravity, ForceMode.Acceleration);



            if (canMove == true)
            {
                float horizontal = _joystick.Horizontal;
                float vertical = _joystick.Vertical;

                                                                        if (IsOnSlipperySurface)
        {
            _slidingDirection = (_slidingDirection + (rb.velocity.normalized * slidingForce)) * slidingDamping;
            rb.AddForce(_slidingDirection, ForceMode.Acceleration);
        }


                // if (transform.position.z < ManageGameState.backwardBoundary && vertical < 0)
                // {
                //     vertical = 0;
                // }
                // if (transform.position.z > ManageGameState.forwardBoundary && vertical > 0)
                // {
                //     vertical = 0;
                // }
                if (transform.position.y < ManageGameState.fallBoundary)
                {
                    transform.position = playerInitialPos;
                }



               
               rb.velocity = new Vector3((horizontal+horzOffset) * speed, rb.velocity.y, (vertical+vertOffset) * speed);

                if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
                {
                    if (rb.velocity != Vector3.zero)
                        transform.rotation = Quaternion.LookRotation(rb.velocity);

                    _animationController.PlayAnimation(AnimationType.Move);
                    if (!RunSfxHasPlayed)
                    {
                        AudioSource1.volume = 0.2f;
                        AudioSource1.PlayOneShot(RunSfx);
                        RunSfxHasPlayed = true;
                    }

                }
                else
                {
                    _animationController.StopAnimation(AnimationType.Move);
                    AudioSource1.Stop();
                    RunSfxHasPlayed = false;
                   // rb.velocity = Vector3.zero;

                }


                if (gameObject.transform.rotation.eulerAngles.x >= 2)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }

            }
            else
            {
                // transform.rotation = Quaternion.Euler(0, 0, 0);
               // rb.velocity = Vector3.zero;
                _animationController.StopAnimation(AnimationType.Move);
                AudioSource1.Stop();
                RunSfxHasPlayed = false;

            }


        }
        else
        {
            //Time.timeScale = 0f;
        }
    }




    public void PlayHitAnimation(bool flag)
    {
        if (flag == true)
        {
            isHitting = true;
            canMove = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animationController.PlayAnimation(AnimationType.Hit);
        }
        else
        {
            _animationController.StopAnimation(AnimationType.Hit);
            canMove = true;
            isHitting = false;
        }
    }

    public bool GetHitState()
    {
        return _animationController.GetHitState();
    }

    public int getHitIndex()
    {
        return _animationController.GetHitIndex();
    }

    public void PlaySlipAnimation(bool flag)
    {
        if (flag == true)
        {
            isSlipping = true;
            canMove = false;
            _animationController.PlayAnimation(AnimationType.Slip);
        }
        else
        {
            _animationController.StopAnimation(AnimationType.Slip);
            canMove = true;
            isSlipping = false;
        }
    }

    public bool GetSlipState()
    {
        return _animationController.GetSlipState();
    }

        public void PlayJumpAnimation(bool flag)
    {
        if (flag == true)
        {
            rb.velocity = Vector3.zero;
            isJumping = true;
            canMove = false;
           // rb.useGravity = false;
            _animationController.PlayAnimation(AnimationType.Jump);
        }
        else
        {
           _animationController.StopAnimation(AnimationType.Jump);
            canMove = true;
            isJumping = false;
        }
    }

    IEnumerator LetMove(){
       yield return new WaitForSeconds(2f);
       canMove=true;
    }
    //     public bool GetJumpState()
    // {
    //     return _animationController.GetJumpState();
    // }


    // void OnCollisionEnter(Collision collisionInfo)
    // {
    //             if(collisionInfo.collider.tag == "Ground" && onGround==false){
    //         Debug.Log("hitting the ground");
    //         PlayJumpAnimation(false);
    //         onGround=true;
    //     }
    // }


    //  void OnCollisionExit(Collision collisionInfo)
    // {
    //             if(collisionInfo.collider.tag == "Ground"){
    //         Debug.Log("Leaving the ground");
    //         StartCoroutine(InAir());
    //         //PlayJumpAnimation(false);
    //     }
    // }

    // IEnumerator InAir(){
    //         yield return new WaitForSeconds(0.1f);
    //         onGround = false;
    // }

    // void OnCollisionEnter(Collision collisionInfo)
    // {
    //       if(collisionInfo.collider.tag == "Ground" && onGround==false){
    //         Debug.Log("Back on ground");
    //           PlayJumpAnimation(false);
    //       }
    // }

}
