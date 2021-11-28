namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SnakeMovementBetter : MonoBehaviour
    {
        public Event OnApplePickUp;

        public float Speed { get { return timeToMove; } set { if (value >= 0.3f && value <= 0.8f) timeToMove = value;  } }
        [SerializeField, Tooltip("Seconds to move"), Range(0.3f, 0.8f)] private float timeToMove = 0.8f;
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject body;
        [SerializeField] private Sensor frontSensor, groundSensor, edgeSensor, insideSensor, leftGroundSensor, rightGroundSensor, leftSensor, rightSensor;
        [SerializeField] private BodyCube bodyCubePrefab;
        [SerializeField] private PickupSpawner spawner;

        [Header("Camera pivot")]
        [SerializeField] private CameraPivot cameraPivot;

        public List<Cube> bodyCubes = new List<Cube>();
        private Vector3 movementDirection = Vector3.forward;
        private float timeSinceLastMove;
        private Vector3[] nextPositions;
        private enum Direction
        {
            Forward,
            Downward,
            Upward,
            Left,
            Right
        }
        private Direction direction = Direction.Forward;
        private bool isReadyForRotation = false;

        private void RefreshColor()
        {
            //bodyCubePrefab.
        }

        void Awake()
        {
            OnApplePickUp = new Event();
            foreach (var cube in head.GetComponentsInChildren<Cube>())
            {
                bodyCubes.Add(cube);
                Transform cubePos = cube.transform;
                Transform found = spawner.freeSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));
                spawner.takenSpawnPositions.Add(found);
                spawner.freeSpawnPositions.Remove(found);
            }
            foreach (var cube in body.GetComponentsInChildren<Cube>())
            {
                bodyCubes.Add(cube);
                Transform cubePos = cube.transform;
                Transform found = spawner.freeSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));
                spawner.takenSpawnPositions.Add(found);
                spawner.freeSpawnPositions.Remove(found);
            }
        }

        void FixedUpdate()
        {
            timeSinceLastMove += Time.fixedDeltaTime;
            if (timeSinceLastMove > timeToMove)
            {
                Move();
                timeSinceLastMove = 0f;
                direction = Direction.Forward;
            }
        }

        public void TurnRight()
        {
            direction = Direction.Right;
        }
        public void TurnLeft()
        {
            direction = Direction.Left;
        }

        private void Move()
        {
            if (DetectCollision())
            {
                Debug.Log("Collision!!!");
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }


            if (!edgeSensor.IsCollidingBoard && direction == Direction.Forward
               || frontSensor.IsCollidingBoard && direction == Direction.Forward
               || (!leftGroundSensor.IsCollidingBoard && direction == Direction.Left && groundSensor.IsCollidingBoard) ||
               (!rightGroundSensor.IsCollidingBoard && direction == Direction.Right && groundSensor.IsCollidingBoard))
            {
                RotateCamera();
            }

            ReleaseTakenSpaces();
            if (groundSensor.IsCollidingBoard && frontSensor.IsCollidingBoard)
            {
                //TurnHead();
                MoveUpward();
                Debug.Log("Upward");
            }
            else if (groundSensor.IsCollidingBoard)
            {
                TurnHead();
                MoveForward();
                Debug.Log("Forward");
            }
            else if (!groundSensor.IsCollidingBoard){
                MoveDownward();
                Debug.Log("Downward");
            }


            TakeSpaces();
            isReadyForRotation = true;
        }

        private bool DetectCollision()
        {
            return insideSensor.IsCollidingBody;
        }

        private void MoveDownward()
        {
            bodyCubes[0].transform.Rotate(Vector3.right, 90f, Space.Self);
            direction = Direction.Downward;
            MoveForward();
        }

        private void MoveUpward()
        {
            bodyCubes[0].transform.Rotate(Vector3.right, -90f, Space.Self);
            direction = Direction.Upward;
            MoveForward();
        }

        private void TurnHead()
        {
            if (direction == Direction.Left)
            {
                bodyCubes[0].transform.Rotate(Vector3.up, -90f, Space.Self);
            }
            else if (direction == Direction.Right)
            {
                bodyCubes[0].transform.Rotate(Vector3.up, 90f, Space.Self);
            }
            direction = Direction.Forward;
        }

        private void RotateCamera()
        {
            Vector3 headVecUp = (insideSensor.transform.position - groundSensor.transform.position).normalized;
            Vector3 headVecForward = (frontSensor.transform.position - insideSensor.transform.position).normalized;
            Vector3 camVec = (Vector3.zero - cameraPivot.camera.transform.position).normalized;
            //Debug.Log(Vector3.Dot(headVecUp, camVecUp));

            //if (Vector3.Dot(headVecForward, camVec) > 0)
            if (VectorExtensions.AlmostEqual(headVecUp, cameraPivot.transform.up, 0.1f)) 
                //VectorExtensions.AlmostEqual(headVecForward, cameraPivot.transform.up, 0.1f))
            {
                Debug.Log($"Forward vector {GetForward()} Down vector {groundSensor.transform.position - insideSensor.transform.position}");
                cameraPivot.Rotate(Vector3.Cross(GetForward(), groundSensor.transform.position - insideSensor.transform.position), 0.4f);
                isReadyForRotation = false;
            }
        }

        private void MoveForward()
        {
            nextPositions = new Vector3[bodyCubes.Count];
            nextPositions[0] = bodyCubes[0].transform.position + GetForward();
            for (int i = 0; i < bodyCubes.Count - 1; i++)
            {
                //nextPositions[i + 1] = bodyCubes[i].GetComponent<Rigidbody>().position;
                nextPositions[i + 1] = bodyCubes[i].transform.position;
            }
            for (int i = 0; i < bodyCubes.Count; i++)
            {
                bodyCubes[i].GetComponent<Rigidbody>().MovePosition(nextPositions[i]);
                bodyCubes[i].transform.position = nextPositions[i];
            }
        }

        private Vector3 GetForward()
        {
            Vector3 position = Vector3.zero;
            if (direction == Direction.Forward || direction == Direction.Downward || direction == Direction.Upward)
                position = frontSensor.transform.position;
            else if (direction == Direction.Left)
                position = leftSensor.transform.position;
            else if (direction == Direction.Right)
                position = rightSensor.transform.position;
            return (position - bodyCubes[0].transform.position).normalized;
        }

        public void IncreaseBodyLength(int length)
        {
            //BodyCube cube = Instantiate(bodyCubePrefab, bodyCubes[bodyCubes.Count - 1].transform.position, Quaternion.identity, body.transform);
            BodyCube cube = Instantiate(bodyCubePrefab, Vector3.zero, Quaternion.identity, body.transform);
            bodyCubes.Add(cube);
            //Vector3 cubePos = cube.transform.position;
            Transform cubePos = bodyCubes[bodyCubes.Count - 1].transform;
            Transform found = spawner.freeSpawnPositions.Find(pos => pos.position.x == cubePos.position.x && 
                                                              pos.position.y == cubePos.position.y && 
                                                              pos.position.z == cubePos.position.z);
            spawner.takenSpawnPositions.Add(found);
            spawner.freeSpawnPositions.Remove(found);
        }

        private void ReleaseTakenSpaces()
        {
            int released = 0;
            foreach (var cube in bodyCubes)
            {
                Transform cubePos = cube.transform;
                //Debug.Log($"PRE {spawner}\n{spawner.takenSpawnPositions.Count}\ncubePos {cubePos}");
                Transform found = null;
                try
                {
                    //found = spawner.takenSpawnPositions.FirstOrDefault(pos => AlmostEqual(pos.position, cubePos.position, 0.1f));
                    found = spawner.takenSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));

                }
                catch (System.Exception ex)
                {
                    Debug.Log($"EXception {ex.StackTrace}");
                }
                //Debug.Log($"POST {found}\n{spawner}\n{spawner.takenSpawnPositions.Count}");
                if (found != null)
                {
                    if (found.position != Vector3.zero)
                    {
                        released += 1;
                        spawner.takenSpawnPositions.Remove(found);
                        spawner.freeSpawnPositions.Add(found);
                    }
                }
            }
        }

        private void TakeSpaces()
        {
            int taken = 0;
            foreach (var cube in bodyCubes)
            {
                Transform cubePos = cube.transform;
                Transform found = spawner.freeSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));
                if (found != null)
                {
                    if (found.position != Vector3.zero)
                    {
                        taken += 1;
                        spawner.freeSpawnPositions.Remove(found);
                        spawner.takenSpawnPositions.Add(found);
                    }
                }
            }
        }
    }
}