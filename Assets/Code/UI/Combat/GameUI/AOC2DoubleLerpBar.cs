using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// @author Rob Giusti
/// A UI fixture using two filled sprites as bars to show the difference in change in
/// an amount.
/// </summary>
public class AOC2DoubleLerpBar : MonoBehaviour, AOC2Poolable {
	
	/// <summary>
	/// The top bar, which signifies the actual amount of health when losing health
	/// and the lerping amount when gaining health
	/// </summary>
	[SerializeField]
	UIFilledSprite topBar;
	
	/// <summary>
	/// The bottom bar, which signifies the lerping amount when losing health
	/// and the acutal amount when gaining health
	/// </summary>
	[SerializeField]
	UIFilledSprite bottomBar;
	
	/// <summary>
	/// The background holder
	/// </summary>
	[SerializeField]
	UISprite background;
	
	UIFilledSprite lerpingBar;
	
	UIFilledSprite staticBar;
	
	public Transform trans;
	
	public GameObject gameObj;
	
	private AOC2DoubleLerpBar _prefab;
	
	public AOC2Poolable prefab
	{
		get
		{
			return _prefab;
		}
		set
		{
			_prefab = value as AOC2DoubleLerpBar;
		}
	}
	
	public Transform transf{
		get
		{
			return trans;
		}
	}
	
	public GameObject gObj{
		get
		{
			return gameObj;
		}
	}
	
	/// <summary>
	/// The current amount that is being displayed
	/// If currAmount > actualAmount, that means health is lerping 
	/// down due to damage.
	/// If currAmount < acutalAmount, that means health is lerping
	/// up due to healing.
	/// </summary>
	float _currAmount;
	
	/// <summary>
	/// The actual amount that this bar should fill. The goal
	/// that we're lerping to.
	/// </summary>
	float _finalAmount;
	
	/// <summary>
	/// The minimum amount of damage that will cause the health
	/// bar to lerp instead of jump.
	/// If the amount is below this, the coroutine will go straight
	/// to fade
	/// </summary>
	public float minLerpAmount = .1f;
	
	/// <summary>
	/// The percentage of the health bar that can change in a second
	/// </summary>
	public float lerpStep = .3f;
	
	/// <summary>
	/// How long to hold the difference before actually operating
	/// the lerp
	/// </summary>
	public float beginningWaitTime = .5f;
	
	/// <summary>
	/// Whether or not this bar fades and recycles after lerping
	/// </summary>
	public bool fade = true;
	
	/// <summary>
	/// How long to hold the bar up between lerp and fade
	/// </summary>
	public float holdBeforeFade = .5f;
	
	/// <summary>
	/// How long to fade the bar over
	/// </summary>
	public float fadeTime = .5f;
	
	/// <summary>
	/// The tint on the bottom bar when the value is decreasing
	/// </summary>
	public Color decreaseBackTint = Color.red;
	
	/// <summary>
	/// The tint on the bottom bar when the value is increasing
	/// </summary>
	public Color increaseBottomTint = Color.green;
	
	/// <summary>
	/// When set to zero, fades out while lerping
	/// </summary>
	public bool quickDie = true;
	
	/// <summary>
	/// The coroutine pointer, which points to the current place
	/// that the bar is at.
	/// A pointer is used and operated on rather than the normal coroutine
	/// system so that we can pull the coroutine back to the beginning by just redirecting it
    /// without using flags or manually stopping coroutines. 
	/// </summary>
	IEnumerator routine;
	
	public Action OnPool;
	
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2DoubleLerpBar item = Instantiate(this, origin, Quaternion.identity) as AOC2DoubleLerpBar;
		item.prefab = this;
		return item;
	}
	
	void Awake()
	{
		trans = transform;
		gameObj = gameObject;
	}
	
	// Use this for initialization
	public virtual void OnEnable () 
	{
		StartCoroutine(RunCoroutine());
	}
	
	public void Init()
	{
		_finalAmount = 1;
		_currAmount = 1;
	}
	
	/// <summary>
	/// Sets the amounts of the bars.
	/// </summary>
	/// <param name='prevAmount'>
	/// Previous amount.
	/// </param>
	/// <param name='endAmount'>
	/// End amount.
	/// </param>
	public void SetAmounts(float prevAmount, float endAmount)
	{
		SetAlpha(1f);
		
		if (_finalAmount == _currAmount)
		{
			_currAmount = prevAmount;
		}
		_finalAmount = endAmount;
		
		if (_finalAmount > _currAmount)
		{
			staticBar = bottomBar;
			lerpingBar = topBar;
			bottomBar.color = increaseBottomTint;
		}
		else
		{
			staticBar = topBar;
			lerpingBar = bottomBar;
			bottomBar.color = decreaseBackTint;
		}
		
		routine = LerpValues();
	}
	
	/// <summary>
	/// The wrapper function that runs the main coroutine as long
	/// as there's something to run.
	/// </summary>
	IEnumerator RunCoroutine()
	{
		while(true)
		{
			if (routine != null && routine.MoveNext())
			{
				yield return routine.Current;
			}
			else
			{
				yield return null;
			}
		}
	}
	
	/// <summary>
	/// Sets the bars to their current amounts
	/// </summary>
	void FillBars ()
	{
		lerpingBar.fillAmount = _currAmount;
		staticBar.fillAmount = _finalAmount;
	}
	
	/// <summary>
	/// Steps the current amount to the final amount
	/// over time
	/// </summary>
	IEnumerator LerpValues()
	{
		FillBars();
		
		yield return new WaitForSeconds(beginningWaitTime);
		
		while (_currAmount != _finalAmount)
		{
			float step = lerpStep * Time.deltaTime;
			if (Mathf.Abs(_currAmount - _finalAmount) < step)
			{
				_currAmount = _finalAmount;
			}
			else if (_currAmount < _finalAmount)
			{
				_currAmount += step;
			}
			else
			{
				_currAmount -= step;
			}
			FillBars();
			yield return null;
		}
		
		if (fade)
		{
			routine = FadeOut();
		}
	}
	
	/// <summary>
	/// Fades the whole UI element out over time
	/// </summary>
	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(holdBeforeFade);
		float currTime = 0;
		float lerp;
		while (currTime < fadeTime)
		{
			currTime += Time.deltaTime;
			lerp = Mathf.Min(currTime/fadeTime, 1f);
			SetAlpha(1 - lerp);
			yield return null;
		}
		Pool();
	}
	
	/// <summary>
	/// Sends this instance to the pool manager
	/// </summary>
	public void Pool ()
	{
		OnPool();
		AOC2ManagerReferences.poolManager.Pool(this);
	}
	
	/// <summary>
	/// Sets the alpha of all parts of the bar
	/// </summary>
	/// <param name='alph'>
	/// Alpha value to set all parts to
	/// </param>
	void SetAlpha(float alph)
	{
		background.alpha = topBar.alpha = bottomBar.alpha = alph;
	}
	
}
