using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] Image buttonImage;
    [SerializeField] Sprite mute;
    [SerializeField] Sprite sound;
    public void ToggleMuteAll(){
        if(Audiomanager.Instance.ToggleMuteAll()){
            buttonImage.sprite = mute;
        } else{
            buttonImage.sprite = sound;
        }
    }
}
