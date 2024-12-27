using System;
using UnityEngine;


namespace Milen.UnityUIThings
{
	class AndroidNativeToast : IDisposable
	{
		AndroidJavaClass m_toastClass;
		AndroidJavaObject m_activity;

		public void Dispose()
		{
			m_activity?.Dispose();
			m_toastClass?.Dispose();
		}

		public void Init()
		{
#if UNITY_ANDROID
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				if (unityPlayer != null)
				{
					m_activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				}
			}

			m_toastClass = new AndroidJavaClass("android.widget.Toast");
#endif
		}

		public void Show(string message)
		{
			if (string.IsNullOrWhiteSpace(message))
				return;

#if UNITY_EDITOR
			Debug.Log("Showing toast message: " + message);

#elif UNITY_ANDROID
			m_activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			{
				using (AndroidJavaObject toastObject = m_toastClass.CallStatic<AndroidJavaObject>("makeText", m_activity, message, 0))
				{
					toastObject.Call("show");
				}
			}));
#endif
		}
	}
}