using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TMP_Text))]

public class Toast : MonoBehaviour
{
    public float duration = 5.0f;
    public float fadeTime = 1.0f;

    private TMPro.TMP_Text _text;

	// Use this for initialization
	void Start ()
    {
        _text = GetComponent<TMPro.TMP_Text>();

        StartCoroutine(DoTheThing());
	}

    private IEnumerator DoTheThing()
    {
        float timeToLive = duration;
        while (timeToLive > 0)
        {
            timeToLive -= Time.deltaTime;
            yield return null;
        }

        _text.CrossFadeAlpha(0.0f, fadeTime, false);
        Destroy(gameObject, fadeTime);
    }
}
