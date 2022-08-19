using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public string SelectedLayerName = "Selection";
    private int selectedLayerId = 7;
    private int defaultLayerId = 0;
    public string DefaultLayerName = "Default";
    public bool Selected { get { return this.gameObject.layer == selectedLayerId; } set { SetSelected(value); } }

    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private MeshRenderer[] meshRenderers;

   

    public void SetSelected(bool selected)
    {

        if (selected)
        {
            this.gameObject.layer = this.selectedLayerId;
            foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
            {
                if (skinnedMeshRenderer != null)
                {
                    skinnedMeshRenderer.gameObject.layer = this.selectedLayerId;
                }
            }
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer != null)
                {
                    meshRenderer.gameObject.layer = this.selectedLayerId;
                }
            }
            return;
        };
        this.gameObject.layer = this.defaultLayerId;
        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            if (skinnedMeshRenderer != null)
            {
                skinnedMeshRenderer.gameObject.layer = this.defaultLayerId;
            }
        }
        foreach (var meshRenderer in meshRenderers)
        {
            if (meshRenderer != null)
            {
                meshRenderer.gameObject.layer = this.defaultLayerId;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        selectedLayerId = LayerMask.NameToLayer(SelectedLayerName);
        defaultLayerId = LayerMask.NameToLayer(DefaultLayerName);
        skinnedMeshRenderers= this.GetComponentsInChildren<SkinnedMeshRenderer>();
        meshRenderers = this.GetComponentsInChildren<MeshRenderer>();
    }

    private void OnMouseDown() {
        SetSelected(!Selected);
    }

    //private void OnMouseExit()
    //{
    //    SetSelected(false);
    //}
    //private void OnMouseEnter()
    //{
    //    SetSelected(true);
    //}
}
