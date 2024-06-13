using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtension 
{
    
    public static  Vector3 CalculateNormal(Vector3 startPoint,Vector3 midPoint, Vector3 endPoint)
    {
        Vector3 normal;

        if (startPoint.y>endPoint.y)
            normal = Vector3.Cross(startPoint - midPoint, endPoint - midPoint).normalized;

        else
        {
            normal = Vector3.Cross(midPoint - startPoint, endPoint - startPoint).normalized;
        }

        return normal;
    }

    public static Vector3 GetPostiveVetcto(Vector3 dir)
    {

        if (dir.y < 0)
        {
            dir.y = dir.y * -1f;
        }

        if (dir.z < 0)
        {
            dir.z = dir.z * -1f;
        }

        return dir;

    }
}
