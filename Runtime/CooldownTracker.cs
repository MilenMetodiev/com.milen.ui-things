using UnityEngine;

namespace Milen.UnityUIThings
{
	class CooldownTracker
	{
		private float? m_lastEventTime;
		private float m_cooldownDuration = 2.0f;

		public bool IsInCooldown()
		{
			if (m_lastEventTime.HasValue &&
				(Time.time < m_lastEventTime.Value + m_cooldownDuration))
			{
				return true;
			}

			return false;
		}

		public void StoreLastEventTime()
		{
			m_lastEventTime = Time.time;
		}
	}
}