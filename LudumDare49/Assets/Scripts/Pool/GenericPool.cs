using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This code was created with the help of the YouTube Tutorial "Object Pooling (in depth) [...]" by Jason Weimann
public abstract class GenericPool<T> : MonoBehaviour where T : Component
{
    // prefab / type management
    // for editing in the inspector 
    [SerializeField]
    private List<T> listOfPrefabs = new List<T>();

    private int currentPrefabCount;
    private int prefabCount;

    // pool
    public static GenericPool<T> Instance { get; private set; }
    private List<T> objects = new List<T>();

    [SerializeField] GameObject poolObject;

    [SerializeField] Slider uiElement;

    private void Awake()
    {    
        // simple generic singleton
        Instance = this;

        // check if prefabs are assigned
        prefabCount = listOfPrefabs.Count;
        if (prefabCount < 1)
        {
            Debug.LogWarning("No prefabs of objects are found.");
        }
    }

    private void Start()
    {
        if(poolObject != null){
            // add all prefabs to the pool
            AddObjects(prefabCount);
        }
        else{
            poolObject = this.gameObject;
        }

    }

    /// <summary>Get random object out of pool.</summary>
    public T Get()
    {
        int objectCount = objects.Count;

        if(objectCount <= 0){
            return null;
        }

        // random id
        int randomId = Random.Range(0, objectCount);

        // remove object out of pool and return it
        T currentObject = objects[randomId];
        objects.RemoveAt(randomId);

        UpdateUI();

        return currentObject;
       
    }

    /// <summary>Objects can return themself to the pool.</summary>
    /// <param name="objectToReturn">Object that should be returned to the pool.</param>
    public void ReturnToPool(T objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Add(objectToReturn);

        UpdateUI();
    }

    /// <summary>Instantiate new object and add it to pool.</summary>
    /// <param name="count">Number of objects to be added.</param>
    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = GameObject.Instantiate(listOfPrefabs[i]);
            newObject.transform.parent = poolObject.transform;
            newObject.gameObject.SetActive(false);
            objects.Add(newObject);

            currentPrefabCount++;
            if (currentPrefabCount > prefabCount)
            {
                currentPrefabCount = 0;
            }
        }

        UIStart();
        UpdateUI();
    }

    public void ReturnAll(){
        
        for(int i = 0; i < poolObject.transform.childCount; i++)
        {
            T thing = poolObject.transform.GetChild(i).GetComponent<T>();
            if(thing.gameObject.activeSelf)
                ReturnToPool(thing);
        }

        UpdateUI();
    }

    public int PoolObjectCount(){
        int count = objects.Count;
        return count;
    }

    private void UIStart(){
        if(uiElement!=null){
            uiElement.maxValue = PoolObjectCount();
        }
    }

    private void UpdateUI(){
        if(uiElement != null){
            uiElement.value = PoolObjectCount();
        }
    }

}
