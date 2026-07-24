namespace Unity.Netcode.Components
{
	public interface IContactEventHandler
	{
		global::UnityEngine.Rigidbody GetRigidbody();

		void ContactEvent(ulong eventId, global::UnityEngine.Vector3 averagedCollisionNormal, global::UnityEngine.Rigidbody collidingBody, global::UnityEngine.Vector3 contactPoint, bool hasCollisionStay = false, global::UnityEngine.Vector3 averagedCollisionStayNormal = default(global::UnityEngine.Vector3));
	}
}
