using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Sharpen")]
public class CC_Sharpen : CC_Base
{
	[Range(0f, 5f)]
	public float strength = 0.6f;

	[Range(0f, 1f)]
	public float clamp = 0.05f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (strength == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetFloat("_PX", 1.0f / (float)Screen.width);
		material.SetFloat("_PY", 1.0f / (float)Screen.height);
		material.SetFloat("_Strength", strength);
		material.SetFloat("_Clamp", clamp);
		Graphics.Blit(source, destination, material);
	}
}
