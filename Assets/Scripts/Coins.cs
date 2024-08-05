using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    public GameObject GemText;
    // Start is called before the first frame update
    void Start()
    {
        GemText = transform.GetChild(1).gameObject;
        GemText.GetComponent<TextMeshProUGUI>().text = "" + ManageGameState.gemCount;
    }

}
