using UnityEngine;

namespace Hx
{
    [CreateAssetMenu(fileName = "SwordPierce", menuName = "Skill/Config/Pierce", order = 3)]

    public class SkillSwordConfigPierce : SkillSwordConfigBase
    {
        public int pierceAmount;
    }
}