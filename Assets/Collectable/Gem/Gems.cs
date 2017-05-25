using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
       // Level.current.addGems(1);
        this.CollectedHide();
    }
}
