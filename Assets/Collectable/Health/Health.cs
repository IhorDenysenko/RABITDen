using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Collectable {

    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addHealth(1);
        this.CollectedHide();
    }
}
