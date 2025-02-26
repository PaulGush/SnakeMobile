using _Project.Scripts.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class SnakeLocomotion : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader m_inputReader;
        [InlineEditor, SerializeField] private LevelGrid m_levelGrid;
        
        [Header("Settings")]
        [SerializeField] private float m_moveDelay = 1f;
        
        private float m_nextMoveTime = 0f;

        private Vector2Int m_gridPosition;
        private Vector2Int m_direction;

        private void Awake()
        {
            m_gridPosition = new Vector2Int(0, 0);
        }

        private void Start()
        {
            m_inputReader?.EnablePlayerActions();
            
            if (m_inputReader != null)
            {
                m_inputReader.Move += OnMove;
            }
            
            m_direction = Vector2Int.right;
            
            transform.position = new Vector3(m_gridPosition.x, m_gridPosition.y, 0);
            m_levelGrid.SpawnFood();
        }
        private void OnMove(Vector2 newValue)
        {
            if (newValue == Vector2.zero) return;
            
            m_direction = new Vector2Int((int)newValue.x, (int)newValue.y);
        }

        private void Update()
        {
            Move();
        }
        
        private void Move()
        {
            if (Time.time < m_nextMoveTime) return;

            m_gridPosition += m_direction;
            
            this.transform.position = new Vector3(m_gridPosition.x, m_gridPosition.y, 0);
            m_levelGrid.SnakeMoved(m_gridPosition);
            
            m_nextMoveTime = Time.time + m_moveDelay;
        }
    }
}
