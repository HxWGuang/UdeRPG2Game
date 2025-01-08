using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hx.Utils
{
    public class ObjectPool<T> where T : Object
    {
        private Queue<T> pool; // 对象池队列
        private T prefab; // 预制体
        private int maxSize; // 对象池最大容量
        private Action<T> onGet; // 获取对象时回调
        private Action<T> onReturn; // 回收对象时回调
        private Action<T> onDestroy; // 销毁对象时回调
        
        /// 初始化对象池
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <param name="maxSize">对象池最大容量</param>
        /// <param name="onGet"></param>
        /// <param name="onReturn"></param>
        /// <param name="onDestroy"></param>
        public ObjectPool(T prefab, int maxSize = 100, Action<T> onGet = null, Action<T> onReturn = null, Action<T> onDestroy = null)
        {
            this.prefab = prefab;
            this.maxSize = maxSize;
            pool = new Queue<T>();
            this.onGet = onGet;
            this.onReturn = onReturn;
            this.onDestroy = onDestroy;
        }

        /// <summary>
        /// 从对象池中获取一个对象
        /// </summary>
        /// <returns>对象</returns>
        public T Get()
        {
            T obj;
            if (pool.Count > 0)
            {
                obj = pool.Dequeue(); // 从队列中取出对象
                // obj.SetActive(true); // 激活对象
            }
            else
            {
                // 如果对象池为空，则实例化新的对象
                obj = GameObject.Instantiate(prefab);
            }
            onGet?.Invoke(obj);
            return obj;
        }

        /// <summary>
        /// 将对象回收到对象池
        /// </summary>
        /// <param name="obj">要回收的对象</param>
        public void Return(T obj)
        {
            if (pool.Count < maxSize)
            {
                // obj.gameObject.SetActive(false); // 禁用对象
                onReturn?.Invoke(obj);
                pool.Enqueue(obj); // 将对象加入队列
            }
            else
            {
                // 如果对象池已满，则销毁对象
                // UnityEngine.GameObject.Destroy(obj.gameObject);
                onDestroy?.Invoke(obj);
            }
        }
    }
}