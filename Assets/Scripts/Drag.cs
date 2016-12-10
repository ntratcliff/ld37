using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    public LayerMask RaycastLayers;

    [Tooltip("Maximum raycast distance")]
    public float MaxDist = 4;

    [Tooltip("Scalar force for dragging")]
    public float DragForce = 1f;

    // member variables for dragging
    private Rigidbody selectedBody;
    private float selectionDist;
    private Vector3 bodyLocalPoint;

    private DebugRenderer debug;

    // Use this for initialization
    void Start()
    {
        // find debug renderer
        debug = FindObjectOfType<DebugRenderer>();
    }

    private void raycastSelect()
    {
        Ray ray = new Ray(transform.position, transform.forward); // raycast forward

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxDist, RaycastLayers)
            && hit.rigidbody)
        {
            selectedBody = hit.rigidbody;
            selectionDist = hit.distance;
            bodyLocalPoint = hit.transform.InverseTransformPoint(hit.point);
        }

        // draw debug
        if (debug)
            debug.DrawLine(ray.origin, ray.origin + ray.direction * hit.distance, debug.Materials[3]);
    }

    private void dragSelected()
    {
        Vector3 selWorldPos = selectedBody.transform.TransformPoint(bodyLocalPoint);
        Vector3 curWorldPos = transform.position + transform.forward * selectionDist;

        Vector3 delta = curWorldPos - selWorldPos;

        selectedBody.AddForceAtPosition(delta * DragForce, selWorldPos);

        // draw debug
        if (debug)
        {
            debug.DrawLine(transform.position, curWorldPos, debug.Materials[3]);
            debug.DrawCrosshair(selWorldPos, 0.4f, debug.Materials[0]);
            debug.DrawCrosshair(curWorldPos, 0.4f, debug.Materials[1]);
        }

    }

    private void clearSelect()
    {
        selectedBody = null;
        selectionDist = 0f;
        bodyLocalPoint = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // no selected body for dragging
        if (!selectedBody && Input.GetMouseButtonDown(0))
        {
            raycastSelect();
            Debug.Log("Select");
        }
        else if (selectedBody && Input.GetMouseButton(0))
        {
            dragSelected();
            Debug.Log("Drag");
        }
        // clear selected body if mouse up
        else if (selectedBody && !Input.GetMouseButton(0))
        {
            clearSelect();
            Debug.Log("Clear");
        }
    }



}
