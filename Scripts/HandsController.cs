using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ActionBasedController))]
public class HandsController : MonoBehaviour
{
    [SerializeField] InputActionReference gripActionRef;
    [SerializeField] InputActionReference triggerActionRef;

    ActionBasedController controller;

    public Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        //hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        //hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
