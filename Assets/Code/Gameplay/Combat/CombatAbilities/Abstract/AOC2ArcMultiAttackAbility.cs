using UnityEngine;
using System.Collections;

/// <summary>
/// @author Rob Giusti
/// A special attack that fires multiple deliveries in an arc
/// from the user. Can define the arc size and number of deliveries
/// </summary>
public class AOC2ArcMultiAttackAbility : AOC2AttackAbility {
	
	/// <summary>
	/// The number of shots in this attack
	/// </summary>
	int _shots;
	
	/// <summary>
	/// The arc, stored in radians
	/// NOTE: Arc is passed in in degrees to make it more readable,
	/// it's chnaged in the constructor
	/// </summary>
	float _arc;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="AOC2ArcMultiAttackAbility"/> class.
	/// </summary>
	/// <param name='attack'>
	/// Attack.
	/// </param>
	/// <param name='abName'>
	/// Name.
	/// </param>
	/// <param name='cast'>
	/// Cast Time.
	/// </param>
	/// <param name='cool'>
	/// Cooldown Time.
	/// </param>
	/// <param name='mana'>
	/// Mana cost.
	/// </param>
	/// <param name='target'>
	/// Target position.
	/// </param>
	/// <param name='shots'>
	/// Number of shots
	/// </param>
	/// <param name='arc'>
	/// Arc width, in degrees
	/// </param>
	public AOC2ArcMultiAttackAbility(AOC2Attack attack, string abName, float cast, 
		float cool, int mana, AOC2Values.Abilities.TargetType target, int shots, float arc)
		: base(attack, abName, cast, cool, mana, target)
	{
		_shots = shots;
		_arc = arc * Mathf.Deg2Rad; //Store arc as radians, so that we don't have to repeatedly convert later
	}
    
    public AOC2ArcMultiAttackAbility(AOC2ArcMultiAttackAbility ability)
        :base(ability)
    {
        _shots = ability._shots;
        _arc = ability._arc;
    }
	
	/// <summary>
	/// Use the specified user, origin and target.
	/// </summary>
	/// <param name='user'>
	/// The user
	/// </param>
	/// <param name='origin'>
	/// The origin position of the delivery
	/// </param>
	/// <param name='target'>
	/// The target position
	/// </param>
	public override bool Use (AOC2Unit user, Vector3 origin, Vector3 target, bool ignoreCooldown = false)
	{
		if (!_onCool || ignoreCooldown)
		{
			float angle = Mathf.Atan2(target.z - origin.z, target.x - origin.x);
			float minAngle = angle - _arc/2;
			for (int i = 0; i < _shots; i++) 
			{
				angle = minAngle + i * _arc / _shots;
				Vector3 dir = new Vector3(Mathf.Cos (angle), 0, Mathf.Sin(angle));
				_attack.Use(user, origin, origin + dir); 
			}
			if (!_onCool)
			{
				AOC2ManagerReferences.combatManager.CoolAbility(this, user);
			}
			return true;
		}
		return false;
	}
}
