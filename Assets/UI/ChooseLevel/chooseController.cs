using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chooseController : MonoBehaviour {

    private LevelStat stats = null;
    private LevelStat stats1 = null;
    private string statcoins = null;

    public UILabel coins;

    public GameObject compl1;
    public GameObject compl2;
    public GameObject allFr1;
    public GameObject allFr2;
    public GameObject allGem1;
    public GameObject allGem2;

    public GameObject locked;

    public static bool isScecondOpen;


    // Use this for initialization
    void Start ()
    {

        int coin = PlayerPrefs.GetInt("coins", 0);

        if (coin < 10)
        {
            coins.text = "000" + coin;
        }
        if (coin < 100 && coin >= 10)
        {
            coins.text = "00" + coin;
        }
        if (coin < 1000 && coin >= 100)
        {
            coins.text = "0" + coin;
        }
        else if (coin >= 1000)
        {
            coins.text = "" + coin;
        }



        ////////////////////////////////////



        string str = PlayerPrefs.GetString("firstScene", null);
        this.stats = JsonUtility.FromJson<LevelStat>(str);

        if (this.stats == null)
        {
            this.stats = new LevelStat();
        }

        if (stats.levelPassed)
        {
            compl1.SetActive(true);
            locked.SetActive(false);
        }
       
        if(stats.hasAllFruits)
        {
            allFr1.SetActive(true);
        }
        if (stats.hasCrystals)
        {
            allGem1.SetActive(true);
        }



        string str1 = PlayerPrefs.GetString("secondScene", null);
        this.stats1 = JsonUtility.FromJson<LevelStat>(str1);

        if (this.stats1 == null)
        {
            this.stats1 = new LevelStat();
        }

        if (stats1.levelPassed)
        {
            compl2.SetActive(true);
            
        }

        if (stats1.hasAllFruits)
        {
            allFr2.SetActive(true);
        }
        if (stats1.hasCrystals)
        {
            allGem2.SetActive(true);
        }




    }
	
	
}
