using System.Collections;
using Hx.Component;
using UnityEngine;

namespace Hx
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Knock back Info")] 
        [SerializeField] private Vector2 knockbackForce;
        [SerializeField] private float knockbackDuration;
        private bool isKnocked = false;
        
        [Header("Collision Info")] 
        [SerializeField] protected Transform groundCheckPos;
        [SerializeField] protected float groundCheckDis;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected Transform wallCheckPos;
        [SerializeField] protected float wallCheckDis;
        [SerializeField] protected LayerMask wallLayer;
        public Transform attackCheckPoint;
        public float attackCheckRadius;
        
        [Space]
        public int facingDir = 1;
        public bool facingRight = true;
        
        #region Component

        public Animator animator { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public ComponentAnimEventListener compAnimEventListener { get; private set; }
        public ComponentEntityFx compFx { get; private set; }

        #endregion
        
        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            compAnimEventListener = GetComponent<ComponentAnimEventListener>();
            compFx = GetComponent<ComponentEntityFx>();
        }

        protected virtual void Start()
        {

        }


        protected virtual void Update()
        {

        }

        public virtual void DoDamage()
        {
            compFx.PlayFlashFx();
            StartCoroutine(nameof(DoKnockback));
        }

        private IEnumerator DoKnockback()
        {
            isKnocked = true;
            rb.velocity = new Vector2(knockbackForce.x * -facingDir, knockbackForce.y);
            yield return new WaitForSeconds(knockbackDuration);
            isKnocked = false;
        }
        
        public virtual void SetVelocity(float xInput, float yInput)
        {
            if (isKnocked) return;
            rb.velocity = new Vector2(xInput, yInput);
            FlipController(xInput);
        }

        public virtual void SetZeroVelocity()
        {
            if (isKnocked) return;
            rb.velocity = new Vector2(0, 0);
        }

        protected virtual void FlipController(float _x)
        {
            if (_x > 0 && !facingRight) Flip();
            else if (_x < 0 && facingRight) Flip();
        }

        public virtual void Flip()
        {
            facingDir *= -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
        
        public virtual bool GroundedCheck()
        {
            return Physics2D.Raycast(groundCheckPos.position, Vector2.down, groundCheckDis, groundLayer);
        }

        public virtual bool WallCheck()
        {
            return Physics2D.Raycast(wallCheckPos.position, Vector3.right * facingDir, wallCheckDis, wallLayer);
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawRay(groundCheckPos.position, Vector2.down * groundCheckDis);
            Gizmos.DrawRay(wallCheckPos.position, Vector3.right * facingDir * wallCheckDis);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackCheckPoint.position, attackCheckRadius);
        }
    }
}
