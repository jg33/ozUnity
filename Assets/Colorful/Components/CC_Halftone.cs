using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Halftone")]
public class CC_Halftone : CC_Base
{
	[Range(0f, 512f)]
	public float density = 64.0f;
	public int mode = 1;
	public bool antialiasing = true;
	public bool showOriginal = false;

	protected Camera m_Camera;

	protected override void Start()
	{
		base.Start();
		m_Camera = GetComponent<Camera>();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Density", density);
		material.SetFloat("_AspectRatio", m_Camera.aspect);

		int pass = 0;

		// Black and white
		if (mode == 0)
		{
			if (antialiasing && showOriginal) pass = 3;
			else if (antialiasing) pass = 1;
			else if (showOriginal) pass = 2;
		}

		// CMYK
		else if (mode == 1)
		{
			pass = antialiasing ? 5 : 4;
		}

		Graphics.Blit(source, destination, material, pass);
	}
}
