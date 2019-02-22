using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public VertexController Head { get; set; }
    public VertexController Tail { get; set; }
    public int Value { get; set; }

    NeatLine _line;
    public NeatLine Line
    {
        get
        {
            if (_line == null)
                _line = GetComponent<NeatLine>();
            if (_line == null)
                _line = gameObject.AddComponent<NeatLine>();
            return _line;
        }
    }

    public void SetEnd(VertexController vertex, bool isHead)
    {
        if (isHead)
        {
            Head = vertex;
            Line.HeadLocalPosition = vertex.transform.position;
            Line.TailLocalPosition = vertex.transform.position;
        }
        else
        {
            Tail = vertex;
            Line.TailLocalPosition = vertex.transform.position;
            Line.Color = Color.white;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Line.Color = new Color(1.0f, 1.0f, 1.0f, 0.9f);
        Line.Thickness = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
