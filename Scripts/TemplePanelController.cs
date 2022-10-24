using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplePanelController : MonoBehaviour
{
    [SerializeField] private float timeBetweenPress = 1f;
    private float timestamp;

    private Animator anim;
    public GameObject[] panels;
    public bool pressed { get; set; }
    private bool show = false;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            anim = this.GetComponent<Animator>();
        }
        catch
        {
            if (anim == null)
            {
                Debug.LogError("ERROR: Animator not found!");
            }

            return;
        }

        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed && !show && Time.time >= timestamp)
        {
            show = true;
            anim.SetBool("ShowPanel", true);
            pressed = false;
            timestamp = Time.time + timeBetweenPress;
        }
        if (pressed && show && Time.time >= timestamp)
        {
            show = false;
            anim.SetBool("ShowPanel", false);
            pressed = false;
            timestamp = Time.time + timeBetweenPress;
        }
    }

    public void ShowPanels()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(true);
        }
    }
    
    public void ClosePanels()
    {
        foreach(GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
