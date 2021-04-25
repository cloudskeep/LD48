using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float _speed = 2f;
    public float JumpForce = 1f;

    private Rigidbody2D _rigidbody;

    private bool isPaused = false;
    private bool cooldownActivated = false;
    public GameObject pauseMenu;

    private GameObject contactPoint;
    public GameObject wildRock;

    private Animator boss;

    public Vector3 restartPoint; // this is for debugging purposes and should be removed in the final build

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        boss = GameObject.Find("Boss Test").GetComponent<Animator>();

        contactPoint = GameObject.Find("ContactPoint");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger entered");
        if (other.CompareTag("DamageTrigger"))
        {
            TimerManager.timeAdd = 10;
        }
        
        if (other.CompareTag("LeftPunch"))
        {
            print("left");
            StartCoroutine("LeftPunch");
        }
        
        if (other.CompareTag("RestartTrigger"))
        {
            StartCoroutine("LastSpot");
        }
        
        if (other.CompareTag("RainTrigger"))
        {
            print("rain");
            other.tag = "Untagged";
            StartCoroutine("Rain");
        }
    }

    IEnumerator LeftPunch()
    {
        boss.SetBool("leftPunch", true);
        yield return new WaitForSeconds(1.0f);
        boss.SetBool("leftPunch", false);
    }

    void Update()
    {
        var _move = Input.GetAxisRaw("Horizontal");
        transform.position = transform.position + new Vector3(_move * _speed * Time.deltaTime, 0, 0);

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.R)) // this is for debugging purposes and should be removed in the final build
        {
            StartCoroutine("Restart");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine("PauseGame");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            boss.enabled = true;
            StartCoroutine("Rain");
        }
    }

    IEnumerator Restart() // this is for debugging purposes and should be removed in the final build
    {
        _rigidbody.isKinematic = true; // kinematic is set to true for 0.01 of a second so you lose all momentum when restarting
        transform.position = restartPoint;
        yield return new WaitForSeconds(0.01f);
        _rigidbody.isKinematic = false;
    }

    IEnumerator PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            yield return null;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            yield return null;

            if (!cooldownActivated) // this implements a slight pause cooldown so you cant spam the button as a strategy
            {
                cooldownActivated = true;
                yield return new WaitForSeconds(0.25f);
                isPaused = false;
                cooldownActivated = false;
            }
        }
    }
    public IEnumerator LastSpot()
    {
        transform.position = contactPoint.transform.position;
        _rigidbody.isKinematic = true;
        yield return new WaitForSeconds(0.2f);
        _rigidbody.isKinematic = false;

        TimerManager.penalty += 30;
    }

    IEnumerator Rain()
    {
        GameObject latestRock;
        int rocksToThrow;
        float rockCooldown;
        boss.SetBool("rainBegin", true);
        rocksToThrow = Random.Range(6, 14);
        rockCooldown = Random.Range(0.5f, 1);
    
        while (rocksToThrow > 0)
        {
            latestRock = Instantiate(wildRock, null, true);
            latestRock.transform.position = transform.position + new Vector3(0.0f, 10.0f, 0.0f);
            rocksToThrow -= 1;
            yield return new WaitForSeconds(rockCooldown);
        }
        boss.SetBool("rainBegin", false);
        boss.Play("Neutral");
        yield return new WaitForSeconds(1f);
        boss.enabled = false;
    }
}

