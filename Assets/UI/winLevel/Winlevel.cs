using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winlevel : MonoBehaviour
{

    private LevelStat stats=null;

    public LevelController levelthis;

    public GameObject red;
    public GameObject blue;
    public GameObject green;

    public UILabel coins;
    public UILabel fruits;


    public string NameOfLevel;


    public void initStats()
    {
        if(levelthis.leftGem.activeInHierarchy)
            blue.SetActive(true);

        if (levelthis.centerGem.activeInHierarchy)
            red.SetActive(true);

        if (levelthis.rightGem.activeInHierarchy)
           green.SetActive(true);

        coins.text = "+" + levelthis.coins;
        fruits.text = "" + levelthis.fruits + "/" + levelthis.allFruits;




        int coinsAll = PlayerPrefs.GetInt("coins", 0);

        coinsAll += levelthis.coins;

        PlayerPrefs.SetInt("coins", coinsAll);



        string str = PlayerPrefs.GetString(NameOfLevel, null);
        this.stats = JsonUtility.FromJson<LevelStat>(str);

        if (this.stats==null)
        {
            this.stats = new LevelStat();
        }

        stats.levelPassed = true;

        if (blue.activeInHierarchy && red.activeInHierarchy && green.activeInHierarchy)
            stats.hasCrystals = true;

        if (levelthis.fruits == levelthis.allFruits)
            stats.hasAllFruits = true;
        


        str = JsonUtility.ToJson(this.stats);
        PlayerPrefs.SetString(NameOfLevel, str);

    }
}
