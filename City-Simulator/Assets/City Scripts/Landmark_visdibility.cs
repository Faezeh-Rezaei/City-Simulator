using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmark_visdibility : MonoBehaviour
{
    Ray ray = new Ray();
    float numHit = 0, numNothit = 0;
    float visibility;
    private UiManager uiManager;
    private int RaysToShoot = 360;
    public TextMesh visText;
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
    }

    void Update()
    {

    }
    public void CalculateVisiblity()
    {
        for (int i = 0; i < 31; i++)
        {
            ray.origin = new Vector3(this.transform.position.x, i, this.transform.position.z);
            for (int j = 0; j < 360; j++)
            {
                float angle = Mathf.Deg2Rad * j;
                ray.direction = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle));
                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 100f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    numHit++;
                }
                else
                {
                    numNothit++;
                }
            }
        }
        visibility = (numHit / (numNothit + numHit));
        Instantiate(visText, new Vector3(this.transform.position.x,40 ,this.transform.position.z), Quaternion.Euler(60, 0, 0));
        visText.text = visibility.ToString("0.00");
    }

}
