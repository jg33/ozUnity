using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Hue, Saturation, Value")]
public class CC_HueSaturationValue : CC_Base
{
	#region Retro compatibility
	public float hue { get { return masterHue; } set { masterHue = value; } }
	public float saturation { get { return masterSaturation; } set { masterSaturation = value; } }
	public float value { get { return masterValue; } set { masterValue = value; } }
	#endregion

	[Range(-180f, 180f)]
	public float masterHue = 0.0f;
	[Range(-100f, 100f)]
	public float masterSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float masterValue = 0.0f;

	[Range(-180f, 180f)]
	public float redsHue = 0.0f;
	[Range(-100f, 100f)]
	public float redsSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float redsValue = 0.0f;

	[Range(-180f, 180f)]
	public float yellowsHue = 0.0f;
	[Range(-100f, 100f)]
	public float yellowsSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float yellowsValue = 0.0f;

	[Range(-180f, 180f)]
	public float greensHue = 0.0f;
	[Range(-100f, 100f)]
	public float greensSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float greensValue = 0.0f;

	[Range(-180f, 180f)]
	public float cyansHue = 0.0f;
	[Range(-100f, 100f)]
	public float cyansSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float cyansValue = 0.0f;

	[Range(-180f, 180f)]
	public float bluesHue = 0.0f;
	[Range(-100f, 100f)]
	public float bluesSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float bluesValue = 0.0f;

	[Range(-180f, 180f)]
	public float magentasHue = 0.0f;
	[Range(-100f, 100f)]
	public float magentasSaturation = 0.0f;
	[Range(-100f, 100f)]
	public float magentasValue = 0.0f;

	public bool advanced = false;
	public int currentChannel = 0;

	[ImageEffectTransformsToLDR]
	protected virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		material.SetVector("_Master", new Vector3(masterHue / 360f, masterSaturation * 0.01f, masterValue * 0.01f));

		if (advanced)
		{
			material.SetVector("_Reds", new Vector3(redsHue / 360f, redsSaturation * 0.01f, redsValue * 0.01f));
			material.SetVector("_Yellows", new Vector3(yellowsHue / 360f, yellowsSaturation * 0.01f, yellowsValue * 0.01f));
			material.SetVector("_Greens", new Vector3(greensHue / 360f, greensSaturation * 0.01f, greensValue * 0.01f));
			material.SetVector("_Cyans", new Vector3(cyansHue / 360f, cyansSaturation * 0.01f, cyansValue * 0.01f));
			material.SetVector("_Blues", new Vector3(bluesHue / 360f, bluesSaturation * 0.01f, bluesValue * 0.01f));
			material.SetVector("_Magentas", new Vector3(magentasHue / 360f, magentasSaturation * 0.01f, magentasValue * 0.01f));
			Graphics.Blit(source, destination, material, 1);
		}
		else
		{
			Graphics.Blit(source, destination, material, 0);
		}
	}
}
