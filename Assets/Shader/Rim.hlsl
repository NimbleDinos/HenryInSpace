void RimLighting_float(float4 RimColor, float3 WorldNormal, float3 ViewNormal, float RimThreshold, float3 Light, float RimAmount, out float4 RimOut)
{
	float WdotL = dot(Light, WorldNormal);

	float4 rimDot = dot(ViewNormal, WorldNormal);
	float rimIntensity = rimDot * pow(WdotL, RimThreshold);
	rimIntensity = smoothstep(RimAmount - 0.01, RimAmount + 0.01, rimIntensity);
	RimOut = rimIntensity * RimColor;
}