using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Fast Vignette")]
public class CC_FastVignette : CC_Base
{
	public Vector2 center = new Vector2(0.5f, 0.5f);

	[Range(-100f, 100f)]
	public float sharpness = 10f;

	[Range(0f, 100f)]
	public float darkness = 30f;

	public bool desaturate = false;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("_Data", new Vector4(center.x, center.y, sharpness * 0.01f, darkness * 0.02f));
		Graphics.Blit(source, destination, material, desaturate ? 1 : 0);
	}
}
