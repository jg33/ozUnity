using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Radial Blur")]
public class CC_RadialBlur : CC_Base
{
	[Range(0f, 1f)]
	public float amount = 0.1f;

	[Range(2, 24)]
	public int samples = 10;

	public Vector2 center = new Vector2(0.5f, 0.5f);
	public int quality = 1;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetFloat("_Amount", amount);
		material.SetVector("_Center", center);
		material.SetFloat("_Samples", samples);

		Graphics.Blit(source, destination, material, quality);
	}
}
