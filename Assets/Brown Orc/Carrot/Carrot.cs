using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {


    Vector3 speed=new Vector3(5,0,0);

    bool direct;

    SpriteRenderer sr;


    void Start()
    {

        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(destroyLater());
    }
    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(1.0f);
       

       Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (direct)
            this.transform.position += speed * Time.deltaTime;
        else
            this.transform.position -= speed * Time.deltaTime;
    }

    public void launch(float direction)
    {
        sr = GetComponent<SpriteRenderer>();
        if (direction==1)
        {
            direct = true;
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }



    protected override void OnRabitHit(HeroRabit rabit)
    {

        HeroRabit.deathFromOrc = true;

    }

}
