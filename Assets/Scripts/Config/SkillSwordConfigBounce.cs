using UnityEngine;

namespace Hx
{
    [CreateAssetMenu(fileName = "SwordBounce", menuName = "Skill/Config/Bounce", order = 2)]

    public class SkillSwordConfigBounce : SkillSwordConfigBase
    {
        public int bounceAmount;
        public float bounceSpeed;
    }
}