using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hx
{
    public class ComponentAnimEventDispatcher : MonoBehaviour
    {
        public Dictionary<string, Action> NormalEventDict = new Dictionary<string, Action>();

        public void NormalEvent(string eventName)
        {
            NormalEventDict.TryGetValue(eventName, out var cb);
            cb?.Invoke();
        }
    }
}