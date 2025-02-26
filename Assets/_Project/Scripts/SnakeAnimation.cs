using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts
{
    public class SnakeAnimation : MonoBehaviour
    {
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
    
        [Header("References")]
        [SerializeField] private InputReader m_inputReader;
        [SerializeField] private Animator m_animator;
    
        private void Start()
        {
            m_inputReader?.EnablePlayerActions();

            if (m_inputReader != null)
            {
                m_inputReader.Move += OnMove;
            }
        }
        private void OnMove(Vector2 newValue)
        {
            if (newValue == Vector2.zero) return;
            
            m_animator.SetFloat(Horizontal, newValue.x);
            m_animator.SetFloat(Vertical, newValue.y);
        }
    
    }
}
