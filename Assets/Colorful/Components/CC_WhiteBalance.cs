using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/White Balance")]
public class CC_WhiteBalance : CC_Base
{
	public Color white = new Color(0.5f, 0.5f, 0.5f);
	public int mode = 1;

	void Reset()
	{
		white = IsLinear() ? new Color(0.72974005284f, 0.72974005284f, 0.72974005284f) : new Color(0.5f, 0.5f, 0.5f);
	}

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetColor("_White", white);
		Graphics.Blit(source, destination, material, mode);
	}
}
