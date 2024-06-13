using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traingle
{
    Vector3 v1;      
    Vector3 v2;
    Vector3 v3;

    Vector3 n1;
    Vector3 n2;
    Vector3 n3;

    Vector2 uv1;
    Vector2 uv2;
    Vector2 uv3;

    public Vector3 V1 { get => v1; set => v1 = value; }
    public Vector3 V2 { get => v2; set => v2 = value; }
    public Vector3 V3 { get => v3; set => v3 = value; }

    public Vector3 N1 { get => n1; set => n1 = value; }
    public Vector3 N2 { get => n2; set => n2 = value; }
    public Vector3 N3 { get => n3; set => n3 = value; }

    public Vector2 Uv1 { get => uv1; set => uv1 = value; }
    public Vector2 Uv2 { get => uv2; set => uv2 = value; }
    public Vector2 Uv3 { get => uv3; set => uv3 = value; }


    public void InitiateVertex(Vector3 p1,Vector3 p2,Vector3 p3)
    {
        V1 = p1;
        V2 = p2;
        V3 = p3;
    }

    public void InitiateNormal(Vector3 n1, Vector3 n2, Vector3 n3)
    {
        N1 = n1;
        N2 = n2;
        N3 = n3;
    }

    public void InitiateUVs(Vector2 uv1, Vector2 uv2, Vector2 uv3)
    {
        Uv1 = uv1;
        Uv2 = uv2;
        Uv3 = uv3;
    }



}
