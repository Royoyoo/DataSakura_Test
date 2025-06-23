using UnityEngine;

namespace Gameplay
{
    public class ZooSetup : MonoBehaviour
    {
        private const float SizeToUnits = 0.1f;

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform groundTransform;


        public void Initialize(float zooSize)
        {
            // Zoo aspect ratio defaults to 2:1 
            cameraTransform.localPosition = new Vector3(0f, zooSize / 2f, 0f);
            groundTransform.localScale = new Vector3(zooSize * SizeToUnits, 1f, zooSize * SizeToUnits / 2f);
        }
    }
}
