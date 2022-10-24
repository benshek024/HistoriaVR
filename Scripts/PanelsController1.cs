using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsController1 : MonoBehaviour
{
    [SerializeField] private float timeBetweenPress = 1f;
    private float timestamp;

    private Animator _anim;
    public bool pressed { get; set; }
    private bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        // Try getting the animator
        try
        {
            _anim = this.GetComponent<Animator>();
        }
        catch
        {
            if (_anim == null)
            {
                Debug.LogError("ERROR: Animator not found!");
            }

            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Shows panels if player pressed the button and panels are currently not showing
        if (pressed && !show && Time.time >= timestamp)
        {
            show = true;
            _anim.SetBool("Show", true);
            pressed = false;
            timestamp = Time.time + timeBetweenPress;
        }

        // Same as above but does the oppsite things
        if (pressed && show && Time.time >= timestamp)
        {
            show = false;
            _anim.SetBool("Show", false);
            pressed = false;
            timestamp = Time.time + timeBetweenPress;
        }
    }
}
