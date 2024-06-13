using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject presentagePanel;

    public void ShowPresentagePanel(Vector3 frontTextPos,Vector3 backTextPos,Quaternion frontTxtRot,Quaternion backTxtRot,float frontPresentage)
    {
        presentagePanel.SetActive(true);
        //acces to presentagePanelScript;
        ThePresentagePanel presentagePanelScript=presentagePanel.GetComponent<ThePresentagePanel>();

        presentagePanelScript.SetTheTextPos(frontTextPos,backTextPos);
        presentagePanelScript.SetTheTextRot(frontTxtRot, backTxtRot);
        presentagePanelScript.SetFrontText(frontPresentage);
        presentagePanelScript.SetBackText((100f - frontPresentage));

    }


    public void PresentagePanelWorkDone()
    {
        
    }


    public void GemPanelWorkDone()
    {

    }



}
