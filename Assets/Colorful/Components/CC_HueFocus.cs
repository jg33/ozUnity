using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Hue Focus")]
public class CC_HueFocus : CC_Base
{
	[Range(0f, 360f)]
	public float hue = 0f;

	[Range(1f, 180f)]
	public float range = 30f;

	[Range(0f, 1f)]
	public float boost = 0.5f;

	[Range(0f, 1f)]
	public float amount = 1f;

	[ImageEffectTransformsToLDR]
	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		float h = hue / 360f;
		float r = range / 180f;
		material.SetVector("_Range", new Vector2(h - r, h + r));
		material.SetVector("_Params", new Vector3(h, boost + 1f, amount));
		Graphics.Blit(source, destination, material);
	}
}
