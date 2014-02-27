using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface AOC2Poolable {
	
	AOC2Poolable prefab {get;set;}
	GameObject gObj {get;}
	Transform transf {get;}
	
	AOC2Poolable Make(Vector3 origin);
	
	void Pool();
	
}
