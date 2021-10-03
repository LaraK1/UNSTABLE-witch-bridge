using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This definitely doesn't exist only because I was too lazy to get to grips with shaders.
public class MagicSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void OnMouseOver(){
        spriteRenderer.sprite = sprite2;
    }

    void OnMouseExit(){
        spriteRenderer.sprite = sprite1;
    }
}
