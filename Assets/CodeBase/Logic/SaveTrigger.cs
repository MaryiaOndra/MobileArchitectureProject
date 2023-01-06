using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider collider;
        [SerializeField] private Color color;

        private ISaveLoadService saveLoadServise;

        private void Awake()
        {
            saveLoadServise = AllServices.Container.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            saveLoadServise.SaveProgress();
            Debug.Log("Progress saved!");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if(!collider) return;
            
            Gizmos.color = color;
            Gizmos.DrawCube(transform.position + collider.center, collider.size);
        }
    }
}