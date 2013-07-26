using UnityEngine;
using System.Collections;

public class AOC2AbilityButtonPanel : MonoBehaviour {
	
	[SerializeField]
	protected AOC2SetAttackButton abilityButtonPrefab;
	
	[SerializeField]
	protected float yOffset = 150;
	
	private Transform trans;
	
	void Awake()
	{
		trans = transform;
	}
	
	void OnEnable()
	{
		AOC2EventManager.Combat.OnSpawnPlayer += OnPlayerSpawn;
	}
	
	void OnDisable()
	{
		AOC2EventManager.Combat.OnSpawnPlayer -= OnPlayerSpawn;
	}
	
	/// <summary>
	/// When we spawn the player, get the list of their attacks
	/// and create the appropriate buttons
	/// </summary>
	/// <param name='unit'>
	/// Unit component of the player
	/// </param>
	void OnPlayerSpawn(AOC2Unit unit)
	{
		AOC2Player player = unit.GetComponent<AOC2Player>();
		AOC2SetAttackButton button;
		Transform butTrans;
		for (int i = 0; i < player.abilities.Length; i++) 
		{
			button = Instantiate(abilityButtonPrefab)
				as AOC2SetAttackButton;
			
			butTrans = button.transform;
			butTrans.parent = trans;
			butTrans.rotation = trans.rotation;
			butTrans.localPosition = new Vector3(0, yOffset*i, 15);
			butTrans.localScale = Vector3.one;
			
			button.Setup(player.abilities[i], i);
		}
	}
}
