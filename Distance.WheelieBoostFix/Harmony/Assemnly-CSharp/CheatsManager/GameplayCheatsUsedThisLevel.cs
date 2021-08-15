using HarmonyLib;

namespace Distance.WheelieBoostFix.Harmony
{
	[HarmonyPatch(typeof(CheatsManager), "GameplayCheatsUsedThisLevel_", MethodType.Getter)]
	internal static class CheatsManager__GameplayCheatsUsedThisLevel
	{
		[HarmonyPostfix]
		internal static void Postfix(out bool __result)
		{
			__result = true;
		}
	}
}
