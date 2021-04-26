using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject player;
    public Vector2 targetLeft;
    public Vector2 targetRight;
    public float speed = 10.0f;
    public GameObject leftFist;
    public GameObject rightFist;
    private bool leftMoving;
    private bool rightMoving;
    protected bool cooledDown;
    protected bool cooledDownRight;
    public Vector2 originalPosLeft;
    public Vector2 originalPosRight;
    private bool CancelReturnLeft;
    private bool CancelReturnRight;
    private bool leftReturning;
    private bool rightReturning;

    private void Awake()
    {
        originalPosLeft = leftFist.transform.position;
        originalPosRight = rightFist.transform.position;
        StartCoroutine("RandomActions");
    }

    public void UpdateOriginalPos(int fistNum, Vector2 newPos)
    {
        if (fistNum == 1)
            originalPosLeft = newPos;
        else if (fistNum == 2)
            originalPosRight = newPos;
    }
    void PunchAttack(int fistNum) // 1 is left 2 is right
    {
        float step = speed * Time.deltaTime;
        print("check");
        if (Vector2.Distance(transform.position, player.transform.position) > 0.01)
        {
            print("passed distance");
            if (fistNum == 1)
            {
                targetLeft = player.transform.position;
                leftMoving = true;
                print("movetowards");
                //leftFist.transform.position = Vector2.MoveTowards(leftFist.transform.position, target, step);
            }
            else
            {
                targetRight = player.transform.position;
                rightMoving = true;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PunchAttack(1);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            PunchAttack(2);
        }

        if (leftMoving && Vector2.Distance(leftFist.transform.position, targetLeft) > speed * Time.deltaTime)
        {
            //CancelReturnLeft = true;
            print("punching");
            float step = speed * Time.deltaTime;
            leftFist.transform.position = Vector2.MoveTowards(leftFist.transform.position, targetLeft, step);
        }
        else if (leftMoving)
        {
            //CancelReturnLeft = false;
            print("returning");
            StartCoroutine("LeftFistReturn");
        }

        if (rightMoving && Vector2.Distance(rightFist.transform.position, targetRight) > speed * Time.deltaTime)
        {
            //CancelReturnRight = true;
            print("punching");
            float step = speed * Time.deltaTime;
            rightFist.transform.position = Vector2.MoveTowards(rightFist.transform.position, targetRight, step);
        }
        else if (rightMoving)
        {
           // CancelReturnRight = false;
            print("returning right");
            StartCoroutine("RightFistReturn");
        }
    }

    IEnumerator LeftFistReturn()
    {
        if (!CancelReturnLeft)
        {
            if (!cooledDown)
            {
                leftReturning = true;
                cooledDown = true;
                leftMoving = false;
                yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));
            }

            float step = speed * Time.deltaTime;
            leftFist.transform.position = Vector2.MoveTowards(leftFist.transform.position, originalPosLeft, step);

            if (Vector2.Distance(leftFist.transform.position, originalPosLeft) > speed * Time.deltaTime)
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine("LeftFistReturn");
            }
            else
            {
                leftReturning = false;
                cooledDown = false;
            }
        }
    }

    IEnumerator RightFistReturn()
    {
        if (!CancelReturnRight)
        {
            if (!cooledDownRight)
            {
                rightReturning = true;
                cooledDownRight = true;
                rightMoving = false;
                yield return new WaitForSeconds(Random.Range(0.5f, 1.2f));
            }

            float step = speed * Time.deltaTime;
            rightFist.transform.position = Vector2.MoveTowards(rightFist.transform.position, originalPosRight, step);

            if (Vector2.Distance(rightFist.transform.position, originalPosRight) > speed * Time.deltaTime)
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine("RightFistReturn");
            }
            else
            {
                rightReturning = false;
                cooledDownRight = false;
            }
        }
    }

    IEnumerator RandomActions()
    {
        int attackToLaunch;
        attackToLaunch = Random.Range(1, 4);

        switch (attackToLaunch)
        {
            case 1:
                if (!leftMoving && !leftReturning)
                {
                    PunchAttack(1);
                }
                
                break;
            case 2:
                if (!rightMoving && !rightReturning)
                {
                    PunchAttack(2);
                }
                break;
            case 3:
                if (!CharacterController.rainActive)
                {
                    player.GetComponent<CharacterController>().StartCoroutine("Rain");
                }
                break;
            case 4:
                print("do stuff");
                break;
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        StartCoroutine("RandomActions");
    }
}