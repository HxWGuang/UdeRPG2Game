using System;
using UnityEngine;

namespace Hx
{
    public class Entity : MonoBehaviour
    {
        
        [Header("Collision Info")] 
        [SerializeField] protected Transform groundCheckPos;
        [SerializeField] protected float groundCheckDis;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected Transform wallCheckPos;
        [SerializeField] protected float wallCheckDis;
        [SerializeField] protected LayerMask wallLayer;
        
        [Space]
        public int facingDir = 1;
        public bool facingRight = true;
        
        #region Component

        public Animator animator { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public AnimationEventListener animationEventListener { get; private set; }

        #endregion
        
        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            animationEventListener = GetComponent<AnimationEventListener>();
        }

        protected virtual void Start()
        {

        }


        protected virtual void Update()
        {

        }
        
        public virtual void SetVelocity(float xInput, float yInput)
        {
            rb.velocity = new Vector2(xInput, yInput);
            FlipController(xInput);
        }

        public virtual void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

        protected virtual void FlipController(float _x)
        {
            if (_x > 0 && !facingRight) Flip();
            else if (_x < 0 && facingRight) Flip();
        }

        protected virtual void Flip()
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
            Gizmos.color = Color.red;
            Gizmos.DrawRay(groundCheckPos.position, Vector2.down * groundCheckDis);
            Gizmos.DrawRay(wallCheckPos.position, Vector3.right * facingDir * wallCheckDis);
        }
    }
}
