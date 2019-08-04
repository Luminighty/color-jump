using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicsObject
{
	private Animator animator;
	private Player player;
	[SerializeField]
	private GameObject colorHolder;

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

	private Delay walkParticleDelay = new Delay(0.2f);

	private bool wasGrounded = false;

	private void Awake() {
		player = GetComponent<Player>();
		animator = GetComponent<Animator>();
	}

	protected override void ComputeVelocity() {
        if(!wasGrounded && grounded)
			Instantiate(player.LandParticlePrefab, transform.position + (Vector3.down / 2f), Quaternion.identity);
		if(Input.GetButtonDown("Quit"))
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		Move();
		Jump();
		wasGrounded = grounded;
	}

	protected override void OnGroundTouched() {
		animator.SetBool("grounded", true);
		jumpCount.Restart();
		if(jumpBuffer.isDelayed() && CanJump())
			DoJump();
	}

	void Jump() {
		// higher jump
		if(velocity.y > 0f && !Input.GetButton("Jump"))
				velocity.y *= jumpFallMultiplier;
		
		velocity.y = Mathf.Max(velocity.y, maxFallSpeed);
		// fall buffer
		if(velocity.y < 0f) {
			fallBuffer += Time.deltaTime;
			if(jumpCount == 0 && !fallBuffer.isDelayed()) {
				jumpCount++;
			}
		}
		jumpBuffer += Time.deltaTime;
		if(Input.GetButtonDown("Jump"))
			jumpBuffer.Reset();
		
		if(!Input.GetButtonDown("Jump") || !CanJump())
			return;
		DoJump();
	}

	void DoJump() {
		grounded = false;
		jumpCount++;
		velocity.y = jumpSize;
		animator.SetBool("grounded", false);
		animator.SetTrigger("jump");
		player.soundEffectSource.PlayOneShot(player.jumpClip);
	}

	private bool CanJump() {
		return jumpCount.hasMore() && !colorHolder.activeSelf;
	}

	void Move() {
		float horizontal = (colorHolder.activeSelf) ? 0f : Input.GetAxis("Horizontal");
		animator.SetFloat("speed", Mathf.Abs(horizontal));
		walkParticleDelay += Time.deltaTime;
		if(horizontal != 0f && !walkParticleDelay.isDelayed() && grounded) {
			Instantiate(player.WalkParticlePrefab, transform.position + (Vector3.down / 2f), Quaternion.identity);
			walkParticleDelay.Reset();
		}
		targetVelocity.x = horizontal * Time.deltaTime * speed;
	}

	public bool CanOpenMenu() {
		return grounded;
	}

}
