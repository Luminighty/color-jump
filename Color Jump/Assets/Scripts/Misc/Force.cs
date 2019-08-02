using UnityEngine;

[System.Serializable]
public class Force {

	[Tooltip("The velocity the force start with")]
	[SerializeField]
	public Vector2 velocity;
	[Tooltip("Change of velocity every frame * deltaTime")]
	[SerializeField]
	public Vector2 gravity;
	[Tooltip("How long the force lasts")]
	[SerializeField]
	public float aliveTime = 1.0f;
	[Tooltip("How strong the velocity is (overriding the first velocity) (1.0 override it fully, 0.0 doesn't do anything)")]
	[Range(0f, 1f)]
	[SerializeField]
	public float forceMultiplier = 0.9f;


	/// <param name="startVelocity">The velocity the force start with</param>
	/// <param name="gravity">Change of velocity every frame * deltaTime</param>
	/// <param name="forceMultiplier">How strong the velocity is (overriding the first velocity) (1.0 override it fully, 0.0 doesn't do anything)</param>
	/// <param name="aliveTime">How long the force lasts</param>
	public Force(Vector2 startVelocity, Vector2 gravity, float forceMultiplier = 0.9f, float aliveTime = 0.5f) {
		this.velocity = startVelocity;
		this.gravity = gravity;
		this.forceMultiplier = forceMultiplier;;
		this.aliveTime = aliveTime;
	}

	public Force() {
		velocity = Vector2.zero;
		gravity = Vector2.zero;
	}
	
	/// <param name="startVelocity">The velocity the force start with</param>
	/// <param name="forceMultiplier">How strong the velocity is (overriding the first velocity) (1.0 override it fully, 0.0 doesn't do anything)</param>
	/// <param name="aliveTime">How long the force lasts</param>
	public Force(Vector2 velocity, float forceMultiplier = 0.9f, float aliveTime = 0.5f) {
		this.velocity = velocity;
		this.gravity = Vector2.zero;
		this.aliveTime = aliveTime;
		this.forceMultiplier = forceMultiplier;
	}

	public Force Clone() {
		return new Force(velocity, gravity, forceMultiplier, aliveTime);
	}

	public void ApplyForce(ref Vector2 velocity, float deltaTime) {
		if(!IsAlive())
			return;
		aliveTime -= deltaTime;
		velocity *= 1f - forceMultiplier;
		velocity += this.velocity;
		this.velocity += gravity * deltaTime;
	}

	public bool IsAlive() {return aliveTime > 0;}

	public void DrawGizmos(Vector2 position, float iterationDistance = 0.05f) {
		Vector3 tempVeloc = velocity;
		Vector3 tempGravity = gravity;
		Vector3 lastPos = position;
		for(float i = 0; i < aliveTime; i += iterationDistance) {
			Vector3 newPos = lastPos + (Vector3)(tempVeloc * iterationDistance);
			Gizmos.DrawLine(lastPos, newPos);
			lastPos = newPos;
			tempVeloc += tempGravity * iterationDistance;
		}
	}

}