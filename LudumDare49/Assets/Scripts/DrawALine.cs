using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawALine : MonoBehaviour
{

    private Camera cam;
    private LineRenderer lineRenderer;

    void Awake(){
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            lineRenderer.enabled = true;
        }

        if(Input.GetMouseButtonUp(0)){
            lineRenderer.enabled = false;
        }

        if(Input.GetMouseButton(0)){
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, GetMousePosition());
        }
    }

    private Vector3 GetMousePosition(){
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
