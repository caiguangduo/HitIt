Shader "Leon/Dif_Bump_Light_Ref_Spec_CutOff" 
{
	Properties 
	{
		_Color("Color(Diffuse)",Color)=(.5,.5,.5,1)
		_MainTex("MainTex(uv1)", 2D) = "white" {}
		_BumpMap("BumpMap(uv1)",2D)="bump"{}
		
		_LigShadow("LigShadow(0-2)",Range(0,2))=0
		_LigMap("LigMap(uv2)",2D)="black"{}
		
		_RefColor("RefColor(Reflection)",Color)=(.5,.5,.5,1)
		_RefCubeMap("RefCubeMap",CUBE)="black"{}
		
		_RefAreaPower("RefAreaPower(0-1)",Range(0,1))=0
		_RefArea("RefArea(uv1)",2D)="black"{}
		
		_SpecColor ("SpecColor(Specular)", Color) = (0.5, 0.5, 0.5, 0.5)
		_SpecSize("SpecSize(0-1)",Range(0,1))=1
		_SpecArea("SpecArea(uv1)",2D)="white"{}
		
		_Cutoff ("Cutoff(0-1)", Range(0,1)) = 0
	}
	
	SubShader 
	{
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		LOD 400
		Cull Off

		CGPROGRAM
		#pragma surface surf BlinnPhong alphatest:_Cutoff
		#pragma target 3.0
		
		struct Input
		{
			fixed2 uv_MainTex;
			fixed2 uv2_LigMap;
			fixed3 worldRefl;
			fixed3 viewDir;
			fixed3 normal;
			INTERNAL_DATA 
		};
		
		fixed4 _Color,_RefColor;
		sampler2D _MainTex,_BumpMap,_LigMap,_RefArea,_SpecArea;
		samplerCUBE _RefCubeMap;
		float _LigShadow;
		fixed _RefAreaPower,_SpecSize;
		
		void surf(Input IN ,inout SurfaceOutput o)
		{
			/////////////////Diffuse
			fixed4 dif=tex2D(_MainTex,IN.uv_MainTex)*_Color;
			
			////////////////LightMap
			fixed4 lig=tex2D(_LigMap,IN.uv2_LigMap);
			fixed4 ligResult=lig*_LigShadow+fixed4(1,1,1,1)*(1-_LigShadow);
			ligResult=fixed4(saturate(ligResult.r),saturate(ligResult.g),saturate(ligResult.b),0);
			
			/////////////////Normal
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_MainTex));
			
			///////////////////////Reflection
			fixed4 ref=texCUBE(_RefCubeMap,WorldReflectionVector(IN,o.Normal))*_RefColor;
			
			///////////////////////Reflection Cull
			fixed4 refArea=tex2D(_RefArea,IN.uv_MainTex);
			fixed refAreaAverage=(refArea.r+refArea.g+refArea.b)/3;
			fixed refFlat=refAreaAverage<=_RefAreaPower?(refAreaAverage<=_RefAreaPower*0.7?0:(refAreaAverage-_RefAreaPower*0.7)/(_RefAreaPower*0.3)):1;
			refArea*=refFlat;
			
			///////////////////////Specular
			fixed4 spec=tex2D(_SpecArea,IN.uv_MainTex);
			
			///////////////////////
			o.Albedo=dif.rgb*ligResult.rgb;
			o.Emission=dif.rgb*lig.rgb+ref.rgb*refArea.rgb*ligResult.rgb;
			o.Gloss=(spec.r+spec.g+spec.b)/3*((ligResult.r+ligResult.g+ligResult.b)/3);
			o.Specular=_SpecSize;
			o.Alpha=dif.a;
		}
		ENDCG
	} 
	FallBack "Transparent/Cutout/VertexLit"
}
