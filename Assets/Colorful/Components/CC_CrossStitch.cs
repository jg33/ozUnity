using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Cross Stitch")]
public class CC_CrossStitch : CC_Base
{
	[Range(1, 128)]
	public int size = 8;
	public float brightness = 1.5f;
	public bool invert = false;
	public bool pixelize = true;

	protected Camera m_Camera;

	protected override void Start()
	{
		base.Start();
		m_Camera = GetComponent<Camera>();
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_StitchSize", size);
		material.SetFloat("_Brightness", brightness);

		int pass = invert ? 1 : 0;

		if (pixelize)
		{
			pass += 2;
			material.SetFloat("_Scale", m_Camera.pixelWidth / size);
			material.SetFloat("_Ratio", m_Camera.pixelWidth / m_Camera.pixelHeight);
		}

		Graphics.Blit(source, destination, material, pass);
	}
}
