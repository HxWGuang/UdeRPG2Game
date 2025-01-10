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

        public void Setup(Vector2 dir, float gravityScale)
        {
            rb.velocity = dir;
            rb.gravityScale = gravityScale;
            
            animator.SetBool("Spin", true);
        }
    }
}