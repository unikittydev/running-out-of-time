using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CameraBlender : MonoBehaviour
    {
        [System.Serializable]
        public class BlenderSettings
        {
            public Transform anchorFrom;
            public Transform anchorTo;
            public CinemachineVirtualCamera camFrom;
            public CinemachineVirtualCamera camTo;
        }

        [SerializeField]
        private CinemachineBrain brain;

        [SerializeField]
        private CinemachineMixingCamera mixer;

        [SerializeField]
        private BlenderSettings[] blends;

        [SerializeField]
        private Transform target;

        private void LateUpdate()
        {
            foreach (var blend in blends)
            {
                if (Between(target, blend.anchorFrom, blend.anchorTo))
                {
                }
            }
        }

        private bool Between(Transform target, Transform from, Transform to)
        {
            return from.position.x > target.position.x && target.position.x < to.position.x;
        }
    }
}
