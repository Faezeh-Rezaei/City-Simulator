using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sky_Exposure : MonoBehaviour
{
    Ray ray = new Ray();
    float numHit = 0, numNothit = 0;
    float exposure;
    private UiManager uiManager;
    public TextMesh exText;
    void Start()
    {

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        for (int i = 180; i < 270; i++)
        {
            for (int j = 0; j < 360; j++)
            {
                this.transform.rotation = Quaternion.Euler(i, j, 0);
                ray.origin = this.transform.position;
                ray.direction = transform.forward;
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
        exposure = (numNothit / (numNothit + numHit));
        Instantiate(exText, this.transform.position, Quaternion.Euler(60,0,0));
        exText.text = exposure.ToString("0.00");
    }



}
