using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{

	public int color;
	protected bool isSameColor {get {return (colorSwitcher == null) ? false : color == colorSwitcher.CurrentColor;}}
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

		if(collider.GetType() == typeof(BoxCollider2D)) {
			BoxCollider2D box = collider as BoxCollider2D;
			if(box.autoTiling) {
				box.autoTiling = false;
				box.size = renderer.size;
			}
		}
	}


	void OnColorSwitch(int newColor) {
		if(newColor == color) {
			renderer.sprite = fullSprite;
			collider.enabled = true;
			BoxCollider2D box = collider as BoxCollider2D;
			Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, box.size, 0f);
			foreach(Collider2D c in cols)
				if(c.tag == "Player")
					c.GetComponent<Player>().Respawn();
		} else {
			renderer.sprite = emptySprite;
			collider.enabled = false;
		}
	}

}
