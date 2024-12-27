using UnityEngine;
using Milen.UnityUIThings;

public class ShowMessageOnClick : MonoBehaviour
{
	[Tooltip("An object that provides a message to be displayed. Should implement the IMessageProvider interface.")]
	public GameObject MessageProvider;

	private IMessageProvider m_messageProvider;
	private readonly CooldownTracker m_cooldownTracker = new();
	private readonly AndroidNativeToast m_androidToast = new();

	void Start()
	{
		if (MessageProvider != null)
		{
			MessageProvider.TryGetComponent(out m_messageProvider);
		}

		if (m_messageProvider == null)
		{
			Debug.LogWarningFormat("{0} ShowMessageOnClick does not have a message provider assigned.", gameObject.name);
		}

		m_androidToast.Init();
	}

	private void OnDestroy()
	{
		m_androidToast.Dispose();
	}

#if UNITY_EDITOR
	void OnMouseDown()
	{
		if (m_messageProvider == null)
			return;

		if (m_cooldownTracker.IsInCooldown())
			return;

		m_androidToast.Show(m_messageProvider.Message);
		m_cooldownTracker.StoreLastEventTime();
	}
#endif

	void Update()
	{
		if (m_messageProvider == null)
			return;

		if (m_cooldownTracker.IsInCooldown())
			return;

		for (int i = 0; i < Input.touchCount; i++)
		{
			if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
			{
				Vector2 touchPositionInScreenCoords = Input.GetTouch(i).position;

				if (HitTestWith2DColliders(touchPositionInScreenCoords) ||
					HitTestWith3DColliders(touchPositionInScreenCoords))
				{
					m_androidToast.Show(m_messageProvider.Message);
					m_cooldownTracker.StoreLastEventTime();
				}
			}
		}
	}

	private bool HitTestWith2DColliders(Vector2 touchPositionInScreenCoords)
	{
		Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touchPositionInScreenCoords);

		RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

		if ((hit.collider != null) && ReferenceEquals(hit.collider.gameObject, gameObject))
		{			
			return true;
		}

		return false;
	}

	private bool HitTestWith3DColliders(Vector2 touchPositionInScreenCoords)
	{
		Ray ray = Camera.main.ScreenPointToRay(touchPositionInScreenCoords);

		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			if (ReferenceEquals(hit.collider.gameObject, this.gameObject))
			{
				return true;
			}
		}

		return false;
	}
}

