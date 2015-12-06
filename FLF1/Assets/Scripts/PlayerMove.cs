using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Controller2D))]
public class PlayerMove : MonoBehaviour {

	private static PlayerMove _instance;
	public static PlayerMove Instance
	{
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
			}
			return _instance;
		}
	}

    public enum PlayerState {IDLE, MOVE, JUMP, DEATH, WIN}
    public PlayerState CurrentPlayerState = PlayerState.IDLE;
    
	public GameObject SpawnPoint;

    public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;
	
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
    private bool isLeft = false;
	
	Controller2D controller;

    public Animator PlayerAnimator;

	void Awake ()
	{
        
		// retrieve the correct gameobject
		Object[] instance = GameObject.FindObjectsOfType(typeof(PlayerMove));
        if (instance.Length > 1)
        {
            // destroy it if there is more than one easyaccessresources
            Destroy(gameObject);
        }
        else
        {
            // set the _instance variable
            _instance = (PlayerMove)instance[0];
            DontDestroyOnLoad(this.gameObject);
        }
	}

	void Start() {
        this.CurrentPlayerState = PlayerState.IDLE;
        TimeManager.OnTimeChange += OnChangePeriod;

		controller = GetComponent<Controller2D> ();
		
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		//print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);

        PlayerAnimator = this.GetComponent<Animator>();
	}
	

	void Update() {

		if (this.CurrentPlayerState == PlayerState.WIN || this.CurrentPlayerState == PlayerState.DEATH)
		{
			input = Vector2.zero;
		}
		else
		{
			input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		}

		int wallDirX = (controller.collisions.left) ? -1 : 1;


		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			
		bool wallSliding = false;

		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
		{
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
        if (Input.GetAxis("Horizontal") < 0) { isLeft = true; }
        if (Input.GetAxis("Horizontal") > 0) { isLeft = false; }

        // scale -1 is player is going to the left
		this.transform.localScale = new Vector3((isLeft ? -1f : 1f),
												this.transform.localScale.y,
												this.transform.localScale.z);
			
        if (PlayerAnimator == null)    { PlayerAnimator = this.GetComponent<Animator>(); }
        switch (CurrentPlayerState)
        {
            case PlayerState.IDLE :
                if (Input.GetAxis("Horizontal") != 0)       //in idle and moving => set in anim move
                {
                    CurrentPlayerState = PlayerState.MOVE;
                    PlayerAnimator.SetTrigger("move");
                }
                else if (Input.GetButtonDown("Jump"))       //in idle and jumping => set in anim jump
                {
                    CurrentPlayerState = PlayerState.JUMP;
                    PlayerAnimator.SetTrigger("jump");
                }
                break;

            case PlayerState.MOVE :
                if (Input.GetAxis("Horizontal") == 0)       //in move and stop moving => set in anim idle
                {
                    CurrentPlayerState = PlayerState.IDLE;
                    PlayerAnimator.SetTrigger("idle");
                }
                else if (Input.GetButtonDown("Jump"))       //in move and jumping => set in anim jump
                {
                    CurrentPlayerState = PlayerState.JUMP;
                    PlayerAnimator.SetTrigger("jump");
                }
                break;

            case PlayerState.JUMP :
                if (controller.collisions.below)            //in jump and touching ground => stop fall in anim and set idle or move
                {
                    bool isMoving = Input.GetAxis("Horizontal") != 0;
                    CurrentPlayerState = (isMoving ? PlayerState.MOVE : PlayerState.IDLE);
                    PlayerAnimator.SetTrigger(isMoving ? "move" : "idle");
                }
                break;

            case PlayerState.WIN :
                if (Input.GetKeyUp(KeyCode.Return))  {NextLevel(); }
                break;

        }

        if (Input.GetKeyUp(KeyCode.F))
        {
			AnimationManager.Instance.SetAction(AnimationManager.ActionAnimation.ACTION);
        }


        if (Input.GetKeyUp(KeyCode.R))  {Reset(); }

    }

    public void Reset()
    {
        if (CurrentPlayerState != PlayerState.IDLE)
        {
            PlayerAnimator.SetTrigger("idle");
            CurrentPlayerState = PlayerState.IDLE;
		}
		this.transform.position = this.SpawnPoint.transform.position;

		UIManager.Instance.GetCanvas(UIManager.UIObjects.MAIN).GetComponent<MainUI>().Reset();
    }

    void OnLevelWasLoaded(int level)
    {
        SpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        // Positionnement de la camera
        GameObject LocalCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (LocalCamera != null)
        {
            LocalCamera.SetActive(true);
            LocalCamera.GetComponent<CameraFollow>().target = gameObject.GetComponent<Controller2D>();
            LocalCamera.GetComponent<CameraFollow>().enabled = true;
        }

        Reset();
    }

    void NextLevel()
    {
        PlayerAnimator.SetTrigger("idle");
        CurrentPlayerState = PlayerState.IDLE;

        GameManager.NextScene();
    }

    void OnChangePeriod(int newPeriod)
    {
		AnimationManager.Instance.SetAction(AnimationManager.ActionAnimation.CHANGE_TIME);
    }

	public void EndOfLevel()
	{
		PlayerAnimator.SetTrigger("actions");
		PlayerAnimator.SetTrigger("win");

		//FIXME what to do later ?
	}

	public void SetState(PlayerState state)
	{
		this.CurrentPlayerState = state;
	}
}