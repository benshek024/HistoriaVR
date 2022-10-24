using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class TeleportationController : MonoBehaviour
{
    public GameObject distGrabController;
    public GameObject teleportController;

    public InputActionReference teleportActivationRef;

    public UnityEvent onTeleportActive;
    public UnityEvent onTeleportCancel;

    // Start is called before the first frame update
    void Start()
    {
        if (!distGrabController || !teleportController)
        {
            Debug.Log("ERROR: Distance Grab Controller or Teleport Controller not assigned!");
            return;
        }
        else if (!teleportActivationRef)
        {
            Debug.Log("ERROR: Teleportation input action reference not assigned!");
            return;
        }
        else
        {
            Debug.Log("Teleportation and distance grab controller is good to go!");
            teleportActivationRef.action.performed += TeleportModeActivate;
            teleportActivationRef.action.canceled += TeleportModeCancel;
        }
    }

    void TeleportModeActivate(InputAction.CallbackContext obj)
    {
        onTeleportActive.Invoke();
    }

    void TeleportModeCancel(InputAction.CallbackContext obj)
    {
        Invoke("StartTeleport", .1f);
    }

    void StartTeleport()
    {
        onTeleportCancel.Invoke();
    }
}
