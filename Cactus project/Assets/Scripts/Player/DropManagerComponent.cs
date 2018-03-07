using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManagerComponent : MonoBehaviour {


	public static DropManagerComponent globalDropManager;
	public EauLancee[] eauPool;

	void Start () {
		globalDropManager = this;
	}

	public static void SpawnDrop(Vector3 position, float rot_Z)
	{
		for(int i = 0; i < globalDropManager.eauPool.Length; i++)
		{
			if(globalDropManager.eauPool[i].dispo)
			{
				globalDropManager.eauPool[i].transform.position = position;
				globalDropManager.eauPool [i].transform.rotation = Quaternion.Euler (0, 0, rot_Z);
				globalDropManager.eauPool [i].gameObject.SetActive (true);
				globalDropManager.eauPool [i].dispo = false;
				return;
			}
		}
	}

	public static void RemoveDrop(EauLancee removedDrop)
	{
		removedDrop.gameObject.SetActive (false);
		removedDrop.dispo = true;
	}
}
