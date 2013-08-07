using UnityEngine;
using System.Collections;

public class AOC2LogicHFAbility : AOC2HFSMLogic {

	AOC2LogicState _moveInRange;
	
	//AOC2LogicState _moveSquare;
	
	AOC2LogicState _useAbility;
	
	AOC2Ability _ability;
	
	AOC2Unit _unit;
	
	override public bool Complete
	{
		get
		{
			return _useAbility.Complete;
		}
	}
	
	public AOC2LogicHFAbility(AOC2Unit unit, AOC2Ability ability)
	{
		_unit = unit;
		_ability = ability;
		
		//float findGridSquareDist = _ability.range + AOC2ManagerReferences.gridManager.spaceSize;
		
		_moveInRange = new AOC2LogicMoveTowardTarget(_unit);
		//_moveSquare = new AOC2LogicFollowPath(_unit);
		_useAbility = new AOC2LogicUseAbility(_unit, _ability);
		
		_moveInRange.AddExit(new AOC2ExitTargetInRange(_useAbility, _unit, _ability.range));
		
		//_moveSquare.AddExit(new AOC2ExitTargetInRange(_useAbility, _unit, _ability.range));
		//_moveSquare.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, _unit, findGridSquareDist), _moveInRange));
		
		_useAbility.AddExit(new AOC2ExitNotOther(new AOC2ExitTargetInRange(null, _unit, _ability.range), _moveInRange));
		
		_current = _baseState = _useAbility;
		
	}
}
