using UnityEngine;
using System.Collections;

public class AOC2LogicUseAttack : AOC2LogicState {
	
	private AOC2Unit _unit;
	
	private AOC2Attack _attack;
	
	private bool _isEnemy;
	
	public AOC2LogicUseAttack(AOC2Unit unit, AOC2Attack att, bool enemy)
		: base()
	{
		_attack = att;
		_unit = unit;
	}
	
	protected override IEnumerator Logic ()
	{
		while(true)
		{
			//Wait for the cast time if there is one
			if (_attack.castTime > 0)
			{
				yield return new WaitForSeconds(_attack.castTime);
			}
			
			AOC2Delivery deliv = null;
			while(deliv == null)
			{
				deliv = _attack.Use(_unit.aPos.position,
					(_unit.targetPos.position - _unit.aPos.position).normalized);
				if(deliv == null)
				{
					yield return null;
				}
			}
			
			if(deliv != null){
				if(_unit.isEnemy)
				{
					deliv.gameObject.layer = AOC2Values.Layers.TARGET_PLAYER;
				}
				else
				{
					deliv.gameObject.layer = AOC2Values.Layers.TARGET_ENEMY;
				}
			}
			yield return null;
		}
	}
	
}
