using UnityEngine;

namespace Distance.WheelieBoostFix.Scripts.RuntimeData
{
	public class CarLogicData : MonoBehaviour
	{
		public float PreviousJumpTimer { get; set; } = 0;

		public int FramesSinceJump { get; set; } = 0;
	}
}
