using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public string Name;
    protected GameSceneController gameSceneController; //referenciando o GameScene ( script ) para utiliza-lo
    protected float halfWidth;
    protected float halfHeight;
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        //encontrou a Classe GameScene
        gameSceneController = FindObjectOfType<GameSceneController>();
        spriteRenderer = GetComponent<SpriteRenderer>(); //--acessando o objeto SpriteRender do 

        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public void SetColor(float red, float green, float blue)
    {
        Color newColor = new Color(red, green, blue);
        spriteRenderer.color = newColor; 
    }
}
