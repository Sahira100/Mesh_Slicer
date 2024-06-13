using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceData 
{
    private Vector3 pointA;
    private Vector3 pointB;
    private Vector2 uv=Vector2.zero;   //Default uv you have to change this
    public bool isPointASet;

    public Vector3 PointA { get => pointA; set => pointA = value; }
    public Vector3 PointB { get => pointB; set => pointB = value; }
    public Vector2 Uv { get => uv; set => uv =value; }


    public bool checkPointInside(Vector3 point)
    {
        if (PointA == point)
        {
            return true;
        }

        if (PointB == point)
        {
            ChangeThePointOrder();
            return true;


        }

        return false;
    }

    public bool JustCheckPointInside(Vector3 point)
    {
        if (PointA == point)
        {
            return true;
        }

        if (PointB == point)
        {
            return true;


        }

        return false;
    }

    public void ChangeThePointOrder()
    {
        Vector3 p = PointA;

        PointA = PointB;
        PointB = p;

    }

    public void AddPoint(Vector3 point)
    {
        if (isPointASet)
        {
            pointB = point;
        }
        else
        {
            pointA = point;
            isPointASet = true;
        }
    }

    public bool ChekcPointABIsEqual()
    {
        if (pointA == pointB)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
