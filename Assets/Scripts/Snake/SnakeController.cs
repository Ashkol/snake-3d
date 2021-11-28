namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(SnakeMovement))]
    public class SnakeController : MonoBehaviour
    {
        [SerializeField] private Transform snakeHead;
        [SerializeField] private CameraPivot cameraPivot;
        [SerializeField] private List<Transform> floorTransforms;
        private SnakeMovement movement;

        void Start()
        {
            movement = GetComponent<SnakeMovement>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement.TurnLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                movement.TurnRight();
            }
        }

        public void TurnUpLeft()
        {
            // Top Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraUp, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraForward, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraForward, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Left Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, -cameraPivot.targetCameraForward, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Right Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraRight, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraUp, snakeHead.right, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraUp, snakeHead.right, 0.1f))
                    movement.TurnRight();
            }
        }

        public void TurnUpRight()
        {
            // Top Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraUp, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraForward, -snakeHead.right, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraForward, snakeHead.right, 0.1f))
                    movement.TurnRight();
            }
            // Left Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, -cameraPivot.targetCameraForward, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraRight, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraRight, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Right Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraRight, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnRight();
                else if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
            }
        }

        public void TurnDownRight()
        {
            // Top Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraUp, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraForward, -snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraForward, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Left Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, -cameraPivot.targetCameraForward, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Right Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraRight, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraForward, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraForward, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
        }

        public void TurnDownLeft()
        {
            // Top Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraUp, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraRight, -snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraRight, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Left Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, -cameraPivot.targetCameraForward, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraRight, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraRight, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
            // Right Floor
            if (VectorExtensions.AlmostEqual(snakeHead.up, cameraPivot.targetCameraRight, 0.1f))
            {
                if (VectorExtensions.AlmostEqual(cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnLeft();
                else if (VectorExtensions.AlmostEqual(-cameraPivot.targetCameraUp, snakeHead.forward, 0.1f))
                    movement.TurnRight();
            }
        }
    }
}