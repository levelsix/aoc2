using UnityEngine;
using System.Collections;
using com.lvl6.proto;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class AOC2Sprite : MonoBehaviour {
	
	#region Members
	
	#region Public
	
	/// <summary>
	/// The ratio of the height to the width of this sprite. 
	/// Set individually so that ever
	/// </summary>
	public float height;
	
	#endregion
	
	#region Private
	
	/// <summary>
	/// The mesh filter, which we'll pass the mesh that we make
	/// </summary>
	private MeshFilter _filter;
	
	/// <summary>
	/// The _mesh, which we hold onto so that we can tint it and such
	/// </summary>
	private Mesh _mesh;

	#endregion
	
	#region Constant
	
	/// <summary>
	/// Constant building offset.
	/// </summary>
	const float BUILDING_Y_OFFSET = -.4f;
	
	#endregion
	
	#endregion
	
	#region Functions
	
	#region Instantiation/Clean-up
	
	/// <summary>
	/// Awake this instance.
	/// Get the mesh components
	/// </summary>
	void Awake()
	{
		_filter = GetComponent<MeshFilter>();
	}
	
	#endregion
	
	#region Mesh Generation
	
	/// <summary>
	/// Makes a mesh to use as a sprite for a building.
	/// Uses the quaternion of the camera to properly rotate the mesh
	/// </summary>
	/// <param name='building'>
	/// Building to generate a sprite for
	/// </param>
	public void MakeBuildingMesh(AOC2Building building)
	{	
		Quaternion rotation = Camera.main.transform.rotation;
		
		//Quaternion rotation = Quaternion.identity;
		
		Vector3[] vertices = {
			rotation * new Vector3(-building.width/2.0f * AOC2GridManager.SPACE_HYPOTENUSE, BUILDING_Y_OFFSET * building.width),
			rotation * new Vector3(-building.width/2.0f * AOC2GridManager.SPACE_HYPOTENUSE, BUILDING_Y_OFFSET * building.width + building.width * height),
			rotation * new Vector3(building.width/2.0f * AOC2GridManager.SPACE_HYPOTENUSE, BUILDING_Y_OFFSET * building.width + building.width * height),
			rotation * new Vector3(building.width/2.0f * AOC2GridManager.SPACE_HYPOTENUSE, BUILDING_Y_OFFSET * building.width)
		};
		
		MakeSpriteMesh(vertices);
		
	}
	
	/// <summary>
	/// Makes the background mesh.
	/// Uses the quaternion of the camera to properly rotate the mesh
	/// </summary>
	public void MakeBackgroundMesh()
	{
		Quaternion rotation = Camera.main.transform.rotation;
		
		Vector3[] vertices = {
			rotation * new Vector3(-height/2, -height/2),
			rotation * new Vector3(-height/2, height/2),
			rotation * new Vector3(height/2, height/2),
			rotation * new Vector3(height/2, -height/2)
		};
		
		MakeSpriteMesh(vertices);
	}
	
	/// <summary>
	/// Makes the sprite mesh from the given vertices
	/// </summary>
	/// <param name='vertices'>
	/// Four vertices to make the mesh at
	/// </param>
	public void MakeSpriteMesh(Vector3[] vertices)
	{
		_mesh = new Mesh();
		_filter.sharedMesh = _mesh;
		
		int[] triangles = { 0, 1, 2, 2, 3, 0 };
		
		Vector2[] uvs = {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};
		
		_mesh.vertices = vertices;
		_mesh.triangles = triangles;
		
		_filter.sharedMesh.uv = uvs;
		
		_mesh.RecalculateNormals();
		_mesh.RecalculateBounds();
	}
	
	#endregion
	
	#region Utilities
	
	/// <summary>
	/// Sets the color for each vertex
	/// </summary>
	/// <param name='color'>
	/// Color for the vertex color to be changed to
	/// </param>
	public void SetColor(Color color)
	{
		Color[] colors = {color, color, color, color};
		_mesh.colors = colors;
	}
	
	#endregion
	
	#endregion
	
}
