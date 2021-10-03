using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandgraphics : MonoBehaviour
{
    
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private SpriteRenderer spriteWand;

    public static Wandgraphics Instance;

    void Awake(){
        Instance = this;
    }

    public void WandOn(bool on){
        if(!on){
            Audiomanager.Instance.Stop("Magic");
            spriteWand.sprite = sprite1;
        } else{
            spriteWand.sprite = sprite2;
            Audiomanager.Instance.Play("Magic");
        }
    }

}
