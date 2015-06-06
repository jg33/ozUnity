using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Channel Swapper")]
public class CC_ChannelSwapper : CC_Base
{
	public int red = 0;
	public int green = 1;
	public int blue = 2;

	static Vector4[] m_Channels = new Vector4[]
	{
		new Vector4(1f, 0f, 0f, 0f),
		new Vector4(0f, 1f, 0f, 0f),
		new Vector4(0f, 0f, 1f, 0f)
	};

	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("_Red", m_Channels[red]);
		material.SetVector("_Green", m_Channels[green]);
		material.SetVector("_Blue", m_Channels[blue]);
		Graphics.Blit(source, destination, material);
	}
}
