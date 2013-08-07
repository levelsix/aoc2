using UnityEngine;
using System.Collections;

public class AOC2ExitPlayerHasTarget : AOC2ExitLogicState {

    AOC2Player _unit;
    
    public AOC2ExitPlayerHasTarget(AOC2Player unit, AOC2LogicState state)
        : base(state)
    {
        _unit = unit;
    }
    
    public override bool Test ()
    {
        if (_unit.unit.targetUnit != null)
        {
            _unit.unit.targetPos = _unit.unit.targetUnit.aPos;
            return true;
        }
        return false;
    }
    
}
