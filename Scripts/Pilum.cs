using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pilum : MonoBehaviour
{
    // public GameObject pilumGO;
    [SerializeField] private Collider[] _colliders;
    [SerializeField] private Rigidbody _rb;
    private bool isAllowHit = true;
    private AudioSource _audio;

    private void Awake()
    {
        _audio = this.GetComponent<AudioSource>();
    }

    // When collide with a bullseye, reduce the number of pilums by 1 and
    // check its tag to give a corresponding score, update score text afterward.
    // Set the isAllowHit false to prevent consecutive scoring.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HighTarget") && isAllowHit)
        {
            _audio.pitch = Random.Range(0.8f, 1.1f);
            _audio.volume = Random.Range(0.7f, 0.8f);
            _audio.Play();
            Debug.Log("hit High Target");
            StartCoroutine("OnHit");
            PilumThrowerController.instance.currentScore += PilumThrowerController.highTargetScore;
            PilumThrowerController.instance.scoreText.text = PilumThrowerController.instance.currentScore.ToString();
            PilumThrowerController.instance.remainingPilums--;
            isAllowHit = false;
        }
        if (other.gameObject.CompareTag("MidTarget") && isAllowHit)
        {
            _audio.pitch = Random.Range(0.8f, 1.1f);
            _audio.volume = Random.Range(0.7f, 0.8f);
            _audio.Play();
            Debug.Log("hit Mid Target");
            StartCoroutine("OnHit");
            PilumThrowerController.instance.currentScore += PilumThrowerController.midTargetScore;
            PilumThrowerController.instance.scoreText.text = PilumThrowerController.instance.currentScore.ToString();
            PilumThrowerController.instance.remainingPilums--;
            isAllowHit = false;
        }
        if (other.gameObject.CompareTag("LowTarget") && isAllowHit)
        {
            _audio.pitch = Random.Range(0.9f, 1.1f);
            _audio.volume = Random.Range(0.5f, 0.6f);
            _audio.Play();
            Debug.Log("hit Low Target");
            StartCoroutine("OnHit");
            PilumThrowerController.instance.currentScore += PilumThrowerController.lowTargetScore;
            PilumThrowerController.instance.scoreText.text = PilumThrowerController.instance.currentScore.ToString();
            PilumThrowerController.instance.remainingPilums--;
            isAllowHit = false;
        }

        if (other.gameObject.CompareTag("PilumGround") && isAllowHit)
        {
            Debug.Log("hit Ground");
            this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            PilumThrowerController.instance.remainingPilums--;
            isAllowHit = false;
        }
    }

    // When it is called, set all of the non-trigger colliders in pilum to trigger to let it "penetrate" the bullseye.
    // And disable XR Grab Interactable script to prevent it being picked up by player again.
    // After 2 seconds, freeze the position and rotation of the pilum to make it stay on the bullseye.
    IEnumerator OnHit()
    {
        Debug.Log("HIT");
        //_rb.isKinematic = true;
        //collider.enabled = false;
        //b_collider.isTrigger = true;
        foreach (Collider col in _colliders)
        {
            col.isTrigger = true;
        }
        this.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        yield return new WaitForSeconds(.2f);
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.constraints = RigidbodyConstraints.FreezePosition;
        _rb.isKinematic = true;
        //collider.enabled = true;
    }
}
