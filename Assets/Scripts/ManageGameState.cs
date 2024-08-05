using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class ManageGameState : MonoBehaviour
{

    public static int selectedCharacter = 0;

    public int requiredBalloons = 1;
    public int startingBalloons = 1;

    public int totalGems = 9;

    public float restartDelay = 2f;

    public static float leftBoundary = -13.35f;
    public static float rightBoundary = 13.35f;
    public static float forwardBoundary = 50f;
    public static float backwardBoundary = -50f;

    public static float fallBoundary = -30f;

    public static int successfulBalloonsCount = 0;

    public static int gemCount = 0;

    public static int aliveBalloonsCount;

    // public GameObject GemText;
    // public GameObject GoalText;
    // public GameObject startingBalloonText; //shows in pause menu
    // public GameObject startingBalloonText2; // shows in success menu
    // public GameObject[] requiredBalloonText; // 0 for pause menu, 1 for successmenu, 2 for fail menu
    // public GameObject[] successfulBalloonText; // 0 for successmenu, 1 for fail menu

    public GameObject LevelText;

    public GameObject PauseMenu;
    public GameObject SuccessMenu;

    public GameObject InstructionMenu;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public Sprite StarSprite;

    public Sprite EmptyStarSprite;

    public GameObject Confetti1;
    public GameObject Confetti2;
    private bool hasPlayed = false;

    public AudioSource audioSource;

    public int showalive; //just to display aliveBalloonsCount variable in inspector
    public int showGem; //just to display gemCount variable in inspector

    public static bool gameIsPaused = false;
    public bool levelPassed = false;

    private static int totalLevels = 9;

    public Animator _animator;

    public GameObject GoalDetector;


    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;

        aliveBalloonsCount = startingBalloons;
        showalive = aliveBalloonsCount;

        showGem = gemCount;

        Confetti1 = transform.GetChild(1).gameObject;
        Confetti2 = transform.GetChild(2).gameObject;

        levelPassed = false;
        successfulBalloonsCount = 0;

        //Pause();

        // Star1.GetComponent<Image>().sprite = EmptyStarSprite;
        // Star2.GetComponent<Image>().sprite = EmptyStarSprite;
        // Star3.GetComponent<Image>().sprite = EmptyStarSprite;


        // GoalText.GetComponent<TextMeshProUGUI>().text = "" + requiredBalloons;

        // startingBalloonText.GetComponent<TextMeshProUGUI>().text = "" + startingBalloons;
        // startingBalloonText2.GetComponent<TextMeshProUGUI>().text = "" + startingBalloons;
        // requiredBalloonText[0].GetComponent<TextMeshProUGUI>().text = "" + requiredBalloons;

        LevelText.GetComponent<TextMeshProUGUI>().text = "Level " + SceneManager.GetActiveScene().buildIndex;

        if(SceneManager.GetActiveScene().buildIndex == 1){
            InstructionMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(GoalDetector.GetComponent<DetectPlayer>().playerDetected()==true){
                  SuccessMenu.SetActive(true);
                  if(levelPassed==false)
                  StartCoroutine(LoadLevel());
        }


        if (successfulBalloonsCount >= requiredBalloons)
        {

            if (hasPlayed == false)
            {
                Confetti1.GetComponent<ParticleSystem>().Play();
                Confetti2.GetComponent<ParticleSystem>().Play();
                audioSource.Play();
                hasPlayed = true;
            }

            levelPassed = true;

            if (aliveBalloonsCount - successfulBalloonsCount == 0)
            {
                // requiredBalloonText[1].GetComponent<TextMeshProUGUI>().text = "" + requiredBalloons;
                // successfulBalloonText[0].GetComponent<TextMeshProUGUI>().text = "" + successfulBalloonsCount;

                if (successfulBalloonsCount == requiredBalloons)
                    Star1.GetComponent<Image>().sprite = StarSprite;
                if (successfulBalloonsCount > requiredBalloons && successfulBalloonsCount < startingBalloons)
                {
                    Star1.GetComponent<Image>().sprite = StarSprite;
                    Star2.GetComponent<Image>().sprite = StarSprite;
                }
                if (successfulBalloonsCount == startingBalloons)
                {
                    Star1.GetComponent<Image>().sprite = StarSprite;
                    Star2.GetComponent<Image>().sprite = StarSprite;
                    Star3.GetComponent<Image>().sprite = StarSprite;
                }
                SuccessMenu.SetActive(true);
            }
        }

        if (aliveBalloonsCount < requiredBalloons)
        {
            // requiredBalloonText[2].GetComponent<TextMeshProUGUI>().text = "" + requiredBalloons;
            // successfulBalloonText[1].GetComponent<TextMeshProUGUI>().text = "" + successfulBalloonsCount;
           // FailMenu.SetActive(true);
            // Debug.Log("Level Failed");
            // Invoke("Restart", restartDelay);
        }

        showalive = aliveBalloonsCount;
        showGem = gemCount;

        // GemText.GetComponent<TextMeshProUGUI>().text = "" + gemCount;

        if (gameIsPaused)
        {
            PauseMenu.SetActive(true);
        }
        else
        {
            PauseMenu.SetActive(false);
        }

    }


    public void Restart()
    {
        gemCount = gemCount - totalGems;
        if (gemCount < 0)
            gemCount = 0;

        successfulBalloonsCount = 0;
        //SceneManager.LoadScene("Level01");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        //disable UI
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        //enable UI
        gameIsPaused = true;
        Time.timeScale = 0f;
    }


    //for closing instruction menu in level 1
        public void Ok()
    {
        InstructionMenu.SetActive(false);
    }

    public void Quit()
    {
        if (levelPassed == false)
        {
            gemCount = gemCount - totalGems;
            if (gemCount < 0)
                gemCount = 0;

            successfulBalloonsCount = 0;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < totalLevels)
        {
            SuccessMenu.SetActive(false);
            successfulBalloonsCount = 0;
            _animator.SetTrigger("Start");
            StartCoroutine(LoadLevel());
        }
    }

    private IEnumerator LoadLevel()
    {
                      levelPassed = true;
                       audioSource.Play();
        yield return new WaitForSeconds(3);
         if (SceneManager.GetActiveScene().buildIndex < totalLevels)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                else 
        {
              SceneManager.LoadScene("MainMenu");
        }
    }


}
