using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Whirl");
    }

    IEnumerator Whirl()
    {
        print("started whirl coroutine");
        yield return new WaitForSeconds(Random.Range(5, 21));
        print("whirl");
        anim.SetBool("startAttack", true);
        yield return new WaitForSeconds(1.11f);
        anim.SetBool("startAttack", false);
        StartCoroutine("Whirl");
    }
}
