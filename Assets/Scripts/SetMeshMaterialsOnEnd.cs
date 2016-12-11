using UnityEngine;
using System.Collections;
using System;

public class SetMeshMaterialsOnEnd : MonoBehaviour, IRoundStatusTarget
{
    public float S;

    public float DeltaV;

    private MeshRenderer meshRenderer;

    private Color setSdV(Color c, float sat, float dv)
    {
        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);
        s = sat;
        v += dv;
        Mathf.Clamp(v, 0, 1);
        return Color.HSVToRGB(h, s, v);
    }

    public void RoundEnd()
    {
        foreach (Material mat in meshRenderer.materials)
            mat.color = setSdV(mat.color, S, DeltaV);
    }

    public void RoundStart() { }

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

}
