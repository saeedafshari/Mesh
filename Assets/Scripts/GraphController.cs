using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GraphController : MonoBehaviour
{
    GameObject fabVertex;
    GameObject fabLine2D;

    List<VertexController> vertices = new List<VertexController>();
    List<LineController> lines = new List<LineController>();

    LineController currentLine = null;
    Vector3? headPos = null;

    const float SnapDistance = .4f;

    Graph graph;

    GameSceneController game;

    // Start is called before the first frame update
    void Start()
    {
        fabVertex = Resources.Load<GameObject>("Prefabs/Vertex");
        fabLine2D = Resources.Load<GameObject>("Prefabs/Line");
        game = FindObjectOfType<GameSceneController>();

        var count = 5;
        var radius = 1.5f;
        float rdeltaX = .0f;
        float rdeltaY = .0f;

        graph = new Graph(count, false);
        float initAlpha = Random.Range(0, 4) * 90; // 0; // RANDF * 360.0f;
        if (count == 3)
        {
            initAlpha = Random.value > 0.5f ? 90 : 270;
        }

        if (count == 4)
        {
            initAlpha = Random.value > 0.5f ? 0 : 45;
        }

        float rx = radius + Random.value * rdeltaX;
        float ry = radius + Random.value * rdeltaY;

        float delta = 360.0f / (float)count;
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(fabVertex);
            obj.transform.SetParent(transform);
            obj.transform.localScale = Vector3.one;
            var vertex = obj.GetComponent<VertexController>();
            vertices.Add(vertex);

            var alpha = Mathf.Deg2Rad *
                (float)System.Math.IEEERemainder(initAlpha + delta * (float)i, 360.0f);

            Debug.Log($"Alpha: {alpha}");
            obj.transform.localPosition = new Vector3(
                rx * Mathf.Cos(alpha),
                ry * Mathf.Sin(alpha));

            vertex.Program(graph.Values[i]);
        }
    }

    Vector3 GetTouchPosition()
    {
        var tp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tp.z = 0;
        return tp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseRelease();
        }
        else if (Input.GetMouseButton(0))
        {
            HandleMouseMove();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ScreenCapture.CaptureScreenshot(
                System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                DateTime.Now.Ticks + ".png"));
        }
    }

    void HandleMouseClick()
    {
        if (currentLine != null)
        {
            DestroyImmediate(currentLine.gameObject);
        }

        var tp = GetTouchPosition();
        var obj = Instantiate(fabLine2D);
        currentLine = obj.GetComponent<LineController>();
        headPos = null;

        foreach (var vertex in vertices)
        {
            var dist = (tp - vertex.transform.position).magnitude;
            if (dist < SnapDistance)
            {
                tp = vertex.transform.position;
                currentLine.SetEnd(vertex, true);
                break;
            }
        }

        headPos = tp;
        currentLine.Line.HeadLocalPosition = tp;
        currentLine.Line.TailLocalPosition = tp;
        Validate();
    }

    void HandleMouseRelease()
    {
        bool found = false;
        bool fail = false;
        var tp = GetTouchPosition();
        if (currentLine.Head != null)
        {
            foreach (var vertex in vertices)
            {
                var dist = (tp - vertex.transform.position).magnitude;
                if (dist < SnapDistance)
                {
                    if (vertex == currentLine.Head)
                    {
                        fail = true;
                        break;
                    }

                    currentLine.SetEnd(vertex, false);
                    currentLine.Value = Mathf.Min(currentLine.Head.Value, currentLine.Tail.Value);
                    tp = vertex.transform.position;

                    currentLine.Head.Value -= currentLine.Value;
                    currentLine.Tail.Value -= currentLine.Value;
                    currentLine.Head.UpdateUI();
                    currentLine.Tail.UpdateUI();

                    found = true;
                    break;
                }
            }
        }

        if (found)
        {
            currentLine.transform.SetParent(transform);
            lines.Add(currentLine);
        }
        else if (!fail)
        {
            foreach (var line in lines.ToArray())
            {
                if (NeatLib.LinesIntersect(
                    line.Head.transform.position,
                    line.Tail.transform.position,
                    headPos.Value,
                    tp))
                {
                    // Delete line
                    line.Head.Value += line.Value;
                    line.Tail.Value += line.Value;

                    Destroy(line.gameObject);
                    lines.Remove(line);

                    line.Head.UpdateUI();
                    line.Tail.UpdateUI();
                }
            }
            DestroyImmediate(currentLine.gameObject);
        }
        else
        {
            DestroyImmediate(currentLine.gameObject);
        }
        
        currentLine = null;
        Validate();
    }

    void HandleMouseMove()
    {
        currentLine.Line.TailLocalPosition = GetTouchPosition();
    }

    void Validate()
    {
        foreach (var vertex in vertices)
        {
            if (vertex.Value > 0) return;
        }

        game.Next(true);
        GetComponent<PanTransitionController>().HideTransition();
        Destroy(this);
        if (currentLine != null)
        {
            DestroyImmediate(currentLine.gameObject);
        }
    }
}
