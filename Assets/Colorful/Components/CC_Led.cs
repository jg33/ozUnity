using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/LED")]
public class CC_Led : CC_Base
{
	[Range(1f, 255f)]
	public float scale = 80.0f;

	[Range(0f, 10f)]
	public float brightness = 1.0f;

	public bool automaticRatio = false;
	public float ratio = 1.0f;
	public int mode = 0;

	protected Camera m_Camera;

	protected override void Start()
	{
		base.Start();
		m_Camera = GetComponent<Camera>();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		switch (mode)
		{
			case 0:
				material.SetFloat("_Scale", scale);
				break;
			case 1:
			default:
				material.SetFloat("_Scale", m_Camera.pixelWidth / scale);
				break;
		}

		material.SetFloat("_Ratio", automaticRatio ? ((float)m_Camera.pixelWidth / (float)m_Camera.pixelHeight) : ratio);
		material.SetFloat("_Brightness", brightness);
		Graphics.Blit(source, destination, material);
	}
}
