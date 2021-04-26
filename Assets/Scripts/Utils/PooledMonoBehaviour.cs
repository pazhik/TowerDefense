using UnityEngine;

namespace Utils
{
    public class PooledMonoBehaviour: MonoBehaviour
    {
        private int m_PrefabId;

        public int PrefabId => m_PrefabId;

        public void SetPrefabId(int id)
        {
            m_PrefabId = id;
        }
        
        public virtual void AwakePooled(){}
    }
}