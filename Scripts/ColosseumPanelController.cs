using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColosseumPanelController : MonoBehaviour
{
    [SerializeField] private float timeBetweenPress = 1f;
    private float timestamp;

    private Animator _anim;
    public GameObject[] panels;
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

        // Set each panel inactive
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
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

    public void ShowPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true);
        }
    }

    public void ClosePanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
