using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class MultiRaycast
{
	[SerializeField]
	private List<VectorPair> pairs = new List<VectorPair>();
	[SerializeField]
	private float distance = 0.3f;
	[SerializeField]
	private LayerMask layerMask = 0;
	[SerializeField]
	private bool isDebugging = false;
	[SerializeField]
	private Color debugColor = Color.green;

	public MultiRaycast() {}

	public MultiRaycast(float distance, int layerMask) {
		this.distance = distance;
		this.layerMask = layerMask;
	}

	public void AddPair(Vector2 offset, Vector2 direction) {
		pairs.Add(new VectorPair(offset, direction));
	}

	/// <summary>
	/// Casts multiple raycasts
	/// </summary>
	/// <param name="position">Starting position</param>
	/// <returns>True if all of the raycasts hit something, otherwise false</returns>
	public bool HitAll(Vector3 position, out Vector2 notFoundPosition, GameObject ignoreObject = null) {
		notFoundPosition = new Vector2();
		foreach(VectorPair p in pairs) {
			RaycastHit2D[] hits = Cast(p, position, distance, layerMask);
			bool found = false;
			foreach(RaycastHit2D hit in hits) {
				if(hit.transform.gameObject != ignoreObject)
					found = true;
			}
			notFoundPosition = p.offset;
			if(!found)
				return false;
		}
		return true;
	}

	/// <summary>
	/// Casts multiple raycasts
	/// </summary>
	/// <param name="position">Starting position</param>
	/// <returns>True if any of the raycasts hit something, otherwise false</returns>
	public bool HitAny(Vector3 position, out Vector2 foundPosition, GameObject ignoreObject = null) {
		foundPosition = new Vector2();
		foreach(VectorPair p in pairs) {
			RaycastHit2D[] hits = Cast(p, position, distance, layerMask);
			foreach(RaycastHit2D hit in hits)
				if(hit && hit.transform.gameObject != ignoreObject) {
					foundPosition = p.offset;
					return true;
				}
		}
		return false;
	}
	/// <summary>
	/// Casts multiple raycasts
	/// </summary>
	/// <param name="position">Starting position</param>
	/// <returns>True if any of the raycasts hit something, otherwise false</returns>
	public bool HitAny(Vector3 position, GameObject ignoreObject = null) {
		Vector2 foundPosition;
		return HitAny(position, out foundPosition, ignoreObject);
	}

	/// <summary>
	/// Casts multiple raycasts
	/// </summary>
	/// <param name="position">Starting position</param>
	/// <returns>True if all of the raycasts hit something, otherwise false</returns>
	public bool HitAll(Vector3 position, GameObject ignoreObject = null) {
		Vector2 notFoundPosition;
		return HitAll(position, out notFoundPosition, ignoreObject);
	}


	private RaycastHit2D[] Cast(VectorPair pair, Vector3 position, float distance, int layerMask) {
		Vector2 origin = new Vector2(position.x + pair.offset.x, position.y + pair.offset.y);
		return Physics2D.RaycastAll(origin, pair.direction, distance, layerMask);
	}

	public void OnDrawGizmos(Vector3 position) {
		if(!isDebugging)
			return;
		Color prevColor = Gizmos.color;
		Gizmos.color = debugColor;

		foreach(VectorPair p in pairs) {
			Vector3 from = new Vector3(position.x + p.offset.x, position.y + p.offset.y, position.z);
			Vector3 direction = new Vector3(p.direction.x, p.direction.y, 0);
			Gizmos.DrawLine(from, from + direction * distance);
		}

		Gizmos.color = prevColor;
	}

	[System.Serializable]
	struct VectorPair {
		public VectorPair(Vector2 offset, Vector2 direction) {
			this.offset = offset;
			this.direction = direction;
		}

		public Vector2 offset;
		public Vector2 direction;
	}

}
