
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorHolder : MonoBehaviour
{
	public ColorSwitcher playerColorSwitcher;
	public ColorSlotHolder colorSlotHolder;
	public GameObject colorPrefab;

	private void Start() {
		AddColors(playerColorSwitcher.Colors);
		gameObject.SetActive(false);
	}

	void AddColors(Color[] colors) {
		foreach(Transform child in transform)
			Destroy(child.gameObject);
		for(int i = 0; i < colors.Length; i++) {
			Color c = colors[i];
			GameObject slot = Instantiate(colorPrefab, transform.position,Quaternion.identity) as GameObject;
			slot.transform.SetParent(transform);
			slot.GetComponent<Image>().color = c;
			ColorSlot s = slot.GetComponent<ColorSlot>();
			s.index = i;
			s.colorSlotHolder = colorSlotHolder;
		}
	}

}
