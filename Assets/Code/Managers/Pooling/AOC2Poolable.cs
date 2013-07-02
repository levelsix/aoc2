using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface AOC2Poolable {
	
	AOC2Poolable prefab {get;set;}
	GameObject gameObject {get;}
	Transform transform {get;}
	
	AOC2Poolable Make(Vector3 origin);
	
	void Pool();
	
}
