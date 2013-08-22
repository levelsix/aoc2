using UnityEngine;
using System.Collections;

public class AOC2HealthBar : AOC2DoubleLerpBar {
	
	[SerializeField]
	UILabel label;
	
	public override void OnEnable()
	{
		base.OnEnable();
		AOC2EventManager.Combat.OnPlayerHealthChange += OnPlayerHealthChange;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Combat.OnPlayerHealthChange -= OnPlayerHealthChange;
	}
	
	void OnPlayerHealthChange(AOC2Unit player, int amount)
	{
		//Make this a float so that all division is float division
		float maxHP = player.GetStat(AOC2Values.UnitStat.HEALTH);
		
		SetAmounts((player.health - amount) / maxHP, player.health / maxHP);
		
		label.text = player.health + "/" + player.GetStat(AOC2Values.UnitStat.HEALTH);
	}
	
}
