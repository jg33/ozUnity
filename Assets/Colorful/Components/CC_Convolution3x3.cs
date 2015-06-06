using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Convolution Matrix 3x3")]
public class CC_Convolution3x3 : CC_Base
{
	public Vector3 kernelTop = Vector3.zero;
	public Vector3 kernelMiddle = Vector3.up;
	public Vector3 kernelBottom = Vector3.zero;
	public float divisor = 1.0f;

	[Range(0f, 1f)]
	public float amount = 1.0f;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_PX", 1.0f / (float)Screen.width);
		material.SetFloat("_PY", 1.0f / (float)Screen.height);
		material.SetFloat("_Amount", amount);
		material.SetVector("_KernelT", kernelTop / divisor);
		material.SetVector("_KernelM", kernelMiddle / divisor);
		material.SetVector("_KernelB", kernelBottom / divisor);
		Graphics.Blit(source, destination, material);
	}
}
