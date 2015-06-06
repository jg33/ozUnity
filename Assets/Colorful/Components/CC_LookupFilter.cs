using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Lookup Filter (Color Grading)")]
public class CC_LookupFilter : CC_Base
{
	public Texture lookupTexture;

	[Range(0f, 1f)]
	public float amount = 1f;

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (lookupTexture == null)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetTexture("_LookupTex", lookupTexture);
		material.SetFloat("_Amount", amount);
		Graphics.Blit(source, destination, material, IsLinear() ? 1 : 0);
	}
}
