using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPath : MonoBehaviour
{
    public Transform[] points;
    void Start()
    {
        
        
    }

    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
                if(i+1 < points.Length)
                Debug.DrawLine(points[i].position, points[i + 1].position, Color.red);
                else
                Debug.DrawLine(points[i].position, points[0].position, Color.red);
        }
    }

}
