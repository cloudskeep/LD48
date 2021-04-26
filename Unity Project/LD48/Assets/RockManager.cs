using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{

    public bool isRock = false;

    private void Awake()
    {
        if (isRock)
            Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossHead"))
        {
            TimerManager.penalty = 5;
        }
        else if (collision.gameObject.CompareTag("BossFist"))
        {
            TimerManager.penalty = 3;
        }
    }
}
