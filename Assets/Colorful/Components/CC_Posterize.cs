using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Posterize")]
public class CC_Posterize : CC_Base
{
	[Range(2, 255)]
	public int levels = 4;

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Levels", (float)levels);
		Graphics.Blit(source, destination, material);
	}
}
