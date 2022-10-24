using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLogs : MonoBehaviour
{
    public bool isHit = false;
    private static int glaScore = 20;
    private static int scuScore = 10;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Gladius") && !isHit)
        {
            _audio.pitch = Random.Range(0.8f, 1.1f);
            _audio.volume = Random.Range(0.7f, 0.8f);
            _audio.Play();
            WoodSlasherController.instance.currentScore += glaScore;
            Debug.Log("WS: Hit Gladius");
            isHit = false;
        }

        if (other.gameObject.CompareTag("Scutum") && !isHit)
        {
            _audio.pitch = Random.Range(0.8f, 1.1f);
            _audio.volume = Random.Range(0.7f, 0.8f);
            _audio.Play();
            WoodSlasherController.instance.currentScore += scuScore;
            Debug.Log("WS: Hit Scutum");
            isHit = false;
        }

        if (other.gameObject.CompareTag("WSBound") && !isHit)
        {
            Debug.Log("WS: Hit Bound");
            isHit = false;
        }
    }
}
