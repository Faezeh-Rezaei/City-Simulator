using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapScript : MonoBehaviour
{
    private int noX, noZ;
    private float xO, zO, width, height, maxTemp = 0, timeCounter;
    public float updateTime;
    public GameObject sun;
    float[,] temps, cfh;
    Vector3[,] pointsM;
    public Material baseMat;
    float stepX, stepZ;
    private bool calculateHeatmap = false, calcCFH = false;
    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        width = bounds.extents.x * transform.localScale.x * 2;
        height = bounds.extents.z * transform.localScale.z * 2;
        noX = Mathf.RoundToInt(width);
        noZ = Mathf.RoundToInt(height);


        pointsM = new Vector3[noX, noZ];
        temps = new float[noX, noZ];
        cfh = new float[noX, noZ];
        stepX = width / noX;
        stepZ = height / noZ;
        xO = transform.position.x - width / 2;
        zO = transform.position.z + height / 2;
        for (int i = 0; i < noX; i++)
        {
            for (int j = 0; j < noZ; j++)
            {
                pointsM[i, j] = new Vector3(xO + (stepX / 2 + stepX * i), transform.position.y, zO - (stepZ / 2 + stepZ * j));
            }
        }
    }
    void Update()
    {

        //making the rays and calculating the heat for each point
        for (int i = 0; i < noX; i++)
        {
            for (int j = 0; j < noZ; j++)
            {
                Vector3 fromPosition = pointsM[i, j];
                Vector3 toPosition = sun.transform.position;
                Vector3 direction = toPosition - fromPosition;
                RaycastHit hit;
                if (toPosition.y < 0)
                {
                    if (temps[i, j] > 0)
                    {
                        temps[i, j]--;
                    }
                    if (calcCFH)
                    {
                        if (cfh[i, j] > 0)
                        {
                            cfh[i, j]--;
                        }
                    }
                }
                else
                {
                    if (Physics.Raycast(fromPosition, direction, out hit, Mathf.Infinity))
                    {
                        if (hit.transform.tag == "Building")
                        {
                            if (temps[i, j] > 0)
                            {
                                temps[i, j]--;
                            }
                            if (calcCFH)
                            {

                                if (cfh[i, j] > 0)
                                {
                                    cfh[i, j]--;
                                }
                            }
                        }
                    }
                    else
                    {
                        temps[i, j]++;
                        if (calcCFH)
                        {
                            cfh[i, j]++;
                        }
                    }
                }
            }


        }
        if (calculateHeatmap)
        {
            FillHeatmap(temps);
        }
    }
    void ResetColor()
    {
        if (timeCounter < updateTime)
        {
            timeCounter += Time.deltaTime;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            enabled = false;
            GetComponent<Renderer>().material = baseMat;
        }
    }

    public void CalculateHeatmap()
    {
        if (calculateHeatmap)
        {
            calculateHeatmap = false;
            ResetColor();
        }
        else
        {
            calculateHeatmap = true;
        }
    }

    private void FillHeatmap(float[,] itemps)
    {
        for (int i = 0; i < noX; i++)
        {
            for (int j = 0; j < noZ; j++)
            {
                if (itemps[i, j] > maxTemp)
                    maxTemp = itemps[i, j];
            }
        }
        //coloring the heatmap
        Texture2D texture = new Texture2D(noX, noZ);
        GetComponent<MeshRenderer>().materials = new Material[0];
        GetComponent<Renderer>().material.mainTexture = texture;
        GetComponent<Renderer>().material.mainTexture.filterMode = FilterMode.Point;
        for (int i = 0; i < noX; i++)
        {
            for (int j = 0; j < noZ; j++)
            {
                float r = itemps[i, j] / maxTemp;
                float b = 1 - (itemps[i, j] / maxTemp);
                Color color = new Color(r, 0, b);
                texture.SetPixel(Mathf.RoundToInt(width) - i, j, color);

            }
        }
        texture.Apply();
        ResetColor();
    }
    public void CalculateCFH()
    {
        if (calcCFH)
        {
            calcCFH = false;
            calculateHeatmap = false;
            FillHeatmap(cfh);
        }
        else
        {
            cfh = new float[noX, noZ];
            calcCFH = true;
        }
    }
}
