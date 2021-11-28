namespace MobileGame.Snake
{
    using UnityEngine;

    public class Sensor : MonoBehaviour
    {
        public bool IsCollidingBoard { get; private set; }
        public bool IsCollidingBody { get; private set; }
        public bool IsCollidingPickUp { get; private set; }
        public Collider CollidedObject { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            CollidedObject = other;
            if (other.CompareTag("Snake"))
            {
                //if (!other.GetComponent<BodyCube>().IsTail)
                    IsCollidingBody = true;
            }
            else if (other.CompareTag("Pick Up"))
            {
                IsCollidingPickUp = true;
            }
            else
            {
                IsCollidingBoard = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CollidedObject = null;
            IsCollidingBody = false;
            IsCollidingBoard = false;
            IsCollidingPickUp = false;
        }

    }
}