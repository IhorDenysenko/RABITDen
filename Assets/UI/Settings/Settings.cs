using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    public MyButton settingsButton;

    public GameObject settingsPopUp;
    void Start()
    {
        settingsButton.signalOnClick.AddListener(this.onPlay);
    }
    void onPlay()
    {
        settingsPopUp.SetActive(true);


    }
}
