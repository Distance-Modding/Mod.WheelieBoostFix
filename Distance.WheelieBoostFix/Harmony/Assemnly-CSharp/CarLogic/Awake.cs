using Distance.WheelieBoostFix.Scripts.RuntimeData;
using HarmonyLib;

namespace Distance.WheelieBoostFix.Harmony
{
	[HarmonyPatch(typeof(CarLogic), "Awake")]
	internal static class CarLogic__Awake
	{
		[HarmonyPostfix]
		internal static void Postfix(CarLogic __instance)
		{
			if (!__instance.HasComponent<CarLogicData>())
			{
				__instance.gameObject.AddComponent<CarLogicData>();
			}
		}
	}
}
