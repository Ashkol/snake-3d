namespace MobileGame.Snake
{
    using UnityEngine;
    using UnityEngine.Events;

    public class HeadCube : Cube
    {
        [SerializeField] private Sensor insideSensor;

        public override Color GetColor()
        {
            return GetComponent<MeshRenderer>().material.color;
        }

        public override void SetColor(ColorScheme scheme)
        {
            GetComponent<MeshRenderer>().material.color = scheme.snakeHeadColor;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pick Up"))
            {
                var pickup = other.GetComponent<Pickup>();
                pickup.PickUp();
            }
        }
    }
}