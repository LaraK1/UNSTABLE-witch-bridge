using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] GameObject witch;
    [SerializeField] Animator animatorUI;
    public static Gamemanager Instance;

    [SerializeField] GameObject instructions;


    [SerializeField] Vector3 startPositionWitch;

    [SerializeField] List<GameObject> levelObjects = new List<GameObject>();
    int levelCount;
    int currentLevel;
    void Awake()
    {
        Instance = this;
        currentLevel = 0;
    }

    void Start(){        
        levelCount = levelObjects.Count;

        startPositionWitch = new Vector3(0.6f, -2.25f, 0);
        witch.SetActive(false);

        Audiomanager.Instance.Play("BG");
    }

    public void StartGame(){
        witch.transform.position = startPositionWitch;

        animatorUI.SetTrigger("End");
        witch.SetActive(true);
    }

    public void End(){
        Audiomanager.Instance.Stop("Magic");
        animatorUI.SetTrigger("Start");
        witch.SetActive(false);
        instructions.SetActive(false);
        ThingPool.Instance.ReturnAll();
    }

    public void HasWon(){
        levelObjects[currentLevel].SetActive(false);

        currentLevel ++;
        if(currentLevel >= levelCount)
            currentLevel = 0;
        
        levelObjects[currentLevel].SetActive(true);

        End();
    }


}
