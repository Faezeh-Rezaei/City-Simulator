using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exposure_manager : MonoBehaviour
{
    [SerializeField]
    GameObject exposure_prefab;
    public Camera camera;
    private bool shift = false;
    void Update()
    {
        if (shift)
        {

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 pos = hit.point;
                    Instantiate(exposure_prefab, pos, Quaternion.identity);
                }
            }
        }
    }
    void OnGUI()
    {
        Event e = Event.current;
        if (e.shift)
        {
            shift = true;
        }
        else
        {
            shift = false;
        }
    }

}
