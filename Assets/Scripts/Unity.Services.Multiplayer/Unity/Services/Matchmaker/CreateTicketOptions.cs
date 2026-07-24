namespace Unity.Services.Matchmaker
{
	public class CreateTicketOptions
	{
		public string QueueName { get; set; }

		public global::System.Collections.Generic.Dictionary<string, object> Attributes { get; set; }

		public CreateTicketOptions(string queueName = null, global::System.Collections.Generic.Dictionary<string, object> attributes = null)
		{
			QueueName = queueName;
			Attributes = attributes;
		}
	}
}
