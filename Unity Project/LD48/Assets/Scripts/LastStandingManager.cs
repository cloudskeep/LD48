using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandingManager : MonoBehaviour
{
    public GameObject lastPosition;

    void Start()
    {
        lastPosition = GameObject.Find("ContactPoint");
    }
    
    void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.GetContact(0);
        lastPosition.transform.position = contact.point + new Vector2(0.0f, 8.0f);
    }
}
