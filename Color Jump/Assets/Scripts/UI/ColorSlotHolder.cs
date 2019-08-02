using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorSlotHolder : MonoBehaviour
{
	
	public ColorHolder colorHolder;
	public ColorSwitcher playerColorSwitcher;
	public GameObject slotPrefab;
	public static int selectedSlot = -1;

	private void Start() {
		AddSlots(playerColorSwitcher.PickedColors, playerColorSwitcher.Colors);
	}

	private void Update() {
		if(Input.GetButtonDown("Select")) {
			if(colorHolder.gameObject.activeSelf) {
				colorHolder.gameObject.SetActive(false);
				EventSystem.current.SetSelectedGameObject(null);

			} else if(playerColorSwitcher.GetComponent<PlayerMovement>().CanOpenMenu()) {
				colorHolder.gameObject.SetActive(true);
				EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
				selectedSlot = 0;

			}
		}
	}

	void AddSlots(List<int> pickedColors, Color[] colors) {
		foreach(Transform child in transform)
			Destroy(child.gameObject);
		for(int i = 0; i < pickedColors.Count; i++) {
			int c = pickedColors[i];
			GameObject slot = Instantiate(slotPrefab, transform.position,Quaternion.identity) as GameObject;
			slot.transform.SetParent(transform);
			Slot s = slot.GetComponent<Slot>();
			s.index = i;

			SetSlotColor(slot, colors[c]);
		}
		for(int i = 0; i < transform.childCount; i++) {
			Button btn = transform.GetChild(i).GetComponent<Button>();
			Navigation nav = btn.navigation;
			if(i != 0)
				nav.selectOnLeft = transform.GetChild(i-1).GetComponent<Button>();
			if(i != transform.childCount-1)
				nav.selectOnRight = transform.GetChild(i+1).GetComponent<Button>();
			btn.navigation = nav;
		}
	}


	void SetSlotColor(GameObject slot, Color color) {
		slot.GetComponent<Image>().color = color;
	}

	public void ClickSlot(int slot) {
		selectedSlot = slot;
		EventSystem.current.SetSelectedGameObject(colorHolder.transform.GetChild(playerColorSwitcher.PickedColors[slot]).gameObject);
	}

	public void SelectColor(int color) {
		if(selectedSlot < 0) {
			Debug.LogWarning("Selected Slot is -1!");
			return;
		}
		playerColorSwitcher.PickedColors[selectedSlot] = color;
		if(playerColorSwitcher.CurrentColor == playerColorSwitcher.PickedColors[selectedSlot])
			playerColorSwitcher.UpdateColor();

		SetSlotColor(transform.GetChild(selectedSlot).gameObject, playerColorSwitcher.Colors[color]);
		EventSystem.current.SetSelectedGameObject(transform.GetChild(selectedSlot).gameObject);
		selectedSlot = -1;
	}



}
