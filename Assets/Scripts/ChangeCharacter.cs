using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{

    // public GameObject Character1;
    // public GameObject Character2;
    // public GameObject Character3;

    public GameObject Canvas;

    HandleClicks Clickscript;

    void Start()
    {
        // Character1 = transform.GetChild(0).gameObject;
        // Character2 = transform.GetChild(1).gameObject;
        // Character3 = transform.GetChild(2).gameObject;

        Canvas = GameObject.FindWithTag("Canvas");
        Clickscript = Canvas.GetComponent<HandleClicks>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Clickscript.characterChanged == true)
        {
            for (int i = 0; i < Clickscript.totalCharacters; i++)
            {
                if (i == Clickscript.currentSelector)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            Clickscript.characterChanged = false;
        }
    }
}
