using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndDoor : MonoBehaviour
{

    public GameObject winPopUp;
    public GameObject rabit;

    public AudioClip winSound = null;
    AudioSource winSource = null;


    void Start()
    {
        winSource = gameObject.AddComponent<AudioSource>();
        winSource.clip = winSound;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        winPopUp.SetActive(true);
        rabit.SetActive(false);
        winPopUp.GetComponent<Winlevel>().initStats();

        if (SoundManager.Instance.isSoundOn())
            winSource.Play();
    }
}
