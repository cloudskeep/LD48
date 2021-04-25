using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    private AudioSource music;
    public bool debugthing = false; // enabled in inspector, should be removed in final build

    private void Awake()
    {
        music = GetComponent<AudioSource>();
        StartCoroutine(nameof(AudioCheck));
    }

    private IEnumerator AudioCheck()
    {
        yield return new WaitForSeconds(0.05f); // this is to try and enhance this things performance, 0.05 of a second shouldnt be noticeable for volume changes

        music.volume = MenuManager.volume;

        if (debugthing) // enabled in inspector, should be removed in final build
        {
            Debug.Log(music.volume + " is audio volume");
            Debug.Log(MenuManager.volume + " is menumanager volume");
        }

        StartCoroutine(nameof(AudioCheck));
    }
}
