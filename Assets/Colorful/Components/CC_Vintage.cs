using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Colorful/Vintage")]
public class CC_Vintage : CC_LookupFilter
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

	public Filter filter = Filter.None;

	protected Filter m_CurrentFilter = Filter.None;

	protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (filter != m_CurrentFilter)
		{
			m_CurrentFilter = filter;

			if (filter == Filter.None)
				lookupTexture = null;
			else
				lookupTexture = Resources.Load<Texture2D>("Instagram/" + filter.ToString());
		}

		base.OnRenderImage(source, destination);
	}
}
