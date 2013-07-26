using UnityEngine;
using System.Collections;

///<summary>
///@author Rob Giusti
///Rewrite of PrefabGenerator.js from the Dynamic Elements Effects package
///Rewritten in C# using coroutines for efficiency and for ability to use
///alongside our other gameplay code without having to make cross-language
///calls. 
///</summary>
public class AOC2ParticleGenerator : MonoBehaviour {
	
	/// <summary>
	/// The list of prefabs that this can make.
	/// Note that they all have to be poolable.
	/// </summary>
	public AOC2Particle[] prefabs;

	public int thisManyTimes = 3;

	public float overThisTime = 1.0f;

	public float xWidth, yWidth, zWidth;

	public float xRotMax, yRotMax = 180, zRotMax;

	public bool allUseSameRotation = false;

	private bool allRotationDecided = false;

	private float x_cur, y_cur, z_cur;

	private float xRotCur, yRotCur, zRotCur;

	private Transform trans;

	void Awake()
	{
		trans = transform;
	}

	public void Init() 
	{
		StartCoroutine(RunGenerator());
	}
	
	// Update is called once per frame
	IEnumerator RunGenerator () 
	{
		int rndNr;
		for (int i = 0; i < thisManyTimes; i++)
		{
			yield return new WaitForSeconds(overThisTime / thisManyTimes);
			rndNr = (int)Mathf.Floor(Random.value * prefabs.Length);

			x_cur = trans.position.x + (Random.value * xWidth) - (xWidth * 0.5f);
			y_cur = trans.position.y + (Random.value * yWidth) - (yWidth * 0.5f);
			z_cur = trans.position.z + (Random.value * zWidth) - (zWidth * 0.5f);
			
			if (!allUseSameRotation || !allRotationDecided)
			{
				xRotCur = trans.rotation.x + (Random.value * xRotMax * 2) - (xRotMax);
				yRotCur = trans.rotation.y + (Random.value * yRotMax * 2) - (yRotMax);
				zRotCur = trans.rotation.z + (Random.value * zRotMax * 2) - (zRotMax);
				allRotationDecided = true;
			}
			
			AOC2Particle particle = AOC2ManagerReferences.poolManager.Get(prefabs[rndNr], new Vector3(x_cur, y_cur, z_cur)) as AOC2Particle;
			particle.Init();
		}
	}
}
