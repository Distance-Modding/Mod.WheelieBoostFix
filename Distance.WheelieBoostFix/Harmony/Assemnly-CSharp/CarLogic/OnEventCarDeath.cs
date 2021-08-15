using Distance.WheelieBoostFix.Scripts.RuntimeData;
using HarmonyLib;

namespace Distance.WheelieBoostFix.Harmony
{
	[HarmonyPatch(typeof(CarLogic), "OnEventCarDeath")]
	internal static class CarLogic__OnEventCarDeath
	{
		[HarmonyPostfix]
		internal static void Postfix(CarLogic __instance)
		{
			CarLogicData data = __instance.GetOrAddComponent<CarLogicData>();

			data.FramesSinceJump = Mod.Instance.Configuration.JumpBoostMultiplierFrames;
		}
	}
}
