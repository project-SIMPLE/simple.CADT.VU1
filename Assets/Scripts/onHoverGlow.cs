using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class OnHoverGlow : MonoBehaviour
{
    public Renderer targetRenderer;

    public Material normalMat;
    public Material outlineMat;

    private XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();

        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        SetAllMaterials(outlineMat);
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        SetAllMaterials(normalMat);
    }

    void SetAllMaterials(Material mat)
    {
        Material[] mats = targetRenderer.materials;

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = mat;
        }

        targetRenderer.materials = mats;
    }
}