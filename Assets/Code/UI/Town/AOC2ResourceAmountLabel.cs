using UnityEngine;
using System.Collections;
using System;

public class AOC2ResourceAmountLabel : MonoBehaviour {
	
	UILabel label;
	
	[SerializeField]
	AOC2Values.Buildings.ResourceType resource;
	
	void Awake()
	{
		label = GetComponent<UILabel>();
	}
	
	void OnEnable()
	{
		AOC2EventManager.UI.OnChangeResource[(int)resource] += OnChangeGold;
	}
	
	void OnDisable()
	{
		AOC2EventManager.UI.OnChangeResource[(int)resource] -= OnChangeGold;
	}
	
	void OnChangeGold(int amount)
	{
		string formatted = String.Format("{0:n0}", amount);
		label.text = formatted;
	}
}
