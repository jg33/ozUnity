using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Frost")]
public class CC_Frost : CC_Base
{
	[Range(0f, 16f)]
	public float scale = 1.2f;

	[Range(-100f, 100f)]
	public float sharpness = 40f;

	[Range(0f, 100f)]
	public float darkness = 35f;

	public bool enableVignette = true;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (scale == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetFloat("_Scale", scale);
		material.SetFloat("_Sharpness", sharpness * 0.01f);
		material.SetFloat("_Darkness", darkness * 0.02f);
		Graphics.Blit(source, destination, material, enableVignette ? 1 : 0);
	}
}
