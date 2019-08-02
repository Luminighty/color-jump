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
	public int CurrentColor {get { return _pickedColors[_currentColor];}}

	[SerializeField]
	private Color[] _colors;
	public Color[] Colors {get { return _colors;}}

	[SerializeField]
	[Tooltip("The colors' index the player picked, these are the ones the game will loop through")]
	private List<int> _pickedColors = new List<int>();
	public  List<int> PickedColors {get { return _pickedColors;}}

	[SerializeField]
	private int _currentDelay = 0;
	
	[SerializeField]
	private Delay[] delays;

	private void Awake() {
		SwitchColor(0);
		
	}

	private void Update() {
		delays[_currentDelay] += Time.deltaTime;
		if(!delays[_currentDelay].isDelayed()) {
			delays[_currentDelay].Reset();
			_currentDelay = (++_currentDelay) % delays.Length;
			SwitchColor();
		}
	}

	public void UpdateColor() {
		SwitchColor(_currentColor);
	}

	private void SwitchColor(int color) {
		_currentColor = color;
		onColorSwitch?.Invoke(CurrentColor);
		renderer.color = Colors[CurrentColor];
	}

	private void SwitchColor() {
		_currentColor = (++_currentColor) % _pickedColors.Count;
		SwitchColor(_currentColor);
	}
}
