using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    public class AgentMoveToPlayer : MonoBehaviour
    {
        private const float MinimalDistamce = 1;
        
        [SerializeField] private NavMeshAgent agent;
        private Transform heroTransform;
        private IGameFactory gameFactory;

        private void Start()
        {
            gameFactory = AllServices.Container.Single<IGameFactory>();
            if (gameFactory.HeroGameObject != null)
                InitializeHeroTransform();
            else
                gameFactory.HeroCreated += HeroCreated;
        }

        private void Update()
        {
            if(Initialized() && HeroNotReached())
                agent.destination = heroTransform.position;
        }

        private bool Initialized() => heroTransform != null;

        private bool HeroNotReached() =>
            Vector3.Distance(agent.transform.position, heroTransform.position) >= MinimalDistamce;

        private void HeroCreated() =>
            InitializeHeroTransform();

        private void InitializeHeroTransform() =>
            heroTransform = gameFactory.HeroGameObject.transform;
    }
}