using UnityEngine;
using System.Collections;
using proto;

/// <summary>
/// @author Rob Giusti
/// Component for handling character models and animations.
/// Since Unity has a legacy animation system and a newer animation system (Mecanim)
/// that both function differently, this component helps create a base through
/// which we can integrate both together, and interact with them uniformly.
/// </summary>
public class AOC2Model : MonoBehaviour {
	
	/// <summary>
	/// The models, which should be children of
	/// the gameObject to which this component is attached.
	/// Set in inspecter.
	/// </summary>
	[SerializeField]
	SkinnedMeshRenderer[] models;
	
	/// <summary>
	/// The animation component, if this model uses the legacy system.
	/// </summary>
	public Animation anima;
	
	/// <summary>
	/// The animator component, if this model uses the Mecanim system.
	/// </summary>
	public Animator animator;
	
	/// <summary>
	/// The boolean for whether this model uses animation triggers.
	/// If it does, that means that its using the Legacy system.
	/// TODO: Figure out how to properly create triggers in Mecanim
	/// </summary>
	public bool usesTriggers;
	
	/// <summary>
	/// The unit component.
	/// Used to tie into events.
	/// </summary>
	AOC2Unit _unit;
	
	/// <summary>
	/// The type of the model, derived from a Proto.
	/// Used to determine what model to load on create, as well
	/// as to know what each animation is called for that model.
	/// </summary>
	public MonsterType modelType;
	
	/// <summary>
	/// Awake this instance.
	/// Set up internal component references.
	/// </summary>
	void Awake()
	{
		anima = animation;
		animator = GetComponent<Animator>();
		_unit = GetComponent<AOC2Unit>();
	}
	
	/// <summary>
	/// Raises the enable event.
	/// Registers responses to Unit events.
	/// </summary>
	void OnEnable()
	{
		_unit.OnActivate += OnActivate;
		_unit.OnDeactivate += OnDeactivate;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// Releases event responses.
	/// </summary>
	void OnDisable()
	{
		_unit.OnActivate -= OnActivate;
		_unit.OnDeactivate -= OnDeactivate;
	}
	
	/// <summary>
	/// Stops the animation of a particular type.
	/// Only works for the Legacy system. (Mecanim stops in SetAnimationFlag).
	/// </summary>
	/// <param name='animationType'>
	/// Animation type.
	/// </param>
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
	
	/// <summary>
	/// Legacy animation system. Starts the animation of a given type.
	/// If the character is already doing this animation, does not
	/// restart the animation.
	/// </summary>
	/// <param name='animationType'>
	/// Animation type.
	/// </param>
	public void Animate(AOC2Values.Animations.Anim animationType)
	{
		if (anima != null)
		{
			string animName = AOC2Values.Animations.anims[modelType][animationType];
			if (anima.IsPlaying(animName))
			{
				if (anima[animName].wrapMode != WrapMode.Loop)
				{
					//anima.Stop();
				}
				anima.CrossFade(animName);
			}
			else
			{
				anima.CrossFade(animName);
				//StartCoroutine(WaitForAnimationEnd(animName));
			}
		}
	}
	
	/// <summary>
	/// Sets the specified animation to be active or inactive.
	/// Legacy system: Calls another helper function to start or stop animation.
	/// Mecanim system: Sets flags in state machine to change current state
	/// </summary>
	/// <param name='animationType'>
	/// Animation type.
	/// </param>
	/// <param name='val'>
	/// Value.
	/// </param>
	public void SetAnimation(AOC2Values.Animations.Anim animationType, bool val)
	{
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
			//Debug.Log("Set flag: " + AOC2Values.Animations.anims[modelType][animationType] + " for " + modelType +  " to " + val);
			animator.SetBool(AOC2Values.Animations.anims[modelType][animationType], val);
		}
	}
	
	/// <summary>
	/// Tints each vertex of the model the specified color.
	/// Note that this will only effictively tint the model if the texture shader
	/// takes vertex color into account.
	/// </summary>
	/// <param name='color'>
	/// Color to tint
	/// </param>
	public void Tint(Color color)
	{
		Mesh mesh;
		foreach (SkinnedMeshRenderer item in models)
		{
			Debug.Log("Tinting " + name + " " + color);
			
			//item.BakeMesh(item.sharedMesh);
			mesh = item.sharedMesh;
			Color[] colorArr = new Color[mesh.vertices.Length];
			for (int i = 0; i < mesh.vertices.Length; i++) {
				colorArr[i] = color;
			}
			mesh.colors = colorArr;
		}
	}
	
	/// <summary>
	/// Raises the activate event.
	/// </summary>
	void OnActivate()
	{
		SetActivation(true);
	}
	
	/// <summary>
	/// Raises the deactivate event.
	/// </summary>
	void OnDeactivate()
	{
		SetActivation(false);
	}
	
	/// <summary>
	/// Sets the activation of the animation system.
	/// </summary>
	/// <param name='active'>
	/// Whether to activate or deactivate
	/// </param>
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
	
	/// <summary>
	/// Sets the animation speed.
	/// Note: Animation speed is animation-specific in Legacy but not
	/// in Mecanim, so animation speed will be set to 1 on every SetAnimation
	/// </summary>
	/// <param name='scale'>
	/// Scale.
	/// </param>
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
	
	/// <summary>
	/// Determines whether this instance is playing the specified animationType.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this instance is playing the specified animationType; otherwise, <c>false</c>.
	/// </returns>
	/// <param name='animationType'>
	/// The animation to check if playing
	/// </param>
	public bool IsPlaying(AOC2Values.Animations.Anim animationType)
	{
		if (anima != null)
		{
			return anima.IsPlaying(AOC2Values.Animations.anims[modelType][animationType]);
		}
		else
		{
			
			return animator.GetCurrentAnimatorStateInfo(0).IsName("Base." + AOC2Values.Animations.anims[modelType][animationType]);
		}
	}
	
	public IEnumerator WaitUntilAnimationComplete(AOC2Values.Animations.Anim animationType)
	{
		while(!IsPlaying(animationType))
		{
			yield return null;
		}
		
		while(IsPlaying(animationType))
		{
			yield return null;
		}
	}
}
