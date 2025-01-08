using UnityEngine;

namespace Hx.Skill
{
    public class SkillBase : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;

        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        public virtual bool CheckCanUse()
        {
            if (cooldownTimer < 0)
            {
                return true;
            }

            return false;
        }

        public virtual void UseSkill()
        {
            // DoSomething
        }
    }
}