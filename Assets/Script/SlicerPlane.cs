using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicerPlane
{

    private Vector3 normal;          //Normal of the Slice plane
    private Vector3 pointOnPlane;    //A Point on the Plane

    public SlicerPlane()
    {

    }

    public SlicerPlane(Vector3 normal,Vector3 pointOnPlane)
    {
        this.normal = normal.normalized;
        this.pointOnPlane = pointOnPlane;
    }

    public Vector3 Normal { get { return normal; } set { normal = value; } }

    public Vector3 GetPointOnPlane()
    {
        return pointOnPlane;
    }

    public void Initiate(Vector3 pointA,Vector3 pointB,Vector3 pointC)
    {
        pointOnPlane = pointA;

        normal = CalculatePlaneNormal(pointA,pointB,pointC);
    }

    private Vector3 CalculatePlaneNormal(Vector3 pointA,Vector3 pointB,Vector3 pointC)
    {
        Vector3 dir1 = pointB - pointA;
        Vector3 dir2 = pointC - pointA;

        Vector3 normal=Vector3.Cross(dir1, dir2).normalized;

        return normal;

    }



}
