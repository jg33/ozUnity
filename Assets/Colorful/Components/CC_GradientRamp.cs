using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Gradient Ramp")]
public class CC_GradientRamp : CC_Base
{
	public Texture rampTexture;

	[Range(0f, 1f)]
	public float amount = 1f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (rampTexture == null || amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetTexture("_RampTex", rampTexture);
		material.SetFloat("_Amount", amount);
		Graphics.Blit(source, destination, material);
	}
}
