using UnityEngine;
using System.Collections;

public class AOC2HealthBar : MonoBehaviour {
	
	[SerializeField]
	UILabel label;
	
	[SerializeField]
	UIFilledSprite bar;
	
	void OnEnable()
	{
		AOC2EventManager.Combat.OnPlayerHealthChange += OnPlayerHealthChange;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Combat.OnPlayerHealthChange -= OnPlayerHealthChange;
	}
	
	void OnPlayerHealthChange(AOC2Unit player)
	{
		bar.fillAmount = (float)player.health / player.stats.maxHealth;
		label.text = player.health + "/" + player.stats.maxHealth;
	}
	
}
