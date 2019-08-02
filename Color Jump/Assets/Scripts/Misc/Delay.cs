using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Delay {

	public Delay() {
		delay = 0.5f;
	}
	public Delay(float delay) {
		this.delay = delay;
	}

	public float delay;
	private float currentDelay = 0f;

	/// <summary>
	/// Sets the delay to maximum
	/// </summary>
	public void Stop() {
		currentDelay = delay;
	}

	/// <returns>True if the delay is still on</returns>
	public bool isDelayed() {
		return currentDelay < delay;
	}

	/// <summary>
	/// Sets the delay to 0 (Use this for START)
	/// </summary>
	public void Reset() {
		currentDelay = 0f;
	}

	/// <summary>
	/// Steps the delay forward
	/// </summary>
	/// <param name="deltaTime">Step size</param>
	/// <returns>Returns the same value as isDelayed() after calling Step</returns>
	public bool Step(float deltaTime) {
		if(isDelayed())
			currentDelay += deltaTime;
		return isDelayed();
	}
	public static Delay operator +(Delay d, float delay) {
		d.Step(delay);
		return d;
	}
	public static Delay operator -(Delay d, float delay) {
		d.Step(-delay);
		return d;
	}

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Delay))]
public class DelayDrawer: PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		label = EditorGUI.BeginProperty(position,label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		Rect delayRect = new Rect (position.x, position.y, position.width, position.height);
		EditorGUI.PropertyField (delayRect, property.FindPropertyRelative ("delay"), GUIContent.none);
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
		
	}
}
#endif