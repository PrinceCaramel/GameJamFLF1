﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Controller2D))]
public class PlayerMove : MonoBehaviour {

    public enum PlayerState {IDLE, MOVE, JUMP}
    public PlayerState CurrentPlayerState = PlayerState.IDLE;
    
    public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;
	
	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;
	
	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	Vector2 input;
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	
	Controller2D controller;

    private Animator _playerAnimator;

	void Start() {
        this.CurrentPlayerState = PlayerState.IDLE;

		controller = GetComponent<Controller2D> ();
		GameObject LocalCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		if (LocalCamera != null) {
			Debug.Log ("found" + LocalCamera.name);
			LocalCamera.SetActive (true);
			LocalCamera.GetComponent<CameraFollow> ().target = gameObject.GetComponent<Controller2D> ();
			LocalCamera.GetComponent<CameraFollow> ().enabled = true;
		}
        
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		//print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);

        _playerAnimator = this.GetComponent<Animator>();
	}
	

	void Update() {
        

		input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		int wallDirX = (controller.collisions.left) ? -1 : 1;


		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			
		bool wallSliding = false;

		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;
				
			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}
				
			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;
					
				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				} else {
					timeToWallUnstick = wallStickTime;
				}
			} else {
				timeToWallUnstick = wallStickTime;
			}
				
		}


        if (Input.GetButtonDown("Jump"))
        {
            // if player is sliding on a wall
            if (wallSliding)
            {
                // if player is pushing direction of the wall, make jump in opposite horizontal direction (far-, high+)
                if (wallDirX == input.x)
                {
                    velocity.x = -wallDirX * wallJumpClimb.x;
                    velocity.y = wallJumpClimb.y;
                }
                else if (input.x == 0)  // if player is not pressing left or right, make weaker bound (far+, high--)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                else // if player is pressing opposite direction of the wall, make farer bound (far++, high+)
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }

            // player on ground => velocity max on y
            else if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
            }
        }
				

		//Jump
		if (Input.GetButtonUp ("Jump")) {
			if (velocity.y > minJumpVelocity) {
				velocity.y = minJumpVelocity;
			}
		}

		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);
	
	
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}




		if (Input.GetButtonUp("NextItem"))
		{
			TimeManager.Instance.NextTime();
		}
		
		if (Input.GetButtonUp("PreviousItem"))
		{
			TimeManager.Instance.PreviousTime();
		}

        // ANIMATION PART

        // scale -1 is player is going to the left
		this.transform.localScale = new Vector3((Input.GetAxis("Horizontal") < -0.005f ? -1f : 1f),
												this.transform.localScale.y,
												this.transform.localScale.z);
			
        if (_playerAnimator == null)    { _playerAnimator = this.GetComponent<Animator>(); }
        if (CurrentPlayerState == PlayerState.IDLE)     //if player in idle
        {
            if (Input.GetAxis("Horizontal") != 0)       //in idle and moving => set in anim move
            {
                CurrentPlayerState = PlayerState.MOVE;
                _playerAnimator.SetTrigger("move");
            }
            else if (Input.GetButtonDown("Jump"))       //in idle and jumping => set in anim jump
            {
                CurrentPlayerState = PlayerState.JUMP;
                _playerAnimator.SetTrigger("jump");
            }
        }
        else if (CurrentPlayerState == PlayerState.MOVE)
        {
            if (Input.GetAxis("Horizontal") == 0)       //in move and stop moving => set in anim idle
            {
                CurrentPlayerState = PlayerState.IDLE;
                _playerAnimator.SetTrigger("idle");
            }
            else if (Input.GetButtonDown("Jump"))       //in move and jumping => set in anim jump
            {
                CurrentPlayerState = PlayerState.JUMP;
                _playerAnimator.SetTrigger("jump");
            }
        }
        else if (CurrentPlayerState == PlayerState.JUMP)
        {
            if (controller.collisions.below)            //in jump and touching ground => stop fall in anim and set idle or move
            {
                bool isMoving = Input.GetAxis("Horizontal") != 0;
                CurrentPlayerState = (isMoving ? PlayerState.MOVE : PlayerState.IDLE);
                _playerAnimator.SetTrigger(isMoving ? "move" : "idle");
            }
        }

    }
}