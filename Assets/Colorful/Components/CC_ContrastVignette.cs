using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Contrast Vignette")]
public class CC_ContrastVignette : CC_Base
{
	public Vector2 center = new Vector2(0.5f, 0.5f);

	[Range(-100f, 100f)]
	public float sharpness = 32f;

	[Range(0f, 100f)]
	public float darkness = 28f;

	[Range(0f, 200f)]
	public float contrast = 20.0f;

	[Range(0f, 1f)]
	public float redCoeff = 0.5f;

	[Range(0f, 1f)]
	public float greenCoeff = 0.5f;

	[Range(0f, 1f)]
	public float blueCoeff = 0.5f;

	[Range(0f, 200f)]
	public float edge = 0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("_Data", new Vector4(sharpness * 0.01f, darkness * 0.02f, contrast * 0.01f, edge * 0.01f));
		material.SetVector("_Coeffs", new Vector4(redCoeff, greenCoeff, blueCoeff, 1.0f));
		material.SetVector("_Center", center);
		Graphics.Blit(source, destination, material);
	}
}
