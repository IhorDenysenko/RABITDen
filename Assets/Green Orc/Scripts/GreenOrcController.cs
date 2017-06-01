using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrcController : MonoBehaviour {


    Vector3 rabit_pos; 

    Vector3 pointA;
    public  Vector3 pointB;

    public Vector3 speed;

    private bool walk;
    private bool run;
    private bool death;

    SpriteRenderer sr;


    bool collidedWithRabit;

    Mode mode;

    void Start ()
    {

        sr = GetComponent<SpriteRenderer>();
        
        mode = Mode.GoToB;

        walk = true;
        GetComponent<Animator>().SetBool("walk",true);

        pointA = this.transform.position;
	}



	void FixedUpdate ()
    {
        rabit_pos = HeroRabit.lastRabit.transform.position;
        float value = this.getDirection();


        

        if (!collidedWithRabit&&!death)
        {
            if (value == 1)
            {
                sr.flipX = true;
            }
            else if (value == -1)
            {
                sr.flipX = false;
            }


            if (mode == Mode.GoToA)
            {
                if (isArrived(pointA))
                {
                    mode = Mode.GoToB;

                }

                if (walk)
                    this.transform.position -= speed * Time.deltaTime;


            }
            else if (mode == Mode.GoToB)
            {
                if (isArrived(pointB))
                {
                    mode = Mode.GoToA;
                }

                if (walk)
                    this.transform.position += speed * Time.deltaTime;

            }

            if (mode == Mode.Attack)
            {

                if ((rabit_pos.x > transform.position.x) && run)
                {
                    sr.flipX = true;
                    this.transform.position += speed * 2 * Time.deltaTime;


                }
                else if (run)
                {
                    sr.flipX = false;
                    this.transform.position -= speed * 2 * Time.deltaTime;

                }


            }


            if (rabit_pos.x > pointA.x && rabit_pos.x < pointB.x)
            {
                mode = Mode.Attack;
                if (!GetComponent<Animator>().GetBool("run"))
                {
                    GetComponent<Animator>().SetBool("run", true);
                    walk = false;
                    run = true;
                }

            }
            else
            {
                // mode = Mode.GoToA;
                if (GetComponent<Animator>().GetBool("run"))
                {
                    GetComponent<Animator>().SetBool("run", false);
                    walk = true;
                    run = false;
                    mode = Mode.GoToA;
                }

            }




        }
        else
        {
            if(rabit_pos.y-transform.position.y>1.4f)
            {
                HeroRabit.AfterOrcDeathJump = true;
                GetComponent<Animator>().SetBool("die",true);
                death = true;
                collidedWithRabit = false;
                StartCoroutine(DieOrc(1f));
            }
            else
            {
                if(!GetComponent<Animator>().GetBool("die"))
                {
                    collidedWithRabit = false;
                    GetComponent<Animator>().SetTrigger("attack");
                    HeroRabit.deathFromOrc = true;
                }
               
            }

        }
    }


    IEnumerator DieOrc(float duration)
    {
        
        yield return new WaitForSeconds(duration);

        Destroy(this.gameObject);
    }



    float getDirection()
    {
        if (mode==Mode.GoToA)
        {
            return -1; //Move left
        }
        else if (mode == Mode.GoToB)
        {
            return 1; //Move right
        }
        return 0; //No movement


    }



    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
        
    }



    bool isArrived( Vector3 target)
    {
    
        target.z = 0;
        return Vector3.Distance(this.transform.position, target) < 0.2f;
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.GetComponent<HeroRabit>())
        {

            collidedWithRabit = true;
        }

    }


}
