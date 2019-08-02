using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSlot : MonoBehaviour
{
	public int index;
	public ColorSlotHolder colorSlotHolder;

	public void OnClick() {
		colorSlotHolder.SelectColor(index);
	}
}
