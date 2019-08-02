using UnityEngine;

[System.Serializable]
public abstract class ShapeCast {

	public Vector2 offset;
	[SerializeField]
	protected LayerMask layerMask;
	[SerializeField]
	protected Color debugColor = Color.red;
	[SerializeField]
	protected bool isDebugging;

	public abstract void OnDrawGizmos(Vector3 position);
	public abstract Collider2D Cast(Vector2 position);
	public abstract Collider2D[] CastAll(Vector2 position);
	public Collider2D Cast(Vector3 position) {return Cast(new Vector2(position.x, position.y));}
	public Collider2D[] CastAll(Vector3 position) {return CastAll(new Vector2(position.x, position.y));}

}

[System.Serializable]
public class CircleCast : ShapeCast {

	public float radius;

	public override Collider2D Cast(Vector2 position) {
		return Physics2D.OverlapCircle(position + offset, radius, layerMask);
	}

	public override Collider2D[] CastAll(Vector2 position) {
		return Physics2D.OverlapCircleAll(position + offset, radius, layerMask);
	}

	public override void OnDrawGizmos(Vector3 position) {
		if(!isDebugging)
			return;
		Color oldColor = Gizmos.color;
		Gizmos.color = debugColor;
		Vector3 offset = new Vector3(this.offset.x, this.offset.y, 0f);
		position += offset;
		Gizmos.DrawWireSphere(position, radius);
		Gizmos.color = oldColor;

	}

}

[System.Serializable]
public class RectangleCast : ShapeCast {

	public Vector2 size;
	public float angle;

	public override Collider2D Cast(Vector2 position) {
		return Physics2D.OverlapBox(position + offset, size, angle, layerMask);
	}

	public override Collider2D[] CastAll(Vector2 position) {
		return Physics2D.OverlapBoxAll(position + offset, size, angle, layerMask);
	}

	public override void OnDrawGizmos(Vector3 position) {
		if(!isDebugging)
			return;
		Color oldColor = Gizmos.color;
		Gizmos.color = debugColor;
		Vector3 offset = new Vector3(this.offset.x, this.offset.y, 0f);
		Vector3 size = new Vector3(this.size.x, this.size.y, 0f);
		position += offset;
		Gizmos.DrawWireCube(position, size);
		Gizmos.color = oldColor;
	}
}