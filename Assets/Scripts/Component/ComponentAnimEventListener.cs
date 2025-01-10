using System;
using Hx.Utils;
using UnityEngine;

namespace Hx.Component
{
    public class ComponentAnimEventListener : MonoBehaviour
    {
        private ComponentAnimEventDispatcher _dispatcher;

        private ComponentAnimEventDispatcher dispatcher
        {
            get
            {
                if (!_dispatcher) _dispatcher = GetComponentInChildren<ComponentAnimEventDispatcher>();
                return _dispatcher;
            }
        }

        public void RegisterAnimationCb(string eventName, Action callback)
        {
            if (dispatcher.NormalEventDict.TryGetValue(eventName, out var cb))
                LogUtils.LogErrorFormat("{0}: Animation Event {1} already registered", gameObject.name, eventName);
            else
                dispatcher.NormalEventDict.Add(eventName, callback);
        }
    }
}