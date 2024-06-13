using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMesh
{
    private List<Vector3> vertecis;
    private List<Vector2> uv;
    private List<Vector2> uv2;
    private List<int> traingles;
    private List<Vector3> normals;

    public SampleMesh()
    {
        vertecis = new List<Vector3>();
        normals = new List<Vector3>();
        uv = new List<Vector2>();
        uv2 = new List<Vector2>();
        traingles = new List<int>();
    }


    public void AddVertex(Vector3 v1)
    {
        vertecis.Add(v1);
    }
    public void AddVertex(Vector3 v1,Vector3 v2,Vector3 v3)
    {
        vertecis.Add(v1);
        vertecis.Add(v2);
        vertecis.Add(v3);
    }
    public void AddVertex(Vector3 v1, Vector3 v2)
    {
        vertecis.Add(v1);
        vertecis.Add(v2);
    }


    public void AddTraingle(int i)
    {
        traingles.Add(i);
    }
    public void AddTraingle(int i1,int i2,int i3)
    {
        traingles.Add(i1);
        traingles.Add(i2);
        traingles.Add(i3);
    }



    public void AddUVs(Vector2 uv)
    {
        this.uv.Add(uv);
    }
    public void AddUVs(Vector2 uv1, Vector2 uv2)
    {
        this.uv.Add(uv1);
        this.uv.Add(uv2);
    }
    public void AddUVs(Vector2 uv1,Vector2 uv2,Vector2 uv3)
    {
        this.uv.Add(uv1);
        this.uv.Add(uv2);
        this.uv.Add(uv3);
    }


    public void AddNormal(Vector3 n1)
    {
        normals.Add(n1);
    }
    public void AddNormal(Vector3 n1,Vector3 n2)
    {
        normals.Add(n1);
        normals.Add(n2);
    }
    public void AddNormal(Vector3 n1,Vector3 n2,Vector3 n3)
    {
        normals.Add(n1);
        normals.Add(n2);
        normals.Add(n3);
    }


    public int[] ReturnTrianglesList()
    {
        return traingles.ToArray();
    }
    public Vector3[] ReturnVertcisList()
    {
        return vertecis.ToArray();
    }

    public Vector3 GetVertecis(int index)
    {
        if (index < vertecis.Count)
        {
            return vertecis[index];
        }
        else
        {
            Debug.LogError("index is bounded");
            return Vector3.zero;

        }
    }

    public int ReturnVertexCount()
    {
        return vertecis.Count;
    }

    public Mesh ReturnMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertecis.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = traingles.ToArray();
        mesh.uv = uv.ToArray();

        return mesh;


    }
}
