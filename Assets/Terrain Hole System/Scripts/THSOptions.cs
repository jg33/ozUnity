using UnityEngine;

[ExecuteInEditMode] public class THSOptions : MonoBehaviour
{
	public bool noShadowsMode = false;
	
	void Update()
	{
		if (noShadowsMode)
		{
			if (Shader.globalMaximumLOD != 1000)
				Shader.globalMaximumLOD = 1000;
		}
		else
			if (Shader.globalMaximumLOD != 1001)
				Shader.globalMaximumLOD = 1001;
	}
}