using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	private ColorSwitcher switcher;
	private Animator animator;
	public int index;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	public void OnClick() {
		transform.parent.GetComponent<ColorSlotHolder>().ClickSlot(index);	
	}

	private void OnEnable() {
		switcher = FindObjectOfType<ColorSwitcher>();
		switcher.onColorSwitch += onColorSwitch;
	}

	private void OnDisable() {
		if(switcher != null)
			switcher.onColorSwitch -= onColorSwitch;
		
	}

	void onColorSwitch(int newColor) {
		animator.SetBool("currentColor", newColor == switcher.PickedColors[index]);
	}

}
