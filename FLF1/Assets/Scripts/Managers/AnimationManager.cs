using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	// instance of singleton
	private static AnimationManager _instance = null;
	
	// getting the singleton
	public static AnimationManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<AnimationManager>();
			}
			if (_instance == null)
			{
				_instance = new GameObject().AddComponent<AnimationManager>();
			}
			return _instance;
		}
	}
	
	// constructor in private to avoid a construction
	private AnimationManager()
	{

	}
	
	// killing the singleton
	public static void Kill()
	{
		_instance = null;
	}


	// Save Animation HashName
//	static int idleState = Animator.StringToHash("Base Layer.PlayerIdle"); //these names must be the same as in Animator
//	static int moveState = Animator.StringToHash("Base Layer.Move.PlayerStartMove");
//	static int jumpState = Animator.StringToHash("Base Layer.Jump.PlayerJump");
//	static int winState = Animator.StringToHash("Base Layer.PlayerWin");
//	static int actionState = Animator.StringToHash("Base Layer.PlayerAction");
//	static int deathState = Animator.StringToHash("Base Layer.PlayerDeath");
//	static int timeState = Animator.StringToHash("Base Layer.PlayerTravelTime");

	public enum ActionAnimation { IDLE, MOVE, JUMP, ACTION, CHANGE_TIME, WIN, DEATH }

	private Animator _playerAnimator;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void SetAction(ActionAnimation action, bool val = false)
	{
		Debug.Log("**** ActionAnimation : " + action.ToString());
		if (_playerAnimator == null)
		{
			this._playerAnimator = PlayerMove.Instance.PlayerAnimator;
		}

		switch (action)
		{
		case ActionAnimation.DEATH :
			PlayerMove.Instance.SetState(PlayerMove.PlayerState.DEATH);
			_playerAnimator.Play("Base Layer.PlayerDeath");
			break;
		case ActionAnimation.WIN :
			PlayerMove.Instance.SetState(PlayerMove.PlayerState.WIN);
			_playerAnimator.Play("Base Layer.PlayerWin");
			break;
		case ActionAnimation.ACTION :
			PlayerMove.Instance.SetState(PlayerMove.PlayerState.IDLE);
			_playerAnimator.Play("Base Layer.PlayerAction");
			break;
		case ActionAnimation.CHANGE_TIME :
			PlayerMove.Instance.SetState(PlayerMove.PlayerState.IDLE);
			_playerAnimator.Play("Base Layer.PlayerTravelTime");
			break;
		}

	}
}
