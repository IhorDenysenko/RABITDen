using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public MyButton musicButton;

    public GameObject musicOnButton;

    public LevelController contr;

    void Start()
    {
        musicButton.signalOnClick.AddListener(this.onPlay);

        if (MusicManager.Instance.isMusicOn())
            musicOnButton.SetActive(true);
        else
            musicOnButton.SetActive(false);
    }
    void onPlay()
    {

        if (!musicOnButton.activeInHierarchy)
        {
            musicOnButton.SetActive(true);
            contr.MusicON();
        }         
        else
        {
            musicOnButton.SetActive(false);
            contr.MusicOFF();
        }
           


    }
}
