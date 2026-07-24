namespace Newtonsoft.Json.Linq
{
	public abstract class JContainer : global::Newtonsoft.Json.Linq.JToken, global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>, global::System.Collections.IEnumerable, global::System.ComponentModel.ITypedList, global::System.ComponentModel.IBindingList, global::System.Collections.ICollection, global::System.Collections.IList, global::System.Collections.Specialized.INotifyCollectionChanged
	{
		internal global::System.ComponentModel.ListChangedEventHandler? _listChanged;

		internal global::System.ComponentModel.AddingNewEventHandler? _addingNew;

		internal global::System.Collections.Specialized.NotifyCollectionChangedEventHandler? _collectionChanged;

		private object? _syncRoot;

		private bool _busy;

		protected abstract global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> ChildrenTokens { get; }

		public override bool HasValues => ChildrenTokens.Count > 0;

		public override global::Newtonsoft.Json.Linq.JToken? First
		{
			get
			{
				global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		public override global::Newtonsoft.Json.Linq.JToken? Last
		{
			get
			{
				global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		global::Newtonsoft.Json.Linq.JToken global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>.this[int index]
		{
			get
			{
				return GetItem(index);
			}
			set
			{
				SetItem(index, value);
			}
		}

		bool global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>.IsReadOnly => false;

		bool global::System.Collections.IList.IsFixedSize => false;

		bool global::System.Collections.IList.IsReadOnly => false;

		object? global::System.Collections.IList.this[int index]
		{
			get
			{
				return GetItem(index);
			}
			set
			{
				SetItem(index, EnsureValue(value));
			}
		}

		public int Count => ChildrenTokens.Count;

		bool global::System.Collections.ICollection.IsSynchronized => false;

		object global::System.Collections.ICollection.SyncRoot
		{
			get
			{
				if (_syncRoot == null)
				{
					global::System.Threading.Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}
				return _syncRoot;
			}
		}

		bool global::System.ComponentModel.IBindingList.AllowEdit => true;

		bool global::System.ComponentModel.IBindingList.AllowNew => true;

		bool global::System.ComponentModel.IBindingList.AllowRemove => true;

		bool global::System.ComponentModel.IBindingList.IsSorted => false;

		global::System.ComponentModel.ListSortDirection global::System.ComponentModel.IBindingList.SortDirection => global::System.ComponentModel.ListSortDirection.Ascending;

		global::System.ComponentModel.PropertyDescriptor? global::System.ComponentModel.IBindingList.SortProperty => null;

		bool global::System.ComponentModel.IBindingList.SupportsChangeNotification => true;

		bool global::System.ComponentModel.IBindingList.SupportsSearching => false;

		bool global::System.ComponentModel.IBindingList.SupportsSorting => false;

		public event global::System.ComponentModel.ListChangedEventHandler ListChanged
		{
			add
			{
				_listChanged = (global::System.ComponentModel.ListChangedEventHandler)global::System.Delegate.Combine(_listChanged, value);
			}
			remove
			{
				_listChanged = (global::System.ComponentModel.ListChangedEventHandler)global::System.Delegate.Remove(_listChanged, value);
			}
		}

		public event global::System.ComponentModel.AddingNewEventHandler AddingNew
		{
			add
			{
				_addingNew = (global::System.ComponentModel.AddingNewEventHandler)global::System.Delegate.Combine(_addingNew, value);
			}
			remove
			{
				_addingNew = (global::System.ComponentModel.AddingNewEventHandler)global::System.Delegate.Remove(_addingNew, value);
			}
		}

		public event global::System.Collections.Specialized.NotifyCollectionChangedEventHandler? CollectionChanged
		{
			add
			{
				_collectionChanged = (global::System.Collections.Specialized.NotifyCollectionChangedEventHandler)global::System.Delegate.Combine(_collectionChanged, value);
			}
			remove
			{
				_collectionChanged = (global::System.Collections.Specialized.NotifyCollectionChangedEventHandler)global::System.Delegate.Remove(_collectionChanged, value);
			}
		}

		internal async global::System.Threading.Tasks.Task ReadTokenFromAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? options, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(reader, "reader");
			int startDepth = reader.Depth;
			if (!(await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false)))
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading {0} from JsonReader.", global::System.Globalization.CultureInfo.InvariantCulture, GetType().Name));
			}
			await ReadContentFromAsync(reader, options, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (reader.Depth > startDepth)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end of content while loading {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType().Name));
			}
		}

		private async global::System.Threading.Tasks.Task ReadContentFromAsync(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			global::Newtonsoft.Json.IJsonLineInfo lineInfo = reader as global::Newtonsoft.Json.IJsonLineInfo;
			global::Newtonsoft.Json.Linq.JContainer parent = this;
			do
			{
				if (parent is global::Newtonsoft.Json.Linq.JProperty { Value: not null })
				{
					if (parent == this)
					{
						break;
					}
					parent = parent.Parent;
				}
				switch (reader.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.StartArray:
				{
					global::Newtonsoft.Json.Linq.JArray jArray = new global::Newtonsoft.Json.Linq.JArray();
					jArray.SetLineInfo(lineInfo, settings);
					parent.Add(jArray);
					parent = jArray;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndArray:
					if (parent == this)
					{
						return;
					}
					parent = parent.Parent;
					break;
				case global::Newtonsoft.Json.JsonToken.StartObject:
				{
					global::Newtonsoft.Json.Linq.JObject jObject = new global::Newtonsoft.Json.Linq.JObject();
					jObject.SetLineInfo(lineInfo, settings);
					parent.Add(jObject);
					parent = jObject;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					if (parent == this)
					{
						return;
					}
					parent = parent.Parent;
					break;
				case global::Newtonsoft.Json.JsonToken.StartConstructor:
				{
					global::Newtonsoft.Json.Linq.JConstructor jConstructor = new global::Newtonsoft.Json.Linq.JConstructor(reader.Value.ToString());
					jConstructor.SetLineInfo(lineInfo, settings);
					parent.Add(jConstructor);
					parent = jConstructor;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndConstructor:
					if (parent == this)
					{
						return;
					}
					parent = parent.Parent;
					break;
				case global::Newtonsoft.Json.JsonToken.Integer:
				case global::Newtonsoft.Json.JsonToken.Float:
				case global::Newtonsoft.Json.JsonToken.String:
				case global::Newtonsoft.Json.JsonToken.Boolean:
				case global::Newtonsoft.Json.JsonToken.Date:
				case global::Newtonsoft.Json.JsonToken.Bytes:
				{
					global::Newtonsoft.Json.Linq.JValue jValue = new global::Newtonsoft.Json.Linq.JValue(reader.Value);
					jValue.SetLineInfo(lineInfo, settings);
					parent.Add(jValue);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.Comment:
					if (settings != null && settings.CommentHandling == global::Newtonsoft.Json.Linq.CommentHandling.Load)
					{
						global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateComment(reader.Value.ToString());
						jValue.SetLineInfo(lineInfo, settings);
						parent.Add(jValue);
					}
					break;
				case global::Newtonsoft.Json.JsonToken.Null:
				{
					global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateNull();
					jValue.SetLineInfo(lineInfo, settings);
					parent.Add(jValue);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.Undefined:
				{
					global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateUndefined();
					jValue.SetLineInfo(lineInfo, settings);
					parent.Add(jValue);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					global::Newtonsoft.Json.Linq.JProperty jProperty2 = ReadProperty(reader, settings, lineInfo, parent);
					if (jProperty2 != null)
					{
						parent = jProperty2;
					}
					else
					{
						await reader.SkipAsync().ConfigureAwait(continueOnCapturedContext: false);
					}
					break;
				}
				default:
					throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("The JsonReader should not be on a token of type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, reader.TokenType));
				case global::Newtonsoft.Json.JsonToken.None:
					break;
				}
			}
			while (await reader.ReadAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false));
		}

		internal JContainer()
		{
		}

		internal JContainer(global::Newtonsoft.Json.Linq.JContainer other, global::Newtonsoft.Json.Linq.JsonCloneSettings? settings)
			: this()
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(other, "other");
			bool flag = settings?.CopyAnnotations ?? true;
			if (flag)
			{
				CopyAnnotations(this, other);
			}
			int num = 0;
			foreach (global::Newtonsoft.Json.Linq.JToken item in (global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken>)other)
			{
				TryAddInternal(num, item, skipParentCheck: false, flag);
				num++;
			}
		}

		internal void CheckReentrancy()
		{
			if (_busy)
			{
				throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Cannot change {0} during a collection change event.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
			}
		}

		internal virtual global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> CreateChildrenCollection()
		{
			return new global::System.Collections.Generic.List<global::Newtonsoft.Json.Linq.JToken>();
		}

		protected virtual void OnAddingNew(global::System.ComponentModel.AddingNewEventArgs e)
		{
			_addingNew?.Invoke(this, e);
		}

		protected virtual void OnListChanged(global::System.ComponentModel.ListChangedEventArgs e)
		{
			global::System.ComponentModel.ListChangedEventHandler listChanged = _listChanged;
			if (listChanged != null)
			{
				_busy = true;
				try
				{
					listChanged(this, e);
				}
				finally
				{
					_busy = false;
				}
			}
		}

		protected virtual void OnCollectionChanged(global::System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			global::System.Collections.Specialized.NotifyCollectionChangedEventHandler collectionChanged = _collectionChanged;
			if (collectionChanged != null)
			{
				_busy = true;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					_busy = false;
				}
			}
		}

		internal bool ContentsEqual(global::Newtonsoft.Json.Linq.JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		public override global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken> Children()
		{
			return new global::Newtonsoft.Json.Linq.JEnumerable<global::Newtonsoft.Json.Linq.JToken>(ChildrenTokens);
		}

		public override global::System.Collections.Generic.IEnumerable<T?> Values<T>()
		{
			return ChildrenTokens.Convert<global::Newtonsoft.Json.Linq.JToken, T>();
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> Descendants()
		{
			return GetDescendants(self: false);
		}

		public global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> DescendantsAndSelf()
		{
			return GetDescendants(self: true);
		}

		internal global::System.Collections.Generic.IEnumerable<global::Newtonsoft.Json.Linq.JToken> GetDescendants(bool self)
		{
			if (self)
			{
				yield return this;
			}
			foreach (global::Newtonsoft.Json.Linq.JToken o in ChildrenTokens)
			{
				yield return o;
				if (!(o is global::Newtonsoft.Json.Linq.JContainer jContainer))
				{
					continue;
				}
				foreach (global::Newtonsoft.Json.Linq.JToken item in jContainer.Descendants())
				{
					yield return item;
				}
			}
		}

		internal bool IsMultiContent([global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] object? content)
		{
			if (content is global::System.Collections.IEnumerable && !(content is string) && !(content is global::Newtonsoft.Json.Linq.JToken))
			{
				return !(content is byte[]);
			}
			return false;
		}

		internal global::Newtonsoft.Json.Linq.JToken EnsureParentToken(global::Newtonsoft.Json.Linq.JToken? item, bool skipParentCheck, bool copyAnnotations)
		{
			if (item == null)
			{
				return global::Newtonsoft.Json.Linq.JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				global::Newtonsoft.Json.Linq.JsonCloneSettings settings = (copyAnnotations ? null : global::Newtonsoft.Json.Linq.JsonCloneSettings.SkipCopyAnnotations);
				item = item.CloneToken(settings);
			}
			return item;
		}

		internal abstract int IndexOfItem(global::Newtonsoft.Json.Linq.JToken? item);

		internal virtual bool InsertItem(int index, global::Newtonsoft.Json.Linq.JToken? item, bool skipParentCheck, bool copyAnnotations)
		{
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new global::System.ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			CheckReentrancy();
			item = EnsureParentToken(item, skipParentCheck, copyAnnotations);
			global::Newtonsoft.Json.Linq.JToken jToken = ((index == 0) ? null : childrenTokens[index - 1]);
			global::Newtonsoft.Json.Linq.JToken jToken2 = ((index == childrenTokens.Count) ? null : childrenTokens[index]);
			ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jToken;
			if (jToken != null)
			{
				jToken.Next = item;
			}
			item.Next = jToken2;
			if (jToken2 != null)
			{
				jToken2.Previous = item;
			}
			childrenTokens.Insert(index, item);
			if (_listChanged != null)
			{
				OnListChanged(new global::System.ComponentModel.ListChangedEventArgs(global::System.ComponentModel.ListChangedType.ItemAdded, index));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new global::System.Collections.Specialized.NotifyCollectionChangedEventArgs(global::System.Collections.Specialized.NotifyCollectionChangedAction.Add, item, index));
			}
			return true;
		}

		internal virtual void RemoveItemAt(int index)
		{
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
			if (index < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new global::System.ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			CheckReentrancy();
			global::Newtonsoft.Json.Linq.JToken jToken = childrenTokens[index];
			global::Newtonsoft.Json.Linq.JToken jToken2 = ((index == 0) ? null : childrenTokens[index - 1]);
			global::Newtonsoft.Json.Linq.JToken jToken3 = ((index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1]);
			if (jToken2 != null)
			{
				jToken2.Next = jToken3;
			}
			if (jToken3 != null)
			{
				jToken3.Previous = jToken2;
			}
			jToken.Parent = null;
			jToken.Previous = null;
			jToken.Next = null;
			childrenTokens.RemoveAt(index);
			if (_listChanged != null)
			{
				OnListChanged(new global::System.ComponentModel.ListChangedEventArgs(global::System.ComponentModel.ListChangedType.ItemDeleted, index));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new global::System.Collections.Specialized.NotifyCollectionChangedEventArgs(global::System.Collections.Specialized.NotifyCollectionChangedAction.Remove, jToken, index));
			}
		}

		internal virtual bool RemoveItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			if (item != null)
			{
				int num = IndexOfItem(item);
				if (num >= 0)
				{
					RemoveItemAt(num);
					return true;
				}
			}
			return false;
		}

		internal virtual global::Newtonsoft.Json.Linq.JToken GetItem(int index)
		{
			return ChildrenTokens[index];
		}

		internal virtual void SetItem(int index, global::Newtonsoft.Json.Linq.JToken? item)
		{
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
			if (index < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new global::System.ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			global::Newtonsoft.Json.Linq.JToken jToken = childrenTokens[index];
			if (!IsTokenUnchanged(jToken, item))
			{
				CheckReentrancy();
				item = EnsureParentToken(item, skipParentCheck: false, copyAnnotations: true);
				ValidateToken(item, jToken);
				global::Newtonsoft.Json.Linq.JToken jToken2 = ((index == 0) ? null : childrenTokens[index - 1]);
				global::Newtonsoft.Json.Linq.JToken jToken3 = ((index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1]);
				item.Parent = this;
				item.Previous = jToken2;
				if (jToken2 != null)
				{
					jToken2.Next = item;
				}
				item.Next = jToken3;
				if (jToken3 != null)
				{
					jToken3.Previous = item;
				}
				childrenTokens[index] = item;
				jToken.Parent = null;
				jToken.Previous = null;
				jToken.Next = null;
				if (_listChanged != null)
				{
					OnListChanged(new global::System.ComponentModel.ListChangedEventArgs(global::System.ComponentModel.ListChangedType.ItemChanged, index));
				}
				if (_collectionChanged != null)
				{
					OnCollectionChanged(new global::System.Collections.Specialized.NotifyCollectionChangedEventArgs(global::System.Collections.Specialized.NotifyCollectionChangedAction.Replace, item, jToken, index));
				}
			}
		}

		internal virtual void ClearItems()
		{
			CheckReentrancy();
			global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken> childrenTokens = ChildrenTokens;
			foreach (global::Newtonsoft.Json.Linq.JToken item in childrenTokens)
			{
				item.Parent = null;
				item.Previous = null;
				item.Next = null;
			}
			childrenTokens.Clear();
			if (_listChanged != null)
			{
				OnListChanged(new global::System.ComponentModel.ListChangedEventArgs(global::System.ComponentModel.ListChangedType.Reset, -1));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new global::System.Collections.Specialized.NotifyCollectionChangedEventArgs(global::System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
			}
		}

		internal virtual void ReplaceItem(global::Newtonsoft.Json.Linq.JToken existing, global::Newtonsoft.Json.Linq.JToken replacement)
		{
			if (existing != null && existing.Parent == this)
			{
				int index = IndexOfItem(existing);
				SetItem(index, replacement);
			}
		}

		internal virtual bool ContainsItem(global::Newtonsoft.Json.Linq.JToken? item)
		{
			return IndexOfItem(item) != -1;
		}

		internal virtual void CopyItemsTo(global::System.Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new global::System.ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new global::System.ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new global::System.ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (Count > array.Length - arrayIndex)
			{
				throw new global::System.ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (global::Newtonsoft.Json.Linq.JToken childrenToken in ChildrenTokens)
			{
				array.SetValue(childrenToken, arrayIndex + num);
				num++;
			}
		}

		internal static bool IsTokenUnchanged(global::Newtonsoft.Json.Linq.JToken currentValue, global::Newtonsoft.Json.Linq.JToken? newValue)
		{
			if (currentValue is global::Newtonsoft.Json.Linq.JValue jValue)
			{
				if (newValue == null)
				{
					return jValue.Type == global::Newtonsoft.Json.Linq.JTokenType.Null;
				}
				return jValue.Equals(newValue);
			}
			return false;
		}

		internal virtual void ValidateToken(global::Newtonsoft.Json.Linq.JToken o, global::Newtonsoft.Json.Linq.JToken? existing)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == global::Newtonsoft.Json.Linq.JTokenType.Property)
			{
				throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Can not add {0} to {1}.", global::System.Globalization.CultureInfo.InvariantCulture, o.GetType(), GetType()));
			}
		}

		public virtual void Add(object? content)
		{
			TryAddInternal(ChildrenTokens.Count, content, skipParentCheck: false, copyAnnotations: true);
		}

		internal bool TryAdd(object? content)
		{
			return TryAddInternal(ChildrenTokens.Count, content, skipParentCheck: false, copyAnnotations: true);
		}

		internal void AddAndSkipParentCheck(global::Newtonsoft.Json.Linq.JToken token)
		{
			TryAddInternal(ChildrenTokens.Count, token, skipParentCheck: true, copyAnnotations: true);
		}

		public void AddFirst(object? content)
		{
			TryAddInternal(0, content, skipParentCheck: false, copyAnnotations: true);
		}

		internal bool TryAddInternal(int index, object? content, bool skipParentCheck, bool copyAnnotations)
		{
			if (IsMultiContent(content))
			{
				global::System.Collections.IEnumerable obj = (global::System.Collections.IEnumerable)content;
				int num = index;
				foreach (object item2 in obj)
				{
					TryAddInternal(num, item2, skipParentCheck, copyAnnotations);
					num++;
				}
				return true;
			}
			global::Newtonsoft.Json.Linq.JToken item = CreateFromContent(content);
			return InsertItem(index, item, skipParentCheck, copyAnnotations);
		}

		internal static global::Newtonsoft.Json.Linq.JToken CreateFromContent(object? content)
		{
			if (content is global::Newtonsoft.Json.Linq.JToken result)
			{
				return result;
			}
			return new global::Newtonsoft.Json.Linq.JValue(content);
		}

		public global::Newtonsoft.Json.JsonWriter CreateWriter()
		{
			return new global::Newtonsoft.Json.Linq.JTokenWriter(this);
		}

		public void ReplaceAll(object content)
		{
			ClearItems();
			Add(content);
		}

		public void RemoveAll()
		{
			ClearItems();
		}

		internal abstract void MergeItem(object content, global::Newtonsoft.Json.Linq.JsonMergeSettings? settings);

		public void Merge(object? content)
		{
			if (content != null)
			{
				ValidateContent(content);
				MergeItem(content, null);
			}
		}

		public void Merge(object? content, global::Newtonsoft.Json.Linq.JsonMergeSettings? settings)
		{
			if (content != null)
			{
				ValidateContent(content);
				MergeItem(content, settings);
			}
		}

		private void ValidateContent(object content)
		{
			if (content.GetType().IsSubclassOf(typeof(global::Newtonsoft.Json.Linq.JToken)) || IsMultiContent(content))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not determine JSON object type for type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, content.GetType()), "content");
		}

		internal void ReadTokenFrom(global::Newtonsoft.Json.JsonReader reader, global::Newtonsoft.Json.Linq.JsonLoadSettings? options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Error reading {0} from JsonReader.", global::System.Globalization.CultureInfo.InvariantCulture, GetType().Name));
			}
			ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw global::Newtonsoft.Json.JsonReaderException.Create(reader, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Unexpected end of content while loading {0}.", global::System.Globalization.CultureInfo.InvariantCulture, GetType().Name));
			}
		}

		internal void ReadContentFrom(global::Newtonsoft.Json.JsonReader r, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings)
		{
			global::Newtonsoft.Json.Utilities.ValidationUtils.ArgumentNotNull(r, "r");
			global::Newtonsoft.Json.IJsonLineInfo lineInfo = r as global::Newtonsoft.Json.IJsonLineInfo;
			global::Newtonsoft.Json.Linq.JContainer jContainer = this;
			do
			{
				if (jContainer is global::Newtonsoft.Json.Linq.JProperty { Value: not null })
				{
					if (jContainer == this)
					{
						break;
					}
					jContainer = jContainer.Parent;
				}
				switch (r.TokenType)
				{
				case global::Newtonsoft.Json.JsonToken.StartArray:
				{
					global::Newtonsoft.Json.Linq.JArray jArray = new global::Newtonsoft.Json.Linq.JArray();
					jArray.SetLineInfo(lineInfo, settings);
					jContainer.Add(jArray);
					jContainer = jArray;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndArray:
					if (jContainer == this)
					{
						return;
					}
					jContainer = jContainer.Parent;
					break;
				case global::Newtonsoft.Json.JsonToken.StartObject:
				{
					global::Newtonsoft.Json.Linq.JObject jObject = new global::Newtonsoft.Json.Linq.JObject();
					jObject.SetLineInfo(lineInfo, settings);
					jContainer.Add(jObject);
					jContainer = jObject;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndObject:
					if (jContainer == this)
					{
						return;
					}
					jContainer = jContainer.Parent;
					break;
				case global::Newtonsoft.Json.JsonToken.StartConstructor:
				{
					global::Newtonsoft.Json.Linq.JConstructor jConstructor = new global::Newtonsoft.Json.Linq.JConstructor(r.Value.ToString());
					jConstructor.SetLineInfo(lineInfo, settings);
					jContainer.Add(jConstructor);
					jContainer = jConstructor;
					break;
				}
				case global::Newtonsoft.Json.JsonToken.EndConstructor:
					if (jContainer == this)
					{
						return;
					}
					jContainer = jContainer.Parent;
					break;
				case global::Newtonsoft.Json.JsonToken.Integer:
				case global::Newtonsoft.Json.JsonToken.Float:
				case global::Newtonsoft.Json.JsonToken.String:
				case global::Newtonsoft.Json.JsonToken.Boolean:
				case global::Newtonsoft.Json.JsonToken.Date:
				case global::Newtonsoft.Json.JsonToken.Bytes:
				{
					global::Newtonsoft.Json.Linq.JValue jValue = new global::Newtonsoft.Json.Linq.JValue(r.Value);
					jValue.SetLineInfo(lineInfo, settings);
					jContainer.Add(jValue);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.Comment:
					if (settings != null && settings.CommentHandling == global::Newtonsoft.Json.Linq.CommentHandling.Load)
					{
						global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateComment(r.Value.ToString());
						jValue.SetLineInfo(lineInfo, settings);
						jContainer.Add(jValue);
					}
					break;
				case global::Newtonsoft.Json.JsonToken.Null:
				{
					global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateNull();
					jValue.SetLineInfo(lineInfo, settings);
					jContainer.Add(jValue);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.Undefined:
				{
					global::Newtonsoft.Json.Linq.JValue jValue = global::Newtonsoft.Json.Linq.JValue.CreateUndefined();
					jValue.SetLineInfo(lineInfo, settings);
					jContainer.Add(jValue);
					break;
				}
				case global::Newtonsoft.Json.JsonToken.PropertyName:
				{
					global::Newtonsoft.Json.Linq.JProperty jProperty2 = ReadProperty(r, settings, lineInfo, jContainer);
					if (jProperty2 != null)
					{
						jContainer = jProperty2;
					}
					else
					{
						r.Skip();
					}
					break;
				}
				default:
					throw new global::System.InvalidOperationException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("The JsonReader should not be on a token of type {0}.", global::System.Globalization.CultureInfo.InvariantCulture, r.TokenType));
				case global::Newtonsoft.Json.JsonToken.None:
					break;
				}
			}
			while (r.Read());
		}

		private static global::Newtonsoft.Json.Linq.JProperty? ReadProperty(global::Newtonsoft.Json.JsonReader r, global::Newtonsoft.Json.Linq.JsonLoadSettings? settings, global::Newtonsoft.Json.IJsonLineInfo? lineInfo, global::Newtonsoft.Json.Linq.JContainer parent)
		{
			global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling duplicatePropertyNameHandling = settings?.DuplicatePropertyNameHandling ?? global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling.Replace;
			global::Newtonsoft.Json.Linq.JObject obj = (global::Newtonsoft.Json.Linq.JObject)parent;
			string text = r.Value.ToString();
			global::Newtonsoft.Json.Linq.JProperty jProperty = obj.Property(text, global::System.StringComparison.Ordinal);
			if (jProperty != null)
			{
				switch (duplicatePropertyNameHandling)
				{
				case global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling.Ignore:
					return null;
				case global::Newtonsoft.Json.Linq.DuplicatePropertyNameHandling.Error:
					throw global::Newtonsoft.Json.JsonReaderException.Create(r, global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Property with the name '{0}' already exists in the current JSON object.", global::System.Globalization.CultureInfo.InvariantCulture, text));
				}
			}
			global::Newtonsoft.Json.Linq.JProperty jProperty2 = new global::Newtonsoft.Json.Linq.JProperty(text);
			jProperty2.SetLineInfo(lineInfo, settings);
			if (jProperty == null)
			{
				parent.Add(jProperty2);
			}
			else
			{
				jProperty.Replace(jProperty2);
			}
			return jProperty2;
		}

		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (global::Newtonsoft.Json.Linq.JToken childrenToken in ChildrenTokens)
			{
				num ^= childrenToken.GetDeepHashCode();
			}
			return num;
		}

		string global::System.ComponentModel.ITypedList.GetListName(global::System.ComponentModel.PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		global::System.ComponentModel.PropertyDescriptorCollection global::System.ComponentModel.ITypedList.GetItemProperties(global::System.ComponentModel.PropertyDescriptor[] listAccessors)
		{
			return (First as global::System.ComponentModel.ICustomTypeDescriptor)?.GetProperties() ?? new global::System.ComponentModel.PropertyDescriptorCollection(global::Newtonsoft.Json.Utilities.CollectionUtils.ArrayEmpty<global::System.ComponentModel.PropertyDescriptor>());
		}

		int global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>.IndexOf(global::Newtonsoft.Json.Linq.JToken item)
		{
			return IndexOfItem(item);
		}

		void global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>.Insert(int index, global::Newtonsoft.Json.Linq.JToken item)
		{
			InsertItem(index, item, skipParentCheck: false, copyAnnotations: true);
		}

		void global::System.Collections.Generic.IList<global::Newtonsoft.Json.Linq.JToken>.RemoveAt(int index)
		{
			RemoveItemAt(index);
		}

		void global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>.Add(global::Newtonsoft.Json.Linq.JToken item)
		{
			Add(item);
		}

		void global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>.Clear()
		{
			ClearItems();
		}

		bool global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>.Contains(global::Newtonsoft.Json.Linq.JToken item)
		{
			return ContainsItem(item);
		}

		void global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>.CopyTo(global::Newtonsoft.Json.Linq.JToken[] array, int arrayIndex)
		{
			CopyItemsTo(array, arrayIndex);
		}

		bool global::System.Collections.Generic.ICollection<global::Newtonsoft.Json.Linq.JToken>.Remove(global::Newtonsoft.Json.Linq.JToken item)
		{
			return RemoveItem(item);
		}

		private global::Newtonsoft.Json.Linq.JToken? EnsureValue(object? value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is global::Newtonsoft.Json.Linq.JToken result)
			{
				return result;
			}
			throw new global::System.ArgumentException("Argument is not a JToken.");
		}

		int global::System.Collections.IList.Add(object? value)
		{
			Add(EnsureValue(value));
			return Count - 1;
		}

		void global::System.Collections.IList.Clear()
		{
			ClearItems();
		}

		bool global::System.Collections.IList.Contains(object? value)
		{
			return ContainsItem(EnsureValue(value));
		}

		int global::System.Collections.IList.IndexOf(object? value)
		{
			return IndexOfItem(EnsureValue(value));
		}

		void global::System.Collections.IList.Insert(int index, object? value)
		{
			InsertItem(index, EnsureValue(value), skipParentCheck: false, copyAnnotations: false);
		}

		void global::System.Collections.IList.Remove(object? value)
		{
			RemoveItem(EnsureValue(value));
		}

		void global::System.Collections.IList.RemoveAt(int index)
		{
			RemoveItemAt(index);
		}

		void global::System.Collections.ICollection.CopyTo(global::System.Array array, int index)
		{
			CopyItemsTo(array, index);
		}

		void global::System.ComponentModel.IBindingList.AddIndex(global::System.ComponentModel.PropertyDescriptor property)
		{
		}

		object global::System.ComponentModel.IBindingList.AddNew()
		{
			global::System.ComponentModel.AddingNewEventArgs e = new global::System.ComponentModel.AddingNewEventArgs();
			OnAddingNew(e);
			if (e.NewObject == null)
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("Could not determine new value to add to '{0}'.", global::System.Globalization.CultureInfo.InvariantCulture, GetType()));
			}
			if (!(e.NewObject is global::Newtonsoft.Json.Linq.JToken jToken))
			{
				throw new global::Newtonsoft.Json.JsonException(global::Newtonsoft.Json.Utilities.StringUtils.FormatWith("New item to be added to collection must be compatible with {0}.", global::System.Globalization.CultureInfo.InvariantCulture, typeof(global::Newtonsoft.Json.Linq.JToken)));
			}
			Add(jToken);
			return jToken;
		}

		void global::System.ComponentModel.IBindingList.ApplySort(global::System.ComponentModel.PropertyDescriptor property, global::System.ComponentModel.ListSortDirection direction)
		{
			throw new global::System.NotSupportedException();
		}

		int global::System.ComponentModel.IBindingList.Find(global::System.ComponentModel.PropertyDescriptor property, object key)
		{
			throw new global::System.NotSupportedException();
		}

		void global::System.ComponentModel.IBindingList.RemoveIndex(global::System.ComponentModel.PropertyDescriptor property)
		{
		}

		void global::System.ComponentModel.IBindingList.RemoveSort()
		{
			throw new global::System.NotSupportedException();
		}

		internal static void MergeEnumerableContent(global::Newtonsoft.Json.Linq.JContainer target, global::System.Collections.IEnumerable content, global::Newtonsoft.Json.Linq.JsonMergeSettings? settings)
		{
			switch (settings?.MergeArrayHandling ?? global::Newtonsoft.Json.Linq.MergeArrayHandling.Concat)
			{
			case global::Newtonsoft.Json.Linq.MergeArrayHandling.Concat:
			{
				foreach (object item in content)
				{
					target.Add(CreateFromContent(item));
				}
				break;
			}
			case global::Newtonsoft.Json.Linq.MergeArrayHandling.Union:
			{
				global::System.Collections.Generic.HashSet<global::Newtonsoft.Json.Linq.JToken> hashSet = new global::System.Collections.Generic.HashSet<global::Newtonsoft.Json.Linq.JToken>(target, global::Newtonsoft.Json.Linq.JToken.EqualityComparer);
				{
					foreach (object item2 in content)
					{
						global::Newtonsoft.Json.Linq.JToken jToken2 = CreateFromContent(item2);
						if (hashSet.Add(jToken2))
						{
							target.Add(jToken2);
						}
					}
					break;
				}
			}
			case global::Newtonsoft.Json.Linq.MergeArrayHandling.Replace:
				if (target == content)
				{
					break;
				}
				target.ClearItems();
				{
					foreach (object item3 in content)
					{
						target.Add(CreateFromContent(item3));
					}
					break;
				}
			case global::Newtonsoft.Json.Linq.MergeArrayHandling.Merge:
			{
				int num = 0;
				{
					foreach (object item4 in content)
					{
						if (num < target.Count)
						{
							if (target[num] is global::Newtonsoft.Json.Linq.JContainer jContainer)
							{
								jContainer.Merge(item4, settings);
							}
							else if (item4 != null)
							{
								global::Newtonsoft.Json.Linq.JToken jToken = CreateFromContent(item4);
								if (jToken.Type != global::Newtonsoft.Json.Linq.JTokenType.Null)
								{
									target[num] = jToken;
								}
							}
						}
						else
						{
							target.Add(CreateFromContent(item4));
						}
						num++;
					}
					break;
				}
			}
			default:
				throw new global::System.ArgumentOutOfRangeException("settings", "Unexpected merge array handling when merging JSON.");
			}
		}
	}
}
