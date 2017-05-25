using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{
    protected override void OnRabitHit(HeroRabit rabit)
    {
        // Level.current.addMushrooms(1);
        rabit.GetBig();
        this.CollectedHide();
    }
}
