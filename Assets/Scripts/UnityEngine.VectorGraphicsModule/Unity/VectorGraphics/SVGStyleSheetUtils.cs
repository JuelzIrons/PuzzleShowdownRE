namespace Unity.VectorGraphics
{
	internal static class SVGStyleSheetUtils
	{
		public static global::Unity.VectorGraphics.SVGStyleSheet Parse(string cssText)
		{
			global::Unity.VectorGraphics.SVGStyleSheet sVGStyleSheet = new global::Unity.VectorGraphics.SVGStyleSheet();
			global::System.Collections.Generic.List<string> tokens = Tokenize(cssText);
			global::Unity.VectorGraphics.SVGStyleSheet sVGStyleSheet2 = new global::Unity.VectorGraphics.SVGStyleSheet();
			while (ParseSelector(tokens, sVGStyleSheet2))
			{
				global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>(sVGStyleSheet.selectors);
				foreach (string selector in sVGStyleSheet2.selectors)
				{
					if (list.Contains(selector))
					{
						CombineProperties(sVGStyleSheet[selector], sVGStyleSheet2[selector]);
					}
					else
					{
						sVGStyleSheet[selector] = sVGStyleSheet2[selector];
					}
				}
				sVGStyleSheet2.Clear();
			}
			return sVGStyleSheet;
		}

		public static global::Unity.VectorGraphics.SVGPropertySheet ParseInline(string cssText)
		{
			global::System.Collections.Generic.List<string> tokens = Tokenize(cssText);
			global::Unity.VectorGraphics.SVGPropertySheet sVGPropertySheet = new global::Unity.VectorGraphics.SVGPropertySheet();
			ParseProperties(tokens, sVGPropertySheet);
			return sVGPropertySheet;
		}

		private static bool ParseSelector(global::System.Collections.Generic.List<string> tokens, global::Unity.VectorGraphics.SVGStyleSheet sheet)
		{
			if (tokens.Count == 0)
			{
				return false;
			}
			global::Unity.VectorGraphics.SVGStyleSheet sVGStyleSheet = new global::Unity.VectorGraphics.SVGStyleSheet();
			do
			{
				string text = PopToken(tokens);
				while (PeekToken(tokens) != "" && PeekToken(tokens) != "," && PeekToken(tokens) != "{")
				{
					text = text + " " + PopToken(tokens);
				}
				sVGStyleSheet[text] = new global::Unity.VectorGraphics.SVGPropertySheet();
				while (PeekToken(tokens) == ",")
				{
					PopToken(tokens);
				}
			}
			while (!(PeekToken(tokens) == "") && !(PeekToken(tokens) == "{"));
			string text2 = PopToken(tokens);
			if (text2 != "{")
			{
				global::UnityEngine.Debug.LogError("Invalid CSS selector opening bracket: \"" + text2 + "\"");
				return false;
			}
			global::Unity.VectorGraphics.SVGPropertySheet props = new global::Unity.VectorGraphics.SVGPropertySheet();
			ParseProperties(tokens, props);
			foreach (string selector in sVGStyleSheet.selectors)
			{
				sheet[selector] = CopyProperties(props);
			}
			text2 = PopToken(tokens);
			if (text2 != "}")
			{
				global::UnityEngine.Debug.LogError("Invalid CSS selector closing bracket: \"" + text2 + "\"");
				return false;
			}
			return true;
		}

		private static void CombineProperties(global::Unity.VectorGraphics.SVGPropertySheet first, global::Unity.VectorGraphics.SVGPropertySheet second)
		{
			foreach (string key in second.Keys)
			{
				first[key] = second[key];
			}
		}

		private static global::Unity.VectorGraphics.SVGPropertySheet CopyProperties(global::Unity.VectorGraphics.SVGPropertySheet props)
		{
			global::Unity.VectorGraphics.SVGPropertySheet sVGPropertySheet = new global::Unity.VectorGraphics.SVGPropertySheet();
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> prop in props)
			{
				sVGPropertySheet[prop.Key] = prop.Value;
			}
			return sVGPropertySheet;
		}

		private static bool ParseProperties(global::System.Collections.Generic.List<string> tokens, global::Unity.VectorGraphics.SVGPropertySheet props)
		{
			string name;
			string value;
			while (ParseProperty(tokens, out name, out value))
			{
				props[name] = value;
				while (PeekToken(tokens) == ";")
				{
					PopToken(tokens);
				}
			}
			return true;
		}

		private static bool ParseProperty(global::System.Collections.Generic.List<string> tokens, out string name, out string value)
		{
			name = null;
			value = null;
			if (PeekToken(tokens) == "" || PeekToken(tokens) == "}")
			{
				return false;
			}
			name = PopToken(tokens);
			string text = PopToken(tokens);
			if (text != ":")
			{
				global::UnityEngine.Debug.LogError("Invalid CSS property separator: \"" + text + "\"");
				return false;
			}
			value = "";
			while (PeekToken(tokens) != "" && PeekToken(tokens) != ";" && PeekToken(tokens) != "}")
			{
				value = ((value == "") ? PopToken(tokens) : (value + " " + PopToken(tokens)));
				if (PeekToken(tokens) == "(")
				{
					value += ParseParenValue(tokens);
				}
			}
			return true;
		}

		private static string ParseParenValue(global::System.Collections.Generic.List<string> tokens)
		{
			string text = PopToken(tokens);
			if (text != "(")
			{
				global::UnityEngine.Debug.LogError("Invaid CSS value opening");
				return "";
			}
			string text2 = text;
			while (PeekToken(tokens) != "" && PeekToken(tokens) != ")")
			{
				text2 += PopToken(tokens);
			}
			if (PeekToken(tokens) != ")")
			{
				global::UnityEngine.Debug.LogError("Invaid CSS value closing");
				return "";
			}
			return text2 + PopToken(tokens);
		}

		public static global::System.Collections.Generic.List<string> Tokenize(string cssText)
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			cssText = cssText.Replace(global::System.Environment.NewLine, "");
			cssText = global::System.Text.RegularExpressions.Regex.Replace(cssText, "/\\*.*?\\*/", "");
			cssText = global::System.Text.RegularExpressions.Regex.Replace(cssText, "<!--.*?-->", "");
			int i = 0;
			int num = 0;
			while (i < cssText.Length)
			{
				for (; i < cssText.Length && IsWhitespace(cssText[i]); i++)
				{
				}
				for (num = i; num < cssText.Length && !IsSeparator(cssText[num]); num++)
				{
				}
				if (i == num)
				{
					if (i < cssText.Length)
					{
						list.Add(cssText[i].ToString());
					}
					num++;
				}
				else
				{
					list.Add(cssText.Substring(i, num - i));
				}
				i = num;
			}
			return list;
		}

		private static string PeekToken(global::System.Collections.Generic.List<string> tokens)
		{
			if (tokens.Count == 0)
			{
				return "";
			}
			return tokens[0];
		}

		private static string PopToken(global::System.Collections.Generic.List<string> tokens)
		{
			if (tokens.Count == 0)
			{
				return "";
			}
			string result = tokens[0];
			tokens.RemoveAt(0);
			return result;
		}

		private static bool IsSeparator(char ch)
		{
			return IsWhitespace(ch) || ch == ';' || ch == ':' || ch == '{' || ch == '}' || ch == '(' || ch == ')' || ch == ',';
		}

		private static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\n' || ch == '\t';
		}
	}
}
