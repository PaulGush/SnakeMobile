using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private LevelGrid m_levelGrid;
        [SerializeField] private TextMeshProUGUI m_scoreText;
        private int m_score = 0;
        
        private void Start()
        {
            m_levelGrid.OnFoodEaten += UpdateScore;
        }
        private void UpdateScore()
        {
            m_score++;
            m_scoreText.text = $"Score: {m_score}"; 
        }
    }
}