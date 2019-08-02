using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{

	[SerializeField]
	private new SpriteRenderer renderer;
	
	public delegate void ColorSwitch(int newColor);
	public event ColorSwitch onColorSwitch;

	[SerializeField]
	private int _currentColor = 0;
	public int CurrentColor {get { return pickedColors[_currentColor];}}

	[SerializeField]
	private Color[] _colors;
	public Color[] Colors {get { return _colors;}}

	[SerializeField]
	[Tooltip("The colors' index the player picked, these are the ones the game will loop through")]
	private List<int> pickedColors = new List<int>();

	[SerializeField]
	private Delay delay = new Delay(2f);

	private void Awake() {
		SwitchColor(0);
		
	}

	private void Update() {
		delay += Time.deltaTime;
		if(!delay.isDelayed()) {
			delay.Reset();
			SwitchColor();
		}
	}

	private void SwitchColor(int color) {
		_currentColor = color;
		onColorSwitch?.Invoke(CurrentColor);
		renderer.color = Colors[CurrentColor];
	}

	private void SwitchColor() {
		_currentColor = (++_currentColor) % pickedColors.Count;
		SwitchColor(_currentColor);
	}

	public void SetDelay(float newDelay) {
		delay = new Delay(newDelay);
	}

}
