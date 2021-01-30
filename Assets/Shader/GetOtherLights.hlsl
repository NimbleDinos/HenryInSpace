void OtherLights_half(float3 WorldPos, out half3 Direction, out half3 Color, out half DistanceAtten, out half ShadowAtten) 
{
#if SHADERGRAPH_PREVIEW
	Direction = half3(0.5, 0.5, 0);
	Color = 1;
	DistanceAtten = 1;
	ShadowAtten = 1;
#else
#if SHADOWS_SCREEN
	half4 clipPos = TransformWorldToHClip(WorldPos);
	half4 shadowCoord = ComputeScreenPos(clipPos);
#else
	half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif
	int lightCount = GetAdditionalLightsCount();

	for (int i = 0; i < lightCount; i++)
	{
		Light light = GetAdditionalLight(i, WorldPos);

		Direction += light.direction;
		Color += light.color;
		ShadowAtten += light.shadowAttenuation;
	}

	DistanceAtten = half(1);
#endif
}