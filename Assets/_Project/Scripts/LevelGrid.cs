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

        public void SpawnFood()
        {
            m_foodGridPosition = new Vector2Int(Random.Range(-Width, Width), Random.Range(-Height, Height));
            
            m_activeFoodObject = Instantiate(FoodPrefab, new Vector3(m_foodGridPosition.x, m_foodGridPosition.y, 0), Quaternion.identity);
        }
        
        public void SnakeMoved(Vector2Int snakeGridPosition)
        {
            if (snakeGridPosition.x > Width || snakeGridPosition.x < -Width || snakeGridPosition.y > Height || snakeGridPosition.y < -Height)
            {
                Debug.Log("Game Over");
            }

            if (snakeGridPosition == m_foodGridPosition)
            {
                Destroy(m_activeFoodObject);
                SpawnFood();
            }
        }
    }
}