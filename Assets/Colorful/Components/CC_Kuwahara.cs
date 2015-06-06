using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Kuwahara")]
public class CC_Kuwahara : CC_Base
{
	[Range(1, 4)]
	public int radius = 3;

	protected Camera m_Camera;

	protected override void Start()
	{
		base.Start();
		m_Camera = GetComponent<Camera>();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		radius = Mathf.Clamp(radius, 1, 4);
		material.SetVector("_TexelSize", new Vector2(1f / (float)m_Camera.pixelWidth, 1f / (float)m_Camera.pixelHeight));
		Graphics.Blit(source, destination, material, radius - 1);
	}
}
