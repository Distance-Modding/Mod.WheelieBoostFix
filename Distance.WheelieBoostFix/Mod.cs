using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using UnityEngine;

namespace Distance.WheelieBoostFix
{
	[ModEntryPoint("com.seekr.wbfix")]
	public sealed class Mod : MonoBehaviour
	{
		public static Mod Instance { get; private set; }

		public IManager Manager { get; private set; }

		public Log Logger { get; private set; }

		public ConfigurationLogic Configuration { get; private set; }

		public void Initialize(IManager manager)
		{
			DontDestroyOnLoad(this);

			Instance = this;

			Manager = manager;

			Logger = LogManager.GetForCurrentAssembly();
			Configuration = gameObject.AddComponent<ConfigurationLogic>();

			CreateSettingsMenu();

			RuntimePatcher.AutoPatch();
		}

		private void CreateSettingsMenu()
		{
			MenuTree settingsMenu = new MenuTree("menu.mod.wbfix", "Wheelie Boost Fix Settings")
			{
				new CheckBox(MenuDisplayMode.Both, "setting:debug", "DEBUG MODE")
				.WithGetter(() => Configuration.Debug)
				.WithSetter((x) => Configuration.Debug = x)
				.WithDescription("Output debugging information to the console and log file."),
				new FloatSlider(MenuDisplayMode.Both, "settings:default_boost_multiplier", "DEFAULT BOOST MULTIPLIER")
				.LimitedByRange(0, 10)
				.WithDefaultValue(1.05f)
				.WithGetter(() => Configuration.DefaultBoostMultiplier)
				.WithSetter((x) => Configuration.DefaultBoostMultiplier = x)
				.WithDescription("Default multiplier applied to the boost speed (lower values means a lower speed)."),
				new FloatSlider(MenuDisplayMode.Both, "settings:jump_boost_multiplier", "JUMP BOOST MULTIPLIER")
				.LimitedByRange(0, 10)
				.WithDefaultValue(0.79f)
				.WithGetter(() => Configuration.JumpBoostMultiplier)
				.WithSetter((x) => Configuration.JumpBoostMultiplier = x)
				.WithDescription("Boost multiplier applied after jumping (lower values means a lower speed)."),
				new IntegerSlider(MenuDisplayMode.Both, "setting:jump_boost_mltiplier_frames", "JUMP BOOST MULTIPLIER FRAMES")
				.LimitedByRange(0, 500)
				.WithDefaultValue(60)
				.WithGetter(() => Configuration.JumpBoostMultiplierFrames)
				.WithSetter((x) => Configuration.JumpBoostMultiplierFrames = x)
				.WithDescription("How long (in frames) should the jump boost multiplier be applied."),
				new IntegerSlider(MenuDisplayMode.Both, "setting:wheel_threshold", "WHEEL THRESHOLD")
				.LimitedByRange(0, 4)
				.WithDefaultValue(1)
				.WithGetter(() => Configuration.WheelThreshold)
				.WithSetter((x) => Configuration.WheelThreshold = x)
				.WithDescription("The numbers of wheels contacting a surface needed for the car to be considered 'grounded'.")
			};

			Menus.AddNew(MenuDisplayMode.Both, settingsMenu, "WHEELIE BOOST FIX", "Change settings of the Wheelie Boost Fix.");
		}

		public bool GameplayCheatsAllowed()
		{
			NetworkingManager networking = G.Sys.NetworkingManager_;
			return networking?.IsOnline_ == false;
		}
	}
}