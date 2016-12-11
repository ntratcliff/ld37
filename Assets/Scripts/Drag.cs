﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    public LayerMask RaycastLayers;

    [Tooltip("Maximum raycast distance")]
    public float MaxDist = 4;

    [Tooltip("Scalar force for dragging")]
    public float DragForce = 1f;

    [Tooltip("Maximum dragging force")]
    public float MaxForce = 5f;

    [Tooltip("Maximum dragging speed")]
    public float MaxSpeed = 5f;

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
        // calculate dragging force 
        Vector3 selWorldPos = selectedBody.transform.TransformPoint(bodyLocalPoint);
        Vector3 curWorldPos = transform.position + transform.forward * selectionDist;

        Vector3 delta = curWorldPos - selWorldPos;

        Vector3 force = delta * DragForce * Time.deltaTime * 60f;
        force = Vector3.ClampMagnitude(force, MaxForce);

        // apply dragging force
        selectedBody.AddForceAtPosition(force, selWorldPos);

        // clamp speed
        selectedBody.velocity = Vector3.ClampMagnitude(selectedBody.velocity, MaxSpeed);

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
        }
        else if (selectedBody && Input.GetMouseButton(0))
        {
            dragSelected();
        }
        // clear selected body if mouse up
        else if (selectedBody && !Input.GetMouseButton(0))
        {
            clearSelect();
        }
    }



}
