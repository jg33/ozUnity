using UnityEngine;

[RequireComponent(typeof(Camera))] public class SkyboxClearFix : MonoBehaviour
{
	void Start() {}
	void OnPreRender()
	{
		if(GetComponent<Camera>().clearFlags == CameraClearFlags.Skybox)
			GL.ClearWithSkybox(true, GetComponent<Camera>());
	}
}