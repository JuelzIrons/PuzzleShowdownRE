namespace Unity.VisualScripting
{
	public sealed class GraphNest<TGraph, TMacro> : global::Unity.VisualScripting.IGraphNest, global::Unity.VisualScripting.IAotStubbable where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.GraphSource _source = global::Unity.VisualScripting.GraphSource.Macro;

		[global::Unity.VisualScripting.DoNotSerialize]
		private TMacro _macro;

		[global::Unity.VisualScripting.DoNotSerialize]
		private TGraph _embed;

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IGraphNester nester { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.GraphSource source
		{
			get
			{
				return _source;
			}
			set
			{
				if (value != source)
				{
					BeforeGraphChange();
					_source = value;
					AfterGraphChange();
				}
			}
		}

		[global::Unity.VisualScripting.Serialize]
		public TMacro macro
		{
			get
			{
				return _macro;
			}
			set
			{
				if (!(value == macro))
				{
					BeforeGraphChange();
					_macro = value;
					AfterGraphChange();
				}
			}
		}

		[global::Unity.VisualScripting.Serialize]
		public TGraph embed
		{
			get
			{
				return _embed;
			}
			set
			{
				if (value != embed)
				{
					BeforeGraphChange();
					_embed = value;
					AfterGraphChange();
				}
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public TGraph graph
		{
			get
			{
				switch (source)
				{
				case global::Unity.VisualScripting.GraphSource.Embed:
					return embed;
				case global::Unity.VisualScripting.GraphSource.Macro:
				{
					TMacro val = macro;
					if ((object)val == null)
					{
						return null;
					}
					return val.graph;
				}
				default:
					throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.GraphSource>(source);
				}
			}
		}

		global::Unity.VisualScripting.IMacro global::Unity.VisualScripting.IGraphNest.macro
		{
			get
			{
				return macro;
			}
			set
			{
				macro = (TMacro)value;
			}
		}

		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphNest.embed
		{
			get
			{
				return embed;
			}
			set
			{
				embed = (TGraph)value;
			}
		}

		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphNest.graph => graph;

		global::System.Type global::Unity.VisualScripting.IGraphNest.graphType => typeof(TGraph);

		global::System.Type global::Unity.VisualScripting.IGraphNest.macroType => typeof(TMacro);

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ISerializationDependency> deserializationDependencies
		{
			get
			{
				if (macro != null)
				{
					yield return macro;
				}
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool hasBackgroundEmbed
		{
			get
			{
				if (source == global::Unity.VisualScripting.GraphSource.Macro)
				{
					return embed != null;
				}
				return false;
			}
		}

		public event global::System.Action beforeGraphChange;

		public event global::System.Action afterGraphChange;

		public void SwitchToEmbed(TGraph embed)
		{
			if (source != global::Unity.VisualScripting.GraphSource.Embed || this.embed != embed)
			{
				BeforeGraphChange();
				_source = global::Unity.VisualScripting.GraphSource.Embed;
				_embed = embed;
				_macro = null;
				AfterGraphChange();
			}
		}

		public void SwitchToMacro(TMacro macro)
		{
			if (source != global::Unity.VisualScripting.GraphSource.Macro || !(this.macro == macro))
			{
				BeforeGraphChange();
				_source = global::Unity.VisualScripting.GraphSource.Macro;
				_embed = null;
				_macro = macro;
				AfterGraphChange();
			}
		}

		private void BeforeGraphChange()
		{
			if (graph != null)
			{
				nester.UninstantiateNest();
			}
			this.beforeGraphChange?.Invoke();
		}

		private void AfterGraphChange()
		{
			this.afterGraphChange?.Invoke();
			if (graph != null)
			{
				nester.InstantiateNest();
			}
		}

		public global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return global::Unity.VisualScripting.LinqUtility.Concat<object>(new global::System.Collections.IEnumerable[1] { graph?.GetAotStubs(visited) });
		}
	}
}
