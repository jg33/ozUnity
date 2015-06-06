using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vibrance")]
public class CC_Vibrance : CC_Base
{
	[Range(-100f, 100f)]
	public float amount = 0.0f;

	[Range(-5f, 5f)]
	public float redChannel = 1.0f;
	[Range(-5f, 5f)]
	public float greenChannel = 1.0f;
	[Range(-5f, 5f)]
	public float blueChannel = 1.0f;

	public bool advanced = false;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (amount == 0f)
		{
			Graphics.Blit(source, destination);
			return;
		}

		if (advanced)
		{
			material.SetFloat("_Amount", amount * 0.01f);
			material.SetVector("_Channels", new Vector3(redChannel, greenChannel, blueChannel));
			Graphics.Blit(source, destination, material, 1);
		}
		else
		{
			material.SetFloat("_Amount", amount * 0.02f);
			Graphics.Blit(source, destination, material, 0);
		}
	}
}
