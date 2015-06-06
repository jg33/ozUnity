using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Wiggle")]
public class CC_Wiggle : CC_Base
{
	public float timer = 0.0f;
	public float speed = 1.0f;
	public float scale = 12.0f;
	public bool autoTimer = true;

	void Update()
	{
		if (autoTimer)
			timer += speed * Time.deltaTime;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Timer", timer);
		material.SetFloat("_Scale", scale);
		Graphics.Blit(source, destination, material);
	}
}
