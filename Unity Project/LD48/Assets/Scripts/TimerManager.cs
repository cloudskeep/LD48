using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timer;
    private Animator anim;
    private bool animPlaying;
    public static float penalty = 1;
    public static int timeAdd = 0;
    public static bool timeCounting = true;
    float minutes = 5;
    float seconds = 0;
    float milliseconds = 0;

    private void Awake()
    {
        anim = GameObject.Find("TimerText").GetComponent<Animator>();
        //anim.enabled = false;
    }

    void Update()
    {
        if (penalty > 1)
        {
            if (!animPlaying)
                StartCoroutine("GoodShake");

            if (seconds < penalty)
            {
                minutes--;
                seconds = 60 - penalty;
            }
            else
            {
                seconds -= penalty;
            }
            penalty = 1;
        }

        if (timeAdd > 0)
        {
            if (!animPlaying)
                StartCoroutine("ShakeAnimation");

            Camera.main.gameObject.GetComponent<CameraShake>().TriggerShake();

            if (seconds + timeAdd >= 60)
            {
                minutes++;
                seconds = seconds + timeAdd - 60;
            }
            else
            {
                seconds += timeAdd;
            }
            timeAdd = 0;
        }

        if (minutes < 0 || seconds == 0 && minutes == 0 && milliseconds >= 0)
        {
            timeCounting = false;
            minutes = 0;
            seconds = 0;
            milliseconds = 0;
        }

        if (milliseconds <= 0 && timeCounting)
        {
            if (seconds <= 0 || seconds < penalty)
            {
                minutes--;
                seconds = 60 - penalty;
            }
            else if (seconds >= 0)
            {
                seconds -= penalty;
            }

            milliseconds = 100;
        }

        if (timeCounting)
            milliseconds -= Time.deltaTime * 100;

        if (seconds >= 10 && milliseconds >= 10)
            timer.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)milliseconds);
        else if (seconds < 10 && milliseconds >= 10)
            timer.text = string.Format("{0}:0{1}:{2}", minutes, seconds, (int)milliseconds);
        else if (seconds >= 10 && milliseconds < 10)
            timer.text = string.Format("{0}:{1}:0{2}", minutes, seconds, (int)milliseconds);
        else if (seconds < 10 && milliseconds < 10)
            timer.text = string.Format("{0}:0{1}:0{2}", minutes, seconds, (int)milliseconds);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TimePenalty(10);
        }
    }

    void TimePenalty(int pen)
    {
        penalty += pen;
    }

    IEnumerator ShakeAnimation()
    {
        animPlaying = true;
        //anim.enabled = true;
        //anim.Play("TimerShake");
        anim.SetBool("damageTaken", true);
        yield return new WaitForSeconds(0.20f);
        anim.SetBool("damageTaken", false);
        //anim.enabled = false;
        animPlaying = false;
    }

    IEnumerator GoodShake()
    {
        animPlaying = true;
        //anim.enabled = true;
        //anim.Play("TimerShake");
        anim.SetBool("timeLowered", true);
        yield return new WaitForSeconds(0.40f);
        anim.SetBool("timeLowered", false);
        //anim.enabled = false;
        animPlaying = false;
    }
}