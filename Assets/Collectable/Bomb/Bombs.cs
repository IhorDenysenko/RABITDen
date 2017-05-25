using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Collectable
{

    protected override void OnRabitHit(HeroRabit rabit)
    {
        // Level.current.addMushrooms(1);
        if (!rabit.between)
            this.CollectedHide();
        rabit.Explode();
       
    }
}
