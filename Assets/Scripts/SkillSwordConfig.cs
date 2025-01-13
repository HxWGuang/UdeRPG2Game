using System.Collections.Generic;
using UnityEngine;

namespace Hx
{
    [CreateAssetMenu(fileName = "SkillSwordConfig", menuName = "Skill/Config/Overall", order = 0)]
    public class SkillSwordConfigOverall : ScriptableObject
    {
        public List<SkillSwordConfigBase> configs = new List<SkillSwordConfigBase>();
    }
    
    public class SkillSwordConfigBase : ScriptableObject
    {
        public Vector2 throwSpeed;
        public float gravityScale;
        public float returnSpeed;
        public Vector2 returnImpact;
    }
    
    [CreateAssetMenu(fileName = "SwordRegular", menuName = "Skill/Config/Regular", order = 1)]
    public class SkillSwordConfigRegular : SkillSwordConfigBase
    {
    }

    [CreateAssetMenu(fileName = "SwordBounce", menuName = "Skill/Config/Bounce", order = 2)]
    public class SkillSwordConfigBounce : SkillSwordConfigBase
    {
        public int bounceAmount;
        public float bounceSpeed;
    }

    [CreateAssetMenu(fileName = "SwordPierce", menuName = "Skill/Config/Pierce", order = 3)]
    public class SkillSwordConfigPierce : SkillSwordConfigBase
    {
        public int pierceAmount;
    }

    [CreateAssetMenu(fileName = "SwordSpin", menuName = "Skill/Config/Spin", order = 4)]
    public class SkillSwordConfigSpin : SkillSwordConfigBase
    {
        
    }
}