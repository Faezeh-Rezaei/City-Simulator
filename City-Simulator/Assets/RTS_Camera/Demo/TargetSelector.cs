using UnityEngine;
using System.Collections;
using RTS_Cam;

[RequireComponent(typeof(RTS_Camera))]
public class TargetSelector : MonoBehaviour 
{
    private RTS_Camera cam;
    private new Camera camera;
    public string targetsTag;
    [SerializeField]
    GameObject exposure_prefab;

    private void Start()
    {
        cam = gameObject.GetComponent<RTS_Camera>();
        camera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            //Instantiate(exposure_prefab, ray.origin, Quaternion.identity);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //if (hit.transform.CompareTag(targetsTag))
                    //cam.SetTarget(hit.transform);
                //else
                    //cam.ResetTarget();
            }
        }
    }
}
