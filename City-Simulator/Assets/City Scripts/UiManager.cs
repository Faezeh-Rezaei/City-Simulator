using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private Text txtTime,txtCFH;
    [SerializeField]
    private RawImage helpImg;
    private Landmark_visdibility landmark;
    private Exposure_manager exposure;
    private HeatmapScript heatmap;
    bool showHelp = false, showHeatmap = false,calcCFH = false;
    Transform camT;



    private void Start()
    {
        camT = Camera.main.transform;
    }

    public void ShowHideHelp()
    {
        showHelp = !showHelp;
        helpImg.enabled = showHelp;
    }
    public void LandmarkVisibility()
    {
        landmark = GameObject.Find("landmark").GetComponent<Landmark_visdibility>();
        if (landmark != null)
        {
            landmark.CalculateVisiblity();
        }
    }
    public void RTSCamAngle(float a)
    {
        float angle = 90 - (90 * a);
        camT.localEulerAngles = new Vector3(angle, camT.localEulerAngles.y, camT.localEulerAngles.z);
    }


    public void StartStopHeatmap()
    {
        heatmap = GameObject.Find("Park").GetComponent<HeatmapScript>();
        showHeatmap = !showHeatmap;
        if (showHeatmap)
        {
            heatmap.CalculateHeatmap();
        }
    }

    public void UpdateTime(int hr, int min)
    {
        txtTime.text = hr + " : " + min.ToString("00");
    }    

    public void UpdateCFHButton()
    {
        if (calcCFH)
        {
            calcCFH = false;
            txtCFH.text = "Start CFH";
        }
        else
        {
            calcCFH = true;
            txtCFH.text = "Stop CFH";
        }

    }


}
