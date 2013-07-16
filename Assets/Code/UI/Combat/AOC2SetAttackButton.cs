using UnityEngine;
using System.Collections;

public class AOC2SetAttackButton : MonoBehaviour {
	
	[SerializeField]
	int abilityIndex;
	
	public UILabel label;
	
	public void Setup(AOC2Ability ability, int index)
	{
		abilityIndex = index;
		label.text = ability.name;
	}
	
	void OnClick()
	{
		if (AOC2EventManager.Combat.SetPlayerAttack != null){
			AOC2EventManager.Combat.SetPlayerAttack(abilityIndex);
		}
	}
}
