using Hx.EnemyStateMachine;
using UnityEngine;


namespace Hx
{
    public class Enemy : MonoBehaviour
    {
        public Animator animator { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public AnimationEventListener animationEventListener { get; private set; }

        public StateMachine stateMachine { get; private set; }

        private void Awake()
        {
            stateMachine = new StateMachine();
        }

        void Start()
        {
            // stateMachine.Init();
        }

        
        void Update()
        {

        }
    }
}
