using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/RGB Split")]
public class CC_RGBSplit : CC_Base
{
	public float amount = 0f;
	public float angle = 0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetFloat("_RGBShiftAmount", amount * 0.001f);
		material.SetFloat("_RGBShiftAngleCos", Mathf.Cos(angle));
		material.SetFloat("_RGBShiftAngleSin", Mathf.Sin(angle));
		Graphics.Blit(source, destination, material);
	}
}
