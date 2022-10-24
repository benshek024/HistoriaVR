using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabOffset : XRGrabInteractable
{
    private Vector3 interactorPos = Vector3.zero;
    private Quaternion interactorRot = Quaternion.identity;

    IXRInteractable interactable;

    private void Start()
    {
        interactable = GetComponent<IXRInteractable>();
    }

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        StoreInteractor(args);
        MatchAttachmentPoint(args);
    }

    private void StoreInteractor(SelectEnterEventArgs args)
    {
        interactorPos = args.interactorObject.GetAttachTransform(interactable).position;
        interactorRot = args.interactorObject.GetAttachTransform(interactable).rotation;
    }

    private void MatchAttachmentPoint(SelectEnterEventArgs args)
    {
        bool hasAttach = attachTransform != null;
        args.interactorObject.GetAttachTransform(interactable).position =
            hasAttach ? attachTransform.position : transform.position;
        args.interactorObject.GetAttachTransform(interactable).rotation =
            hasAttach ? attachTransform.rotation : transform.rotation;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        ResetAttachmentPoint(args);
        ClearInteractor(args);
    }

    private void ResetAttachmentPoint(SelectExitEventArgs args)
    {
        args.interactorObject.GetAttachTransform(interactable).localPosition = interactorPos;
        args.interactorObject.GetAttachTransform(interactable).localRotation = interactorRot;
    }

    private void ClearInteractor(SelectExitEventArgs args)
    {
        interactorPos = Vector3.zero;
        interactorRot = Quaternion.identity;
    }
}
