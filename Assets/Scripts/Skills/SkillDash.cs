namespace Hx.Skill
{
    public class SkillDash : SkillBase
    {
        public override void UseSkill()
        {
            base.UseSkill();
            
            cooldownTimer = cooldown;
        }
    }
}