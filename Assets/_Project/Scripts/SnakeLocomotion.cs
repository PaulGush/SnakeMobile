using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts
{
    public class SnakeLocomotion : MonoBehaviour
    {
        [SerializeField] private InputReader m_inputReader;

        private void Start()
        {
            m_inputReader?.EnablePlayerActions();
        }
    }
}
