using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SpriteReflected : MonoBehaviour {
	
    public SpriteRenderer player;
    private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () 
	{
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        spriteRenderer.sprite = player.sprite;

	}
}
