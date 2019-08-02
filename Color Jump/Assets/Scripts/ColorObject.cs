using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
	
	public int color;

	private new Collider2D collider;
	private static ColorSwitcher colorSwitcher;

	[SerializeField]
	private Sprite fullSprite;
	[SerializeField]
	private Sprite emptySprite;

	[SerializeField]
	private new SpriteRenderer renderer;

	private void Awake() {
		collider = GetComponent<Collider2D>();
		if(colorSwitcher == null)
			colorSwitcher = FindObjectOfType<ColorSwitcher>();
		if(colorSwitcher != null) {
			colorSwitcher.onColorSwitch += OnColorSwitch;
			renderer.color = colorSwitcher.Colors[color];
		}
	}


	void OnColorSwitch(int newColor) {
		if(newColor == color) {
			renderer.sprite = fullSprite;
			collider.enabled = true;
		} else {
			renderer.sprite = emptySprite;
			collider.enabled = false;
		}
	}

}
