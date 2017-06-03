using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : Collectable
{

    //1 blue, 2 green, 3 red

    public int color;

    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addGems(color);
        this.CollectedHide();
    }
}
