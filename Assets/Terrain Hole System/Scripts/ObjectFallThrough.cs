using UnityEngine;
using System.Collections.Generic;

public enum FallThroughColliders
{	
	InThisObject,
	InThisObjectAndChildren,
	NoneOrManual
}
public class ObjectFallThrough : MonoBehaviour
{
	public GameObject[] m_terrainObjects;
	public bool m_autoAddTerrainObjects = true;
	public float m_fallThroughOpacity = .75f;
	public FallThroughColliders m_fallThroughColliders = FallThroughColliders.InThisObjectAndChildren;
	public Collider[] m_extraFallThroughColliders;
	public bool m_snapUpOnTerrainContact = true;
	public float m_snapUpDistanceOffset = 1;
	public float m_maxSnapUpDistance = 2;
	
	List<GameObject> m_terrainObjectList = new List<GameObject>();
	List<TerrainData> m_terrainDataList = new List<TerrainData>();
	List<bool> m_fallenThroughList = new List<bool>();
	
	void Start()
	{
		foreach(GameObject terrainObject in m_terrainObjects)
			AddTerrainObjectToList(terrainObject);
	}
	
	void OnControllerColliderHit(ControllerColliderHit col) // if using a character controller
	{
		if (m_autoAddTerrainObjects && col.collider is TerrainCollider && col.transform.GetComponent<TerrainTransparency>() && !m_terrainObjectList.Contains(col.collider.gameObject))
			AddTerrainObjectToList(col.collider.gameObject);
	}
	void OnCollisionEnter(Collision col) // if using a regular collider
	{		
		if (m_autoAddTerrainObjects && col.collider is TerrainCollider && col.transform.GetComponent<TerrainTransparency>() && !m_terrainObjectList.Contains(col.collider.gameObject))
			AddTerrainObjectToList(col.collider.gameObject);
	}
	
	void AddTerrainObjectToList(GameObject terObj)
	{
		m_terrainObjectList.Add(terObj);
		
		TerrainData terrainData = terObj.GetComponent<Terrain>().terrainData; // only grab once
		
		m_terrainDataList.Add(terrainData);
		m_fallenThroughList.Add(false);
	}
	
	void Update()
	{
		for (var i = 0;i < m_terrainObjectList.Count;i++)
		{
			GameObject terrainObject = m_terrainObjectList[i];
			if (terrainObject == null) // must have been destroyed by another script
				continue;
			TerrainData terrainData = m_terrainDataList[i];
			bool fallenThrough = m_fallenThroughList[i];
			
			float opacity = GetOpacityAt(transform.position - terrainObject.transform.position, terrainData, terrainObject.GetComponent<TerrainTransparency>()); // use position relative to terrain origin
			bool fallThrough = opacity <= m_fallThroughOpacity;
			
			if (m_snapUpOnTerrainContact && fallThrough != fallenThrough && !fallThrough) // if snapping is enabled, and fall-through status is going to change to above-ground
			{
				float dif = (terrainObject.GetComponent<Terrain>().SampleHeight(transform.position) - transform.position.y) + m_snapUpDistanceOffset;	
				if (dif > 0) // if ground is above us (we don't want to 'waste' our 'snap back up' when terrain's actually under us)
					if (dif < m_maxSnapUpDistance) // if snap distance is within max
						transform.Translate(new Vector3(0, dif, 0)); // snap up
					else
						fallThrough = fallenThrough; // cancel changing of fall-through status to above-ground
			}
			
			if (fallThrough != fallenThrough) // if fall-through status is about to change
			{
				m_fallenThroughList[i] = fallThrough;
				if (m_fallThroughColliders == FallThroughColliders.InThisObject)
					foreach (Collider tCol in terrainObject.GetComponents<Collider>())
					{
						foreach (Collider col in GetComponentsInChildren<Collider>())
							Physics.IgnoreCollision(tCol, col, fallThrough);
						foreach (Collider col in m_extraFallThroughColliders)
							Physics.IgnoreCollision(tCol, col, fallThrough);
					}
				else if (m_fallThroughColliders == FallThroughColliders.InThisObjectAndChildren)
					foreach (Collider tCol in terrainObject.GetComponents<Collider>())
					{
						foreach (Collider col in GetComponentsInChildren<Collider>())
							Physics.IgnoreCollision(tCol, col, fallThrough);
						foreach (Collider col in m_extraFallThroughColliders)
							Physics.IgnoreCollision(tCol, col, fallThrough);
					}
				else // the user is setting all object-to-fall-through colliders manually
					foreach (Collider tCol in terrainObject.GetComponents<Collider>())
						foreach (Collider col in m_extraFallThroughColliders)
							Physics.IgnoreCollision(tCol, col, fallThrough);
			}
		}
	}
	float GetOpacityAt(Vector3 position, TerrainData terrainData, TerrainTransparency terrainTransparencySettings)
	{
		var currentLocationOnTerrainAlphaMap = new Vector2((terrainData.alphamapResolution / terrainData.size.x) * position.x, (terrainData.alphamapResolution / terrainData.size.z) * position.z);

	    var aPos = (int)currentLocationOnTerrainAlphaMap.y; // for some reason we need to flip them
	    var bPos = (int)currentLocationOnTerrainAlphaMap.x;
		
		if (aPos >= 0 && aPos < terrainData.alphamapResolution && bPos >= 0 && bPos < terrainData.alphamapResolution) // if in-bounds
		{
			float transparency = terrainTransparencySettings.transparencyMap.GetPixel(bPos, aPos).a; //terrainAlphamaps[aPos, bPos, slotIndex];			
			float opacity = 1.0f - transparency;
			if (terrainTransparencySettings.cutoutMode)
				opacity = opacity > terrainTransparencySettings.cutoutModeHideAlpha ? 1 : 0;
			return opacity;
		}
		
		return 1; // if no texture detected underneath, assume we're on solid/opaque terrain
	}
}