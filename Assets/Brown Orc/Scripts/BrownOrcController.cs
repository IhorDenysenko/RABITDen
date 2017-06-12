using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrcController : MonoBehaviour {

    public AudioClip attackSound = null;
    AudioSource attackSource = null;


    Vector3 rabit_pos;

    Vector3 pointA;
    public Vector3 pointB;

    public Vector3 speed;

    private bool walk;
    private bool attack;
    private bool death;

    SpriteRenderer sr;


    bool collidedWithRabit;

    Mode mode;

    //all for weapon

    public GameObject prefabCarrot;

    float last_carrot = 0;


    void Start()
    {

        attackSource = gameObject.AddComponent<AudioSource>();
        attackSource.clip = attackSound;

        sr = GetComponent<SpriteRenderer>();

        mode = Mode.GoToB;

        walk = true;
        GetComponent<Animator>().SetBool("walk", true);

        pointA = this.transform.position;
    }


    void launchCarrot(float directi)
    {  
            //Створюємо копію Prefab
            GameObject obj = Instantiate(this.prefabCarrot) as GameObject;
            //Розміщуємо в просторі
            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+0.6f,0);
            //Запускаємо в рух
            obj.GetComponent<Carrot>().launch(directi);
            
    }


    void FixedUpdate()
    {
        rabit_pos = HeroRabit.lastRabit.transform.position;
        float value = this.getDirection();


        if (!collidedWithRabit && !death)
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

                if ((rabit_pos.x > transform.position.x) && attack)
                {
                    sr.flipX = true;
                    
                }
                else if (attack)
                {
                    sr.flipX = false;
                    
                }


                GetComponent<Animator>().SetBool("attack", false);

                //check launch time
                if (Time.time - last_carrot > 1.0f)
                {
                    GetComponent<Animator>().SetBool("attack", true);

                    if (SoundManager.Instance.isSoundOn()&&!attackSource.isPlaying)
                        attackSource.Play();

                    last_carrot = Time.time;

                    if (sr.flipX == false)
                    {
                       
                        launchCarrot(-1);
                    }
                    else
                    {
                        
                        launchCarrot(1);
                    }

                   
                }


            }


            if (rabit_pos.x > pointA.x && rabit_pos.x < pointB.x)
            {
                mode = Mode.Attack;
                if (!GetComponent<Animator>().GetBool("attack"))
                {
                    GetComponent<Animator>().SetBool("attack", true);
                    walk = false;
                    attack = true;
                }

            }
            else
            {
                // mode = Mode.GoToA;
                if (GetComponent<Animator>().GetBool("attack"))
                {
                    GetComponent<Animator>().SetBool("attack", false);
                    walk = true;
                    attack = false;
                    mode = Mode.GoToA;
                }

            }




        }
        else
        {
            if (rabit_pos.y - transform.position.y > 1.4f)
            {
                HeroRabit.AfterOrcDeathJump = true;
               
                death = true;
                GetComponent<Animator>().SetBool("die", true);
                collidedWithRabit = false;
                StartCoroutine(DieOrc(0.8f));
            }
            else
            {
                collidedWithRabit = false;
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
        if (mode == Mode.GoToA)
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



    bool isArrived(Vector3 target)
    {

        target.z = 0;
        return Vector3.Distance(this.transform.position, target) < 0.2f;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<HeroRabit>())
        {

            collidedWithRabit = true;
        }

    }
}
