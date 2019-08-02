using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	public int index;
	public void OnClick() {
		transform.parent.GetComponent<ColorSlotHolder>().ClickSlot(index);	
	}
}
