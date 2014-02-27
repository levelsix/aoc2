using UnityEngine;
using System.Collections;
using proto;

public class AOC2SpawnMonster : AOC2Spawnable, AOC2Poolable {
	
	public AOC2Unit _prefab;
	
	public AOC2Poolable prefab {
		get {
			return _prefab;
		}
		set {
			if (_prefab == null)
			{
				_prefab = value as AOC2Unit;
			}
		}
	}
	
	public GameObject gObj {
		get {
			throw new System.NotImplementedException ();
		}
	}
	
	public Transform transf {
		get {
			throw new System.NotImplementedException ();
		}
	}
	
	public MonsterProto proto;
	
	public AOC2SpawnMonster(MonsterProto prot)
	{
		proto = prot;
		_prefab = AOC2ManagerReferences.monsterList.prefabs[(int)prot.type];
	}
	
	public AOC2Poolable Make (Vector3 origin)
	{
		AOC2Unit unit = _prefab.Make(origin) as AOC2Unit;
		unit.prefab = this;
		unit.name = "Monster" + proto.monsterID;
		unit.TintModel(new Color(proto.color.r/255f, proto.color.g/255f, proto.color.b/255f));
		return unit;
	}
	
	public void Pool ()
	{
		throw new System.NotImplementedException ();
	}
	
	/// <summary>
	/// Return a spawnable instance
	/// </summary>
	public void Spawn(Vector3 origin, AOC2UnitSpawner parent)
	{		
		AOC2Unit unit = AOC2ManagerReferences.poolManager.Get(this, origin) as AOC2Unit;
		unit.Init(proto);
		if (parent != null)
		{
			parent.AddUnit(unit);
			unit.trans.parent = parent.trans;
		}
	}
	
	public System.Collections.Generic.Dictionary<AOC2Unit, int> GetCounts ()
	{
		return new System.Collections.Generic.Dictionary<AOC2Unit, int>() {{_prefab, 1}};
	}
}
