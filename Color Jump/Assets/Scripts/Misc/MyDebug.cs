using UnityEngine;

public static class MyDebug {
	
	public static void DebugPoint(Vector3 point, Color color, float duration = 1f, float size = 0.25f) {
		Debug.DrawLine(point + (Vector3.up + Vector3.left) * size, point - (Vector3.up + Vector3.left) * size, color, duration);
		Debug.DrawLine(point + (Vector3.up + Vector3.right) * size, point - (Vector3.up + Vector3.right) * size, color, duration);
	}
	
	public static void DebugPoint(Vector3 point, float duration = 1f, float size = 0.25f) {
		DebugPoint(point, Color.blue, duration, size);
	}

	public static void DebugRectangle(Vector3 center, Vector3 size, Color color, float duration = 1f) {
		Vector3 bottomLeft = center - size / 2;
		float width = size.x;
		float height = size.y;
		Debug.DrawLine(bottomLeft, bottomLeft + (Vector3.right * width), color, duration);
		Debug.DrawLine(bottomLeft, bottomLeft + (Vector3.up * height), color, duration);
		Vector3 topRight = bottomLeft + size;
		Debug.DrawLine(topRight, topRight + (Vector3.down * height), color, duration);
		Debug.DrawLine(topRight, topRight + (Vector3.left * width), color, duration);
	}
	public static void DebugRectangle(Vector3 position, Vector3 size, float duration = 1f) {
		DebugRectangle(position, size, Color.blue, duration);
	}

	public static void DebugSpline(Vector3 position, Vector3 velocity, Vector3 gravity, float length, Color color, float duration = 0.3f, float iterationDistance = 0.01f) {
		Vector3 lastPos = position;
		for(float i = 0; i < length; i += iterationDistance) {
			Vector3 newPos = lastPos + (velocity * iterationDistance);
			Debug.DrawLine(lastPos, newPos, color, duration);
			lastPos = newPos;
			velocity += gravity * iterationDistance;
		}
	}

	
	public static void DebugSpline(Vector3 position, Vector3 velocity, Vector3 gravity, float length = 1f) {
		DebugSpline(position, velocity, gravity, length, Color.red);
	}

}