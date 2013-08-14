using UnityEngine;
using System.Collections;

public class AOC2ExitPlayerHasTarget : AOC2ExitLogicState {

    AOC2Player _player;
    
    public AOC2ExitPlayerHasTarget(AOC2Player player, AOC2LogicState state)
        : base(state)
    {
        _player = player;
    }
    
    public override bool Test ()
    {
        if (_player.unit.targetUnit != null)
        {
            _player.unit.targetPos = _player.unit.targetUnit.aPos;
            return true;
        }
        return false;
    }
    
}
