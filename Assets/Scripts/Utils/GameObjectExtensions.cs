using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        // public static T GetComponentInParent<T>(this Component component) where T : Component
        // 从子级开始查找指定的Component，递归版本
        [CanBeNull]
        public static T GetComponentInChildDirectlyRecursion<T>(this GameObject parent, int maxDepth = 10) where T : Component
        {
            if (maxDepth < 1) return null;
            if (maxDepth > 50) maxDepth = 50;

            foreach (Transform child in parent.transform)
            {
                var res = child.GetComponent<T>();
                if (res != null) return res;

                if (maxDepth > 1)
                {
                    res = GetComponentInChildDirectlyRecursion<T>(child.gameObject, maxDepth - 1);
                    if (res != null) return res;
                }
            }

            return null;
        }

        // 从子级开始查找指定的Component，迭代版本
        [CanBeNull]
        public static T GetComponentInChildDirectly<T>(this GameObject parent, int maxDepth = 10) where T : Component
        {
            if (maxDepth < 1) return null;
            
            Queue<(Transform, int)> queue = new Queue<(Transform, int)>();
            queue.Enqueue((parent.transform, 0));   // 初始深度为 0

            while (queue.Count > 0)
            {
                var (current, depth) = queue.Dequeue();
                if (depth > maxDepth) continue; // 超过最大深度，跳过
                
                foreach (Transform child in current)
                {
                    var res = child.GetComponent<T>();
                    if (res != null) return res;
                    
                    queue.Enqueue((child, depth + 1));
                }
            }

            return null;
        }
    }
}