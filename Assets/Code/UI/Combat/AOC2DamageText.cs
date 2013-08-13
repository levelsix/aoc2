using UnityEngine;
using System.Collections;

[RequireComponent (typeof(UILabel))]
public class AOC2DamageText : MonoBehaviour, AOC2Poolable {
	
	public float lifetime;
	
	float currLife;
	
	/// <summary>
	/// The height that the damage text moves over its lifetime
	/// </summary>
	public float MOVE_HEIGHT = 3f;
	
	public float Y_OFFSET = 2f;
	
	private AOC2DamageText _prefab;
	
	UILabel label;
	
	AOC2Unit curUnit;
	
	Transform trans;
	
	public AOC2Poolable prefab{
		get
		{
			return _prefab;
		}
		set
		{
			_prefab = value as AOC2DamageText;
		}
	}
	
	public AOC2Poolable Make(Vector3 origin)
	{
		AOC2DamageText text = Instantiate(this, origin, Quaternion.identity) as AOC2DamageText;
		text.prefab = this;
		return text;
	}
	
	void Awake()
	{
		label = GetComponent<UILabel>();
		trans = transform;
	}
	
	public void Init(AOC2Unit unit, float damage)
	{
		label.text = damage.ToString();
		label.alpha = 1f;
		
		currLife = 0f;
		
		trans.parent = unit.trans;
		trans.localPosition = new Vector3(0f, Y_OFFSET, 0f);
		
		Debug.Log("Text on layer " + gameObject.layer);
		
		curUnit = unit;
		curUnit.OnDeath += RemoveFromParent;
		
		StartCoroutine(MoveAndFade());
	}
	
	void RemoveFromParent()
	{
		Pool();
	}
	
	IEnumerator MoveAndFade()
	{
		while (currLife < lifetime)
		{
			trans.localPosition = new Vector3(0f, Y_OFFSET + (currLife/lifetime * MOVE_HEIGHT), 0f);
			label.alpha = 1f - (currLife/lifetime);
			yield return null;
			currLife += Time.deltaTime;
		}
		Pool();
	}
	
	public void Pool()
	{
		AOC2ManagerReferences.poolManager.Pool(this);
		curUnit.OnDeath -= RemoveFromParent;
	}
}
