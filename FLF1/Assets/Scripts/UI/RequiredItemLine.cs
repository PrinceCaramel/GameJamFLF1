using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RequiredItemLine : MonoBehaviour {

	public Image Icone;
	public Text Ratio;

	private int _currentCount;
	private int _totalCount;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Init (Sprite icone, int total)
	{
		this.Icone.sprite = icone;

		this._currentCount = 0;
		this._totalCount = total;
		this.Ratio.text = _currentCount + "/" + total.ToString();
	}

	public void Increment()
	{
		if (this._currentCount < _totalCount)
		{
			this._currentCount++;
		}
		else
		{
			throw new UnityException("Error ! More items than waited");
		}

		this.Ratio.text = this._currentCount + "/" + this._totalCount;
		if (this._currentCount == this._totalCount)
		{
			this.Ratio.color = Color.green;
		}
	}

	public bool IsComplete	{ get { return (this._currentCount==this._totalCount); } }
}
