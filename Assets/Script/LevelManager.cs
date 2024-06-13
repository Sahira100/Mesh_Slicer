using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public UIManager uiManager;
    public float offset;

    public void CheckSliceObjectPropterties(float frontPartPresentage, Vector3 sliceNormal)
    {
        //check the condition  to the level pass 
            //if almost to 50 % pass otherwise Fail
                //if passs
                   //to gameManager to load next level


        //show the reasult
        PassSliceDataToUiManager(frontPartPresentage, sliceNormal);
    }


    public void PassSliceDataToUiManager(float frontPartPresentage, Vector3 sliceNormal)
    {
        Debug.Log("Slice it " + Mathf.Round(frontPartPresentage) + "%");
        Debug.Log("Slice Normal :" + sliceNormal);

        Vector3 frontTextPos = this.transform.position + (sliceNormal * offset);
        Vector3 bakcTextPos = this.transform.position + (sliceNormal * -1f * offset);

        float angle = Mathf.Acos(Vector3.Dot(sliceNormal, Vector3.right)) * 180f * (1 / Mathf.PI);

        Quaternion frontTextRot;
        Quaternion backTextRot;

        if (frontTextPos.y > 0)
        {
            frontTextRot = Quaternion.Euler(0f, 0f, (angle - 90f));

            backTextRot = Quaternion.Euler(0f, 0f, -1f * (90f - angle));
        }

        else
        {
            frontTextRot = Quaternion.Euler(0f, 0f, -1f * (angle - 90f));

            backTextRot = Quaternion.Euler(0f, 0f, (90f - angle));
        }


        uiManager.ShowPresentagePanel(frontTextPos, bakcTextPos, frontTextRot, backTextRot, Mathf.Round(frontPartPresentage));
    }
}
