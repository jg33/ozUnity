using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Grayscale")]
public class CC_Grayscale : CC_Base
{
	[Range(0f, 1f)]
	public float redLuminance = 0.299f;
	
	[Range(0f, 1f)]
	public float greenLuminance = 0.587f;

	[Range(0f, 1f)]
	public float blueLuminance = 0.114f;

	[Range(0f, 1f)]
	public float amount = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		material.SetVector("_Data", new Vector4(redLuminance, greenLuminance, blueLuminance, amount));
		Graphics.Blit(source, destination, material);
	}
}
