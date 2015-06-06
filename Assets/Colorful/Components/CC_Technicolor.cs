using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Technicolor")]
public class CC_Technicolor : CC_Base
{
	[Range(0f, 8f)]
	public float exposure = 4f;

	public Vector3 balance = new Vector3(0.25f, 0.25f, 0.25f);

	[Range(0f, 1f)]
	public float amount = 0.5f;

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Exposure", 8f - exposure);
		material.SetVector("_Balance", Vector3.one - balance);
		material.SetFloat("_Amount", amount);
		Graphics.Blit(source, destination, material);
	}
}
