using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePopUp : MonoBehaviour {

    public MyButton CloseButton;

    public GameObject PopUp; 

    void Start()
    {
        CloseButton.signalOnClick.AddListener(this.onPlay);
    }
    void onPlay()
    {

        PopUp.SetActive(false);
    }
}
