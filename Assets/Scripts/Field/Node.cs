using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Field
{
    public enum OccupationAvailability
    {
        CanOccupy,
        CanNotOccupy,
        Undefined
    }
    public class Node
    {
        public Vector3 Position;
        public OccupationAvailability m_OccupationAvailability = OccupationAvailability.Undefined;
        
        public Node NextNode;
        public bool IsOccupied;

        public float PathWeight;
        public List<EnemyData> EnemyDatas = new List<EnemyData>();

        public Node(Vector3 position)
        {
            Position = position;
        }

        public void ResetWeight()
        {
            PathWeight = float.MaxValue;
        }
    }
}