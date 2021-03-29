using System;
using UnityEngine;

namespace Field
{
    public class MovementCursor : MonoBehaviour
    {
        [SerializeField]
        private int m_GridWidth;
        [SerializeField]
        private int m_GridHeight;

        [SerializeField]
        private float m_NodeSize;
        
        private Camera m_Camera;

        private Vector3 m_Offset;
        
        // [SerializeField]
        // private MovementAgent m_MovementAgent;
        
        [SerializeField]
        private GameObject m_Cursor;

        
        private void OnValidate()
        {
            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;

            // Default plane size is 10 by 10
            transform.localScale = new Vector3(
                width * 0.1f, 
                1f,
                height * 0.1f);

            m_Offset = transform.position -
                       (new Vector3(width, 0f, height) * 0.5f);
        }
        
        private void Awake()
        {
            m_Camera = Camera.main;

            // Default plane size is 10 by 10
            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;

            transform.localScale = new Vector3(
                width * 0.1f, 
                1f,
                height * 0.1f);

            m_Offset = transform.position -
                       (new Vector3(width, 0f, height) * 0.5f);
        }

        private void Update()
        {

            Vector3 mousePosition = Input.mousePosition;

            Ray ray = m_Camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform != transform)
                {
                    return;
                }

                if (m_Cursor.activeSelf == false)
                {
                    m_Cursor.SetActive(true);
                }


                Vector3 hitPosition = hit.point;
                Vector3 difference = hitPosition - m_Offset;

                int x = (int) (difference.x / m_NodeSize);
                int z = (int) (difference.z / m_NodeSize);

                Debug.Log(x + " " + z);
                
                m_Cursor.transform.position = new Vector3(m_Offset.x + x * m_NodeSize + m_NodeSize / 2, 0.2f,
                    m_Offset.z + z * m_NodeSize + m_NodeSize / 2);

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 target = new Vector3();
                    target.x = m_Offset.x + x * m_NodeSize + m_NodeSize / 2;
                    // target.y = m_MovementAgent.transform.position.y;
                    target.z = m_Offset.z + z * m_NodeSize + m_NodeSize / 2;
                    // m_MovementAgent.SetTarget(target);
                }
                
            } else {
                m_Cursor.SetActive(false);
            }
            
            // if (m_Cursor.transform.position.x - m_MovementAgent.transform.position.x < 0.0001f 
            //     && m_Cursor.transform.position.z - m_MovementAgent.transform.position.z < 0.0001f)
            // {
            //     m_Cursor.SetActive(false);
            // }
            // else if (m_Cursor.activeSelf == false)
            // {
            //     m_Cursor.SetActive(true);
            // }
            
        }

        private void OnDrawGizmos()
        {
            float width = m_GridWidth * m_NodeSize;
            float height = m_GridHeight * m_NodeSize;
            Gizmos.color = Color.blue;
            for (int i = 0; i < m_GridWidth; i++)
            {
                Gizmos.DrawLine(m_Offset + new Vector3(i * m_NodeSize, 0f, 0f),
                    m_Offset + new Vector3(i * m_NodeSize, 0f, height));
            }
            for (int j = 0; j <= m_GridHeight; j++)
            {
                Gizmos.DrawLine(m_Offset + new Vector3(0f, 0f, j * m_NodeSize),
                    m_Offset + new Vector3(width, 0f, j * m_NodeSize));
            }
        }
    }
}