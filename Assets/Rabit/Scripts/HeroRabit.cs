using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {




    public AudioClip walkSound = null;
    AudioSource walkSource = null;
    public AudioClip landSound = null;
    AudioSource landSource = null;
    public AudioClip dieSound = null;
    AudioSource dieSource = null;





    public static HeroRabit lastRabit = null;

    bool isBig;
    bool dying;
    bool respawned;

    public static bool deathFromOrc;

    float TimeToRespawn;

    public float BetweenExplosionTime;
    float tempExplosionTime;
     public bool between;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    Transform heroParent = null;

    float jumpAfterOrcDeath;
    Vector3 jumpAOD=new Vector3(0,5,0);

    Rigidbody2D myBody = null;
    public float speed = 1f;

   public static bool AfterOrcDeathJump;

    void Awake()
    {
        lastRabit = this;
    }


    // Use this for initialization
    void Start ()
    {
        LevelController.current.setStartPosition(transform.position);
        myBody = this.GetComponent<Rigidbody2D>();

        this.heroParent = this.transform.parent;



        walkSource = gameObject.AddComponent<AudioSource>();
        walkSource.clip = walkSound;

        landSource = gameObject.AddComponent<AudioSource>();
        landSource.clip = landSound;

        dieSource = gameObject.AddComponent<AudioSource>();
        dieSource.clip = dieSound;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
	
        if(deathFromOrc)
        {
            deathFromOrc = false;
            respawned = true;
            StartCoroutine(DieFromOrc(1.7f));
           
        }


        if(AfterOrcDeathJump)
        {
            AfterOrcDeathJump = false;
            jumpAfterOrcDeath = 0.3f;
        }


        if (jumpAfterOrcDeath > 0)
        {
            jumpAfterOrcDeath -= Time.deltaTime;
            this.transform.position += jumpAOD * Time.deltaTime;
        }
	}

    IEnumerator DieFromOrc(float duration)
    {
        GetComponent<Animator>().SetBool("die",true);

        yield return new WaitForSeconds(duration);

        GetComponent<Animator>().SetBool("die", false);
       
        LevelController.current.onRabitDeath(this);
        respawned = false;
    }


    IEnumerator RestoreKeys(float duration)
    {
        

        yield return new WaitForSeconds(duration);
        deathFromOrc = false;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (!deathFromOrc&&!respawned)
        {

            if (tempExplosionTime > 0)
            {
                tempExplosionTime -= Time.deltaTime;

            }
            else if (between)
            {
                between = false;

                SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

                var color = sr.color;

                color.b = 255;
                color.r = 255;
                color.g = 255;

                sr.color = color;

            }


            if (!dying)
            {

                //[-1, 1]
                float value = Input.GetAxis("Horizontal");
                Animator animator = GetComponent<Animator>();


                if (Mathf.Abs(value) > 0)
                {

                    Vector2 vel = myBody.velocity;
                    vel.x = value * speed;
                    myBody.velocity = vel;
                }


                if (isGrounded)
                    if (Mathf.Abs(value) > 0)
                    {
                        animator.SetBool("run", true);

                        if (SoundManager.Instance.isSoundOn() && !walkSource.isPlaying)
                            walkSource.Play();

                    }
                    else
                    {
                        animator.SetBool("run", false);
                        walkSource.Stop();
                    }



                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (value < 0)
                {
                    sr.flipX = true;

                }
                else if (value > 0)
                {
                    sr.flipX = false;
                }



                Vector3 from = transform.position + Vector3.up * 0.3f;
                Vector3 to = transform.position + Vector3.down * 0.1f;
                int layer_id = 1 << LayerMask.NameToLayer("Ground");
                //Перевіряємо чи проходить лінія через Collider з шаром Ground
                RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
                if (hit)
                {

                    if(!isGrounded)
                    if (SoundManager.Instance.isSoundOn() && !landSource.isPlaying)
                        landSource.Play();


                    isGrounded = true;
                    if (hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
                    {
                        //Приліпаємо до платформи
                        SetNewParent(this.transform, hit.transform);
                    }


                }
                else
                {
                    isGrounded = false;
                    SetNewParent(this.transform, this.heroParent);
                }



                //Намалювати лінію (для розробника)
                Debug.DrawLine(from, to, Color.red);


                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    this.JumpActive = true;
                }
                if (this.JumpActive)
                {
                    //Якщо кнопку ще тримають
                    if (Input.GetButton("Jump"))
                    {
                        this.JumpTime += Time.deltaTime;
                        if (this.JumpTime < this.MaxJumpTime)
                        {
                            Vector2 vel = myBody.velocity;
                            vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                            myBody.velocity = vel;
                        }
                    }
                    else
                    {
                        this.JumpActive = false;
                        this.JumpTime = 0;
                    }
                }



                //Animator animator = GetComponent<Animator>();
                if (this.isGrounded)
                {
                    animator.SetBool("jump", false);
                }
                else
                {
                    animator.SetBool("jump", true);
                }
            }
            else if (TimeToRespawn > 0)
            {
                TimeToRespawn -= Time.deltaTime;
            }
            else if (TimeToRespawn <= 0)
            {
                GetComponent<Animator>().SetBool("die", false);
                dying = false;
                LevelController.current.onRabitDeath(this);
            }
        }
    }


    public void GetBig()
    {

        if(!isBig)
        {
            this.transform.localScale *= 2;

        }
        isBig = true;
    }




    public void Explode()
    {
        if(isBig)
        {
            this.transform.localScale *= 0.5f;
            tempExplosionTime = BetweenExplosionTime;
            isBig = false;
            between = true;

            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

            var color = sr.color;

            color.b = 0;
            color.r = 255;
            color.g = 0;

            sr.color = color;


        }
        else if(tempExplosionTime<=0)
        {
            GetComponent<Animator>().SetBool("die", true);

            if (SoundManager.Instance.isSoundOn() )
                dieSource.Play();

            dying = true;
            isGrounded = true;

            AnimatorStateInfo asi= GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

            TimeToRespawn = asi.length;
        }

    }




    static void SetNewParent(Transform obj, Transform new_parent) {
if(obj.transform.parent != new_parent) {
//Засікаємо позицію у Глобальних координатах
Vector3 pos = obj.transform.position;
//Встановлюємо нового батька
obj.transform.parent = new_parent;
//Після зміни батька координати кролика зміняться
//Оскільки вони тепер відносно іншого об’єкта
//повертаємо кролика в ті самі глобальні координати
obj.transform.position = pos;
}
}

}
