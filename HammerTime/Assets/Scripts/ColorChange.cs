using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ColorChange : MonoBehaviour
{
    public Material matA;
    public Material matB;

    private MeshRenderer rendy;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        rendy = GetComponent<MeshRenderer>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.onHoverEnter.AddListener(SetMaterialA);
        grabInteractable.onHoverExit.AddListener(SetMaterialB);
    }

    private void OnDisable()
    {
        grabInteractable.onHoverEnter.RemoveListener(SetMaterialA);
        grabInteractable.onHoverExit.RemoveListener(SetMaterialB);
    }

    private void SetMaterialA(XRBaseInteractor interactor)
    {
        rendy.material = matA;
    }

    private void SetMaterialB(XRBaseInteractor interactor)
    {
        rendy.material = matB;
    }
}
