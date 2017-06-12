using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    public MyButton soundButton;

    public GameObject soundOnButton;

    void Start()
    {
        soundButton.signalOnClick.AddListener(this.onPlay);

        if (SoundManager.Instance.isSoundOn())
            soundOnButton.SetActive(true);
        else
            soundOnButton.SetActive(false);

    }
    void onPlay()
    {
        if(!soundOnButton.activeInHierarchy)
        {
            soundOnButton.SetActive(true);
            SoundManager.Instance.setSoundOn(true);
        }
        else
        {
            soundOnButton.SetActive(false);
            SoundManager.Instance.setSoundOn(false);
        }
            


    }
}
