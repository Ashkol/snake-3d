namespace MobileGame.Snake
{
    using System;
    using UnityEngine;
    using UnityEngine.Assertions;
    using UnityEngine.Events;

    [RequireComponent(typeof(AudioSource))]
    public class Pickup : Cube, IColorable
    {
        //public Event OnPickUp;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private int points = 1;
        private Rigidbody rgbody;
        private AudioSource audioSource;

        public delegate void PickUpHandler(int i);
        public event PickUpHandler OnPickUp;
        
        public enum PickupRotation
        {
            Random,
            AroundZAxis
        }
        [SerializeField] private PickupRotation rotation;

        private void Awake()
        {
            rgbody = GetComponent<Rigidbody>();
            Assert.IsNotNull(rgbody, "No Rigidbody");
            audioSource = GetComponent<AudioSource>();
            Assert.IsNotNull(audioSource, "No AudioSource");
            StartRotation(Vector3.zero);
        }

        public void PickUp()
        {
            Debug.Log("Invoke");
            OnPickUp.Invoke(points);
            if (AudioManager.instance != null)
            {
                if (AudioManager.instance.SoundEffectsOn)
                {
                    audioSource.Play();
                }
            }
            Destroy(gameObject, audioSource.clip.length + 0.5f);
        }

        public override void SetColor(ColorScheme scheme)
        {
            GetComponentInChildren<MeshRenderer>().material.color = scheme.pickupColor;
        }

        public override Color GetColor()
        {
            return GetComponentInChildren<MeshRenderer>().material.color;
        }

        private void StartRotation(Vector3 verticalAxis)
        {
            if (rotation == PickupRotation.AroundZAxis)
                rgbody.angularVelocity = transform.up * rotationSpeed;
            else
                rgbody.angularVelocity = UnityEngine.Random.insideUnitSphere * rotationSpeed;
        }

       
    }

}

