using UnityEngine;

namespace Hx
{
    [CreateAssetMenu(fileName = "SwordSpin", menuName = "Skill/Config/Spin", order = 4)]

    public class SkillSwordConfigSpin : SkillSwordConfigBase
    {
        public float maxFlyingDistance = 7;
        public float hitCooldown = 0.5f;
        public float spinDuration = 2f;
    }
}