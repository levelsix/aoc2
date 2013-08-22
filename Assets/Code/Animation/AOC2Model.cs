using UnityEngine;
using System.Collections;
using proto;

public class AOC2Model : MonoBehaviour {
	
	[SerializeField]
	SkinnedMeshRenderer[] models;
	
	public Animation anima;

	public Animator animator;
	
	public bool usesTriggers;
	
	AOC2Unit _unit;
	
	public MonsterType modelType;

	void Awake()
	{
		anima = animation;
		animator = GetComponent<Animator>();
		_unit = GetComponent<AOC2Unit>();
	}
	
	void OnEnable()
	{
		_unit.OnActivate += OnActivate;
		_unit.OnDeactivate += OnDeactivate;
	}
	
	void OnDisable()
	{
		_unit.OnActivate -= OnActivate;
		_unit.OnDeactivate -= OnDeactivate;
	}
	
	public void StopAnimation(AOC2Values.Animations.Anim animationType)
	{
		if (anima != null)
		{
			string animName = AOC2Values.Animations.anims[modelType][animationType];
			if (anima.IsPlaying(animName))
			{
				anima.Stop(animName);
			}
		}
	}
	
	public void Animate(AOC2Values.Animations.Anim animationType)
	{
		if (anima != null)
		{
			string animName = AOC2Values.Animations.anims[modelType][animationType];
			if (anima.IsPlaying(animName))
			{
				if (anima[animName].wrapMode != WrapMode.Loop)
				{
					anima.Stop();
				}
				anima.CrossFade(animName);
			}
			else
			{
				anima.CrossFade(animName);
				StartCoroutine(WaitForAnimationEnd(animName));
			}
			
			
		}
	}
	
	IEnumerator WaitForAnimationEnd(string animName)
	{
		do
		{
			yield return null;
		}while (anima.IsPlaying(animName));
	}
	
	public void SetAnimationFlag(AOC2Values.Animations.Anim animationType, bool val)
	{
		//Debug.Log("Set flag: " + animationType + " for " + modelType +  " to " + val);
		SetAnimSpeed(1f);
		if (animator == null)
		{
			if (val)
			{
				Animate(animationType);
			}
			else
			{
				StopAnimation(animationType);
			}
		}
		else
		{
			animator.SetBool(AOC2Values.Animations.anims[modelType][animationType], val);
		}
	}
	
	public void Tint(Color color)
	{
		Mesh mesh;
		foreach (SkinnedMeshRenderer item in models) 
		{
			//Debug.Log("Tinting model: " + item.name);
			/*
			foreach (Material mat in item.materials) 
			{
				mat.color = color;
			}
			*/
			mesh = item.sharedMesh;
			Color[] colorArr = new Color[mesh.vertices.Length];
			for (int i = 0; i < mesh.vertices.Length; i++) {
				colorArr[i] = color;
			}
			mesh.colors = colorArr;
			
		}
	}
	
	void OnActivate()
	{
		SetActivation(true);
	}
	
	void OnDeactivate()
	{
		SetActivation(false);
	}
	
	void SetActivation(bool active)
	{
		if (animator != null)
		{
			animator.enabled = active;
		}
		if (anima != null)
		{
			anima.enabled = active;
		}
	}
	
	/// <summary>
	/// Animation event for when the attack frame on an animation
	/// occurs
	/// </summary>
	public void AttackTrigger()
	{
		if (_unit.OnUseAbility != null)
		{
			_unit.OnUseAbility();
		}
	}
	
	/// <summary>
	/// Animation event for when a non-looping animation ends
	/// </summary>
	public void EndAnimationTrigger()
	{
		if (_unit.OnAnimationEnd != null)
		{
			_unit.OnAnimationEnd();
		}
	}
	
	public void SetAnimSpeed(float scale)
	{
		if (animator != null)
		{
			animator.speed = scale;
		}
		if (anima != null)
		{
			foreach (AnimationState item in anima) 
			{
				item.speed = scale;
			}
		}
	}
}
