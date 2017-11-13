using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefabs : MonoBehaviour {

	public List<GameObject> List;

	public GameObject GetByName(string name) {
		foreach(var it in List) if (it.name == name) return it;
		throw new System.Exception(string.Format("Prefab {0} not found", name));
	}
}
