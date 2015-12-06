using UnityEngine;
using System.Collections;

public class StandardPopup : MonoBehaviour {
	
	public UnityEngine.UI.Text ContentComponent;

	private Animator _animator;
	private float _timer = 0f;
	private bool _isOnTimer = false;

	public StandardPopupAttributes Attributes;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(_isOnTimer)
		{
			_timer -= Time.deltaTime;

			if(_timer <= 0f || Input.GetMouseButtonDown(0) )
			{
				ClosePopup();
			}
		}

	}

	/// <summary>
	/// Set the icon.
	/// </summary>
	/// <param name="type">Type.</param>
	public void SetIcon(PopupFactoryManager.IconType type)
	{

	}

	private void SetTimer(float timer)
	{
		_timer = timer;
		_isOnTimer = true;
	}

	public void LaunchPopup()
	{
		if(_animator == null)
			_animator = GetComponent<Animator>();


		// set timer
		if(Attributes.Timer != 0f)
			SetTimer(Attributes.Timer);

		// set title and content
		ContentComponent.text = Attributes.Content;

		switch(Attributes.EffectType)
		{
			case PopupFactoryManager.AnimationEffectType.FADE : _animator.Play("PopupIn");
				break;
			case PopupFactoryManager.AnimationEffectType.SIMPLE_SLIDE : _animator.Play("PopupIn");
				break;
		}
	}

	public void ClosePopup()
	{
		_timer = 0f;
		_isOnTimer = false;

		string animToPlay = string.Empty;

		switch(Attributes.EffectType)
		{
			case PopupFactoryManager.AnimationEffectType.FADE : animToPlay = "PopupOut";
				break;
		case PopupFactoryManager.AnimationEffectType.SIMPLE_SLIDE : animToPlay = "PopupOut";
				break;
		}

		_animator.Play(animToPlay);

		StartCoroutine(CloseProcess(animToPlay));
	}

	public void OkButtonActivation()
	{
		Attributes.OnOKClick();

		ClosePopup();
	}

	IEnumerator CloseProcess(string lastAnimName)
	{
		yield return new WaitForEndOfFrame();

		while(_animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash(lastAnimName)
		      || _animator.IsInTransition(0))
		{
			yield return new WaitForEndOfFrame();
		}
	
		PopupFactoryManager.Instance.CloseStandardPopup(this);
	}
}

public class StandardPopupAttributes
{
	public enum PopupType { OK_WINDOW }

	public PopupType TypeOfPopup;
	
	private PopupFactoryManager.AnimationEffectType _effectType;

	public delegate void OkEvent();
	public event OkEvent OkClickEvent;

	private float _timer = 0f;
	private string _content;

	public float Timer {
		get {
			return _timer;
		}
		set {
			_timer = value;
		}
	}
	
	public string Content {
		get {
			return _content;
		}
		set {
			_content = value;
		}
	}

	
	public PopupFactoryManager.AnimationEffectType EffectType {
		get {
			return _effectType;
		}
		set {
			_effectType = value;
		}
	}

	public void OnOKClick()
	{
		OkEvent animationEvent = (OkEvent)OkClickEvent;
		if(animationEvent != null)
		{
			animationEvent();
		}
	}
}
