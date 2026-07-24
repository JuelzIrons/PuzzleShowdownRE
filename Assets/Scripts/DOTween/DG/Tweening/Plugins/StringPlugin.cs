namespace DG.Tweening.Plugins
{
	public class StringPlugin : global::DG.Tweening.Plugins.Core.ABSTweenPlugin<string, string, global::DG.Tweening.Plugins.Options.StringOptions>
	{
		private static readonly global::System.Text.StringBuilder _Buffer = new global::System.Text.StringBuilder();

		private static readonly global::System.Collections.Generic.List<char> _OpenedTags = new global::System.Collections.Generic.List<char>();

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t, bool isRelative)
		{
			string endValue = t.endValue;
			t.endValue = t.getter();
			t.startValue = endValue;
			t.setter(t.startValue);
		}

		public override void SetFrom(global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t, string fromValue, bool setImmediately, bool isRelative)
		{
			if (fromValue == null)
			{
				fromValue = "";
			}
			if (isRelative)
			{
				string text = t.getter();
				fromValue += text;
			}
			t.startValue = fromValue;
			if (setImmediately)
			{
				t.setter(fromValue);
			}
		}

		public override void Reset(global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t)
		{
			t.startValue = (t.endValue = (t.changeValue = ""));
		}

		public override string ConvertToStartValue(global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t, string value)
		{
			return value;
		}

		public override void SetRelativeEndValue(global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t)
		{
		}

		public override void SetChangeValue(global::DG.Tweening.Core.TweenerCore<string, string, global::DG.Tweening.Plugins.Options.StringOptions> t)
		{
			t.changeValue = t.endValue;
			bool flag = string.IsNullOrEmpty(t.startValue);
			bool flag2 = string.IsNullOrEmpty(t.changeValue);
			t.plugOptions.startValueStrippedLength = ((!flag) ? global::System.Text.RegularExpressions.Regex.Replace(t.startValue, "<[^>]*>", "").Length : 0);
			t.plugOptions.changeValueStrippedLength = ((!flag2) ? global::System.Text.RegularExpressions.Regex.Replace(t.changeValue, "<[^>]*>", "").Length : 0);
			int num = ((!flag) ? t.startValue.Length : 0);
			int num2 = ((!flag2) ? t.changeValue.Length : 0);
			if (num > 3 && t.startValue[num - 1] == '>')
			{
				for (int num3 = num - 3; num3 > -1; num3--)
				{
					if (t.startValue[num3] == '<')
					{
						if (t.startValue[num3 + 1] != '/')
						{
							t.plugOptions.startValueStrippedLength++;
						}
						break;
					}
				}
			}
			if (num2 <= 3 || t.changeValue[num2 - 1] != '>')
			{
				return;
			}
			for (int num4 = num2 - 3; num4 > -1; num4--)
			{
				if (t.changeValue[num4] == '<')
				{
					if (t.changeValue[num4 + 1] != '/')
					{
						t.plugOptions.changeValueStrippedLength++;
					}
					break;
				}
			}
		}

		public override float GetSpeedBasedDuration(global::DG.Tweening.Plugins.Options.StringOptions options, float unitsXSecond, string changeValue)
		{
			float num = (float)(options.richTextEnabled ? options.changeValueStrippedLength : changeValue.Length) / unitsXSecond;
			if (num < 0f)
			{
				num = 0f - num;
			}
			return num;
		}

		public override void EvaluateAndApply(global::DG.Tweening.Plugins.Options.StringOptions options, global::DG.Tweening.Tween t, bool isRelative, global::DG.Tweening.Core.DOGetter<string> getter, global::DG.Tweening.Core.DOSetter<string> setter, float elapsed, string startValue, string changeValue, float duration, bool usingInversePosition, int newCompletedSteps, global::DG.Tweening.Core.Enums.UpdateNotice updateNotice)
		{
			_Buffer.Remove(0, _Buffer.Length);
			if (isRelative && t.loopType == global::DG.Tweening.LoopType.Incremental)
			{
				int num = (t.isComplete ? (t.completedLoops - 1) : t.completedLoops);
				if (num > 0)
				{
					_Buffer.Append(startValue);
					for (int i = 0; i < num; i++)
					{
						_Buffer.Append(changeValue);
					}
					startValue = _Buffer.ToString();
					_Buffer.Remove(0, _Buffer.Length);
				}
			}
			int num2 = (options.richTextEnabled ? options.startValueStrippedLength : ((!string.IsNullOrEmpty(startValue)) ? startValue.Length : 0));
			int num3 = (options.richTextEnabled ? options.changeValueStrippedLength : changeValue.Length);
			int num4 = (int)global::System.Math.Round((float)num3 * global::DG.Tweening.Core.Easing.EaseManager.Evaluate(t.easeType, t.customEase, elapsed, duration, t.easeOvershootOrAmplitude, t.easePeriod));
			if (num4 > num3)
			{
				num4 = num3;
			}
			else if (num4 < 0)
			{
				num4 = 0;
			}
			if (isRelative)
			{
				_Buffer.Append(startValue);
				if (options.scrambleMode != global::DG.Tweening.ScrambleMode.None)
				{
					setter(Append(changeValue, 0, num4, options.richTextEnabled).AppendScrambledChars(num3 - num4, ScrambledCharsToUse(options)).ToString());
				}
				else
				{
					setter(Append(changeValue, 0, num4, options.richTextEnabled).ToString());
				}
				return;
			}
			if (options.scrambleMode != global::DG.Tweening.ScrambleMode.None)
			{
				setter(Append(changeValue, 0, num4, options.richTextEnabled).AppendScrambledChars(num3 - num4, ScrambledCharsToUse(options)).ToString());
				return;
			}
			int num5 = num2 - num3;
			int num6 = num2;
			if (num5 > 0)
			{
				float num7 = (float)num4 / (float)num3;
				num6 -= (int)((float)num6 * num7);
			}
			else
			{
				num6 -= num4;
			}
			Append(changeValue, 0, num4, options.richTextEnabled);
			if (num4 < num3 && num4 < num2)
			{
				Append(startValue, num4, options.richTextEnabled ? (num4 + num6) : num6, options.richTextEnabled);
			}
			setter(_Buffer.ToString());
		}

		private global::System.Text.StringBuilder Append(string value, int startIndex, int length, bool richTextEnabled)
		{
			if (!richTextEnabled)
			{
				_Buffer.Append(value, startIndex, length);
				return _Buffer;
			}
			_OpenedTags.Clear();
			bool flag = false;
			int length2 = value.Length;
			int i;
			for (i = 0; i < length && i <= length2 - 1; i++)
			{
				char c = value[i];
				if (c == '<')
				{
					bool flag2 = flag;
					char c2 = value[i + 1];
					flag = i >= length2 - 1 || c2 != '/';
					if (flag)
					{
						_OpenedTags.Add((c2 == '#') ? 'c' : c2);
					}
					else
					{
						_OpenedTags.RemoveAt(_OpenedTags.Count - 1);
					}
					global::System.Text.RegularExpressions.Match match = global::System.Text.RegularExpressions.Regex.Match(value.Substring(i), "<.*?(>)");
					if (!match.Success)
					{
						continue;
					}
					if (!flag && !flag2)
					{
						char c3 = value[i + 1];
						char[] array = ((c3 != 'c') ? new char[1] { c3 } : new char[2] { '#', 'c' });
						for (int num = i - 1; num > -1; num--)
						{
							if (value[num] == '<' && value[num + 1] != '/' && global::System.Array.IndexOf(array, value[num + 2]) != -1)
							{
								_Buffer.Insert(0, value.Substring(num, value.IndexOf('>', num) + 1 - num));
								break;
							}
						}
					}
					_Buffer.Append(match.Value);
					int num2 = match.Groups[1].Index + 1;
					length += num2;
					startIndex += num2;
					i += num2 - 1;
				}
				else if (i >= startIndex)
				{
					_Buffer.Append(c);
				}
			}
			if (_OpenedTags.Count > 0 && i < length2 - 1)
			{
				while (_OpenedTags.Count > 0 && i < length2 - 1)
				{
					global::System.Text.RegularExpressions.Match match2 = global::System.Text.RegularExpressions.Regex.Match(value.Substring(i), "(</).*?>");
					if (!match2.Success)
					{
						break;
					}
					if (match2.Value[2] == _OpenedTags[_OpenedTags.Count - 1])
					{
						_Buffer.Append(match2.Value);
						_OpenedTags.RemoveAt(_OpenedTags.Count - 1);
					}
					i += match2.Value.Length;
				}
			}
			return _Buffer;
		}

		private char[] ScrambledCharsToUse(global::DG.Tweening.Plugins.Options.StringOptions options)
		{
			return options.scrambleMode switch
			{
				global::DG.Tweening.ScrambleMode.Uppercase => global::DG.Tweening.Plugins.StringPluginExtensions.ScrambledCharsUppercase, 
				global::DG.Tweening.ScrambleMode.Lowercase => global::DG.Tweening.Plugins.StringPluginExtensions.ScrambledCharsLowercase, 
				global::DG.Tweening.ScrambleMode.Numerals => global::DG.Tweening.Plugins.StringPluginExtensions.ScrambledCharsNumerals, 
				global::DG.Tweening.ScrambleMode.Custom => options.scrambledChars, 
				_ => global::DG.Tweening.Plugins.StringPluginExtensions.ScrambledCharsAll, 
			};
		}
	}
}
