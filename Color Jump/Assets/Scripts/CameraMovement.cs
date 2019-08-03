using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private bool drawGizmos;
	[SerializeField]
	private Color gizmoColor = Color.red;
	[SerializeField]
	private Vector2Int gizmoCount;

	[Tooltip("Difference between screens. The player will be teleported with this amount too")]
	[SerializeField]
	private Vector2 screenPadding;
	[Tooltip("How much should the player be pushed when changing screens additionaly to screenPadding")]
	[SerializeField]
	private Vector2 playerPadding;

	private Transform player;
	private Vector2 cameraSize;
	private Vector3 toPosition;

	[SerializeField]
	private float cameraSpeed = 2500;

	private void Awake() {
		toPosition = transform.position;
		player = FindObjectOfType<Player>().transform;
		cameraSize = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
	}

	private void Update() {
		if(TryMoveCamera())
			return;
		
		Vector3 deltaPos = player.position - transform.position;
		if(Mathf.Abs(deltaPos.x) >= cameraSize.x) {
			float sign = Mathf.Sign(deltaPos.x);
			toPosition.x += sign * (cameraSize.x * 2 + screenPadding.x);
			Vector3 playerPos = player.position;
			playerPos.x += sign * (screenPadding.x + playerPadding.x);
			player.position = playerPos;
			PhysicsObject.isPhysicsOn = false;
		}

		if(Mathf.Abs(deltaPos.y) >= cameraSize.y) {
			float sign = Mathf.Sign(deltaPos.y);
			toPosition.y += sign * (cameraSize.y * 2 + screenPadding.y);
			Vector3 playerPos = player.position;
			playerPos.y += sign * (screenPadding.y + playerPadding.y);
			player.position = playerPos;
			PhysicsObject.isPhysicsOn = false;
		}
	}

	/// <returns>Returns true if the camera is being moved</returns>
	bool TryMoveCamera() {
		if(Vector3.SqrMagnitude(transform.position - toPosition) < 0.05f)
			return false;
		transform.position = Vector3.MoveTowards(transform.position, toPosition, cameraSpeed * Time.deltaTime);
		if(Vector3.SqrMagnitude(transform.position - toPosition) < 0.05f)
			PhysicsObject.isPhysicsOn = true;
		return true;
	}


	private void OnDrawGizmos() {
		if(!drawGizmos || Camera.main.gameObject != gameObject)
			return;
		Camera mainCamera = Camera.main;
		Gizmos.color = gizmoColor;
		for(int i = -gizmoCount.x; i <= gizmoCount.x; i++)
			for(int j = -gizmoCount.y; j <= gizmoCount.y; j++) {

				Vector3 center = transform.position;
				Vector2 cameraSize = new Vector2(mainCamera.orthographicSize * mainCamera.aspect * 2, mainCamera.orthographicSize * 2);
				center.x += (cameraSize.x+screenPadding.x) * i;
				center.y += (cameraSize.y+screenPadding.y) * j;

				Gizmos.DrawWireCube(center, cameraSize);

			}
		

	}

}
