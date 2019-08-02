﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicsObject
{
	public float speed;
	public float jumpFallMultiplier;
	[SerializeField]
	private Delay fallBuffer = new Delay(0.1f);


	[Header("Jump")]
	[SerializeField]
	private Counter jumpCount = new Counter(1);
	[SerializeField]
	private Delay jumpBuffer = new Delay(0.1f);

	public float jumpSize = 10f;

	protected override void ComputeVelocity() {
        Move();
		Jump();
	}

	protected override void OnGroundTouched() {
		jumpCount.Restart();
	}

	void Jump() {
		// higher jump
		if(velocity.y > 0f && !Input.GetButton("Jump"))
				velocity.y *= jumpFallMultiplier;
		
		velocity.y = Mathf.Max(velocity.y, maxFallSpeed);
		// fall buffer
		if(velocity.y < 0f) {
			fallBuffer += Time.deltaTime;
			if(jumpCount == 0 && !fallBuffer.isDelayed())
				jumpCount++;
		}

		if(!Input.GetButtonDown("Jump") || !CanJump())
			return;
		grounded = false;
		jumpCount++;
		velocity.y = jumpSize;
	}

	private bool CanJump() {
		return jumpCount.hasMore();
	}

	void Move() {
		float horizontal = Input.GetAxis("Horizontal");
		targetVelocity.x = horizontal * Time.deltaTime * speed;

	}

}
