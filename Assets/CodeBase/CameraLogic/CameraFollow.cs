using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float rotationAngleX;
        [SerializeField] private float distance;
        [SerializeField] private float offsetY;
        [SerializeField] private Transform following;

        private void LateUpdate()
        {
            if(following == null)
                return;
            
            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0f, 0f);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            transform.position = position;
            transform.rotation = rotation;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }

        public void Follow(GameObject following) => this.following = following.transform;
    }
}