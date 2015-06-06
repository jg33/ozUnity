using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Channel Mixer")]
public class CC_ChannelMixer : CC_Base
{
	[Range(-200f, 200f)]
	public float redR = 100.0f;

	[Range(-200f, 200f)]
	public float redG = 0.0f;

	[Range(-200f, 200f)]
	public float redB = 0.0f;

	[Range(-200f, 200f)]
	public float greenR = 0.0f;

	[Range(-200f, 200f)]
	public float greenG = 100.0f;

	[Range(-200f, 200f)]
	public float greenB = 0.0f;

	[Range(-200f, 200f)]
	public float blueR = 0.0f;

	[Range(-200f, 200f)]
	public float blueG = 0.0f;

	[Range(-200f, 200f)]
	public float blueB = 100.0f;

	[Range(-200f, 200f)]
	public float constantR = 0.0f;

	[Range(-200f, 200f)]
	public float constantG = 0.0f;

	[Range(-200f, 200f)]
	public float constantB = 0.0f;

	public int currentChannel = 0;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("_Red", new Vector4(redR * 0.01f, greenR * 0.01f, blueR * 0.01f));
		material.SetVector("_Green", new Vector4(redG * 0.01f, greenG * 0.01f, blueG * 0.01f));
		material.SetVector("_Blue", new Vector4(redB * 0.01f, greenB * 0.01f, blueB * 0.01f));
		material.SetVector("_Constant", new Vector4(constantR * 0.01f, constantG * 0.01f, constantB * 0.01f));

		Graphics.Blit(source, destination, material);
	}
}
