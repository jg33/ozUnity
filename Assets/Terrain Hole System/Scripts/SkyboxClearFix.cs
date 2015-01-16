using UnityEngine;

[RequireComponent(typeof(Camera))] public class SkyboxClearFix : MonoBehaviour
{
	void Start() {}
	void OnPreRender()
	{
		if(camera.clearFlags == CameraClearFlags.Skybox)
			GL.ClearWithSkybox(true, camera);
	}
}