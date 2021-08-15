using Distance.WheelieBoostFix.Scripts.RuntimeData;
using HarmonyLib;
using UnityEngine;
using System.Linq;

namespace Distance.WheelieBoostFix.Harmony
{
	[HarmonyPatch(typeof(GameManager), "SceneLoaded")]
	internal static class GameManager__SceneLoaded
	{
		[HarmonyPostfix]
		internal static void Postfix()
		{
			foreach (CarLogic car in from player in G.Sys.PlayerManager_.LocalPlayers_ where player.playerData_ is PlayerDataLocal select player.playerData_.carLogic_)
			{
				if (car.playerData_ is PlayerDataLocal)
				{
					CarLogicData data = car.GetOrAddComponent<CarLogicData>();
					data.FramesSinceJump = Mod.Instance.Configuration.JumpBoostMultiplierFrames;
				}
			}
		}
	}
}
