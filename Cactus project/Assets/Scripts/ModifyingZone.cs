using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyingZone : MonoBehaviour 
{

	public bool modify = false;
	public SpriteRenderer spriteRend;
	public Animator anim;

	void Start () 
	{
		
	}

	void Update () 
	{
		if (modify == true)
		{
			anim.SetBool ("Modify", modify);
		}
	}

	public void Modified()
	{
		modify = true;
	}
}
