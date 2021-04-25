using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("trigger entered");
        if (other.CompareTag("DamageTrigger"))
        {
            TimerManager.penalty = 10;
        }
        if (other.CompareTag("LeftPunch"))
        {
            print("left punch");
            GameObject.Find("leftFist").GetComponent<Animator>().SetBool("triggerPunch", true);
        }
    }
}
