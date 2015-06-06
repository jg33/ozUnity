#include "UnityCG.cginc"
#include "Colorful.cginc"

sampler2D _MainTex;
half4 _Params; // x: speed, y: intensity, z: maxDisplace, w: yuv offset

inline half rand(half2 seed)
{
	return frac(sin(dot(seed, half2(12.9898, 78.233))) * 43758.5453);
}

inline half offset(half x, half levels)
{
	return floor(x * levels) / levels;
}

inline half2 offset(half2 x, half levels)
{
	return floor(x * levels) / levels;
}

fixed4 frag_tearing(v2f_img i):COLOR
{
	half time = mod(_Time.y * _Params.x, 32.0);
	half change = rand(offset(i.uv.yy, 16.0) + 150.0 * offset(time, 4.0));

	half t = 5.0 * offset(time, 16.0 * change);
	half vt = 0.5 * rand(offset(i.uv.yy + t, 11.0));
	vt += 0.5 * rand(offset(i.uv.yy + t, 7.0));
	vt = vt * 2.0 - 1.0;
	vt = sign(vt) * saturate((abs(vt) - (1.0 - _Params.y)) / _Params.y);

	half2 texcoords = i.uv;
	texcoords = saturate(texcoords + half2(_Params.z * vt, 0.0));

	#ifdef ALLOW_FLIPPING
	half tt = offset(time, 8.0);
	half rnd = rand(half2(tt, tt));
	texcoords.y = (rnd > lerp(1.0, 0.975, _Params.y)) ? 1.0 - texcoords.y : texcoords.y;
	#endif

	half3 color = tex2D(_MainTex, texcoords).rgb;

	#ifdef YUV_COLOR_BLEEDING
	half3 yuv = RGBtoYUV(color);
	yuv.y /= 1.0 - 3.0 * abs(vt) * saturate(_Params.w - vt);
	yuv.z += 0.125 * vt * saturate(vt - _Params.w);
	return half4(YUVtoRGB(yuv), 1.0);
	#else
	return half4(color, 1.0);
	#endif
}
