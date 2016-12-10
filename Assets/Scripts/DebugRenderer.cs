using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugRenderer : MonoBehaviour
{
    [Tooltip("Materials for drawing lines")]
    public Material[] Materials;

    // stack to render on next pass
    private Stack<GLLine> lines;

    // switch to toggle drawing debug lines
    public bool DrawDebug = true;

    // Use this for initialization
    void Start()
    {
        lines = new Stack<GLLine>();
    }

    void Update()
    {
        // toggle DrawDebug when D key is hit
        if (Input.GetKeyDown(KeyCode.D))
        {
            DrawDebug = !DrawDebug;
        }
    }

    /// <summary>
    /// Pushes line data to the buffer to be rendered on next pass
    /// </summary>
    /// <param name="line"></param>
    public void DrawLine(GLLine line)
    {
        lines.Push(line);
        Debug.DrawLine(line.Start, line.End, line.Material.color);
    }

    /// <summary>
    /// Pushes line data to the buffer to be rendered on next pass
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="material"></param>
    public void DrawLine(Vector3 start, Vector3 end, Material material)
    {
        DrawLine(new GLLine(start, end, material));
    }

    /// <summary>
    /// Draws a crosshair at a specified position
    /// </summary>
    /// <param name="pos">Position of the crosshair</param>
    /// <param name="r">Radius of the crosshair</param>
    /// <param name="material">Material for crosshair lines</param>
    public void DrawCrosshair(Vector3 pos, float r, Material material)
    {
        // x
        DrawLine(pos + Vector3.left * r, pos + Vector3.right * r, material);
        // y
        DrawLine(pos + Vector3.down * r, pos + Vector3.up * r, material);
        // z
        DrawLine(pos + Vector3.back * r, pos + Vector3.forward * r, material);
    }

    /// <summary>
    /// Renders debug lines to game
    /// </summary>
    void OnRenderObject()
    {
        // clear the stack if not drawing debug lines this frame
        if (!DrawDebug)
        {
            lines.Clear();
        }

        while(lines.Count > 0)
        {
            drawGLLine(lines.Pop());
        }
    }

    /// <summary>
    /// Uses GL to render a line
    /// </summary>
    /// <param name="line"></param>
    private void drawGLLine(GLLine line)
    {
        // set the material
        line.Material.SetPass(0);

        // draw the line
        GL.Begin(GL.LINES);
        GL.Vertex(line.Start);
        GL.Vertex(line.End);
        GL.End();
    }
}

/// <summary>
/// Struct to hold line data
/// </summary>
public struct GLLine
{
    public Vector3 Start, End;
    public Material Material;

    public GLLine(Vector3 start, Vector3 end, Material material)
    {
        Start = start;
        End = end;
        Material = material;
    }
}
