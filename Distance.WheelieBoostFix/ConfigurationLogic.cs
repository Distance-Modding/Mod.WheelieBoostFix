using Reactor.API.Configuration;
using System;
using UnityEngine;

namespace Distance.WheelieBoostFix
{
	public class ConfigurationLogic : MonoBehaviour
	{
		#region Properties
		public bool Debug
		{
			get => Get<bool>("debug");
			set => Set("debug", value);
		}

		public float DefaultBoostMultiplier
		{
			get => Get<float>("default_boost_multiplier");
			set => Set("default_boost_multiplier", value);
		}

		public float JumpBoostMultiplier
		{
			get => Get<float>("jump_boost_multiplier");
			set => Set("jump_boost_multiplier", value);
		}

		public int JumpBoostMultiplierFrames
		{
			get => Get<int>("jump_boost_multiplier_frames");
			set => Set("jump_boost_multiplier_frames", value);
		}

		public int WheelThreshold
		{
			get => Get<int>("wheel_threshold");
			set => Set("wheel_threshold", value);
		}
		#endregion

		internal Settings Config;

		public event Action<ConfigurationLogic> OnChanged;

		private void Load()
		{
			Config = new Settings("Settings");
		}

		public void Awake()
		{
			Load();

			Get("debug", false);
			Get("default_boost_multiplier", 1.05f);
			Get("jump_boost_multiplier", 0.79f);
			Get("jump_boost_multiplier_frames", 60);
			Get("wheel_threshold", 1);

			Save();
		}

		public T Get<T>(string key, T @default = default)
		{
			return Config.GetOrCreate(key, @default);
		}

		public void Set<T>(string key, T value)
		{
			Config[key] = value;
			Save();
		}

		public void Save()
		{
			Config?.Save();
			OnChanged?.Invoke(this);
		}
	}
}
