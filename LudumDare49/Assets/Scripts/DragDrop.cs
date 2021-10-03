using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Part of the drag and drop code was created with the help of the YouTube Tutorial "Drag and Drop in Unity - 2021 Tutorial" from "Tarodev" - Thank you for offering free learning.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))]

public class DragDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    [SerializeField] private float speed = 10;

    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    public GameObject wand;

    [SerializeField] private int layerMoving = 8;
    [SerializeField] private int layerObject = 7;
    [SerializeField] private int layerOutside = 11;


    void Awake()
    {
        cam = Camera.main;
        rb = this.GetComponent<Rigidbody2D>();

        // do this better... or dont... probably dont
        wand = GameObject.Find("Wand");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    /// <summary> Gets a Vector 3 of the screen to world point the mouse is hitting.</summary>
    private Vector3 GetMousePosition(){
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    void OnMouseDown(){        
        ResetRB();
        rb.isKinematic = true;
        Wandgraphics.Instance.WandOn(true);
            // Reset line
        lineRenderer.SetPosition(1, Vector3.zero);
        lineRenderer.SetPosition(0, Vector3.zero);

        lineRenderer.enabled = true;

        // offset from the pivot to where the mouse hit
        offset = transform.position - GetMousePosition();

        this.gameObject.layer = layerMoving;
    }

    void OnMouseUp(){
        rb.isKinematic = false;
        Wandgraphics.Instance.WandOn(false);

        lineRenderer.enabled = false;
        this.gameObject.layer = layerObject;
    }

    void OnMouseDrag(){
        // changes position of the object
        transform.position = Vector3.MoveTowards(transform.position, GetMousePosition() + offset, speed * Time.deltaTime);

        lineRenderer.SetPosition(0, GetWandPosition());
        lineRenderer.SetPosition(1, this.transform.position);
    }


    Vector3 GetWandPosition(){
        Vector3 position = wand.transform.position;
        position.z = 0;
        return position;
    }

    void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.layer == layerOutside){
             var thing = gameObject.transform.GetComponent<Thing>();
             if(thing != null)
                ThingPool.Instance.ReturnToPool(thing);
         }
     }

     void ResetRB(){
        rb.velocity = new Vector3(0f,0f,0f); 
     }
}
