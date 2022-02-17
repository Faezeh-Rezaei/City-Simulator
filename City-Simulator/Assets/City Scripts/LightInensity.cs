using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInensity : MonoBehaviour
{
    Ray ray = new Ray();
    private GameObject sun;
    private float lightInensity, angle;
    private UiManager uiManager;
    public Vector3 normal;
    public TextMesh liText;
    private TextMesh ti;
    public GameObject bar1, bar2, bar3, bar4, bar5;
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        sun = GameObject.Find("Directional Light");
        ti = Instantiate(liText, new Vector3(this.transform.position.x - 5, this.transform.position.y, this.transform.position.z + 14), Quaternion.Euler(60, 0, 0));
        bar1.SetActive(false);
        bar2.SetActive(false);
        bar3.SetActive(false);
        bar4.SetActive(false);
        bar5.SetActive(false);
    }

    void Update()
    {
        Vector3 fromPosition = this.transform.position;
        Vector3 toPosition = sun.transform.position;
        Vector3 direction = toPosition - fromPosition;
        ray.origin = fromPosition;
        ray.direction = direction;
        angle = Mathf.Abs(Vector3.SignedAngle(direction, normal, Vector3.left));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            ti.text = "0.0";
        }
        else
        {
            if (angle >= 0f && angle <= 90)
            {
                lightInensity = 100 * Mathf.Cos(Mathf.Deg2Rad * angle);
                ti.text = lightInensity.ToString("0.00") + "%";
                UpdateBars((int)lightInensity / 20);

            }
        }


    }
    public void UpdateBars(int num)
    {
        Debug.Log(num);
        if (num > 0)
        {
            bar1.SetActive(true);
            if (num >= 1)
            {
                bar2.SetActive(true);
                if (num >= 2)
                {
                    bar3.SetActive(true);
                    if (num >= 3)
                    {
                        bar4.SetActive(true);
                        if (num == 4)
                        {
                            bar5.SetActive(true);
                        }
                        else
                        {

                            bar5.SetActive(false);
                        }
                    }
                    else
                    {
                        bar4.SetActive(false);

                    }
                }
                else
                {

                    bar3.SetActive(false);
                }
            }
            else
            {
                bar2.SetActive(false);

            }
        }
        else
        {
            bar1.SetActive(false);
        }

    }
}
