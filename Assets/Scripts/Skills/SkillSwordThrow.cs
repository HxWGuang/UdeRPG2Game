using Hx.Module;
using Hx.Utils;
using UnityEngine;

namespace Hx.Skill
{
    public class SkillSwordThrow : SkillBase
    {
        [SerializeField] private GameObject swordPrefab;
        public ObjectPool<GameObject> swordPool;
        
        [Header("Sword Info")]
        [SerializeField] private Vector2 throwSpeed;
        [SerializeField] private float gravityScale;
        [SerializeField] private float swordReturnSpeed = 12;
        [SerializeField] public Vector2 swordReturnImpact;
        
        [Header("Aim Dot")]
        [SerializeField] private Transform dotParent;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private int numOfDot;
        [SerializeField] private float aimDotSpace;
        private GameObject[] aimDots;
        public GameObject throwingSwrodGO { get; private set; }

        private void Awake()
        {
            swordPool = new ObjectPool<GameObject>(swordPrefab, 10, OnGetObj, OnReturnObj, OnDestroyObj);
            
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
            if (!base.CheckCanUse()) return false;
            if (G.player.isSwordThrowing) return false;
            return true;
        }

        public override void UseSkill()
        {
            base.UseSkill();

            var sword = throwingSwrodGO;
            if (sword == null)
            {
                sword = swordPool.Get();
                throwingSwrodGO = sword;
            }
            sword.transform.position = G.player.transform.position;
            sword.transform.rotation = G.player.transform.rotation;
            sword.GetComponent<SkillSwordThrowController>().Setup(GetAimDir() * throwSpeed, gravityScale, swordReturnSpeed);
            
            G.player.isSwordThrowing = true;
        }

        public void CallSwordBack()
        {
            if (throwingSwrodGO != null)
            {
                throwingSwrodGO.GetComponent<SkillSwordThrowController>().SwordGoBack();
            } else
            {
                Debug.LogError("swordThrowing is null");
            }
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
            throwingSwrodGO = null;
        }

        private void OnDestroyObj(GameObject obj)
        {
            Destroy(obj);
            throwingSwrodGO = null;
        }
    }
}