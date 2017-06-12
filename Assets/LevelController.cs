using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {


    public AudioClip music = null;
    AudioSource musicSource = null;

    AudioSource loseSource = null;
    public AudioClip loseSound = null;


    public GameObject losePop;

    public int coins=0;

    public int fruits;
    public int allFruits;

     int lifes=3;

    public UILabel coinLabel;
    public UILabel fruitLabel;

    public GameObject leftHeart;
    public GameObject centerHeart;
    public GameObject rightHeart;

    public GameObject leftGem;
    public GameObject centerGem;
    public GameObject rightGem;


    public static LevelController current;
    void Awake()
    {
        current = this;

        string s = fruits + "/" + allFruits;

        fruitLabel.text = s;
    }


    void Start()
    {
        
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.clip = music;
        musicSource.loop = true;

        if(MusicManager.Instance.isMusicOn())
        musicSource.Play();

        loseSource = gameObject.AddComponent<AudioSource>();
        loseSource.clip = loseSound;

    }
    


    public void MusicON()
    {
        MusicManager.Instance.setMusicOn(true);
        musicSource.Play();
    }

    public void MusicOFF()
    {
        MusicManager.Instance.setMusicOn(false);
        musicSource.Stop();
    }


    Vector3 startingPosition;
    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }
    public void onRabitDeath(HeroRabit rabit)
    {
        lifes--;
        if (lifes == 2)
            rightHeart.SetActive(false);

        if(lifes==1)
           centerHeart.SetActive(false);

        if (lifes == 0)
        {
            leftHeart.SetActive(false);
            
            losePop.SetActive(true);
            rabit.gameObject.SetActive(false);

            if (SoundManager.Instance.isSoundOn())
                loseSource.Play();

                    }
            
        if(lifes != 0)
        rabit.transform.position = this.startingPosition;
        
    }



   public void addCoins(int coin)
    {
        coins += coin;

        string coinLab = "";

        if(coins<10)
        {
            coinLab = "000" + coins;
        }
        if(coins<100&& coins >= 10)
        {
            coinLab = "00" + coins;
        }
        if (coins < 1000 && coins >= 100)
        {
            coinLab = "0" + coins;
        }
        else if(coins >= 1000)
        {
            coinLab =""+ coins;
        }

        coinLabel.text = coinLab;

    }


    public void addGems(int color)
    {
        if(color==1)
        {
            leftGem.SetActive(true);
        }
        if (color == 2)
        {
            rightGem.SetActive(true);
        }
        if (color == 3)
        {
            centerGem.SetActive(true);
        }
    }


   public void addFruits(int num)
    {
        fruits += num;

        string s = fruits + "/" + allFruits;

        fruitLabel.text = s;

    }

}
