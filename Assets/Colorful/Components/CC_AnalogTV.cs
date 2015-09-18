using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Analog TV")]
public class CC_AnalogTV : CC_Base
{
	public bool autoPhase = true;
	public float phase = 0.5f;
	public bool grayscale = false;

	[Range(0f, 1f)]
	public float noiseIntensity = 0.5f;

	[Range(0f, 10f)]
	public float scanlinesIntensity = 2.0f;

	[Range(0f, 4096f)]
	public float scanlinesCount = 768f;

	public float scanlinesOffset = 0.0f;

	[Range(-2f, 2f)]
	public float distortion = 0.2f;

	[Range(-2f, 2f)]
	public float cubicDistortion = 0.6f;

	[Range(0.01f, 2f)]
	public float scale = 0.8f;

	protected virtual void Update()
	{
		if (autoPhase)
			phase += Time.deltaTime * 0.25f;
	}

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Phase", phase);
		material.SetFloat("_NoiseIntensity", noiseIntensity);
		material.SetFloat("_ScanlinesIntensity", scanlinesIntensity);
		material.SetFloat("_ScanlinesCount", (int)scanlinesCount);
		material.SetFloat("_ScanlinesOffset", scanlinesOffset);
		material.SetFloat("_Distortion", distortion);
		material.SetFloat("_CubicDistortion", cubicDistortion);
		material.SetFloat("_Scale", scale);
		Graphics.Blit(source, destination, material, grayscale ? 1 : 0);
	}
}
