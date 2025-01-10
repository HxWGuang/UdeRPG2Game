using Hx.Component;
using Hx.Module;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillCloneController : MonoBehaviour
    {
        [SerializeField] private float transitionDuration = 1f;
        [SerializeField] private Color targetColor = Color.clear;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange;
        
        private Color originColor;
        private Animator animator;
        private ComponentAnimEventListener animEventListener;

        [Space] 
        [SerializeField] private bool canAttack;    // 临时变量，后续需要判断是否学习了虚影攻击技能
        
        private SpriteRenderer _spriteRenderer;
        private float transitionTimer;
        private bool attackEnded = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            // animator.keepAnimatorStateOnDisable = true;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            originColor = _spriteRenderer.color;
            animEventListener = GetComponent<ComponentAnimEventListener>();
            animEventListener.RegisterAnimationCb("AttackCheck", OnAttackCheck);
            animEventListener.RegisterAnimationCb("PrimaryAttackEnd", OnAttackEnd);
        }

        private void OnEnable()
        {
            transitionTimer = transitionDuration;
            attackEnded = false;
            _spriteRenderer.color = originColor;
        }

        public void Setup(/*bool _canAttack*/)
        {
            /*
            canAttack = _canAttack;
            */
            
            transform.position = G.player.transform.position;
            transform.rotation = G.player.transform.rotation;

            if (canAttack)
            {
                // 朝向最近的敌人
                var cols = Physics2D.OverlapCircleAll(transform.position, 20, LayerMask.GetMask("Enemy"));
                float minDist = Mathf.Infinity;
                Transform closestEnemy = null;
                foreach (var col in cols)
                {
                    var enemy = col.transform;
                    var dist = Vector2.Distance(transform.position, enemy.position);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        closestEnemy = enemy;
                    }
                }

                // if (closestEnemy != null)
                // {
                //     if (transform.position.x > closestEnemy.position.x)
                //     {
                //         transform.Rotate(0,180,0);
                //     }
                // }    // transform.LookAt(closestEnemy);
                
                // 随机播放一个攻击
                var randomAttack = Random.Range(1, 4);
                Debug.Log("Random Attack: " + randomAttack);
                animator.SetInteger("AttackNum", randomAttack);
            }
        }

        private void Update()
        {
            if (attackEnded)
            {
                // 在transitionDuration时间内完成颜色到透明的插值
                if (transitionTimer > 0)
                {
                    transitionTimer -= Time.deltaTime;
                    var t = 1 - transitionTimer / transitionDuration;
                    _spriteRenderer.color = Color.Lerp(Color.white, targetColor, t);
                }
                else
                {
                    G.player.compSkillMgr.clone.objectPool.Return(gameObject);
                }
            }
        }
        
        private void OnAttackCheck()
        {
            var colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                var enemyScript = collider.GetComponent<Enemy>();
                enemyScript.DoDamage();
            }
        }

        private void OnAttackEnd()
        {
            Debug.Log("Attack end");
            attackEnded = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}