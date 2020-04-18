using System;
using System.Drawing;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private static Transform[] points;
    private static int pointsCount;

    private void Awake()
    {
        pointsCount = transform.childCount;
        points = new Transform[pointsCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    public static Transform getWayPoint(int index)
    {
        return points[index];
    }

    public static int getCount() { return pointsCount; }
}
