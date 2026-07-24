namespace Unity.VectorGraphics
{
	internal class SVGAttribParser
	{
		private enum AttribPath
		{
			Path = 0
		}

		private enum AttribTransform
		{
			Transform = 0
		}

		private enum AttribStroke
		{
			Stroke = 0
		}

		private global::System.Collections.Generic.LinkedList<global::Unity.VectorGraphics.BezierSegment> currentContour = new global::System.Collections.Generic.LinkedList<global::Unity.VectorGraphics.BezierSegment>();

		private global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierContour> contours = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierContour>();

		private global::UnityEngine.Vector2 penPos;

		private string attribString;

		private char pathCommand;

		private global::Unity.VectorGraphics.Matrix2D transform;

		private global::Unity.VectorGraphics.IFill fill;

		private string attribName;

		private int stringPos;

		private static global::Unity.VectorGraphics.NamedWebColorDictionary namedColors;

		public static global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierContour> ParsePath(global::Unity.VectorGraphics.XmlReaderIterator.Node node)
		{
			string text = node["d"];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			try
			{
				return new global::Unity.VectorGraphics.SVGAttribParser(text, global::Unity.VectorGraphics.SVGAttribParser.AttribPath.Path).contours;
			}
			catch (global::System.Exception ex)
			{
				throw node.GetException(ex.Message);
			}
		}

		public static global::Unity.VectorGraphics.Matrix2D ParseTransform(global::Unity.VectorGraphics.XmlReaderIterator.Node node)
		{
			return ParseTransform(node, "transform");
		}

		public static global::Unity.VectorGraphics.Matrix2D ParseTransform(global::Unity.VectorGraphics.XmlReaderIterator.Node node, string attribName)
		{
			string text = node[attribName];
			if (string.IsNullOrEmpty(text))
			{
				return global::Unity.VectorGraphics.Matrix2D.identity;
			}
			try
			{
				return new global::Unity.VectorGraphics.SVGAttribParser(text, attribName, global::Unity.VectorGraphics.SVGAttribParser.AttribTransform.Transform).transform;
			}
			catch (global::System.Exception ex)
			{
				throw node.GetException(ex.Message);
			}
		}

		public static global::Unity.VectorGraphics.IFill ParseFill(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SVGDictionary dict, global::Unity.VectorGraphics.SVGPostponedFills postponedFills, global::Unity.VectorGraphics.SVGStyleResolver styles, global::Unity.VectorGraphics.Inheritance inheritance = global::Unity.VectorGraphics.Inheritance.Inherited)
		{
			bool isDefaultFill;
			return ParseFill(node, dict, postponedFills, styles, inheritance, out isDefaultFill);
		}

		public static global::Unity.VectorGraphics.IFill ParseFill(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SVGDictionary dict, global::Unity.VectorGraphics.SVGPostponedFills postponedFills, global::Unity.VectorGraphics.SVGStyleResolver styles, global::Unity.VectorGraphics.Inheritance inheritance, out bool isDefaultFill)
		{
			string text = styles.Evaluate("fill-opacity", inheritance);
			float opacity = ((text != null) ? ParseFloat(text) : 1f);
			string text2 = styles.Evaluate("fill-rule", inheritance);
			global::Unity.VectorGraphics.FillMode mode = global::Unity.VectorGraphics.FillMode.NonZero;
			if (text2 != null)
			{
				if (text2 == "nonzero")
				{
					mode = global::Unity.VectorGraphics.FillMode.NonZero;
				}
				else
				{
					if (!(text2 == "evenodd"))
					{
						throw new global::System.Exception("Unknown fill-rule: " + text2);
					}
					mode = global::Unity.VectorGraphics.FillMode.OddEven;
				}
			}
			try
			{
				string text3 = styles.Evaluate("fill", inheritance);
				isDefaultFill = text3 == null && text == null;
				return new global::Unity.VectorGraphics.SVGAttribParser(text3, "fill", opacity, mode, dict, postponedFills).fill;
			}
			catch (global::System.Exception ex)
			{
				throw node.GetException(ex.Message);
			}
		}

		public static global::Unity.VectorGraphics.Stroke ParseStrokeAndOpacity(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SVGDictionary dict, global::Unity.VectorGraphics.SVGStyleResolver styles, global::Unity.VectorGraphics.Inheritance inheritance = global::Unity.VectorGraphics.Inheritance.Inherited)
		{
			string text = styles.Evaluate("stroke", inheritance);
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string text2 = styles.Evaluate("stroke-opacity", inheritance);
			float opacity = ((text2 != null) ? ParseFloat(text2) : 1f);
			global::Unity.VectorGraphics.IFill fill = null;
			try
			{
				fill = new global::Unity.VectorGraphics.SVGAttribParser(text, "stroke", opacity, global::Unity.VectorGraphics.FillMode.NonZero, dict, null).fill;
			}
			catch (global::System.Exception ex)
			{
				throw node.GetException(ex.Message);
			}
			if (fill == null)
			{
				return null;
			}
			return new global::Unity.VectorGraphics.Stroke
			{
				Fill = fill
			};
		}

		public static global::UnityEngine.Color ParseColor(string colorString)
		{
			if (colorString[0] == '#')
			{
				uint num = uint.Parse(colorString.Substring(1), global::System.Globalization.NumberStyles.HexNumber);
				if (colorString.Length == 4)
				{
					return new global::UnityEngine.Color((float)(((num >> 8) & 0xF) | (((num >> 8) & 0xF) << 4)) / 255f, (float)(((num >> 4) & 0xF) | (((num >> 4) & 0xF) << 4)) / 255f, (float)((num & 0xF) | ((num & 0xF) << 4)) / 255f);
				}
				return new global::UnityEngine.Color((float)((num >> 16) & 0xFF) / 255f, (float)((num >> 8) & 0xFF) / 255f, (float)(num & 0xFF) / 255f);
			}
			if (colorString.StartsWith("rgb(") && colorString.EndsWith(")"))
			{
				string text = colorString.Substring(4, colorString.Length - 5);
				string[] array = text.Split(new char[2] { ',', '%' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				if (array.Length != 3)
				{
					throw new global::System.Exception("Invalid rgb() color specification");
				}
				float num2 = (colorString.Contains("%") ? 100f : 255f);
				return new global::UnityEngine.Color((float)(int)byte.Parse(array[0]) / num2, (float)(int)byte.Parse(array[1]) / num2, (float)(int)byte.Parse(array[2]) / num2);
			}
			if (colorString.StartsWith("rgba(") && colorString.EndsWith(")"))
			{
				string text2 = colorString.Substring(5, colorString.Length - 6);
				string[] array2 = text2.Split(new char[2] { ',', '%' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length != 4)
				{
					throw new global::System.Exception("Invalid rgba() color specification");
				}
				float num3 = (colorString.Contains("%") ? 100f : 255f);
				return new global::UnityEngine.Color((float)(int)byte.Parse(array2[0]) / num3, (float)(int)byte.Parse(array2[1]) / num3, (float)(int)byte.Parse(array2[2]) / num3, (num3 == 100f) ? ((float)(int)byte.Parse(array2[3]) / num3) : ParseFloat(array2[3]));
			}
			if (colorString.StartsWith("hsl(") && colorString.EndsWith(")"))
			{
				string text3 = colorString.Substring(4, colorString.Length - 5);
				string[] array3 = text3.Split(new char[2] { ',', '%' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				if (array3.Length != 3)
				{
					throw new global::System.Exception("Invalid hsl() color specification");
				}
				float hue = ParseFloat(array3[0]) / 360f;
				float saturation = ParseFloat(array3[1]) / 100f;
				float lightness = ParseFloat(array3[2]) / 100f;
				return HSLToRGB(hue, saturation, lightness);
			}
			if (namedColors == null)
			{
				namedColors = new global::Unity.VectorGraphics.NamedWebColorDictionary();
			}
			return namedColors[colorString.ToLower()];
		}

		private static float HueToValue(float p, float q, float t)
		{
			if (t < 0f)
			{
				t += 1f;
			}
			if (t > 1f)
			{
				t -= 1f;
			}
			if (t < 1f / 6f)
			{
				return p + (q - p) * 6f * t;
			}
			if (t < 0.5f)
			{
				return q;
			}
			if (t < 2f / 3f)
			{
				return p + (q - p) * (2f / 3f - t) * 6f;
			}
			return p;
		}

		private static global::UnityEngine.Color HSLToRGB(float hue, float saturation, float lightness)
		{
			float num = ((lightness < 0.5f) ? (lightness * (1f + saturation)) : (lightness + saturation - lightness * saturation));
			float p = 2f * lightness - num;
			float r = HueToValue(p, num, hue + 1f / 3f);
			float g = HueToValue(p, num, hue);
			float b = HueToValue(p, num, hue - 1f / 3f);
			return new global::UnityEngine.Color(r, g, b);
		}

		public static string ParseURLRef(string url)
		{
			if (url.StartsWith("url(") && url.EndsWith(")"))
			{
				return url.Substring(4, url.Length - 5);
			}
			return null;
		}

		public static object ParseRelativeRef(string iri, global::Unity.VectorGraphics.SVGDictionary dict)
		{
			if (iri == null)
			{
				return null;
			}
			if (!iri.StartsWith("#"))
			{
				throw new global::System.Exception("Unsupported reference type (" + iri + ")");
			}
			iri = iri.Substring(1);
			dict.TryGetValue(iri, out var value);
			return value;
		}

		public static string CleanIri(string iri)
		{
			if (iri == null)
			{
				return null;
			}
			if (!iri.StartsWith("#"))
			{
				throw new global::System.Exception("Unsupported reference type (" + iri + ")");
			}
			iri = iri.Substring(1);
			return iri;
		}

		private SVGAttribParser(string attrib, global::Unity.VectorGraphics.SVGAttribParser.AttribPath attribPath)
		{
			attribName = "path";
			attribString = attrib;
			NextPathCommand(noCommandInheritance: true);
			if (pathCommand != 'm' && pathCommand != 'M')
			{
				throw new global::System.Exception("Path must start with a MoveTo pathCommand");
			}
			char c = '\0';
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.zero;
			while (NextPathCommand() != 0)
			{
				bool flag = pathCommand >= 'a' && pathCommand <= 'z';
				char c2 = char.ToLower(pathCommand);
				switch (c2)
				{
				case 'm':
					penPos = NextVector2(flag);
					pathCommand = (flag ? 'l' : 'L');
					ConcludePath(joinEnds: false);
					break;
				case 'z':
					if (currentContour.First != null)
					{
						penPos = currentContour.First.Value.P0;
					}
					ConcludePath(joinEnds: true);
					break;
				case 'l':
				{
					global::UnityEngine.Vector2 vector6 = NextVector2(flag);
					if ((vector6 - penPos).magnitude > global::Unity.VectorGraphics.VectorUtils.Epsilon)
					{
						currentContour.AddLast(global::Unity.VectorGraphics.VectorUtils.MakeLine(penPos, vector6));
					}
					penPos = vector6;
					break;
				}
				case 'h':
				{
					float x = (flag ? (penPos.x + NextFloat()) : NextFloat());
					global::UnityEngine.Vector2 vector4 = new global::UnityEngine.Vector2(x, penPos.y);
					if ((vector4 - penPos).magnitude > global::Unity.VectorGraphics.VectorUtils.Epsilon)
					{
						currentContour.AddLast(global::Unity.VectorGraphics.VectorUtils.MakeLine(penPos, vector4));
					}
					penPos = vector4;
					break;
				}
				case 'v':
				{
					float y = (flag ? (penPos.y + NextFloat()) : NextFloat());
					global::UnityEngine.Vector2 vector5 = new global::UnityEngine.Vector2(penPos.x, y);
					if ((vector5 - penPos).magnitude > global::Unity.VectorGraphics.VectorUtils.Epsilon)
					{
						currentContour.AddLast(global::Unity.VectorGraphics.VectorUtils.MakeLine(penPos, vector5));
					}
					penPos = vector5;
					break;
				}
				default:
					if (c2 != 'q')
					{
						if (c2 == 's' || c2 == 't')
						{
							global::UnityEngine.Vector2 p = penPos;
							if (currentContour.Count > 0 && (c == 'c' || c == 'q' || c == 's' || c == 't'))
							{
								p += currentContour.Last.Value.P3 - ((c == 'q' || c == 't') ? vector : currentContour.Last.Value.P2);
							}
							global::Unity.VectorGraphics.BezierSegment bezierSegment = new global::Unity.VectorGraphics.BezierSegment
							{
								P0 = penPos,
								P1 = p
							};
							if (c2 == 's')
							{
								bezierSegment.P2 = NextVector2(flag);
							}
							bezierSegment.P3 = NextVector2(flag);
							if (c2 == 't')
							{
								vector = bezierSegment.P1;
								float num = 2f / 3f;
								bezierSegment.P1 = bezierSegment.P0 + num * (vector - bezierSegment.P0);
								bezierSegment.P2 = bezierSegment.P3 + num * (vector - bezierSegment.P3);
							}
							penPos = bezierSegment.P3;
							if (!global::Unity.VectorGraphics.VectorUtils.IsEmptySegment(bezierSegment))
							{
								currentContour.AddLast(bezierSegment);
							}
						}
						else
						{
							if (c2 != 'a')
							{
								break;
							}
							global::UnityEngine.Vector2 vector2 = NextVector2();
							float num2 = NextFloat();
							bool largeArc = NextBool();
							bool sweep = NextBool();
							global::UnityEngine.Vector2 vector3 = NextVector2(flag);
							if (vector2.magnitude <= global::Unity.VectorGraphics.VectorUtils.Epsilon)
							{
								if ((vector3 - penPos).magnitude > global::Unity.VectorGraphics.VectorUtils.Epsilon)
								{
									currentContour.AddLast(global::Unity.VectorGraphics.VectorUtils.MakeLine(penPos, vector3));
								}
							}
							else
							{
								global::Unity.VectorGraphics.BezierPathSegment[] segments = global::Unity.VectorGraphics.VectorUtils.BuildEllipsePath(penPos, vector3, (0f - num2) * (global::System.MathF.PI / 180f), vector2.x, vector2.y, largeArc, sweep);
								foreach (global::Unity.VectorGraphics.BezierSegment item in global::Unity.VectorGraphics.VectorUtils.SegmentsInPath(segments))
								{
									currentContour.AddLast(item);
								}
							}
							penPos = vector3;
						}
						break;
					}
					goto case 'c';
				case 'c':
				{
					global::Unity.VectorGraphics.BezierSegment bezierSegment2 = new global::Unity.VectorGraphics.BezierSegment
					{
						P0 = penPos,
						P1 = NextVector2(flag)
					};
					if (c2 == 'c')
					{
						bezierSegment2.P2 = NextVector2(flag);
					}
					bezierSegment2.P3 = NextVector2(flag);
					if (c2 == 'q')
					{
						vector = bezierSegment2.P1;
						float num3 = 2f / 3f;
						bezierSegment2.P1 = bezierSegment2.P0 + num3 * (vector - bezierSegment2.P0);
						bezierSegment2.P2 = bezierSegment2.P3 + num3 * (vector - bezierSegment2.P3);
					}
					penPos = bezierSegment2.P3;
					if (!global::Unity.VectorGraphics.VectorUtils.IsEmptySegment(bezierSegment2))
					{
						currentContour.AddLast(bezierSegment2);
					}
					break;
				}
				}
				c = c2;
			}
			ConcludePath(joinEnds: false);
		}

		private SVGAttribParser(string attrib, string attribNameVal, global::Unity.VectorGraphics.SVGAttribParser.AttribTransform attribTransform)
		{
			attribString = attrib;
			attribName = attribNameVal;
			transform = global::Unity.VectorGraphics.Matrix2D.identity;
			while (stringPos < attribString.Length)
			{
				int num = stringPos;
				string text = NextStringCommand();
				if (string.IsNullOrEmpty(text))
				{
					break;
				}
				SkipSymbol('(');
				switch (text)
				{
				case "matrix":
					transform *= new global::Unity.VectorGraphics.Matrix2D
					{
						m00 = NextFloat(),
						m10 = NextFloat(),
						m01 = NextFloat(),
						m11 = NextFloat(),
						m02 = NextFloat(),
						m12 = NextFloat()
					};
					break;
				case "translate":
				{
					float x = NextFloat();
					float y2 = 0f;
					if (!PeekSymbol(')'))
					{
						y2 = NextFloat();
					}
					transform *= global::Unity.VectorGraphics.Matrix2D.Translate(new global::UnityEngine.Vector2(x, y2));
					break;
				}
				case "scale":
				{
					float num3 = NextFloat();
					float y = num3;
					if (!PeekSymbol(')'))
					{
						y = NextFloat();
					}
					transform *= global::Unity.VectorGraphics.Matrix2D.Scale(new global::UnityEngine.Vector2(num3, y));
					break;
				}
				case "rotate":
				{
					float num4 = NextFloat() * (global::System.MathF.PI / 180f);
					float num5 = 0f;
					float num6 = 0f;
					if (!PeekSymbol(')'))
					{
						num5 = NextFloat();
						num6 = NextFloat();
					}
					transform *= global::Unity.VectorGraphics.Matrix2D.Translate(new global::UnityEngine.Vector2(num5, num6)) * global::Unity.VectorGraphics.Matrix2D.RotateLH(0f - num4) * global::Unity.VectorGraphics.Matrix2D.Translate(new global::UnityEngine.Vector2(0f - num5, 0f - num6));
					break;
				}
				default:
					if (!(text == "skewY"))
					{
						throw new global::System.Exception("Unknown transform command at " + num + " in trasform specification");
					}
					goto case "skewX";
				case "skewX":
				{
					float num2 = global::UnityEngine.Mathf.Tan(NextFloat() * (global::System.MathF.PI / 180f));
					global::Unity.VectorGraphics.Matrix2D identity = global::Unity.VectorGraphics.Matrix2D.identity;
					if (text == "skewY")
					{
						identity.m10 = num2;
					}
					else
					{
						identity.m01 = num2;
					}
					transform *= identity;
					break;
				}
				}
				SkipSymbol(')');
			}
		}

		private SVGAttribParser(string attrib, string attribName, float opacity, global::Unity.VectorGraphics.FillMode mode, global::Unity.VectorGraphics.SVGDictionary dict, global::Unity.VectorGraphics.SVGPostponedFills postponedFills, bool allowReference = true)
		{
			this.attribName = attribName;
			if (string.IsNullOrEmpty(attrib))
			{
				if (opacity < 1f)
				{
					fill = new global::Unity.VectorGraphics.SolidFill
					{
						Color = new global::UnityEngine.Color(0f, 0f, 0f, opacity)
					};
				}
				else
				{
					fill = dict[(mode == global::Unity.VectorGraphics.FillMode.NonZero) ? global::Unity.VectorGraphics.SVGDocument.StockBlackNonZeroFillName : global::Unity.VectorGraphics.SVGDocument.StockBlackOddEvenFillName] as global::Unity.VectorGraphics.IFill;
				}
			}
			else
			{
				if (attrib == "none" || attrib == "transparent")
				{
					return;
				}
				if (attrib == "currentColor")
				{
					global::UnityEngine.Debug.LogError("currentColor is not supported as a " + attribName + " value");
					return;
				}
				string[] array = attrib.Split(new char[1] { ' ' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				if (allowReference)
				{
					string text = ParseURLRef(array[0]);
					if (text != null)
					{
						fill = ParseRelativeRef(text, dict) as global::Unity.VectorGraphics.IFill;
						if (fill == null)
						{
							if (array.Length > 1)
							{
								fill = new global::Unity.VectorGraphics.SVGAttribParser(array[1], attribName, opacity, mode, dict, postponedFills, allowReference: false).fill;
							}
							else if (postponedFills != null)
							{
								fill = new global::Unity.VectorGraphics.SolidFill
								{
									Color = global::UnityEngine.Color.clear
								};
								postponedFills[fill] = text;
							}
						}
						if (fill != null)
						{
							fill.Opacity = opacity;
						}
						return;
					}
				}
				global::UnityEngine.Color color = ParseColor(string.Join("", array));
				color.a *= opacity;
				if (array.Length > 1)
				{
				}
				fill = new global::Unity.VectorGraphics.SolidFill
				{
					Color = color,
					Mode = mode
				};
			}
		}

		private void ConcludePath(bool joinEnds)
		{
			if (currentContour.Count > 0)
			{
				global::Unity.VectorGraphics.BezierContour item = new global::Unity.VectorGraphics.BezierContour
				{
					Closed = (joinEnds && currentContour.Count >= 1),
					Segments = new global::Unity.VectorGraphics.BezierPathSegment[currentContour.Count + 1]
				};
				int num = 0;
				foreach (global::Unity.VectorGraphics.BezierSegment item2 in currentContour)
				{
					item.Segments[num++] = new global::Unity.VectorGraphics.BezierPathSegment
					{
						P0 = item2.P0,
						P1 = item2.P1,
						P2 = item2.P2
					};
				}
				global::Unity.VectorGraphics.BezierSegment bezierSegment = global::Unity.VectorGraphics.VectorUtils.MakeLine(currentContour.Last.Value.P3, item.Segments[0].P0);
				item.Segments[num] = new global::Unity.VectorGraphics.BezierPathSegment
				{
					P0 = bezierSegment.P0,
					P1 = bezierSegment.P1,
					P2 = bezierSegment.P2
				};
				contours.Add(item);
			}
			currentContour.Clear();
		}

		private global::UnityEngine.Vector2 NextVector2(bool relative = false)
		{
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(NextFloat(), NextFloat());
			return relative ? (vector + penPos) : vector;
		}

		private float NextFloat()
		{
			SkipWhitespaces();
			if (stringPos >= attribString.Length)
			{
				throw new global::System.Exception(attribName + " specification ended before sufficing numbers required by the last pathCommand");
			}
			int num = stringPos;
			if (attribString[stringPos] == '-' || attribString[stringPos] == '+')
			{
				stringPos++;
			}
			bool flag = false;
			bool flag2 = false;
			while (stringPos < attribString.Length)
			{
				char c = attribString[stringPos];
				if (!flag && c == '.')
				{
					flag = true;
					stringPos++;
				}
				else if (!flag2 && (c == 'e' || c == 'E'))
				{
					flag2 = true;
					stringPos++;
					if (stringPos < attribString.Length && attribString[stringPos] == '-')
					{
						stringPos++;
					}
				}
				else
				{
					if (!char.IsDigit(c))
					{
						break;
					}
					stringPos++;
				}
			}
			if (stringPos - num == 0 || (stringPos - num == 1 && attribString[num] == '-'))
			{
				throw new global::System.Exception("Missing number at " + num + " in " + attribName + " specification");
			}
			return ParseFloat(attribString.Substring(num, stringPos - num));
		}

		internal static float ParseFloat(string s)
		{
			return float.Parse(s, global::System.Globalization.NumberStyles.Number | global::System.Globalization.NumberStyles.AllowExponent, global::System.Globalization.CultureInfo.InvariantCulture);
		}

		private bool NextBool()
		{
			bool result = false;
			bool flag = false;
			SkipWhitespaces();
			if (stringPos < attribString.Length)
			{
				char c = attribString[stringPos];
				stringPos++;
				if (c != '0' && c != '1')
				{
					flag = true;
				}
				else
				{
					result = c == '1';
				}
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				throw new global::System.Exception("Expected bool at " + stringPos + " of " + attribName + " specification");
			}
			return result;
		}

		private char NextPathCommand(bool noCommandInheritance = false)
		{
			SkipWhitespaces();
			if (stringPos >= attribString.Length)
			{
				return '\0';
			}
			char c = attribString[stringPos];
			if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
			{
				pathCommand = c;
				stringPos++;
				return c;
			}
			if (!noCommandInheritance && (char.IsDigit(c) || c == '.' || c == '-'))
			{
				return pathCommand;
			}
			throw new global::System.Exception("Unexpected character at " + stringPos + " in path specification");
		}

		private string NextStringCommand()
		{
			SkipWhitespaces();
			if (stringPos >= attribString.Length)
			{
				return null;
			}
			int num = stringPos;
			while (stringPos < attribString.Length)
			{
				char c = attribString[stringPos];
				if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
				{
					stringPos++;
					continue;
				}
				break;
			}
			if (stringPos - num == 0)
			{
				throw new global::System.Exception("Unexpected character at " + stringPos + " in " + attribName + " specification");
			}
			return attribString.Substring(num, stringPos - num);
		}

		private void SkipSymbol(char s)
		{
			SkipWhitespaces();
			if (stringPos >= attribString.Length || attribString[stringPos] != s)
			{
				throw new global::System.Exception("Expected " + s + " at " + stringPos + " of " + attribName + " specification");
			}
			stringPos++;
		}

		private bool PeekSymbol(char s)
		{
			SkipWhitespaces();
			return stringPos < attribString.Length && attribString[stringPos] == s;
		}

		private void SkipWhitespaces()
		{
			while (stringPos < attribString.Length)
			{
				switch (attribString[stringPos])
				{
				case '\t':
				case '\n':
				case '\r':
				case ' ':
				case ',':
					break;
				default:
					return;
				}
				stringPos++;
			}
		}
	}
}
