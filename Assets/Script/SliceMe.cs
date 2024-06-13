using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
public class SliceMe : MonoBehaviour
{
    public bool CreateslicePart=true;

    public float translaitonSpeed;
    public float translationDistance;

    private SliceObject sliceObject;
    private LevelManager levelManger;
    private float totalArea;

    private MeshFilter meshFilter;
    private Vector3[] vertices;
    private int[] traingles;
    private Vector3[] normals;
    private Vector2[] uv;


    //faceDate
    private List<FaceData> faceDataList;
    private List<Vector3> faceVertecis;


    private void Start()
    {
        Initiate();

    }

    private void Update()
    {
        
    }

    public void Slice(SlicerPlane slicePlane)
    {
        //SLICE PART MESH
        SampleMesh frontMesh = new SampleMesh();
        SampleMesh backMesh = new SampleMesh();

        Vector3 slicePlaneNormal = ConvertToLocalSpace(slicePlane.Normal);
        Vector3 pointOnSlicePlane = ConvertToLocalSpace(slicePlane.GetPointOnPlane());
        float distanceD = CalculateDistanceD(slicePlaneNormal, pointOnSlicePlane);


        //Set up the two parts of the Slice object 
        SetUpSliceMesh(frontMesh, backMesh, slicePlaneNormal, pointOnSlicePlane, distanceD);


        /*
        //Empty-notEmpty
        //ClearListRepetingValues();
        //Add-face
        faceVertecis = GetFaceVertecis(faceDataList);


        if(faceVertecis !=null)
            for (int i = 0; i < faceVertecis.Count; i++)
            {
                Instantiate(prefab,ConvertToWorldSpace(faceVertecis[i]), Quaternion.identity);
           
            }

        */


        Debug.Log("Sliced");

        GameObject slicePart;

        //return front part mesh
        slicePart=MakeGameObject(frontMesh.ReturnMesh());

        SetSlicePartMoveGoalPos(slicePart, this.transform.position + (slicePlane.Normal * translationDistance));


        //retun back part mesh
        MakeGameObject(backMesh.ReturnMesh());




        //send slice part presenatage
        SliceData(PolygenHelper.CalculateOfArea(frontMesh.ReturnVertcisList(), frontMesh.ReturnTrianglesList())*100f/totalArea,slicePlane.Normal);

        this.gameObject.SetActive(false);

    }

    private Vector3 IntersectionPointOfLine(Vector3 from, Vector3 to, float D, Vector3 slicePlaneNormal, Vector3 pointOnslicePlane)
    {

        float t;

        t = (D - Vector3.Dot(slicePlaneNormal, from)) / Vector3.Dot((to - from), slicePlaneNormal);


        Vector3 intesecPosition = from + (t * (to - from));

        return intesecPosition;

    }

    private bool checkLineIntesection(Vector3 from, Vector3 to, Vector3 slicePlaneNormal, Vector3 pointOnslicePlane)
    {
        if (CheckPointInFrontOFTheSlicePlane(from, slicePlaneNormal, pointOnslicePlane) != CheckPointInFrontOFTheSlicePlane(to, slicePlaneNormal, pointOnslicePlane))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsTraingleIntesecThePlane(Traingle traingle, Vector3 slicePlaneNormal, Vector3 pointOnSlicePlane)
    {
        if (checkLineIntesection(traingle.V1, traingle.V2, slicePlaneNormal, pointOnSlicePlane))
        {
            return true;
        }
        else
        {
            if (checkLineIntesection(traingle.V2, traingle.V3, slicePlaneNormal, pointOnSlicePlane))
            {
                return true;
            }
            else
            {
                if (checkLineIntesection(traingle.V3, traingle.V1, slicePlaneNormal, pointOnSlicePlane))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }






    }

    private bool CheckPointInFrontOFTheSlicePlane(Vector3 p, Vector3 slicePlaneNormal, Vector3 pointOnSlicePlane)
    {
        float PDPslice = Vector3.Dot(slicePlaneNormal, p - pointOnSlicePlane);

        if (PDPslice >= 0)
        {
            return true;
        }
        else
        {
            return false;

        }

    }

    private Vector2 InterPolateUVs(Vector2 uv1, Vector2 uv2, float t)
    {
        Vector2 uv = ((1 - t) * uv1) + (t * uv2);
        return uv;
    }

    private Vector3 InterPolateNormal(Vector3 n1, Vector3 n2, float t)
    {

        Vector3 n = ((1 - t) * n1) + (t * n2);

        if (n == Vector3.zero)
        {
            Debug.Log("Zero Normal");
        }
        return n.normalized;
    }

    private Traingle GetTriangle(int traingleIndex, Traingle traingle)
    {


        //check bound the traingle index Number
        int maxTraingleIndex = traingles.Length / 3;

        if (traingleIndex >= maxTraingleIndex)
        {
            ErroMessage("Bound of trianlge Index");
            return null;

        }

        traingle.InitiateVertex(vertices[traingles[3 * traingleIndex]], vertices[traingles[(3 * traingleIndex) + 1]], vertices[traingles[(3 * traingleIndex) + 2]]);
        traingle.InitiateNormal(normals[traingles[3 * traingleIndex]], normals[traingles[(3 * traingleIndex) + 1]], normals[traingles[(3 * traingleIndex) + 2]]);
        traingle.InitiateUVs(uv[traingles[3 * traingleIndex]], uv[traingles[(3 * traingleIndex) + 1]], uv[traingles[(3 * traingleIndex) + 2]]);

        return traingle;



    }

    public Vector3 ConvertToLocalSpace(Vector3 info)
    {
        return this.transform.InverseTransformDirection(info);

    }

    public Vector3 ConvertToWorldSpace(Vector3 info)
    {
        return this.transform.TransformDirection(info);
    }

    private void Initiate()
    {
        sliceObject = this.GetComponent<SliceObject>();
        levelManger = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();


        meshFilter = this.GetComponent<MeshFilter>();
        vertices = meshFilter.mesh.vertices;
        traingles = meshFilter.mesh.triangles;
        normals = meshFilter.mesh.normals;
        uv = meshFilter.mesh.uv;

        totalArea = PolygenHelper.CalculateOfArea(vertices, traingles);
        faceDataList = new List<FaceData>();
    }

    private void ErroMessage(string message)
    {
        Debug.LogError(message + "");
    }

    private void WarningMessage(string message)
    {
        Debug.LogWarning(message + "");
    }

    private float CalculateDistanceD(Vector3 slicePlaneNormal, Vector3 pointOnslicePlane)
    {
        float d = Vector3.Dot(pointOnslicePlane, slicePlaneNormal);
        return d;

    }

    private GameObject  MakeGameObject(Mesh mesh)
    {
        if (!CreateslicePart)
        {
            return null;
        }

        GameObject part = new GameObject();

        part.AddComponent<MeshFilter>();
        part.AddComponent<MeshRenderer>();
        //Apply defult material
      //  part.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Standard"));
        //Apply Original Material
        part.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().sharedMaterial;
        part.GetComponent<MeshFilter>().mesh = mesh;

        part.transform.localScale = this.transform.localScale;
        part.transform.rotation = this.transform.rotation;
        part.transform.position = this.transform.position;

        return part;


    }

    private void SetUpSliceMesh(SampleMesh frontMesh ,SampleMesh backMesh,Vector3 slicePlaneNormal,Vector3 pointOnSlicePlane,float distanceD)
    {


        //Number Of trangles has this object
        int numberOfTrangles = this.traingles.Length / 3;

        //triange data
        Traingle traingle = new Traingle();

        //Lets go throught the trangles
        for (int triangleIndex = 0; triangleIndex < numberOfTrangles; triangleIndex++)
        {


            traingle = GetTriangle(triangleIndex, traingle);

            //if trangle is intesection the plane
            if (IsTraingleIntesecThePlane(traingle, slicePlaneNormal, pointOnSlicePlane))
            {
                //create new faceData struct
                FaceData newFaceData = new FaceData();


                List<Vector3> intersecPoints = new List<Vector3>(2);


                int numberOfExistVertecesOfFront = frontMesh.ReturnVertexCount();
                int numberOfExistVertecesOfBack = backMesh.ReturnVertexCount();


                //v1 - v2 line intesect
                if (checkLineIntesection(traingle.V1, traingle.V2, slicePlaneNormal, pointOnSlicePlane))
                {

                    Vector3 intesecPoint = IntersectionPointOfLine(traingle.V1, traingle.V2, distanceD, slicePlaneNormal, pointOnSlicePlane);

                    float tvalue = (intesecPoint - traingle.V1).magnitude / (traingle.V2 - traingle.V1).magnitude;

                    Vector2 intesecUVs = InterPolateUVs(traingle.Uv1, traingle.Uv2, tvalue);
                    Vector3 intersecNormal = InterPolateNormal(traingle.N1, traingle.N2, tvalue);

                    //faceData
                    //newFaceData.AddPoint(intesecPoint);
                    intersecPoints.Add(intesecPoint);
                    

                    if (CheckPointInFrontOFTheSlicePlane(traingle.V1, slicePlaneNormal, pointOnSlicePlane))
                    {
                        //v1
                        frontMesh.AddVertex(traingle.V1, intesecPoint);
                        frontMesh.AddNormal(traingle.N1, intersecNormal);
                        frontMesh.AddUVs(traingle.Uv1, intesecUVs);

                        //v2
                        backMesh.AddVertex(intesecPoint, traingle.V2);
                        backMesh.AddNormal(intersecNormal, traingle.N2);
                        backMesh.AddUVs(intesecUVs, traingle.Uv2);


                    }
                    else
                    {
                        //v1
                        backMesh.AddVertex(traingle.V1, intesecPoint);
                        backMesh.AddNormal(traingle.N1, intersecNormal);
                        backMesh.AddUVs(traingle.Uv1, intesecUVs);

                        //v2
                        frontMesh.AddVertex(intesecPoint, traingle.V2);
                        frontMesh.AddNormal(intersecNormal, traingle.N2);
                        frontMesh.AddUVs(intesecUVs, traingle.Uv2);
                    }



                }
                else
                {
                    if (CheckPointInFrontOFTheSlicePlane(traingle.V1, slicePlaneNormal, pointOnSlicePlane))
                    {
                        frontMesh.AddVertex(traingle.V1, traingle.V2);

                        frontMesh.AddNormal(traingle.N1, traingle.N2);

                        frontMesh.AddUVs(traingle.Uv1, traingle.Uv2);
                    }
                    else
                    {
                        backMesh.AddVertex(traingle.V1, traingle.V2);

                        backMesh.AddNormal(traingle.N1, traingle.N2);

                        backMesh.AddUVs(traingle.Uv1, traingle.Uv2);
                    }
                }

                //v2-v3 line  intesect
                if (checkLineIntesection(traingle.V2, traingle.V3, slicePlaneNormal, pointOnSlicePlane))
                {

                    Vector3 intesecPoint = IntersectionPointOfLine(traingle.V2, traingle.V3, distanceD, slicePlaneNormal, pointOnSlicePlane);

                    float tvalue = (intesecPoint - traingle.V2).magnitude / (traingle.V3 - traingle.V2).magnitude;

                    Vector2 intesecUVs = InterPolateUVs(traingle.Uv2, traingle.Uv3, tvalue);
                    Vector3 intersecNormal = InterPolateNormal(traingle.N2, traingle.N3, tvalue);

                    //faceData
                    //newFaceData.AddPoint(intesecPoint);
                    intersecPoints.Add(intesecPoint);


                    if (CheckPointInFrontOFTheSlicePlane(traingle.V2, slicePlaneNormal, pointOnSlicePlane))
                    {
                        //v2
                        frontMesh.AddVertex(intesecPoint);
                        frontMesh.AddNormal(intersecNormal);
                        frontMesh.AddUVs(intesecUVs);

                        //v3
                        backMesh.AddVertex(intesecPoint, traingle.V3);
                        backMesh.AddNormal(intersecNormal, traingle.N3);
                        backMesh.AddUVs(intesecUVs, traingle.Uv3);


                    }
                    else
                    {
                        //v2
                        backMesh.AddVertex(intesecPoint);
                        backMesh.AddNormal(intersecNormal);
                        backMesh.AddUVs(intesecUVs);

                        //v3
                        frontMesh.AddVertex(intesecPoint, traingle.V3);
                        frontMesh.AddNormal(intersecNormal, traingle.N3);
                        frontMesh.AddUVs(intesecUVs, traingle.Uv3);
                    }

                }
                else
                {
                    if (CheckPointInFrontOFTheSlicePlane(traingle.V2, slicePlaneNormal, pointOnSlicePlane))
                    {
                        frontMesh.AddVertex(traingle.V3);

                        frontMesh.AddNormal(traingle.N3);

                        frontMesh.AddUVs(traingle.Uv3);
                    }
                    else
                    {
                        backMesh.AddVertex(traingle.V3);

                        backMesh.AddNormal(traingle.N3);

                        backMesh.AddUVs(traingle.Uv3);
                    }
                }

                //v3-v1 line intersect
                if (checkLineIntesection(traingle.V3, traingle.V1, slicePlaneNormal, pointOnSlicePlane))
                {

                    Vector3 intesecPoint = IntersectionPointOfLine(traingle.V3, traingle.V1, distanceD, slicePlaneNormal, pointOnSlicePlane);

                    float tvalue = (intesecPoint - traingle.V3).magnitude / (traingle.V1 - traingle.V3).magnitude;

                    Vector2 intesecUVs = InterPolateUVs(traingle.Uv3, traingle.Uv1, tvalue);
                    Vector3 intersecNormal = InterPolateNormal(traingle.N3, traingle.N1, tvalue);

                    //faceData
                    //newFaceData.AddPoint(intesecPoint);
                    intersecPoints.Add(intesecPoint);

                    if (CheckPointInFrontOFTheSlicePlane(traingle.V3, slicePlaneNormal, pointOnSlicePlane))
                    {
                        //v2
                        frontMesh.AddVertex(intesecPoint);
                        frontMesh.AddNormal(intersecNormal);
                        frontMesh.AddUVs(intesecUVs);

                        //v3
                        backMesh.AddVertex(intesecPoint);
                        backMesh.AddNormal(intersecNormal);
                        backMesh.AddUVs(intesecUVs);


                    }
                    else
                    {
                        //v2
                        backMesh.AddVertex(intesecPoint);
                        backMesh.AddNormal(intersecNormal);
                        backMesh.AddUVs(intesecUVs);

                        //v3
                        frontMesh.AddVertex(intesecPoint);
                        frontMesh.AddNormal(intersecNormal);
                        frontMesh.AddUVs(intesecUVs);
                    }

                }


                //final remembe to add tranglutes after finde intesection points
                int addVertecisCountToFrontMesh = frontMesh.ReturnVertexCount() - numberOfExistVertecesOfFront;
                int addVertecisCountToBackMesh = backMesh.ReturnVertexCount() - numberOfExistVertecesOfBack;

                //Front Mesh trangulation
                if (addVertecisCountToFrontMesh == 3)  //triangle
                {
                    frontMesh.AddTraingle(numberOfExistVertecesOfFront, numberOfExistVertecesOfFront + 1, numberOfExistVertecesOfFront + 2);

                    
                }
                else if (addVertecisCountToFrontMesh == 4) //polygen with 4 vertecis
                {
                    frontMesh.AddTraingle(numberOfExistVertecesOfFront, numberOfExistVertecesOfFront + 1, numberOfExistVertecesOfFront + 2);
                    frontMesh.AddTraingle(numberOfExistVertecesOfFront, numberOfExistVertecesOfFront + 2, numberOfExistVertecesOfFront + 3);

                }

                //Back Mesh trangulation
                if (addVertecisCountToBackMesh == 3) //triangle
                {

                    backMesh.AddTraingle(numberOfExistVertecesOfBack, numberOfExistVertecesOfBack + 1, numberOfExistVertecesOfBack + 2);
                }
                else if (addVertecisCountToBackMesh == 4)
                {
                    backMesh.AddTraingle(numberOfExistVertecesOfBack, numberOfExistVertecesOfBack + 1, numberOfExistVertecesOfBack + 2);
                    backMesh.AddTraingle(numberOfExistVertecesOfBack, numberOfExistVertecesOfBack + 2, numberOfExistVertecesOfBack + 3);
                }


                //faceData

                //check and fill facedata
                if (intersecPoints[0] != intersecPoints[1])
                {
                    newFaceData.AddPoint(intersecPoints[0]);
                    newFaceData.AddPoint(intersecPoints[1]);

                    faceDataList.Add(newFaceData);
                }
                

            }
            else
            {
                if (CheckPointInFrontOFTheSlicePlane(traingle.V1, slicePlaneNormal, pointOnSlicePlane))
                {
                    int numberOfExistVerteces = frontMesh.ReturnVertexCount();


                    frontMesh.AddVertex(traingle.V1, traingle.V2, traingle.V3);

                    frontMesh.AddNormal(traingle.N1, traingle.N2, traingle.N3);

                    frontMesh.AddUVs(traingle.Uv1, traingle.Uv2, traingle.Uv3);

                    frontMesh.AddTraingle(numberOfExistVerteces, numberOfExistVerteces + 1, numberOfExistVerteces + 2);


                }
                else
                {
                    int numberOfExistVerteces = backMesh.ReturnVertexCount();

                    backMesh.AddVertex(traingle.V1, traingle.V2, traingle.V3);

                    backMesh.AddNormal(traingle.N1, traingle.N2, traingle.N3);

                    backMesh.AddUVs(traingle.Uv1, traingle.Uv2, traingle.Uv3);

                    backMesh.AddTraingle(numberOfExistVerteces, numberOfExistVerteces + 1, numberOfExistVerteces + 2);
                }
            }
        }
    }


    //Get the face vertecis from the facedata list
    private List<Vector3> GetFaceVertecis(List<FaceData> facedataList)
    {
        List<Vector3> faceVertecis = new List<Vector3>();


        //intialization 
        Vector3 startVertex = faceDataList[0].PointA;
        Vector3 chainVertex = faceDataList[0].PointB;

        faceVertecis.Add(startVertex);
        faceVertecis.Add(chainVertex);

        facedataList.RemoveAt(0);

        bool isGetVertecisSuccsefully = true;

        while (facedataList.Count>0)
        {
            //check can get loop - defulat is false;
            bool isLoop = false;
            bool isChainVertexExit = false;


            //check  can get loop
            for (int i = 0; i < faceDataList.Count; i++)
            {
                if (facedataList[i].JustCheckPointInside(startVertex) )
                {
                    isLoop = true;

                }
            }

            //check chain algoritgh is works
            for (int i = 0; i < faceDataList.Count; i++)
            {
                if (facedataList[i].JustCheckPointInside(chainVertex))
                {
                    isChainVertexExit = true;

                }
            }

            if (!isChainVertexExit)
            {
                ErroMessage("Face Algorithm is Failed");
                isGetVertecisSuccsefully = false;
                break;
            }

            if (isLoop == false)
            {
                //ErroMessage("Face Algorithm is Failled - cand find loop");
                break;

            }

            //sett in up loop
            for (int i = 0; i < facedataList.Count; i++)
            {

                if (facedataList[i].checkPointInside(chainVertex))
                {
                    chainVertex = facedataList[i].PointB;

                    if (chainVertex != startVertex)
                    {
                        faceVertecis.Add(chainVertex);
                    }
                     

                    facedataList.RemoveAt(i);

                    break;
                }

            }

            //check whether have more than one loop
            if (facedataList.Count > 0)
            {
                if (chainVertex == startVertex)
                {
                    Debug.Log("Find");

                    startVertex = faceDataList[0].PointA;
                    chainVertex = faceDataList[0].PointB;

                    faceVertecis.Add(startVertex);
                    faceVertecis.Add(chainVertex);

                    facedataList.RemoveAt(0);
                }
            }

        }

        if (isGetVertecisSuccsefully)
            return faceVertecis;
        else
            return null;

    }

    private void ClearListRepetingValues()
    {


            for (int i = 0; i < faceDataList.Count; i++)
            {
                if (faceDataList[i].ChekcPointABIsEqual())
                {
                    Debug.Log("Removed");
                    //faceDataList.RemoveAt(i);
                    break;
                }
            }

    }

    private void SliceData(float frontPartPresentage,Vector3 sliceNormal)
    {
        levelManger.CheckSliceObjectPropterties(frontPartPresentage, sliceNormal);
    }

    private void SetSlicePartMoveGoalPos(GameObject slicePart,Vector3 targetPos)
    {
        slicePart.AddComponent<SlicePart>();

        slicePart.GetComponent<SlicePart>().SetUpData(translaitonSpeed, targetPos);
    }


}