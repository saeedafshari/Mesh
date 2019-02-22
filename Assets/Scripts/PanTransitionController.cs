using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanTransitionController : MonoBehaviour
{
    const float FromX = 6.0f;
    const float ToX = 0.0f;
    const float Speed = 10.0f;

    public int State { get; private set; } = 0;

    public void ShowTransition()
    {
        State = 1;
        transform.localPosition = new Vector3(FromX, 0, 0);
    }

    public void HideTransition()
    {
        State = -1;
        transform.localPosition = new Vector3(ToX, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowTransition();
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;
        var dx = dt * Speed;

        if (State == 1) //Show
        {
            var newX = transform.localPosition.x - dx;
            if (newX < ToX)
            {
                newX = ToX;
                State = 0;
            }
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }
        else if (State == -1) //Hide
        {
            var newX = transform.localPosition.x - dx;
            if (newX < -FromX)
            {
                Destroy(gameObject);
                State = 0;
            }
            transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
