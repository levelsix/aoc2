using UnityEngine;
using System.Collections;

public class AOC2SetAbilityButton : MonoBehaviour {
	
	[SerializeField]
	int abilityIndex;
	
	public UILabel label;
	
	public UIFilledSprite overlay;
	
	AOC2Ability _ability;
	
	void OnEnable()
	{
		AOC2EventManager.UI.OnAbilityStartCool += OnAbilityCool;
	}
	
	void OnDisable()
	{
		AOC2EventManager.UI.OnAbilityStartCool -= OnAbilityCool;
	}
	
	public void Setup(AOC2Ability ability, int index)
	{
		abilityIndex = index;
		label.text = ability.name;
		_ability = ability;
	}
	
	void OnClick()
	{
		if (overlay.fillAmount <= .1f && AOC2EventManager.Combat.SetPlayerAttack != null){
			AOC2EventManager.Combat.SetPlayerAttack(abilityIndex);
		}
	}
	
	void OnAbilityCool(AOC2Ability ab, float time)
	{
		if (ab == _ability)
		{
			StartCoroutine(AnimateCool(time));
		}
	}
	
	IEnumerator AnimateCool(float time)
	{
		float timer = time;
		while (timer > 0)
		{
			timer -= Time.deltaTime;
			overlay.fillAmount = timer/time;
			yield return null;
		}
	}
}
