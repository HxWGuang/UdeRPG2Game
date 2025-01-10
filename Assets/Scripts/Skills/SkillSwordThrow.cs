using Hx.Module;
using Hx.Utils;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillSwordThrow : SkillBase
    {
        [SerializeField] private GameObject swordPrefab;
        private ObjectPool<GameObject> swordPool;

        [Header("Aim Dot")]
        [SerializeField] private Transform dotParent;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private Vector2 throwSpeed;
        [SerializeField] private float gravityScale;
        [SerializeField] private int numOfDot;
        [SerializeField] private float aimDotSpace;
        private GameObject[] aimDots;

        private void Awake()
        {
            swordPool = new ObjectPool<GameObject>(swordPrefab, 10);
            
            aimDots = new GameObject[numOfDot];
            for (int i = 0; i < numOfDot; i++)
            {
                var dot = Instantiate(dotPrefab, transform.position, transform.rotation, dotParent);
                aimDots[i] = dot;
                dot.SetActive(false);
            }
        }
        
        protected override void Update()
        {
            base.Update();

            for (int i = 0; i < aimDots.Length; i++)
            {
                var dot = aimDots[i];
                dot.transform.position = GetDotPosition(GetAimDir(), i * aimDotSpace);
            }
        }

        public override bool CheckCanUse()
        {
            return base.CheckCanUse();
        }

        public override void UseSkill()
        {
            base.UseSkill();
            
            var sword = swordPool.Get();
            sword.transform.position = G.player.transform.position;
            sword.transform.rotation = G.player.transform.rotation;
            sword.GetComponent<SkillSwordThrowController>().Setup(GetAimDir() * throwSpeed, gravityScale);
        }

        public void SetDotActive(bool show)
        {
            for (int i = 0; i < aimDots.Length; i++)
            {
                aimDots[i].SetActive(show);
            }
        }

        private Vector2 GetAimDir()
        {
            var playerPos = G.player.transform.position;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (mousePos - playerPos).normalized;
        }

        private Vector2 GetDotPosition(Vector2 dir, float t)
        {
            var playerPos = (Vector2)G.player.transform.position;
            var deltaPos = new Vector2(dir.x * throwSpeed.x * t,
                dir.y * throwSpeed.y * t + .5f * Physics2D.gravity.y * gravityScale * t * t);
            return playerPos + deltaPos;
        }

        private void OnGetObj(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReturnObj(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void OnDestroyObj(GameObject obj)
        {
            Destroy(obj);
        }
    }
}