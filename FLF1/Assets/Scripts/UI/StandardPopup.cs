﻿using UnityEngine;
using System.Collections;

public class StandardPopup : MonoBehaviour {
	
	public UnityEngine.UI.Text TitleComponent;
	public UnityEngine.UI.Text ContentComponent;
	public UnityEngine.UI.RawImage IconComponent;

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
		switch(type)
		{
			case PopupFactoryManager.IconType.VALIDATE : IconComponent.texture = PopupFactoryManager.Instance.ValidateIcon;
				break;
			case PopupFactoryManager.IconType.WARNING : IconComponent.texture = PopupFactoryManager.Instance.WarningIcon;
				break;
			case PopupFactoryManager.IconType.ERROR : IconComponent.texture = PopupFactoryManager.Instance.ErrorIcon;
				break;
		}
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

		// set icon
		SetIcon(Attributes.Icon);

		// set title and content
		TitleComponent.text = Attributes.Title;
		ContentComponent.text = Attributes.Content;

		switch(Attributes.EffectType)
		{
			case PopupFactoryManager.AnimationEffectType.FADE : _animator.Play("FadeIn");
				break;
			case PopupFactoryManager.AnimationEffectType.SIMPLE_SLIDE : _animator.Play("FadeIn");
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
			case PopupFactoryManager.AnimationEffectType.FADE : animToPlay = "FadeOut";
				break;
			case PopupFactoryManager.AnimationEffectType.SIMPLE_SLIDE : animToPlay = "FadeOut";
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
	
	private PopupFactoryManager.IconType _icon;
	private PopupFactoryManager.AnimationEffectType _effectType;

	public delegate void OkEvent();
	public event OkEvent OkClickEvent;

	private float _timer = 0f;
	private string _title, _content;

	public float Timer {
		get {
			return _timer;
		}
		set {
			_timer = value;
		}
	}
	
	public string Title {
		get {
			return _title;
		}
		set {
			_title = value;
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
	
	public PopupFactoryManager.IconType Icon {
		get {
			return _icon;
		}
		set {
			_icon = value;
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
