using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        [SerializeField] private float speed;
        
        private IGameFactory gameFactory;
        private Transform heroTransform;
        private Vector3 positionToLook;

        private void Start()
        {
            gameFactory = AllServices.Container.Single<IGameFactory>();
            if (HeroExist())
                InitializeHeroTransform();
            else
                gameFactory.HeroCreated += InitializeHeroTransform;
        }

        private bool HeroExist() => gameFactory.HeroGameObject != null;
        private void InitializeHeroTransform() =>
            heroTransform = gameFactory.HeroGameObject.transform;

        private void Update()
        {
            if (Initialized())
                RotateTorwardsHero();
        }

        private bool Initialized() => heroTransform != null;

        private void RotateTorwardsHero()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, positionToLook);
        }

        private Quaternion SmoothedRotation(Quaternion transformRotation, Vector3 vector3) =>
            Quaternion.Lerp(transformRotation, TargetRotation(positionToLook), SpeedFactor());
        private Quaternion TargetRotation(Vector3 position) => Quaternion.LookRotation(position);
        private float SpeedFactor() => speed * Time.deltaTime;

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = heroTransform.position - transform.position;
            positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        }
    }
}