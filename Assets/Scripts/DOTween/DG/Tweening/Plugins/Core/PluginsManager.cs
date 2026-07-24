namespace DG.Tweening.Plugins.Core
{
	internal static class PluginsManager
	{
		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _floatPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _doublePlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _intPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _uintPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _longPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _ulongPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _vector2Plugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _vector3Plugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _vector4Plugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _quaternionPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _colorPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _rectPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _rectOffsetPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _stringPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _vector3ArrayPlugin;

		private static global::DG.Tweening.Plugins.Core.ITweenPlugin _color2Plugin;

		private const int _MaxCustomPlugins = 20;

		private static global::System.Collections.Generic.Dictionary<global::System.Type, global::DG.Tweening.Plugins.Core.ITweenPlugin> _customPlugins;

		internal static global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions> GetDefaultPlugin<T1, T2, TPlugOptions>() where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			global::System.Type typeFromHandle = typeof(T1);
			global::System.Type typeFromHandle2 = typeof(T2);
			global::DG.Tweening.Plugins.Core.ITweenPlugin tweenPlugin = null;
			if ((object)typeFromHandle == typeof(global::UnityEngine.Vector3) && (object)typeFromHandle == typeFromHandle2)
			{
				if (_vector3Plugin == null)
				{
					_vector3Plugin = new global::DG.Tweening.Plugins.Vector3Plugin();
				}
				tweenPlugin = _vector3Plugin;
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.Vector3) && (object)typeFromHandle2 == typeof(global::UnityEngine.Vector3[]))
			{
				if (_vector3ArrayPlugin == null)
				{
					_vector3ArrayPlugin = new global::DG.Tweening.Plugins.Vector3ArrayPlugin();
				}
				tweenPlugin = _vector3ArrayPlugin;
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.Quaternion))
			{
				if ((object)typeFromHandle2 == typeof(global::UnityEngine.Quaternion))
				{
					global::DG.Tweening.Core.Debugger.LogError("Quaternion tweens require a Vector3 endValue");
				}
				else
				{
					if (_quaternionPlugin == null)
					{
						_quaternionPlugin = new global::DG.Tweening.Plugins.QuaternionPlugin();
					}
					tweenPlugin = _quaternionPlugin;
				}
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.Vector2))
			{
				if (_vector2Plugin == null)
				{
					_vector2Plugin = new global::DG.Tweening.Plugins.Vector2Plugin();
				}
				tweenPlugin = _vector2Plugin;
			}
			else if ((object)typeFromHandle == typeof(float))
			{
				if (_floatPlugin == null)
				{
					_floatPlugin = new global::DG.Tweening.Plugins.FloatPlugin();
				}
				tweenPlugin = _floatPlugin;
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.Color))
			{
				if (_colorPlugin == null)
				{
					_colorPlugin = new global::DG.Tweening.Plugins.ColorPlugin();
				}
				tweenPlugin = _colorPlugin;
			}
			else if ((object)typeFromHandle == typeof(int))
			{
				if (_intPlugin == null)
				{
					_intPlugin = new global::DG.Tweening.Plugins.IntPlugin();
				}
				tweenPlugin = _intPlugin;
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.Vector4))
			{
				if (_vector4Plugin == null)
				{
					_vector4Plugin = new global::DG.Tweening.Plugins.Vector4Plugin();
				}
				tweenPlugin = _vector4Plugin;
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.Rect))
			{
				if (_rectPlugin == null)
				{
					_rectPlugin = new global::DG.Tweening.Plugins.RectPlugin();
				}
				tweenPlugin = _rectPlugin;
			}
			else if ((object)typeFromHandle == typeof(global::UnityEngine.RectOffset))
			{
				if (_rectOffsetPlugin == null)
				{
					_rectOffsetPlugin = new global::DG.Tweening.Plugins.RectOffsetPlugin();
				}
				tweenPlugin = _rectOffsetPlugin;
			}
			else if ((object)typeFromHandle == typeof(uint))
			{
				if (_uintPlugin == null)
				{
					_uintPlugin = new global::DG.Tweening.Plugins.UintPlugin();
				}
				tweenPlugin = _uintPlugin;
			}
			else if ((object)typeFromHandle == typeof(string))
			{
				if (_stringPlugin == null)
				{
					_stringPlugin = new global::DG.Tweening.Plugins.StringPlugin();
				}
				tweenPlugin = _stringPlugin;
			}
			else if ((object)typeFromHandle == typeof(global::DG.Tweening.Color2))
			{
				if (_color2Plugin == null)
				{
					_color2Plugin = new global::DG.Tweening.Plugins.Color2Plugin();
				}
				tweenPlugin = _color2Plugin;
			}
			else if ((object)typeFromHandle == typeof(long))
			{
				if (_longPlugin == null)
				{
					_longPlugin = new global::DG.Tweening.Plugins.LongPlugin();
				}
				tweenPlugin = _longPlugin;
			}
			else if ((object)typeFromHandle == typeof(ulong))
			{
				if (_ulongPlugin == null)
				{
					_ulongPlugin = new global::DG.Tweening.Plugins.UlongPlugin();
				}
				tweenPlugin = _ulongPlugin;
			}
			else if ((object)typeFromHandle == typeof(double))
			{
				if (_doublePlugin == null)
				{
					_doublePlugin = new global::DG.Tweening.Plugins.DoublePlugin();
				}
				tweenPlugin = _doublePlugin;
			}
			if (tweenPlugin != null)
			{
				return tweenPlugin as global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			return null;
		}

		public static global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions> GetCustomPlugin<TPlugin, T1, T2, TPlugOptions>() where TPlugin : global::DG.Tweening.Plugins.Core.ITweenPlugin, new() where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			global::System.Type typeFromHandle = typeof(TPlugin);
			global::DG.Tweening.Plugins.Core.ITweenPlugin value;
			if (_customPlugins == null)
			{
				_customPlugins = new global::System.Collections.Generic.Dictionary<global::System.Type, global::DG.Tweening.Plugins.Core.ITweenPlugin>(20);
			}
			else if (_customPlugins.TryGetValue(typeFromHandle, out value))
			{
				return value as global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			value = new TPlugin();
			_customPlugins.Add(typeFromHandle, value);
			return value as global::DG.Tweening.Plugins.Core.ABSTweenPlugin<T1, T2, TPlugOptions>;
		}

		internal static void PurgeAll()
		{
			_floatPlugin = null;
			_intPlugin = null;
			_uintPlugin = null;
			_longPlugin = null;
			_ulongPlugin = null;
			_vector2Plugin = null;
			_vector3Plugin = null;
			_vector4Plugin = null;
			_quaternionPlugin = null;
			_colorPlugin = null;
			_rectPlugin = null;
			_rectOffsetPlugin = null;
			_stringPlugin = null;
			_vector3ArrayPlugin = null;
			_color2Plugin = null;
			if (_customPlugins != null)
			{
				_customPlugins.Clear();
			}
		}
	}
}
