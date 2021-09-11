using HarmonyLib;
using UnityEngine;

namespace Distance.WheelieBoostFix.Harmony
{
	[HarmonyPatch(typeof(BoostGadget), "SetFlameIntensity")]
	internal static class BoostGadget__SetFlameIntensity
	{
		[HarmonyPrefix]
		internal static void Prefix(ref float intensity)
		{
			float buffMult = Mod.Instance.Configuration.DefaultBoostMultiplier;
			float nerfMult = Mod.Instance.Configuration.JumpBoostMultiplier;

			if (buffMult.CompareTo(nerfMult) != 0)
			{
				intensity = (intensity + buffMult - (2 * nerfMult)) / (buffMult - nerfMult);
			}
		}
	}
}
