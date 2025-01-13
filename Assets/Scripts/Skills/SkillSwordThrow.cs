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
    
    public class SkillSwordThrow : SkillBase
    {
        private SwordType _swordType = SwordType.Regular; 
        public SwordType swordType
        {
            get => _swordType;
            set
            {
                _swordType = value;
                switch (value)
                {
                    case SwordType.Regular:
                        if (config is not SkillSwordConfigRegular) config = Resources.Load<SkillSwordConfigRegular>("Config/SwordRegular");
                        break;
                    case SwordType.Bounce:
                        if (config is not SkillSwordConfigBounce) config = Resources.Load<SkillSwordConfigBounce>("Config/SwordBounce");
                        break;
                    case SwordType.Pierce:
                        if (config is not SkillSwordConfigPierce) config = Resources.Load<SkillSwordConfigPierce>("Config/SwordPierce");
                        break;
                    case SwordType.Spin:
                        if (config is not SkillSwordConfigSpin) config = Resources.Load<SkillSwordConfigSpin>("Config/SwordSpin");
                        break;
                }
            }
        }
        [SerializeField] private GameObject swordPrefab;
        public ObjectPool<GameObject> swordPool;
        
        
        // **** 待删除 START **** //
        // [Header("Sword Info")]
        // [SerializeField] private Vector2 throwSpeed;
        // [SerializeField] private float gravityScale;
        // [SerializeField] private float swordReturnSpeed = 12;
        // [SerializeField] public Vector2 swordReturnImpact;
        //
        // [Header("Bounce Info")] 
        // [SerializeField] private float bounceTimes = 4;
        // [SerializeField] private float bounceSpeed = 20;
        // **** 待删除 END **** //
        
        
        [Header("Aim Dot")]
        [SerializeField] private Transform dotParent;
        [SerializeField] private GameObject dotPrefab;
        [SerializeField] private int numOfDot;
        [SerializeField] private float aimDotSpace;
        private GameObject[] aimDots;
        public GameObject throwingSwrodGO { get; private set; }
        private Camera cam;
        
        public SkillSwordConfigBase config;

        private void Awake()
        {
            cam = Camera.main;
            swordPool = new ObjectPool<GameObject>(swordPrefab, 10, OnGetObj, OnReturnObj, OnDestroyObj);
            config = Resources.Load<SkillSwordConfigRegular>("Config/SwordRegular");
            
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
            
            // if (swordType == SwordType.Regular)
            // {
            //     if (config is not SkillSwordConfigRegular)
            //     {
            //         config = Resources.Load<SkillSwordConfigRegular>("Config/SwordRegular");
            //     }
            // } else if (swordType == SwordType.Bounce)
            // {
            //     if (config is not SkillSwordConfigBounce)
            //     {
            //         config = Resources.Load<SkillSwordConfigBounce>("Config/SwordBounce");
            //     }
            // } else if (swordType == SwordType.Pierce)
            // {
            //     if (config is not SkillSwordConfigPierce)
            //     {
            //         config = Resources.Load<SkillSwordConfigPierce>("Config/SwordPierce");
            //     }
            // }
            // else
            // {
            //     if (config is not SkillSwordConfigRegular)
            //     {
            //         config = Resources.Load<SkillSwordConfigRegular>("Config/SwordRegular");
            //     }
            // }

            sword.GetComponent<SkillSwordThrowController>().Setup(GetAimDir(), config);
            
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
            var deltaPos = new Vector2(dir.x * config.throwSpeed.x * t,
                dir.y * config.throwSpeed.y * t + .5f * Physics2D.gravity.y * config.gravityScale * t * t);
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