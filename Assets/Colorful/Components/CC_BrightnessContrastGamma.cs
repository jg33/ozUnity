using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Brightness, Contrast, Gamma")]
public class CC_BrightnessContrastGamma : CC_Base
{
	[Range(-100f, 100f)]
	public float brightness = 0.0f;

	[Range(-100f, 100f)]
	public float contrast = 0.0f;

	[Range(0f, 1f)]
	public float redCoeff = 0.5f;

	[Range(0f, 1f)]
	public float greenCoeff = 0.5f;

	[Range(0f, 1f)]
	public float blueCoeff = 0.5f;

	[Range(0.1f, 9.9f)]
	public float gamma = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (brightness == 0f && contrast == 0f && gamma == 1f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetVector("_BCG", new Vector4((brightness + 100f) * 0.01f, (contrast + 100f) * 0.01f, 1.0f / gamma));
		material.SetVector("_Coeffs", new Vector4(redCoeff, greenCoeff, blueCoeff, 1.0f));
		Graphics.Blit(source, destination, material);
	}
}
