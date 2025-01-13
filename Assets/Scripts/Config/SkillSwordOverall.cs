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
}