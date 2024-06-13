using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRender;
    [SerializeField]
    private float lineZValue;
    public SliceObject sliceObject;


    private Vector3 startPoint;
    private Vector3 midPoint;
    private Vector3 endPoint;

    [SerializeField]
    private bool isInputAccepted = true;
    [SerializeField]
    private bool isPlaneCutTheObject;

    public GameObject[] haddels;


    // Update is called once per frame
    void Update()
    {
        MouseInput();
    }

    private void MouseInput()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = lineZValue;


        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerInAvaliableArea(mousePos))
            {
                isInputAccepted = false;
                return;
            }

            SetTheStartHanddle();
            ResetTheLineRender();
            SetLineStartPosition(mousePos);
            SetLineEndPosition(mousePos);

            startPoint = mousePos;
            midPoint = mousePos;
            midPoint.z += 1f;
        }
        else if (Input.GetMouseButton(0) && isInputAccepted)
        {

            SetLineEndPosition(mousePos);
            SetTheEndHanddle();
        }
        else if (Input.GetMouseButtonUp(0))
        {

            if (isInputAccepted)
            {
                if (!IsPointerInAvaliableArea(mousePos))
                {
                    isInputAccepted = true;
                    ResetTheHaddle();
                    ResetTheLineRender();
                    return;
                }
            }
            else
            {
                isInputAccepted = true;
                return;
            }

            ResetTheHaddle();
            SetLineEndPosition(mousePos);
            ResetTheLineRender();

            endPoint = mousePos;


            //check plane cut the object,and change the bool value of isplanecuttheobject;
            CheckObjectGoingToSlide(startPoint, endPoint);

            if (!isPlaneCutTheObject)
            {
                ResetTheHaddle();
                ResetTheLineRender();
                isInputAccepted = true;
                return;
            }
            else
            {
                ResetTheHaddle();
                isPlaneCutTheObject = false;
            }

            SlicerPlane plane = new SlicerPlane(MathExtension.CalculateNormal(startPoint,midPoint,endPoint),startPoint);
            SliceObject(plane);
        }

        
    }

    private void ResetTheLineRender()
    {
        lineRender.SetPosition(0, Vector3.zero);
        lineRender.SetPosition(1, Vector3.zero);
    }

    private void SetLineStartPosition(Vector3 startPosition)
    {
        lineRender.SetPosition(0, startPosition);
    }

    private void SetLineEndPosition(Vector3 endPostion)
    {
        lineRender.SetPosition(1, endPostion);
    }

    private void SliceObject(SlicerPlane plane)
    {
        sliceObject.SliceThisObject(plane);
    }
    
    private bool IsPointerInAvaliableArea(Vector3 pointerPos)
    {

        if(Physics.Raycast(pointerPos,Vector3.forward,5f,5))
        {
            return false;

        }
        else
        {
            return true;
        }
    }

    private void CheckObjectGoingToSlide(Vector3 startPoint,Vector3 endPoint)
    {
        float t = 0;

        while (t < 1)
        {
            if (Physics.Raycast(Vector3.Lerp(startPoint,endPoint,t), Vector3.forward, 10f, 5))
            {
                isPlaneCutTheObject = true;
                break;
            }

            t += 0.1f;

        }


    }

    private void SetTheStartHanddle()
    {
        haddels[0].SetActive(true);
        haddels[0].transform.position=Input.mousePosition;
    }

    private void SetTheEndHanddle()
    {
        haddels[1].SetActive(true);
        haddels[1].transform.position = Input.mousePosition;
    }

    private void ResetTheHaddle()
    {
        foreach (GameObject item in haddels)
        {
            item.SetActive(false);
        }



    }
    

}
