using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Scorable : MonoBehaviour, IRoundStatusTarget
{
    public float MoveTolerance = 0.3f;

    public float Value = 1f;
    public float ImpactValue = 0.01f;

    private float sum;
    private float maxDeltaScore;

    public int Score
    {
        get { return Mathf.RoundToInt(sum); }
    }

    // scoring variables
    private Vector3 rootPos;
    private Vector3 rootAngles;
    private float mass;
    private bool moved;

    private bool roundActive;


    // Use this for initialization
    void Start()
    {
        rootPos = transform.position;
        rootAngles = transform.eulerAngles;
        mass = GetComponent<Rigidbody>().mass;
    }

    private void checkMoved()
    {
        // get delta orientation
        Vector3 deltaPos = transform.position - rootPos;

        // check if object has moved
        moved = deltaPos.sqrMagnitude > Mathf.Pow(MoveTolerance, 2);

        // if moved set score to value
        if (moved)
            sum = Value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moved)
            checkMoved();
    }

    public void OnCollisionEnter(Collision collision)
    {
        // score collision if moved
        if(moved)
        {
            sum += ImpactValue * collision.relativeVelocity.magnitude;
        }
    }

    public void RoundStart()
    {
        roundActive = true;
    }

    private Color setSaturation(Color c, float sat)
    {
        float h, s, v;
        Color.RGBToHSV(c, out h, out s, out v);
        s = sat;
        return Color.HSVToRGB(h, s, v);
    }

    private void desaturateMaterial()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer)
        {
            foreach (Material mat in meshRenderer.materials)
            {
                mat.color = setSaturation(mat.color, 0.05f);

                if (mat.GetColor("_EmissionColor") != null)
                    mat.SetColor("_EmissionColor", setSaturation(mat.GetColor("_EmissionColor"), 0.05f));
            }
        }
    }

    private void disableColliders()
    {
        Collider[] attached = GetComponents<Collider>();
        for (int i = 0; i < attached.Length; i++)
        {
            attached[i].enabled = false;
        }

        Collider[] children = GetComponentsInChildren<Collider>();
        for (int i = 0; i < children.Length; i++)
        {
            children[i].enabled = false;
        }
    }

    public void RoundEnd()
    {
        roundActive = false;
        GetComponent<Rigidbody>().isKinematic = true;
        disableColliders();
        desaturateMaterial();
    }
}
