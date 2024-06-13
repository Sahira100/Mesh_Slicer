using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygenHelper 
{

    public static float CalculateOfArea(Vector3[] vertecis,int[] triangles)
    {
        float totalArea=0f;

        int numberOfTriangles = triangles.Length / 3;

        for (int i = 0; i < numberOfTriangles; i++)
        {
            Vector3 v1 = vertecis[triangles[(3*i) + 1]] - vertecis[triangles[(3 * i)]];
            Vector3 v2 = vertecis[triangles[(3 * i) + 2]] - vertecis[triangles[(3 * i)]];

            float areaOftrinalge = Vector3.Cross(v1, v2).magnitude*0.5f;

            totalArea += areaOftrinalge;
        }

        return totalArea;
    }



    public  void Trangulate(List<Vector3> pointList,Vector3 frontNormal)
    {
        List<Vector3> points = pointList;

        if (!CheckClockWiseOrderPointList(points, frontNormal))
        {
            points = ReverseList(points);
        }




    }
    public bool CheckClockWiseOrderPointList(List<Vector3> pointList,Vector3 frontNormal)
    {
        //213

        Vector3 p12 = GetValue(pointList,- 1) - GetValue(pointList, 0);
        Vector3 p13 = GetValue(pointList,-1) - GetValue(pointList,0);

        for (int i = 0; i < pointList.Count; i++)
        {
            p12 = GetValue(pointList, i - 1) - GetValue(pointList, i);
            p13 = GetValue(pointList, i + 1) - GetValue(pointList, i);


            if (Vector3.Dot(p12.normalized, p13.normalized) != -1f)
            {
                break;
            }


        }

        Vector3 dir = Vector3.Cross(p12.normalized, p13.normalized);

        if (dir == frontNormal * -1f)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }

    public Vector3 GetValue(List<Vector3> pointList,int index)
    {
        if (index >= pointList.Count)
        {
            index = index - pointList.Count;
        }

        if (index < 0)
        {
            index = index + pointList.Count;
        }

        return pointList[index];

    }

    public  List<Vector3> ReverseList(List<Vector3>  pointList)
    {
        List<Vector3> reverseList = new List<Vector3>();

        for (int i = 0; i < pointList.Count; i++)
        {
            reverseList.Add(pointList[pointList.Count - 1 - i]);
        }

        return reverseList;
    }


}
