namespace MobileGame.Snake
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraPivot : MonoBehaviour
    {
        private Rigidbody rgbody;
        public float rotationTime;
        public Transform snakeHead;
        public Transform boardCenter;
        public Camera camera;
        public Vector3 targetCameraUp, targetCameraForward, targetCameraRight;

        private enum RotationDirection
        {
            LeftRight,
            RightLeft,
            LeftTopDown,
            LeftDownTop,
            RightTopDown,
            RightDownTop
        }
        private bool isRotating;


        private void Start()
        {
            rgbody = GetComponent<Rigidbody>();
            targetCameraUp = transform.up;
            targetCameraForward = transform.forward;
            targetCameraRight = transform.right;

            camera.backgroundColor = GameManager.instance.CurrentColorScheme.backgroundColor;
        }

        private void Update()
        {
            if (!isRotating)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    StartCoroutine(Rotate(90f, Vector3.up, rotationTime));
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(Rotate(-90f, Vector3.up, rotationTime));
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(Rotate(90f, Vector3.forward, rotationTime));
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    StartCoroutine(Rotate(-90f, Vector3.forward, rotationTime));
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    StartCoroutine(Rotate(90f, Vector3.right, rotationTime));
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    StartCoroutine(Rotate(-90f, Vector3.right, rotationTime));
                }
            }
        }
        
        public void Rotate(Vector3 rotationAxis)
        {
            StartCoroutine(Rotate(90f, rotationAxis, rotationTime));
        }

        public void Rotate(Vector3 rotationAxis, float rotationTime)
        {
            StartCoroutine(Rotate(90f, rotationAxis, rotationTime));
        }

        IEnumerator Rotate(float angle, Vector3 axis, float inTime)
        {
            isRotating = true;
            var fromRotation = transform.rotation;
            var toRotation = Quaternion.AngleAxis(angle, axis) * fromRotation;
            targetCameraUp = Quaternion.AngleAxis(angle, axis) * transform.up;
            targetCameraForward = Quaternion.AngleAxis(angle, axis) * transform.forward;
            targetCameraRight = Quaternion.AngleAxis(angle, axis) * transform.right;
            for (var t = 0f; t <= 1.1; t += Time.deltaTime / inTime)
            {
                transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);
                //transform.RotateAround(Vector3.zero, axis, (Time.deltaTime / inTime) * angle);
                yield return null;
            }
            transform.rotation = toRotation;
            isRotating = false;
        }

        public void StartRandomRotation()
        {
            rgbody.angularVelocity = UnityEngine.Random.insideUnitSphere * 1f;
        }
    }

}
