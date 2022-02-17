using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Sun_Rotation : MonoBehaviour
{

    public float theta = 1f, angle;
    private float angleFull, timeInSec;
    private int mins, hours, day;
    private string minsDisplay, hoursDisplay, secsDisplay;
    private UiManager uiManager;
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }
    void Update()
    {
        Vector3 dir = this.transform.position - Vector3.zero;
        angle = Vector3.SignedAngle(dir, Vector3.up, Vector3.right);
        angleFull = 180 - angle;
        timeInSec = (angleFull / 360) * 86400;
        mins = ((int)timeInSec / 60) % 60;
        hours = (((int)timeInSec / 60) / 60);
        this.transform.RotateAround(Vector3.zero, Vector3.right, theta * Time.deltaTime);
        uiManager.UpdateTime(hours, mins);

    }

}
