using Hx.Module;
using Hx.Utils;
using Skills.SkillControllers;
using UnityEngine;

namespace Hx.Skill
{
    public enum SwordType {
        Regular,
        Bounce,
        Pierce,
        Spin
    }

    public class SkillSwordConfigBase
    {
        public Vector2 dir;
        public Vector2 throwSpeed;
        public float gravityScale;
        public float returnSpeed;
    }

    public class SkillSwordConfig
    {
        public SkillSwordConfigBase baseCfg;
    }
    public class SkillSwordConfigRegular : SkillSwordConfig {}
    public sealed class SkillSwordConfigBounce : SkillSwordConfig
    {
        public float bounceTimes;
        public float bounceSpeed;
    }
    
    public class SkillSwordThrow : SkillBase
    {
        public SwordType swordType = SwordType.Regular;
        [SerializeField] private GameObject swordPrefab;
        public ObjectPool<GameObject> swordPool;
        
        [Header("Sword Info")]
        [SerializeField] private Vector2 throwSpeed;
        [SerializeField] private float gravityScale;
        [SerializeField] private float swordReturnSpeed = 12;
        [SerializeField] public Vector2 swordReturnImpact;

        [Header("Bounce Info")] 
        [SerializeField] private float bounceTimes = 4;
        [SerializeField] private float bounceSpeed = 20;
        
        [Header("Aim Dot")]
        [SerializeField] private Transform dotParent;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private int numOfDot;
        [SerializeField] private float aimDotSpace;
        private GameObject[] aimDots;
        public GameObject throwingSwrodGO { get; private set; }
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
            swordPool = new ObjectPool<GameObject>(swordPrefab, 10, OnGetObj, OnReturnObj, OnDestroyObj);
            
            GenerateDots();
        }

        protected override void Update()
        {
            base.Update();

            PositionDots();
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

            var baseCfg = new SkillSwordConfigBase()
            {
                dir = GetAimDir(),
                throwSpeed = throwSpeed,
                gravityScale = gravityScale,
                returnSpeed = swordReturnSpeed,
            };
            
            SkillSwordConfig cfg = null;
            if (swordType == SwordType.Regular)
            {
                cfg = new SkillSwordConfigRegular()
                {
                    baseCfg = baseCfg
                };
            } else if (swordType == SwordType.Bounce)
            {
                cfg = new SkillSwordConfigBounce()
                {
                    baseCfg = baseCfg,
                    bounceTimes = bounceTimes,
                    bounceSpeed = bounceSpeed
                };
            }
            else
            {
                cfg = new SkillSwordConfigRegular()
                {
                    baseCfg = baseCfg
                };
            }
            
            sword.GetComponent<SkillSwordThrowController>().Setup(cfg);
            
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

        #region  Aim Dots
        private void GenerateDots()
        {
            aimDots = new GameObject[numOfDot];
            for (int i = 0; i < numOfDot; i++)
            {
                var dot = Instantiate(dotPrefab, transform.position, transform.rotation, dotParent);
                aimDots[i] = dot;
                dot.SetActive(false);
            }
        }

        private void PositionDots()
        {
            for (int i = 0; i < aimDots.Length; i++)
            {
                var dot = aimDots[i];
                dot.transform.position = GetDotPosition(GetAimDir(), i * aimDotSpace);
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
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            return (mousePos - playerPos).normalized;
        }

        private Vector2 GetDotPosition(Vector2 dir, float t)
        {
            var playerPos = (Vector2)G.player.transform.position;
            var deltaPos = new Vector2(dir.x * throwSpeed.x * t,
                dir.y * throwSpeed.y * t + .5f * Physics2D.gravity.y * gravityScale * t * t);
            return playerPos + deltaPos;
        }
        #endregion

        #region ObjectPoolCallback
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
        #endregion
    }
}