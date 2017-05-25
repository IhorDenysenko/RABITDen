using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;


    public Vector3 Speed;


    public float Pause;
    private float tempPause;


    private bool going_to_a=true;

    // Use this for initialization
    void Start ()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
       
    }
	
	
    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.2f;
    }

   


    void Update()
    {

        if (tempPause > 0)
            tempPause -= Time.deltaTime;
        if (tempPause <= 0)
        {


            Vector3 my_pos = this.transform.position;
            Vector3 target;

            if (!going_to_a)
            {
                target = this.pointA;
            }
            else
            {
                target = this.pointB;
            }
            Vector3 destination = target - my_pos;
            destination.z = 0;

            if (isArrived(my_pos, target))
            {
                going_to_a = !going_to_a;
                tempPause = Pause;
            }
            else if (going_to_a)
                this.transform.position += Speed * Time.deltaTime;
            else
                this.transform.position -= Speed * Time.deltaTime;


        }


    }


}
