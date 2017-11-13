using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	
	public static InputManager Instance;

	void Awake() {
		Instance = this;
	}

	private string[] spawnSlotKeys = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0"};

	public GameplayWorld ControlledGameplay;


	void FixedUpdate() {
		if (ControlledGameplay == null) return;

		if (ControlledGameplay.Champion != null && Input.GetMouseButton(1)) {
			ControlledGameplay.Champion.SetMoveTarget(MousePosToWorldPos());
		}
	}

	// Update is called once per frame
	void Update () {
		if (ControlledGameplay == null) return;

		if (ControlledGameplay.Champion != null && Input.GetMouseButtonDown(1)) {
			ControlledGameplay.Champion.SetMoveTarget(MousePosToWorldPos());
		}

		if (Input.GetMouseButtonDown(0)) {
			ControlledGameplay.SpawnUnit(MousePosToWorldPos());
		}

		for(int slot = 0; slot < spawnSlotKeys.Length; ++slot) {
			if (Input.GetKeyDown(spawnSlotKeys[slot])) {
				ControlledGameplay.TriggerSlot(slot);
			}			
		}
	}


	public static Vector3 MousePosToWorldPos()
	{		
		Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		Vector3 worldPos;
		LayerMask mask = 1 << 8;
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000f, mask))
		{
			worldPos = hit.point;
		}
		else
		{
			worldPos = Camera.main.ScreenToWorldPoint(mousePos);
		}
		return worldPos;
	}
}
