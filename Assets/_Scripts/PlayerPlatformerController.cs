using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Chronos;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    private Transform meshTransform;
    
    private float rotationAngle = 45f;
    private float rotationTime = 1;
    private float startAngle;

    private float rotationCyclePosition;
    private float newAngle;
       

    private bool running;

    private Transform body;
    private Transform rightLeg;
    private Transform leftLeg;

    private CyclePositiveNegative cpn;

    public float cycleTime = 0.5f;
    public float cycleAngle = 45f;

    public float tempCycleValue;

    // Use this for initialization
    void Start () {

        meshTransform = transform.GetChild(0);

        body = meshTransform.GetChild(0);
        rightLeg = meshTransform.GetChild(1);
        leftLeg = meshTransform.GetChild(2);

        startAngle = transform.rotation.z;

        cpn = new CyclePositiveNegative();

    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = 1;

        move.x = Input.GetAxis("Horizontal");
        
        //The -1 bug comes from the game pad axis

        //move.x = Input.GetAxis("Horizontal") + Input.GetAxis("HorizontalGamePad");
        //move.x = Input.GetAxis("HorizontalGamePad");

        if (Input.GetButtonDown("xbox button a") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

        }
        else if(Input.GetButtonUp("xbox button a"))
        {
            if(velocity.y > 0)
            {
                velocity.y = velocity.y * .5f;
            }
        }
              
        targetVelocity = move * maxSpeed;

        MeshRotation(move.x);

    }

    private void MeshRotation(float moveX)
    {

        float CycleValue = cpn.ReturnCyclingValue(cycleTime);

        tempCycleValue = CycleValue;

        Quaternion MeshAngles;
        
        if (moveX > 0.1)
        {
            //move right, rotate localrotation positive
            MeshAngles = Quaternion.Euler(0, -45, 0);

            LegRunningMovement(leftLeg, CycleValue);
            LegRunningMovement(rightLeg, CycleValue, - 1);

        }
        else if(moveX < -0.1)
        {
            //move left, rotate negative
            MeshAngles = Quaternion.Euler(0, 45, 0);

            LegRunningMovement(leftLeg, CycleValue);
            LegRunningMovement(rightLeg, CycleValue, - 1);
            
        }
        else
        {
            //not moving, set rotation to zero
            MeshAngles = Quaternion.Euler(0, 0, 0);
        }

        if(!grounded)
        {
            //arms up
            //eyes big? shocked face?
        }
        else
        {
            //arms donw;
        }

        //maybe add some logic here so that this is only updated if changed since last frame!!!!!!!!!!!!!

        meshTransform.localRotation = MeshAngles;

        //here, UpdatePosition for every limb after being manipulated in above if statements

    }

    // Update is called once per frame
    public void LegRunningMovement(Transform trans, float CycleValue, float multiplier = 1)
    {
                
        trans.localEulerAngles = new Vector3(cycleAngle * CycleValue * multiplier, trans.rotation.y, trans.rotation.z);

    }

}



public class Limb : Transform
{    
    public Vector3 standingPos;
    public Vector3 jumpingPos;
    public Vector3 runningLeftPos;
    public Vector3 runningRightPos;
    public Vector3 jumpingLeftPos;
    public Vector3 jumpingRightPos;
    
    public Limb()
    {

    }   

    public void UpdatePosition(Vector3 pos)
    {
        localPosition = pos;        
    }

}





