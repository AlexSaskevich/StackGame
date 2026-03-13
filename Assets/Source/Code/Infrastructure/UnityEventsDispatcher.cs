using System;
using UnityEngine;

namespace Source.Code.Infrastructure
{
    public class UnityEventsDispatcher : MonoBehaviour
    {
        public event Action UpdateEvent;
        public event Action FixedUpdateEvent;
        public event Action ApplicationQuitEvent;
        public event Action<bool> ApplicationFocusEvent;
        public event Action<bool> ApplicationPauseEvent;

        private void Update()
        {
            UpdateEvent?.Invoke();
        }

        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        private void OnApplicationQuit()
        {
            ApplicationQuitEvent?.Invoke();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocusEvent?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            ApplicationPauseEvent?.Invoke(pauseStatus);
        }
    }
}