using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_LightIntensity : MonoBehaviour
{
    public Camera camera;
    public GameObject intensity_prefab;
    private bool ctrl = false;
    void Start()
    {

    }

    void Update()
    {
        if (ctrl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 pos = hit.point;
                    Vector3 normal = hit.normal;
                    var ip = Instantiate(intensity_prefab, pos, Quaternion.Euler(60, 0, 0));
                    ip.GetComponent<LightInensity>().normal = normal;
                }
            }
        }
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.control)
        {
            ctrl = true;
        }
        else
        {
            ctrl = false;
        }    
    }

}
