using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HandleClicks : MonoBehaviour
{

    public AudioSource audioSource;
    public GameObject Panel1;
    public GameObject Panel2;

    // public GameObject Panel3;

    // public GameObject Panel4;

    // public GameObject SelectText;

    public int currentSelector = 0;

    public int totalCharacters = 3;

    public bool characterChanged = false;

    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        Panel1 = transform.GetChild(0).gameObject;
        Panel2 = transform.GetChild(1).gameObject;
        // Panel3 = transform.GetChild(2).gameObject;
        // Panel4 = transform.GetChild(3).gameObject;


        // if (currentSelector == ManageGameState.selectedCharacter)
        // {
           // SelectText.GetComponent<TextMeshProUGUI>().text = "Selected";
        // }
        // else
        // {
           // SelectText.GetComponent<TextMeshProUGUI>().text = "Select";
        // }


    }

    public void PlayClick()
    {
        audioSource.Play();
        Panel1.GetComponent<Animator>().SetTrigger("GoLeft");
        Panel2.GetComponent<Animator>().SetTrigger("GoLeft");

    }

    public void ShopClick()
    {
        audioSource.Play();
        Panel1.GetComponent<Animator>().SetTrigger("GoLeft");
        // Panel3.GetComponent<Animator>().SetTrigger("GoUp");

    }

    public void GoBackClick()
    {
        audioSource.Play();
        Panel1.GetComponent<Animator>().SetTrigger("GoRight");
        Panel2.GetComponent<Animator>().SetTrigger("GoRight");
    }

    //for character panel
    public void GoBackClick2()
    {
        audioSource.Play();
        Panel1.GetComponent<Animator>().SetTrigger("GoRight");
        // Panel3.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void ExitClick()
    {
        audioSource.Play();
        Application.Quit();
    }

    public void NextLevelsClick(){
         audioSource.Play();
         Panel2.GetComponent<Animator>().SetTrigger("GoRight");
        //  Panel4.GetComponent<Animator>().SetTrigger("GoLeft");
    }
        public void GoBackClick3(){
         audioSource.Play();
        //  Panel4.GetComponent<Animator>().SetTrigger("GoRight");
         Panel2.GetComponent<Animator>().SetTrigger("GoLeft");
    }
    public void Level1Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level1"));
        // SceneManager.LoadScene("Level1");
    }
    public void Level2Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level2"));
        //SceneManager.LoadScene("Level2");
    }
    public void Level3Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level3"));
        //SceneManager.LoadScene("Level3");
    }

    public void Level4Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level4"));
        //SceneManager.LoadScene("Level3");
    }

    public void Level5Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level5"));
        //SceneManager.LoadScene("Level3");
    }

    public void Level6Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level6"));
        //SceneManager.LoadScene("Level3");
    }

    public void Level7Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level7"));
        //SceneManager.LoadScene("Level3");
    }
        public void Level8Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level8"));
        //SceneManager.LoadScene("Level3");
    }
    public void Level9Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level9"));
        //SceneManager.LoadScene("Level3");
    }
        public void Level10Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level10"));
        //SceneManager.LoadScene("Level3");
    }
        public void Level11Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level11"));
        //SceneManager.LoadScene("Level3");
    }
        public void Level12Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level12"));
        //SceneManager.LoadScene("Level3");
    }
        public void Level13Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level13"));
        //SceneManager.LoadScene("Level3");
    }
        public void Level14Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level14"));
        //SceneManager.LoadScene("Level3");
    }

            public void Level15Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level15"));
        //SceneManager.LoadScene("Level3");
    }
                public void Level16Click()
    {
        audioSource.Play();
        StartCoroutine(LoadLevel("Level16"));
        //SceneManager.LoadScene("Level3");
    }

    public IEnumerator LoadLevel(string levelname)
    {
        _animator.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelname);
    }

    // public void LeftCharacter()
    // {
    //     audioSource.Play();
    //     currentSelector--;
    //     if (currentSelector == -1)
    //         currentSelector = totalCharacters - 1;


    //     if (currentSelector == ManageGameState.selectedCharacter)
    //     {
    //         // SelectText.GetComponent<TextMeshProUGUI>().text = "Selected";
    //     }
    //     else
    //     {
    //         // SelectText.GetComponent<TextMeshProUGUI>().text = "Select";
    //     }

    //     characterChanged = true;
    // }
    // public void RightCharacter()
    // {
    //     audioSource.Play();
    //     currentSelector++;
    //     if (currentSelector == totalCharacters)
    //         currentSelector = 0;


    //     if (currentSelector == ManageGameState.selectedCharacter)
    //     {
    //         // SelectText.GetComponent<TextMeshProUGUI>().text = "Selected";
    //     }
    //     else
    //     {
    //         // SelectText.GetComponent<TextMeshProUGUI>().text = "Select";
    //     }

    //     characterChanged = true;
    // }

    // public void Select()
    // {
    //     audioSource.Play();
    //     ManageGameState.selectedCharacter = currentSelector;


    //     if (currentSelector == ManageGameState.selectedCharacter)
    //     {
    //         // SelectText.GetComponent<TextMeshProUGUI>().text = "Selected";
    //     }
    //     else
    //     {
    //         // SelectText.GetComponent<TextMeshProUGUI>().text = "Select";
    //     }
    // }

}
