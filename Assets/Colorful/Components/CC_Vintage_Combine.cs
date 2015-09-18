using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vintage_Combine")]
public class CC_Vintage_Combine : CC_Base
{
	public enum Filter
	{
		None,
		F1977,
		Aden,
		Amaro,
		Brannan,
		Crema,
		Earlybird,
		Hefe,
		Hudson,
		Inkwell,
		Juno,
		Kelvin,
		Lark,
		LoFi,
		Ludwig,
		Mayfair,
		Nashville,
		Perpetua,
		Reyes,
		Rise,
		Sierra,
		Slumber,
		Sutro,
		Toaster,
		Valencia,
		Walden,
		Willow,
		XProII
	}

	[Range(0f, 360f)]
	public float hue = 0f;

	[Range(1f, 180f)]
	public float range = 30f;

	[Range(0f, 1f)]
	public float boost = 0.5f;
	
	[Range(0f, 1f)]
	public float hue_amount = 1f;

	[Range(0f, 1f)]
	public float vintage_amount = 1f;

	public Filter filter = Filter.None;

	private Texture lookupTexture;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (filter == Filter.None)
			lookupTexture = null;
		else
			lookupTexture = Resources.Load<Texture2D>("Instagram/" + filter.ToString());

		float h = hue / 360f;
		float r = range / 180f;

		material.SetVector("_Range", new Vector2(h - r, h + r));
		material.SetVector("_Params", new Vector3(h, boost + 1f, hue_amount));
		material.SetTexture("_LookupTex", lookupTexture);
		material.SetFloat("_Amount", vintage_amount);

		//Graphics.Blit(source, destination, material);
		Graphics.Blit(source, destination, material, IsLinear() ? 1 : 0);
	}

}
