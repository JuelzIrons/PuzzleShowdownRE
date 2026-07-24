namespace Newtonsoft.Json.Schema
{
	[global::System.Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModelBuilder
	{
		private global::Newtonsoft.Json.Schema.JsonSchemaNodeCollection _nodes = new global::Newtonsoft.Json.Schema.JsonSchemaNodeCollection();

		private global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Schema.JsonSchemaNode, global::Newtonsoft.Json.Schema.JsonSchemaModel> _nodeModels = new global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Schema.JsonSchemaNode, global::Newtonsoft.Json.Schema.JsonSchemaModel>();

		private global::Newtonsoft.Json.Schema.JsonSchemaNode _node;

		public global::Newtonsoft.Json.Schema.JsonSchemaModel Build(global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			_nodes = new global::Newtonsoft.Json.Schema.JsonSchemaNodeCollection();
			_node = AddSchema(null, schema);
			_nodeModels = new global::System.Collections.Generic.Dictionary<global::Newtonsoft.Json.Schema.JsonSchemaNode, global::Newtonsoft.Json.Schema.JsonSchemaModel>();
			return BuildNodeModel(_node);
		}

		public global::Newtonsoft.Json.Schema.JsonSchemaNode AddSchema(global::Newtonsoft.Json.Schema.JsonSchemaNode existingNode, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			string id;
			if (existingNode != null)
			{
				if (existingNode.Schemas.Contains(schema))
				{
					return existingNode;
				}
				id = global::Newtonsoft.Json.Schema.JsonSchemaNode.GetId(global::System.Linq.Enumerable.Union(existingNode.Schemas, new global::Newtonsoft.Json.Schema.JsonSchema[1] { schema }));
			}
			else
			{
				id = global::Newtonsoft.Json.Schema.JsonSchemaNode.GetId(new global::Newtonsoft.Json.Schema.JsonSchema[1] { schema });
			}
			if (_nodes.Contains(id))
			{
				return _nodes[id];
			}
			global::Newtonsoft.Json.Schema.JsonSchemaNode jsonSchemaNode = ((existingNode != null) ? existingNode.Combine(schema) : new global::Newtonsoft.Json.Schema.JsonSchemaNode(schema));
			_nodes.Add(jsonSchemaNode);
			AddProperties(schema.Properties, jsonSchemaNode.Properties);
			AddProperties(schema.PatternProperties, jsonSchemaNode.PatternProperties);
			if (schema.Items != null)
			{
				for (int i = 0; i < schema.Items.Count; i++)
				{
					AddItem(jsonSchemaNode, i, schema.Items[i]);
				}
			}
			if (schema.AdditionalItems != null)
			{
				AddAdditionalItems(jsonSchemaNode, schema.AdditionalItems);
			}
			if (schema.AdditionalProperties != null)
			{
				AddAdditionalProperties(jsonSchemaNode, schema.AdditionalProperties);
			}
			if (schema.Extends != null)
			{
				foreach (global::Newtonsoft.Json.Schema.JsonSchema extend in schema.Extends)
				{
					jsonSchemaNode = AddSchema(jsonSchemaNode, extend);
				}
			}
			return jsonSchemaNode;
		}

		public void AddProperties(global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchema> source, global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchemaNode> target)
		{
			if (source == null)
			{
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchema> item in source)
			{
				AddProperty(target, item.Key, item.Value);
			}
		}

		public void AddProperty(global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Schema.JsonSchemaNode> target, string propertyName, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			target.TryGetValue(propertyName, out var value);
			target[propertyName] = AddSchema(value, schema);
		}

		public void AddItem(global::Newtonsoft.Json.Schema.JsonSchemaNode parentNode, int index, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			global::Newtonsoft.Json.Schema.JsonSchemaNode existingNode = ((parentNode.Items.Count > index) ? parentNode.Items[index] : null);
			global::Newtonsoft.Json.Schema.JsonSchemaNode jsonSchemaNode = AddSchema(existingNode, schema);
			if (parentNode.Items.Count <= index)
			{
				parentNode.Items.Add(jsonSchemaNode);
			}
			else
			{
				parentNode.Items[index] = jsonSchemaNode;
			}
		}

		public void AddAdditionalProperties(global::Newtonsoft.Json.Schema.JsonSchemaNode parentNode, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			parentNode.AdditionalProperties = AddSchema(parentNode.AdditionalProperties, schema);
		}

		public void AddAdditionalItems(global::Newtonsoft.Json.Schema.JsonSchemaNode parentNode, global::Newtonsoft.Json.Schema.JsonSchema schema)
		{
			parentNode.AdditionalItems = AddSchema(parentNode.AdditionalItems, schema);
		}

		private global::Newtonsoft.Json.Schema.JsonSchemaModel BuildNodeModel(global::Newtonsoft.Json.Schema.JsonSchemaNode node)
		{
			if (_nodeModels.TryGetValue(node, out var value))
			{
				return value;
			}
			value = global::Newtonsoft.Json.Schema.JsonSchemaModel.Create(node.Schemas);
			_nodeModels[node] = value;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchemaNode> property in node.Properties)
			{
				if (value.Properties == null)
				{
					value.Properties = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Schema.JsonSchemaModel>();
				}
				value.Properties[property.Key] = BuildNodeModel(property.Value);
			}
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::Newtonsoft.Json.Schema.JsonSchemaNode> patternProperty in node.PatternProperties)
			{
				if (value.PatternProperties == null)
				{
					value.PatternProperties = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Schema.JsonSchemaModel>();
				}
				value.PatternProperties[patternProperty.Key] = BuildNodeModel(patternProperty.Value);
			}
			foreach (global::Newtonsoft.Json.Schema.JsonSchemaNode item in node.Items)
			{
				if (value.Items == null)
				{
					value.Items = new global::System.Collections.Generic.List<global::Newtonsoft.Json.Schema.JsonSchemaModel>();
				}
				value.Items.Add(BuildNodeModel(item));
			}
			if (node.AdditionalProperties != null)
			{
				value.AdditionalProperties = BuildNodeModel(node.AdditionalProperties);
			}
			if (node.AdditionalItems != null)
			{
				value.AdditionalItems = BuildNodeModel(node.AdditionalItems);
			}
			return value;
		}
	}
}
