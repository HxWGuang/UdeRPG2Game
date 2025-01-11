using Hx.Module;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillSwordThrowController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        private bool isUpdateSwordDir = false;
        private bool isSwordReturning = false;
        private float swordReturningSpeed;

        private void Awake()
        {
            rb = GetComponentInChildren<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (isUpdateSwordDir)
                gameObject.transform.right = rb.velocity;

            if (isSwordReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, G.player.transform.position, swordReturningSpeed * Time.deltaTime);
                if (Vector2.Distance(G.player.transform.position, transform.position) < 1.5f)
                {
                    G.player.isSwordThrowing = false;
                    transform.SetParent(null);
                    G.player.stateMachine.ChangeState(G.player.swordCatchState);
                    G.player.compSkillMgr.swordThrow.swordPool.Return(gameObject);
                }
            }
        }

        public void Setup(Vector2 dir, float gravityScale, float returningSpeed)
        {
            isUpdateSwordDir = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.bodyType = RigidbodyType2D.Dynamic;
            
            rb.velocity = dir;
            rb.gravityScale = gravityScale;
            swordReturningSpeed = returningSpeed;
            isSwordReturning = false;
            
            animator.SetBool("Spin", true);
        }

        public void SwordGoBack()
        {
            isSwordReturning = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;
            animator.SetBool("Spin", true);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            // 在回来的途中碰到的东西不再触发
            if (isSwordReturning) return;
            
            isUpdateSwordDir = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.SetParent(other.transform);

            animator.SetBool("Spin", false);
        }
    }
}