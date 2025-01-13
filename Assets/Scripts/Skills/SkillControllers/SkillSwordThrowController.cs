using System.Collections.Generic;
using Hx;
using Hx.Module;
using UnityEngine;

namespace Skills.SkillControllers
{
    public class SkillSwordThrowController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        private bool isUpdateSwordDir;
        private bool isSwordReturning;
        private bool isSwordSpinWhenMove;
        private float swordReturningSpeed;
        
        private float bounceAmount;
        private float bonceSpeed;
        private List<Transform> bounceTargets;
        private bool canBouncing;
        private int bounceIndex;

        private int pierceAmount;

        private void Awake()
        {
            rb = GetComponentInChildren<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            bounceTargets = new List<Transform>();
        }

        private void Update()
        {
            if (isUpdateSwordDir)
                gameObject.transform.right = rb.velocity;

            if (canBouncing && bounceTargets.Count > 0)
            {
                if (bounceIndex >= bounceTargets.Count) bounceIndex = 0;
                var target = bounceTargets[bounceIndex];
                transform.position = Vector2.MoveTowards(transform.position, target.position, bonceSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, target.position) < 0.1f)
                {
                    bounceIndex++;
                    bounceAmount--;
                    var enemy = target.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.DoDamage();
                        Debug.Log("Do Bounce Damage");
                    }
                    if (bounceAmount <= 0)
                    {
                        canBouncing = false;
                        bounceIndex = 0;
                        SwordGoBack();
                    }
                }
            }

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

        public void Setup(Vector2 dir, SkillSwordConfigBase cfg)
        {
            isUpdateSwordDir = true;
            isSwordReturning = false;
            canBouncing = false;
            
            rb.constraints = RigidbodyConstraints2D.None;
            rb.bodyType = RigidbodyType2D.Dynamic;
            
            rb.velocity = dir * cfg.throwSpeed;
            rb.gravityScale = cfg.gravityScale;
            swordReturningSpeed = cfg.returnSpeed;

            isSwordSpinWhenMove = true;
            
            if (cfg is SkillSwordConfigBounce bounceCfg)
            {
                canBouncing = true;
                bounceAmount = bounceCfg.bounceAmount;
                bonceSpeed = bounceCfg.bounceSpeed;
                isSwordSpinWhenMove = true;
            }

            if (cfg is SkillSwordConfigPierce pierceCfg)
            {
                pierceAmount = pierceCfg.pierceAmount;
                isSwordSpinWhenMove = false;
            }
            
            animator.SetBool("Spin", isSwordSpinWhenMove);
        }

        public void SwordGoBack()
        {
            isSwordReturning = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;
            animator.SetBool("Spin", isSwordSpinWhenMove);
            
            bounceTargets.Clear();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            // 在回来的途中碰到的东西不再触发
            if (isSwordReturning) return;

            if (pierceAmount > 0)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    var enemy = other.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.DoDamage();
                        Debug.Log("Do Piercing Damage");
                        pierceAmount--;
                    }
                }
            }

            if (canBouncing && bounceTargets.Count <= 0)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // 扫描附近的敌人
                    var res = Physics2D.OverlapCircleAll(transform.position, 10, LayerMask.GetMask("Enemy"));
                    foreach (var col in res)
                    {
                        if (col.GetComponent<Enemy>() != null)
                            bounceTargets.Add(col.transform);
                    }
                }
                if (bounceTargets.Count < 2) canBouncing = false;
            }
            
            StuckInto(other);
        }

        private void StuckInto(Collider2D col)
        {
            if (pierceAmount > 0 && col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                return;
            }
            
            isUpdateSwordDir = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (canBouncing) return;
            
            gameObject.transform.SetParent(col.transform);
            animator.SetBool("Spin", false);
        }
    }
}