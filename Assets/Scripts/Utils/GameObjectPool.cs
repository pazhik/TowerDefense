using System;
using System.Collections.Generic;
using RunTime;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Utils
{
    public class GameObjectPool: MonoBehaviour
    {
        private static GameObjectPool s_Instance;
        private static Dictionary<int, Queue<PooledMonoBehaviour>> S_PooledObject = new Dictionary<int, Queue<PooledMonoBehaviour>>();


        private static GameObjectPool Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = FindObjectOfType<GameObjectPool>();
                    if (s_Instance == null)
                    {
                        GameObject gameObject = new GameObject("GameObjectPool");
                        s_Instance = gameObject.AddComponent<GameObjectPool>();
                    }
                    s_Instance.gameObject.SetActive(false);
                }
                return s_Instance;
            }
        }

        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(prefab);
            instance.transform.parent = parent;
            return instance;
        }

        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab, Vector3 position,
                                                                Quaternion rotation, Transform parent)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(prefab);
            var transform1 = instance.transform;
            transform1.parent = parent;
            transform1.position = position;
            transform1.rotation = rotation;
            return instance;
        }

        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab, Vector3 position, Quaternion rotation)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(prefab);
            var transform1 = instance.transform;
            transform1.parent = null;
            transform1.position = position;
            transform1.rotation = rotation;
            return instance;
        }
        
        
        public static TMonoBehaviour InstantiatePooled<TMonoBehaviour>(TMonoBehaviour prefab)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            TMonoBehaviour instance = InstantiatePooledImpl(prefab);
            instance.transform.parent = null;
            return instance;
        }

        private static TMonoBehaviour InstantiatePooledImpl<TMonoBehaviour>(TMonoBehaviour prefab)
            where TMonoBehaviour : PooledMonoBehaviour
        {
            int id = prefab.GetInstanceID();
            TMonoBehaviour instance = null;

            if (S_PooledObject.TryGetValue(id, out Queue<PooledMonoBehaviour> queue))
            {
                if (queue.Count > 0)
                {
                    instance = queue.Peek() as TMonoBehaviour;
                    if (instance == null)
                    {
                        throw new NullReferenceException();
                    }
                }
            }

            if (instance == null)
            {
                instance = Instantiate(prefab);
                instance.SetPrefabId(id);
            }
            instance.AwakePooled();
            return instance;
        }

        public static void ReturnObjectPool(PooledMonoBehaviour instance)
        {
            int id = instance.PrefabId;

            if (S_PooledObject.TryGetValue(id, out Queue<PooledMonoBehaviour> queue))
            {
                queue.Enqueue(instance);
            }
            else
            {
                Queue<PooledMonoBehaviour> newQueue = new Queue<PooledMonoBehaviour>();
                newQueue.Enqueue(instance);
                S_PooledObject[id] = newQueue;
            }

            instance.transform.parent = Instance.transform;
        }
    }
}