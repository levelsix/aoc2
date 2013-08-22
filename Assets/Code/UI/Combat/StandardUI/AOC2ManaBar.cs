using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// The Mana Bar, which fills up according to the player's current mana
/// </summary>
public class AOC2ManaBar : MonoBehaviour {
	
	/// <summary>
	/// The label, which displays how much mana remains
	/// </summary>
	[SerializeField]
	UILabel label;
	
	/// <summary>
	/// The bar; a visual indicator of the amount of mana relatively remaining
	/// </summary>
	[SerializeField]
	UIFilledSprite bar;
	
	/// <summary>
	/// Raises the enable event.
	/// Register Callbacks
	/// </summary>
	void OnEnable()
	{
		AOC2EventManager.Combat.OnPlayerManaChange += OnPlayerManaChange;
	}
	
	/// <summary>
	/// Raises the disable event.
	/// De-register callbacks
	/// </summary>
	void OnDisable()
	{
		AOC2EventManager.Combat.OnPlayerManaChange -= OnPlayerManaChange;
	}
	
	/// <summary>
	/// Update the mana bar to reflect current player mana
	/// </summary>
	/// <param name='player'>
	/// Player unit
	/// </param>
	void OnPlayerManaChange(AOC2Unit player)
	{
		bar.fillAmount = (float)player.mana / player.GetStat(AOC2Values.UnitStat.MANA);
		label.text = player.mana + "/" + player.GetStat(AOC2Values.UnitStat.MANA);
	}
}
