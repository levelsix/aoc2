using UnityEngine;
using System.Collections;
using proto;

public class AOC2MonsterList : MonoBehaviour {
	
	static bool defined = false;
	
	/// <summary>
	/// The prefabs, indexed by monster type
	/// </summary>
	public AOC2Unit[] prefabs;
	
	public static MonsterProto skeleton = new MonsterProto();
	public static MonsterProto redSkeleton = new MonsterProto();
	public static MonsterProto archer = new MonsterProto();
	public static MonsterProto king = new MonsterProto();
	
	public static CombatRoomProto roomA = new CombatRoomProto();
	public static CombatRoomProto roomB = new CombatRoomProto();
	
	void Awake()
	{
		AOC2ManagerReferences.monsterList = this;
	}
	
	void Start()
	{	
		if (!defined)
		{
		
			#region Monsters
			
			#region Skeleton
			skeleton.monsterID = 0;
			skeleton.maxHealth = 300;
			skeleton.attack = 40;
			skeleton.defense = 40;
			skeleton.type = MonsterType.GOBLIN;
			skeleton.color = new ColorProto();
			skeleton.color.r = skeleton.color.g = skeleton.color.b = 122;
			skeleton.size = 1f;
			skeleton.experience = 10;
			skeleton.isBoss = false;
			#endregion
			#region Red Skeleton
			redSkeleton.monsterID = 1;
			redSkeleton.maxHealth = 500;
			redSkeleton.attack = 60;
			redSkeleton.defense = 50;
			redSkeleton.type = MonsterType.GOBLIN;
			redSkeleton.color = new ColorProto();
			redSkeleton.color.g = 255;
			redSkeleton.color.r = redSkeleton.color.b = 122;
			redSkeleton.size = 1.2f;
			redSkeleton.experience = 50;
			redSkeleton.isBoss = false;
			#endregion
			
			#endregion
			
			#region Rooms
			
			#region roomA
			roomA.dungeon = 0;
			roomA.roomID = 0;
			roomA.monsters.AddRange(new int[] {0, 0, 1, 0, 0, 0, 1, 1, 0});
			roomA.spawnPoint.AddRange(new int[] {0, 0, 0, 1, 1, 1, 2, 2, 2});
			#endregion
			#region roomB
			roomB.dungeon = 0;
			roomB.roomID = 1;
			roomB.monsters.AddRange(new int[] {0, 0, 0, 1, 2, 0, 0, 0, 2, 2, 0, 0, 0, 1, 2, 2, 3});
			roomB.spawnPoint.AddRange(new int[] {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2});
			#endregion
			
			#endregion
			
			Load (skeleton);
			Load (redSkeleton);
			
			Load (roomA);
			Load (roomB);
			
			defined = true;
			
		}
	}
	
	void Load(MonsterProto proto)
	{
		AOC2ManagerReferences.dataManager.Load(proto, proto.monsterID);
	}
	
	void Load(CombatRoomProto proto)
	{
		AOC2ManagerReferences.dataManager.Load(proto, AOC2Math.GlobalRoomID(proto));
	}
	
}
