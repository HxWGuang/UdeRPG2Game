using UnityEngine;

namespace Hx.Skill
{
    public class SkillSwordThrowController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;

        private void Awake()
        {
            rb = GetComponentInChildren<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
        }

        public void Setup(Vector2 dir, float gravity)
        {
            rb.velocity = dir;
            rb.gravityScale = gravity;
            
            animator.SetBool("Spin", true);
        }
    }
}