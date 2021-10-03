using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectSpawner : MonoBehaviour
{
    private Camera cam;
    private ParticleSystem particle;

    void Awake(){
        cam = Camera.main;
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Listens for mouse button clicks 
        if (Input.GetMouseButtonDown(1)){
            Spawn();
        }
    }


       /// <summary> Gets an object out of pool and resets it.</summary>
    private void Spawn()
    {
        // get object out of pool
        var thing = ThingPool.Instance.Get();

        if(thing != null){
        // reset the object to the start position
        thing.transform.rotation = transform.rotation;

        //thing.transform.position = transform.position;
        var spawnposition = cam.ScreenToWorldPoint(Input.mousePosition);
        spawnposition.z = 0;
        thing.transform.position = spawnposition;

        thing.gameObject.SetActive(true);
        
        if(particle != null){
            particle.Play();
        }

        Audiomanager.Instance.Play("Spawn");

        } else{
            Debug.Log("No objects anymore!");
        }
    }
}
