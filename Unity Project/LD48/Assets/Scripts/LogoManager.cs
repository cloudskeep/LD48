using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    //todo: will add our own symbol when its made, for now using a placeholder
    public float fadeSpeed = 1f;
    public bool fadeIn = true;
    public SpriteRenderer text;

    void Awake()
    {
        StartCoroutine(FadeTo(0.0f, 1.0f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(aValue, alpha, t));
            transform.GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}