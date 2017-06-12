using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour {

    public MyButton ReplayButton;

    public string NameLevel;

    void Start()
    {
        ReplayButton.signalOnClick.AddListener(this.onPlay);
    }
    void onPlay()
    {
        SceneManager.LoadScene(NameLevel);

    }
}
