using System.Collections.Generic;
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
        [SerializeField] private GameObject m_snakeBodyPrefab;
        
        [Header("Settings")]
        [SerializeField] private float m_moveDelay = 1f;

        [Header("Debug Values")]
        [SerializeField] private bool m_isAlive = true;
        [SerializeField] private int m_snakeBodySize = 0;
        [SerializeField] private List<Vector2Int> m_snakeBody = new List<Vector2Int>();
        [SerializeField] private Vector2Int m_gridPosition;
        [SerializeField] private Vector2Int m_direction;
        [SerializeField] private float m_nextMoveTime = 0f;
        
        private readonly List<Vector2Int> m_invalidSpawnLocations = new List<Vector2Int>();
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
            
            m_invalidSpawnLocations.Add(m_gridPosition);
            foreach (var bodyPart in m_snakeBody)
            {
                m_invalidSpawnLocations.Add(bodyPart);
            }
            
            m_levelGrid.SpawnFood(m_invalidSpawnLocations);
            
            m_levelGrid.OnFoodEaten += () => m_snakeBodySize++;
        }
        private void OnMove(Vector2 newValue)
        {
            if (newValue == Vector2.zero) return;
            
            m_direction = new Vector2Int((int)newValue.x, (int)newValue.y);
        }

        private void Update()
        {
            if (!m_isAlive) return;
            Move();
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void Move()
        {
            if (Time.time < m_nextMoveTime) return;
            
            m_snakeBody.Insert(0, m_gridPosition);
            
            m_gridPosition += m_direction;
            
            m_gridPosition = m_levelGrid.ValidateGridPosition(m_gridPosition);
            
            if (m_snakeBody.Count >= m_snakeBodySize + 1)
            {
                m_snakeBody.RemoveAt(m_snakeBody.Count - 1);
            }
            
            for (var index = 0; index < m_snakeBody.Count; index++)
            {
                var bodyPartPosition = m_snakeBody[index];
                var bodyPartObject = Instantiate(m_snakeBodyPrefab, new Vector3(bodyPartPosition.x, bodyPartPosition.y, 0), Quaternion.identity);

                Destroy(bodyPartObject, m_moveDelay);

                if (m_gridPosition == bodyPartPosition)
                {
                    Debug.Log("Game Over");
                    m_isAlive = false;
                }
            }
            

            m_invalidSpawnLocations.Clear();
            m_invalidSpawnLocations.Add(m_gridPosition);
            foreach (var bodyPart in m_snakeBody)
            {
                m_invalidSpawnLocations.Add(bodyPart);
            }
            
            m_levelGrid.SnakeMoved(m_invalidSpawnLocations);
            
            this.transform.position = new Vector3(m_gridPosition.x, m_gridPosition.y, 0);
            
            m_nextMoveTime = Time.time + m_moveDelay;
        }
        
        public Vector2Int GetGridPosition() => m_gridPosition;
    }
}
