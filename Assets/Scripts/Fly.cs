using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class Fly : MonoBehaviour
{
    public float forwardForce = 80; // for z 
    public float upForce = 80; // for y 
    private float leftRightDirection = 0; // for x
    private bool PopHasPlayed = false;
    private bool HitHasPlayed = false;

    private bool balloonDead = false;

    public bool perfectTime = false;
    public bool okayTime = false;
    public bool badTime = false;

    public bool canNotHit = true;
    public bool isBeingHit = false;

    private bool phase1 = false;
    private bool phase2 = false;
    private bool phase3 = false;

    public AudioClip BalloonPop;
    public AudioClip[] BalloonHit;
    GameObject GreenRing;
    GameObject YellowRing;
    GameObject RedRing;

    public GameObject GrayRing;

    GameObject Balloon;
    GameObject Spark;
    GameObject Confetti;
    GameObject Detector;
    GameObject HitEffect1;
    GameObject HitEffect2;

    public GameObject PerfectText;

    public GameObject PowerUpText;

    Manage parentScript;
    DetectPlayer detectScript;

    playerMovement playerScript;


    PowerButton PowerButtonScript;
    PowerButton PowerButtonScript2;

    public AudioSource AudioSource1;
    public AudioSource AudioSource2;



    GameObject Player;

    GameObject PowerButton;
     GameObject PowerButton2;

    GameObject ObstacleKid;

    bool tutorial;

     private bool detectObstacleKid = false;
     ObstacleKidMovement obstaclekidscript;



    void Start()
    {

        Player = GameObject.FindWithTag("Player");
        playerScript = Player.GetComponent<playerMovement>();

        //PowerButton = GameObject.FindWithTag("PowerButton");
        if(GameObject.FindGameObjectsWithTag("PowerButton").Length > 0){
            if(GameObject.FindGameObjectsWithTag("PowerButton").Length >= 1)
                PowerButton =  GameObject.FindGameObjectsWithTag("PowerButton")[0];
            if(GameObject.FindGameObjectsWithTag("PowerButton").Length == 2)
                PowerButton2 = GameObject.FindGameObjectsWithTag("PowerButton")[1];
        }

        //  ObstacleKid = GameObject.FindWithTag("ObstacleKid");
        //  if (ObstacleKid != null)
        //  {
        //   obstaclekidscript = ObstacleKid.GetComponent<ObstacleKidMovement>();
        //  }

        if (PowerButton != null)
        {
                PowerButtonScript = PowerButton.GetComponent<PowerButton>();
        }
        if (PowerButton2 != null)
        {
                PowerButtonScript2 = PowerButton2.GetComponent<PowerButton>();
        }



        parentScript = transform.parent.GetComponent<Manage>();
        GreenRing = parentScript.GreenRing;
        YellowRing = parentScript.YellowRing;
        RedRing = parentScript.RedRing;
        Detector = parentScript.Detector;
        HitEffect1 = parentScript.HitEffect1;
        HitEffect2 = parentScript.HitEffect2;
        GrayRing = parentScript.GrayRing;

        Balloon = transform.GetChild(0).gameObject;
        Spark = transform.GetChild(1).gameObject;
        Confetti = transform.GetChild(2).gameObject;
        detectScript = Detector.GetComponent<DetectPlayer>();

        

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            tutorial = true;
        }
        else
        {
            tutorial = false;
        }

    }

    void FixedUpdate()
    {
        if (ManageGameState.gameIsPaused == false)
        {

            if (balloonDead == false)
            {
                CheckRays();

                if (isBeingHit == false)
                    ActivateRings();

                PlayerDetection();

                // if (transform.position.x < -4.15 || transform.position.x > 32.5)
                // {
                if (transform.position.y < 5)
                {
                    balloonDead = true;
                    ManageGameState.aliveBalloonsCount--;
                }

                if (PowerButton != null)
                {
                    if (PowerButtonScript.ButtonIsBeingPressed == true)
                    {
                        gameObject.GetComponent<Rigidbody>().AddForce(0, 30, 0, ForceMode.Force);
                        canNotHit = true;
                        //isBeingHit = false;
                    }
                    if (PowerButtonScript.Initiliaze == true)
                    {
                        Initiliaze();
                    }
                
                   // }
                }
                if (PowerButton2 != null)
                {
                    if (PowerButtonScript2.ButtonIsBeingPressed == true)
                    {
                        gameObject.GetComponent<Rigidbody>().AddForce(0, 30, 0, ForceMode.Force);
                        canNotHit = true;
                        //isBeingHit = false;
                    }
                    if (PowerButtonScript2.Initiliaze == true)
                    {
                        Initiliaze();
                    }
                }

                if (playerScript.PowerActivated == true)
                {
                    if (gameObject.GetComponent<Rigidbody>().useGravity == true)
                    {
                        PowerUpText.SetActive(true);
                        StartCoroutine(FreezeBalloon());
                    }
                }
            }
            else
            {
                Initiliaze();

                if (Confetti.GetComponent<ParticleSystem>().isPlaying == false)
                {
                    this.gameObject.SetActive(false);
                }


            }

        }

    }



    void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Ground" && this.enabled == true)
        {
            PopBalloon();
        }
        if (collisionInfo.gameObject.tag == "Wall" && this.enabled == true)
        {
            PopBalloon();
        }
        if (collisionInfo.gameObject.tag == "Obstacle" && this.enabled == true)
        {
            PopBalloon();
        }
    }


    public void Initiliaze()
    {
        perfectTime = false;
        okayTime = false;
        badTime = false;

        phase1 = false;
        phase2 = false;
        phase3 = false;

        GreenRing.SetActive(false);
        YellowRing.SetActive(false);
        RedRing.SetActive(false);
        Detector.SetActive(false);

        HitHasPlayed = false;

    }

    void CheckRays()
    {
        RaycastHit hitinfo;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hitinfo, 100) && hitinfo.collider.tag == "Ground" && perfectTime == false && okayTime == false && badTime == false)
        {
            //canNotHit = true;
            isBeingHit = false;

        }

        if (Physics.Raycast(ray, out hitinfo, 7) && isBeingHit == false && hitinfo.collider.tag == "Ground" && okayTime == false && badTime == false)
        {
            canNotHit = false;
            perfectTime = true;
        }
        if (Physics.Raycast(ray, out hitinfo, 5) && isBeingHit == false && perfectTime == true && badTime == false && hitinfo.collider.tag == "Ground")
        {
            canNotHit = false;
            okayTime = true;
        }
        if (Physics.Raycast(ray, out hitinfo, 3) && isBeingHit == false && okayTime == true && perfectTime == true && hitinfo.collider.tag == "Ground")
        {
            canNotHit = false;
            badTime = true;
        }
    }

    void ActivateRings()
    {
        //using phase booleans so that i dont call this active function again and again.
        if (perfectTime == true && okayTime != true && badTime != true && phase1 == false)
        {
            GreenRing.SetActive(true);
            YellowRing.SetActive(false);
            RedRing.SetActive(false);
            Detector.SetActive(true);
            GrayRing.SetActive(false);
            //Debug.Log("Perfect");
            phase1 = true;
        }
        else if (perfectTime == true && okayTime == true && badTime != true && phase2 == false)
        {
            GreenRing.SetActive(false);
            RedRing.SetActive(false);
            YellowRing.SetActive(true);
            Detector.SetActive(true);
            GrayRing.SetActive(false);
            // Debug.Log("Okay");
            phase2 = true;
        }
        else if (perfectTime == true && okayTime == true && badTime == true && phase3 == false)
        {
            GreenRing.SetActive(false);
            YellowRing.SetActive(false);
            RedRing.SetActive(true);
            Detector.SetActive(true);
            GrayRing.SetActive(false);
            //Debug.Log("Bad");
            phase3 = true;
        }
        else if (perfectTime == false && okayTime == false && badTime == false)
        {
            if (SceneManager.GetActiveScene().buildIndex != 7)
                GrayRing.SetActive(true);
        }


        if (GreenRing.activeInHierarchy)
        {
            GreenRing.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z - 3);

            if (tutorial == true)
                gameObject.GetComponent<Rigidbody>().useGravity = false; //FOR TUTORIAL
        }

        if (YellowRing.activeInHierarchy)
        {
            YellowRing.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z - 3);
        }

        if (RedRing.activeInHierarchy)
        {
            RedRing.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z - 3);
        }
        if (Detector.activeInHierarchy)
        {
            Detector.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z - 3);
        }
        if (GrayRing.activeInHierarchy)
        {
            GrayRing.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z - 3);
        }
    }

    void PlayerDetection()
    {
        if (detectScript.playerDetected() == true && PopHasPlayed == false && playerScript.GetHitState() == false && playerScript.GetSlipState() == false)
        {
            if (perfectTime == true && okayTime == false && badTime == false && isBeingHit == false)
            {
                detectScript.setPlayerDetection(false);
                isBeingHit = true;
                //  Player.transform.position = new Vector3(Detector.transform.position.x, Detector.transform.position.y, Detector.transform.position.z - 2);
                playerScript.PlayHitAnimation(true);
                StartCoroutine(fly("Perfect"));
            }
            else if (perfectTime == true && okayTime == true && badTime == false && isBeingHit == false)
            {
                detectScript.setPlayerDetection(false);
                isBeingHit = true;
                //  Player.transform.position = new Vector3(Detector.transform.position.x, Detector.transform.position.y, Detector.transform.position.z - 2);
                playerScript.PlayHitAnimation(true);
                StartCoroutine(fly("Okay"));

            }
            else if (perfectTime == true && okayTime == true && badTime == true && isBeingHit == false)
            {
                detectScript.setPlayerDetection(false);
                isBeingHit = true;
                // Player.transform.position = new Vector3(Detector.transform.position.x, Detector.transform.position.y, Detector.transform.position.z - 2);
                playerScript.PlayHitAnimation(true);
                StartCoroutine(fly("Bad"));

            }

        }
        else if(/*detectKidScript!= null &&*/ detectObstacleKid==true/*detectKidScript.ObstacleKidDetected()==true*/ && detectScript.playerDetected() == false && PopHasPlayed == false && playerScript.GetHitState() == false ){
           // detectKidScript.setObstacleKidDetection(false);
            detectObstacleKid = false;
             isBeingHit = true;
             obstaclekidscript.playHitAnimation();
             // playerScript.PlayHitAnimation(true); // instead play kid animation
                StartCoroutine(fly("Kid")); 
        }
    }

    IEnumerator fly(string timing)
    {
        yield return new WaitForSeconds(0.5f);


        if (tutorial == true)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true; //JUST FOR TUTORIAL
            tutorial = false;
        }

        if (timing == "Perfect")
        {
            PerfectText.SetActive(false);
            PerfectText.SetActive(true);
            // gameObject.GetComponent<TraumaInducer>().MaximumStress = 0.4f;
            gameObject.GetComponent<TraumaInducer>().enabled = true;
            // MMVibrationManager.Vibrate();
            gameObject.GetComponent<Rigidbody>().AddForce(leftRightDirection, upForce, forwardForce, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20), ForceMode.Impulse);
            playHitEffects();

        }
        else if (timing == "Okay")
        {
            // gameObject.GetComponent<TraumaInducer>().MaximumStress = 0.4f;
            gameObject.GetComponent<TraumaInducer>().enabled = true;
            // MMVibrationManager.Vibrate();
            gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(-40, 40), upForce - 15, forwardForce - 15, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20), ForceMode.Impulse);
            playHitEffects();
            // MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
        else if (timing == "Bad")
        {
            //MMVibrationManager.Vibrate();
            //MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            PopBalloon();
            // MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            // player fall
        }
        else if (timing == "Kid")
        {
           // PerfectText.SetActive(false);
          //  PerfectText.SetActive(true);
            // gameObject.GetComponent<TraumaInducer>().MaximumStress = 0.4f;
           // gameObject.GetComponent<TraumaInducer>().enabled = true;
            // MMVibrationManager.Vibrate();
            gameObject.GetComponent<Rigidbody>().AddForce(leftRightDirection, upForce-15, -(forwardForce-15), ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20), ForceMode.Impulse);
            playHitEffects();

        }

        yield return new WaitForSeconds(0.1f);
        canNotHit = true;
        isBeingHit = false;
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        Initiliaze();

        if(detectScript.playerDetected()==true)
        detectScript.setPlayerDetection(false);
        if(detectObstacleKid==true)
        detectObstacleKid = false;

        yield return new WaitForSeconds(0.4f);

        if(playerScript.GetHitState()==true)
        playerScript.PlayHitAnimation(false);
        if(obstaclekidscript !=null && obstaclekidscript.getHitState()==true)
        obstaclekidscript.stopHitAnimation();
        Initiliaze();

        //PROBLEM: AGAR KID NE PEHLE MAAR DIA PLAYER SE, TOU YE WALI LINE PLAYER KA CAMERA SHAKE DISBALE KRDEGI
        gameObject.GetComponent<TraumaInducer>().enabled = false;
        // gameObject.GetComponent<TraumaInducer>().MaximumStress = 0.4f;

        yield return new WaitForSeconds(0.4f);
        PerfectText.SetActive(false);
    }

    //plays the particle effect and the audio for hit
    void playHitEffects()
    {
        HitEffect1.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        HitEffect2.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

        if (HitEffect1.GetComponent<ParticleSystem>().isPlaying == false)
            HitEffect1.GetComponent<ParticleSystem>().Play();
        if (HitEffect2.GetComponent<ParticleSystem>().isPlaying == false)
            HitEffect2.GetComponent<ParticleSystem>().Play();

        if (!HitHasPlayed)
        {
            //int index = Random.Range(0, 3);
            int index = playerScript.getHitIndex();
            if (index == 0)
            {
                // AudioSource2.pitch = 1.8f;
                AudioSource2.pitch = 1f;
                AudioSource2.volume = 1f;
            }
            else if (index == 1)
            {
                // AudioSource2.pitch = 1.8f;
                AudioSource2.pitch = 2;
                AudioSource2.volume = 1f;
                //index = 0;
            }
            else
            {
                AudioSource2.pitch = 1.3f;
                AudioSource2.volume = 1f;
            }
            AudioSource2.PlayOneShot(BalloonHit[index]);
            HitHasPlayed = true;
        }

    }

    void PopBalloon()
    {
        if (PopHasPlayed == false)
        {

            Balloon.SetActive(false);

            RedRing.SetActive(false);

            GrayRing.SetActive(false);

            gameObject.GetComponent<TraumaInducer>().enabled = true;

            if (!PopHasPlayed)
            {
                AudioSource1.PlayOneShot(BalloonPop);
                PopHasPlayed = true;
                // balloonDead = true;
                ManageGameState.aliveBalloonsCount--;
            }

        }
        if (PopHasPlayed == true && balloonDead == false)
        {
            balloonDead = true;
            if (Spark.GetComponent<ParticleSystem>().isPlaying == false)
                Spark.GetComponent<ParticleSystem>().Play();
            if (Confetti.GetComponent<ParticleSystem>().isPlaying == false)
                Confetti.GetComponent<ParticleSystem>().Play();
        }

    }

    IEnumerator FreezeBalloon()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;

        yield return new WaitForSeconds(4f);

        playerScript.PowerActivated = false;
        PowerUpText.SetActive(false);
        gameObject.GetComponent<Rigidbody>().useGravity = true;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObstacleKid")
        {
            ObstacleKid = other.gameObject;
            obstaclekidscript = other.GetComponent<ObstacleKidMovement>();
            detectObstacleKid = true;
        }
    }
    // void OnTriggerStay(Collider other)
    // {
    //     if (other.tag == "Player")
    //     {
    //         detectPlayer = true;
    //     }
    // }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "ObstacleKid")
        {
            detectObstacleKid= false;
        }

    }

}
