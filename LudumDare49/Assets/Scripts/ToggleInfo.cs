using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInfo : MonoBehaviour
{
    [SerializeField] GameObject instructions;

    public void ToggleInstructions(){
        instructions.SetActive(!instructions.activeSelf);
    }
}
