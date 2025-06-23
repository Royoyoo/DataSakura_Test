using UnityEngine;

namespace Gameplay.FoodChain
{
    public class AnimalCollisionReporter : MonoBehaviour
    {
        private Rigidbody animalRigidbody;
        private LayerMask animalLayerMask;
        private AnimalCollisionResolver animalCollisionResolver;


        public void Initialize(Rigidbody rigidbody, LayerMask animalLayerMask, AnimalCollisionResolver animalCollisionResolver)
        {
            animalRigidbody = rigidbody;
            this.animalLayerMask = animalLayerMask;
            this.animalCollisionResolver = animalCollisionResolver;
        }


        private bool IsInAnimalLayer(GameObject gameObject)
        {
            return (animalLayerMask & (1 << gameObject.layer)) != 0;
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (!IsInAnimalLayer(this.gameObject) || !IsInAnimalLayer(collision.gameObject))
            {
                return;
            }

            animalCollisionResolver.HandleAnimalCollision(animalRigidbody, collision.rigidbody);
        }
    }
}
