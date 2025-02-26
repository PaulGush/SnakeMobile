using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "LevelGrid", menuName = "Level/LevelGrid")]
    public class LevelGrid : ScriptableObject
    {
        public int Width;
        public int Height;

        public GameObject FoodPrefab;

        private GameObject m_activeFoodObject;
        private Vector2Int m_foodGridPosition;
        
        public event System.Action OnFoodEaten;

        public void SpawnFood(List<Vector2Int> snakeGridPositions)
        {
            do
            {
                m_foodGridPosition = new Vector2Int(Random.Range(-Width, Width), Random.Range(-Height, Height));
            } 
            while (snakeGridPositions.Contains(m_foodGridPosition));
            
            m_activeFoodObject = Instantiate(FoodPrefab, new Vector3(m_foodGridPosition.x, m_foodGridPosition.y, 0), Quaternion.identity);
        }
        
        public void SnakeMoved(List<Vector2Int> snakeGridPositions)
        {
            if (snakeGridPositions[0] != m_foodGridPosition) return;
            
            Destroy(m_activeFoodObject);
            SpawnFood(snakeGridPositions);
            OnFoodEaten?.Invoke();
        }
        
        public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
        {
            int x = gridPosition.x;
            int y = gridPosition.y;
            
            if (x < -Width) x = Width - 1;
            if (x > Width) x = -Width;
            if (y < -Height) y = Height - 1;
            if (y > Height) y = -Height;
            
            return new Vector2Int(x, y);
        }
    }
}