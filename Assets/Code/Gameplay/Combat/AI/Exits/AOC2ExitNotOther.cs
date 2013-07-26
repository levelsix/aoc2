using UnityEngine;
using System.Collections;

public class AOC2ExitNotOther : AOC2ExitLogicState {

	AOC2ExitLogicState _other;
    
    public AOC2ExitNotOther(AOC2ExitLogicState other, AOC2LogicState state)
        :base(state)
    {
        _other = other;
    }
    
    public override bool Test()
    {
        return !_other.Test();
    }
    
}
