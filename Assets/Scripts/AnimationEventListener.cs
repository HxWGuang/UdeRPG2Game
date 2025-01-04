using System;
using Hx.Utils;
using UnityEngine;

namespace Hx
{
    public class AnimationEventListener : MonoBehaviour
    {
        private AnimationEventDispatcher _dispatcher;

        private AnimationEventDispatcher dispatcher
        {
            get
            {
                if (!_dispatcher) _dispatcher = GetComponentInChildren<AnimationEventDispatcher>();
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