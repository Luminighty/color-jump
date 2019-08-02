using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Counter {

	public Counter() {
		count = 1;
	}
	public Counter(int count) {
		this.count = count;
	}

	public int count;
	private int currentCount = 0;

	/// <summary>
	/// Restarts the counter setting it to 0 by default
	/// </summary>
	/// <param name="startValue">Overwrites the default value (0)</param>
	public void Restart(int startValue = 0) {
		currentCount = startValue;
	}

	/// <summary>
	/// Did the counter reach the max value
	/// </summary>
	public bool hasMore(int amount = 1) {
		return count >= currentCount + amount;
	}
	public bool isZero() {
		return currentCount == 0;
	}

	/// <summary>
	/// Adds Count to the counter if it hasMore()
	/// </summary>
	/// <param name="count">Count to add</param>
	/// <returns>Returns true if it could add it</returns>
	public bool Count(int count = 1) {
		if(hasMore(count)) {
			currentCount += count;
			return true;
		}
		return false;
	}

	public static bool operator==(Counter c, int count) { return c.currentCount == count; }
	public static bool operator!=(Counter c, int count) { return c.currentCount != count; }

	// override object.Equals
	public override bool Equals(object obj)
	{	
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		return base.Equals (obj);
	}
	
	// override object.GetHashCode
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public static Counter operator++(Counter c) {
		c.Count();
		return c;
	}
	public static Counter operator--(Counter c) {
		c.Count(0);
		return c;
	}
	public static Counter operator +(Counter c, int count) {
		c.count += count;
		return c;
	}
	public static Counter operator -(Counter c, int count) {
		c.count -= count;
		return c;
	}

}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Counter))]
public class CounterDrawer: PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position,label, property);
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;
		Rect countRect = new Rect (position.x, position.y, position.width, position.height);
		EditorGUI.PropertyField (countRect, property.FindPropertyRelative ("count"), GUIContent.none);
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
		
	}
}
#endif