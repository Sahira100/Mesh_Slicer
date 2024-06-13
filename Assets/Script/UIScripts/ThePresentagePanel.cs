using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThePresentagePanel : MonoBehaviour
{
    public float updatingSpeed;
    [SerializeField]
    private Text frontText;
    [SerializeField]
    private Text backText;

    private float fAmount;
    private float bAmount;

    private float updatingTime;

    private bool isJobDone;

    public bool IsJobDone { get => isJobDone;}


    private void Update()
    {
        UpdatingAnimtionText();
    }

    public void SetTheTextPos(Vector3 frontTextPos,Vector3 backTextPos)
    {


        frontTextPos = Camera.main.WorldToScreenPoint(frontTextPos);
        backTextPos = Camera.main.WorldToScreenPoint(backTextPos);

        //set z value to zero
        frontTextPos.z = 0f;
        backTextPos.z = 0f;

        frontText.transform.position = frontTextPos;
        backText.transform.position = backTextPos;
    }

    public void SetTheTextRot( Quaternion frontTextRot,Quaternion backTextRot)
    {
        frontText.transform.rotation = frontTextRot;
        backText.transform.rotation = backTextRot;
    }

    public void SetFrontText(float presentageAmount)
    {
        fAmount = presentageAmount;
    }

    public void SetBackText(float presentageAmount)
    {
        bAmount = presentageAmount;
    }

    private bool UpdatingAnimtionText()
    {
        if (updatingTime >= 1)
        {
            isJobDone = true;
            return true;
        }

        if(fAmount >0 && bAmount > 0)
        {
            float fTextNumber = InterpolateToZeroTo(fAmount, updatingTime);
            float bTextNumber = InterpolateToZeroTo(bAmount, updatingTime);

            frontText.text = Mathf.RoundToInt(fTextNumber)+ "";
            backText.text = +Mathf.RoundToInt(bTextNumber)+ "";

            updatingTime += Time.deltaTime * updatingSpeed;
        }

        return false;
    }


    private float InterpolateToZeroTo(float number,float t)
    {
        if (t > 1)
        {
            t = 1;
        }

        float interpolateNumber = t * number;

        return interpolateNumber;

    }

    
}
