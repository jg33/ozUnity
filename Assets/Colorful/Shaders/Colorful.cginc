/* Utils */
half luminance(half3 color)
{
	return dot(color, half3(0.30, 0.59, 0.11));
}

half3 mod(half3 x, half3 y) // OpenGL version, different from CG/HLSL's fmod
{
	return x - y * floor(x / y);
}

half mod(half x, half y) // OpenGL version, different from CG/HLSL's fmod
{
	return x - y * floor(x / y);
}

half rot(half value, half low, half hi)
{
	return (value < low) ? value + hi : (value > hi) ? value - hi : value;
}

half rot10(half value)
{
	return rot(value, 0.0, 1.0);
}

half4 pixelate(sampler2D tex, half2 uv, half scale, half ratio)
{
	half ds = 1.0 / scale;
	half2 coord = half2(ds * ceil(uv.x / ds), (ds * ratio) * ceil(uv.y / ds / ratio));
	return half4(tex2D(tex, coord).xyzw);
}

half4 pixelate(sampler2D tex, half2 uv, half2 scale)
{
	half2 ds = 1.0 / scale;
	half2 coord = ds * ceil(uv / ds);
	return half4(tex2D(tex, coord).xyzw);
}

half simpleNoise(half x, half y, half seed, half phase)
{
	half n = x * y * phase * seed;
	return fmod(n, 13) * fmod(n, 123);
}

half invlerp(half from, half to, half value)
{
	return (value - from) / (to - from);
}

/* Color conversion */
half3 HSVtoRGB(half3 c)
{
	half4 K = half4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	half3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
	return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
}

half3 RGBtoHSV(half3 c)
{
	half4 K = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	half4 p = lerp(half4(c.bg, K.wz), half4(c.gb, K.xy), step(c.b, c.g));
	half4 q = lerp(half4(p.xyw, c.r), half4(c.r, p.yzx), step(p.x, c.r));

	half d = q.x - min(q.w, q.y);
	half e = 1.0e-10;
	return half3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

half RGBtoHUE(half3 c)
{
	half4 K = half4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	half4 p = lerp(half4(c.bg, K.wz), half4(c.gb, K.xy), step(c.b, c.g));
	half4 q = lerp(half4(p.xyw, c.r), half4(c.r, p.yzx), step(p.x, c.r));

	half d = q.x - min(q.w, q.y);
	return abs(q.z + (q.w - q.y) / (6.0 * d + 1.0e-10));
}

half3 HUEtoRGB(half h)
{
	half h6 = h * 6.0;
	half r = abs(h6 - 3.0) - 1.0;
	half g = 2.0 - abs(h6 - 2.0);
	half b = 2.0 - abs(h6 - 4.0);
	return saturate(half3(r, g, b));
}

half3 RGBtoHSL(half3 c)
{
	half3 hsv = RGBtoHSV(c);
	half l = hsv.z - hsv.y * 0.5;
	half s = hsv.y / (1.0 - abs(l * 2.0 - 1.0) + 1e-10);
	return half3(hsv.x, s, l);
}

half3 HSLtoRGB(half3 c)
{
   half3 rgb = HUEtoRGB(c.x);
   half C = (1.0 - abs(2.0 * c.z - 1.0)) * c.y;
   return (rgb - 0.5) * C + c.z;
}

half3 RGBtoYUV(half3 c)
{
	half3 yuv;
	yuv.x = dot(c, half3(0.299, 0.587, 0.114));
	yuv.y = dot(c, half3(-0.14713, -0.28886, 0.436));
	yuv.z = dot(c, half3(0.615, -0.51499, -0.10001));
	return yuv;
}

half3 YUVtoRGB(half3 c)
{
	half3 rgb;
	rgb.r = c.x + c.z * 1.13983;
	rgb.g = c.x + dot(half2(-0.39465, -0.58060), c.yz);
	rgb.b = c.x + c.y * 2.03211;
	return rgb;
}
