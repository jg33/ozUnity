using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Bleach Bypass")]
public class CC_BleachBypass : CC_Base
{
	[Range(0f, 1f)]
	public float amount = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetFloat("_Amount", amount);
		Graphics.Blit(source, destination, material);
	}
}
