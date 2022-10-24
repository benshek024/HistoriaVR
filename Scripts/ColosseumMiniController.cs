using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColosseumMiniController : MonoBehaviour
{
    [SerializeField] private float timeBetweenPress = 1f;
    private float timestamp;

    private Animator _anim;
    public GameObject coloMini;


    [Header("Colosseum Mini Rotation")]
    [SerializeField] private Vector3 _rot;
    [SerializeField] private float _rotSpeed;

    public bool pressed { get; set; }
    private bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed && !show && Time.time >= timestamp)
        {
            show = true;
            _anim.SetBool("Show", true);
            pressed = false;
            timestamp = Time.time + timeBetweenPress;
        }

        if (pressed && show && Time.time >= timestamp)
        {
            show = false;
            _anim.SetBool("Show", false);
            pressed = false;
            timestamp = Time.time + timeBetweenPress;
        }

        if (show)
        {
            StartRotation();
        }
        else
        {
            StopRotation();
        }

    }

    public void ShowColoMini()
    {
        coloMini.SetActive(true);
    }

    public void CloseColoMini()
    {
        coloMini.SetActive(false);
    }

    public void StartRotation()
    {
        coloMini.transform.Rotate(_rot * _rotSpeed * Time.deltaTime);
    }

    public void StopRotation()
    {
        coloMini.transform.Rotate(0, 0, 0);
    }

    public void ResetRotation()
    {
        coloMini.transform.rotation = Quaternion.identity;
    }
}
