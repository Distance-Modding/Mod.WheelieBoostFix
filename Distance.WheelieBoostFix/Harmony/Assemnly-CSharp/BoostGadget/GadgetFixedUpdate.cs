using Centrifuge.Distance.Game;
using Distance.WheelieBoostFix.Scripts.RuntimeData;
using HarmonyLib;

namespace Distance.WheelieBoostFix.Harmony
{
	[HarmonyPatch(typeof(BoostGadget), "GadgetFixedUpdate")]
	internal static class BoostGadget__GadgetFixedUpdate
	{
		[HarmonyPrefix]
		internal static bool Prefix(BoostGadget __instance)
		{
			CarLogicData data = __instance.carLogic_.GetOrAddComponent<CarLogicData>();

			CarLogic carLogic = __instance.carLogic_;
			CarStats carStats = carLogic?.CarStats_;
			JumpGadget jumpGadget = carLogic?.Jump_;

			if (!carLogic.IsLocalCar_ || !jumpGadget || !Mod.Instance.GameplayCheatsAllowed())
			{
				return true;
			}

			bool debug = Mod.Instance.Configuration.Debug;
			float jumpTimer = jumpGadget.jumpTimer_;

			if (Timex.PhysicsFrameCount_ % 50 == 0 && debug)
			{
				Mod.Instance.Logger.Info($"Boost Mult: {__instance.accelerationMul_}x; FramesSinceJump: {data.FramesSinceJump}");
			}

			int wheelsContacting = carStats.wheelsContactingSmooth_;
			int wheelThreshold = Mod.Instance.Configuration.WheelThreshold;

			if (jumpTimer < data.PreviousJumpTimer) // Just jumped
			{
				if (debug)
				{
					Mod.Instance.Logger.Info("Jumped");
				}

				data.FramesSinceJump = 0;
			}
			else
			{
				data.FramesSinceJump++;
			}

			if (wheelsContacting >= wheelThreshold || data.FramesSinceJump >= Mod.Instance.Configuration.JumpBoostMultiplierFrames)
			{
				__instance.accelerationMul_ = Mod.Instance.Configuration.DefaultBoostMultiplier;
			}
			else
			{
				__instance.accelerationMul_ = Mod.Instance.Configuration.JumpBoostMultiplier;
			}

			data.PreviousJumpTimer = jumpTimer;

			return true;
		}
	}
}
