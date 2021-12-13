namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Events;

    public class SnakeMovement : MonoBehaviour
    {
        private const float MAX_TIME_TO_MOVE = 1f;
        private const float MIN_TIME_TO_MOVE = 0.4f;

        public Event OnApplePickUp;
        public UnityEvent OnDeath;

        [SerializeField]
        private bool isMovementEnabled = true;

        public float Speed { get { return timeToMove; } set { if (value >= MIN_TIME_TO_MOVE && value <= MAX_TIME_TO_MOVE) timeToMove = value; } }
        [SerializeField, Tooltip("Seconds to move"), Range(MIN_TIME_TO_MOVE, MAX_TIME_TO_MOVE)] private float timeToMove = MAX_TIME_TO_MOVE;
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
        private bool isMoving = true;

        void Awake()
        {
            InitializeSnakeCubes();
        }

        void Update()
        {
            //timeSinceLastMove += Time.fixedDeltaTime;
            timeSinceLastMove += Time.deltaTime;
            if (timeSinceLastMove > timeToMove && isMoving && isMovementEnabled)
            {
                Move();
                timeSinceLastMove = 0f;
                direction = Direction.Forward;
            }
        }

        private void InitializeSnakeCubes()
        {
            OnApplePickUp = new Event();
            foreach (var cube in head.GetComponentsInChildren<Cube>())
            {
                bodyCubes.Add(cube);
                Transform cubePos = cube.transform;
                if (spawner != null)
                {
                    Transform found = spawner.freeSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));
                    spawner.takenSpawnPositions.Add(found);
                    spawner.freeSpawnPositions.Remove(found);
                }

            }
            foreach (var cube in body.GetComponentsInChildren<Cube>())
            {
                bodyCubes.Add(cube);
                Transform cubePos = cube.transform;
                if (spawner != null)
                {
                    Transform found = spawner.freeSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));
                    spawner.takenSpawnPositions.Add(found);
                    spawner.freeSpawnPositions.Remove(found);
                }

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
            if (isMoving)
            {
                if (DetectCollision())
                {
                    PlayManager.instance.GameOver();
                    isMoving = false;
                    OnDeath.Invoke();
                }
                else
                {
                    if (!edgeSensor.IsCollidingBoard && direction == Direction.Forward ||
                   (!leftGroundSensor.IsCollidingBoard && direction == Direction.Left && groundSensor.IsCollidingBoard) ||
                   (!rightGroundSensor.IsCollidingBoard && direction == Direction.Right && groundSensor.IsCollidingBoard))
                    {
                        RotateCamera(Direction.Downward);
                    }
                    else if (frontSensor.IsCollidingBoard && direction == Direction.Forward ||
                       (leftSensor.IsCollidingBoard && direction == Direction.Left && groundSensor.IsCollidingBoard) ||
                       (rightSensor.IsCollidingBoard && direction == Direction.Right && groundSensor.IsCollidingBoard))
                    {
                        RotateCamera(Direction.Upward);
                    }

                    if (spawner != null)
                        ReleaseTakenSpaces();
                    if (groundSensor.IsCollidingBoard && frontSensor.IsCollidingBoard)
                    {
                        if (direction == Direction.Left || direction == Direction.Right)
                        {
                            TurnHead();
                            MoveForward();
                        }
                        else
                        {
                            MoveUpward();
                        }
                        //Debug.Log("Upward");
                    }
                    else if (groundSensor.IsCollidingBoard &&
                        ((leftSensor.IsCollidingBoard && direction == Direction.Left) || (rightSensor.IsCollidingBoard && direction == Direction.Right)))
                    {
                        TurnHead();
                        MoveUpward();
                        //Debug.Log("Turn and Upward");
                    }
                    else if (groundSensor.IsCollidingBoard)
                    {
                        TurnHead();
                        MoveForward();
                        //Debug.Log("Forward");
                    }
                    else if (!groundSensor.IsCollidingBoard)
                    {
                        MoveDownward();
                        //Debug.Log("Downward");
                    }
                    if (spawner != null)
                        TakeSpaces();
                    isReadyForRotation = true;
                }
            }
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

        private void RotateCamera(Direction direction)
        {
            Vector3 headVecUp = (insideSensor.transform.position - groundSensor.transform.position).normalized;
            Vector3 headVecForward = (frontSensor.transform.position - insideSensor.transform.position).normalized;
            Vector3 camVec = (Vector3.zero - cameraPivot.camera.transform.position).normalized;
            //Debug.Log(Vector3.Dot(headVecUp, camVecUp));

            //if (Vector3.Dot(headVecForward, camVec) > 0)
            if (VectorExtensions.AlmostEqual(headVecUp, cameraPivot.transform.up, 0.1f) && direction == Direction.Downward)
            //VectorExtensions.AlmostEqual(headVecForward, cameraPivot.transform.up, 0.1f))
            {
                //Debug.Log($"Forward vector {GetForward()} Down vector {groundSensor.transform.position - insideSensor.transform.position}");
                cameraPivot.Rotate(Vector3.Cross(GetForward(), groundSensor.transform.position - insideSensor.transform.position), 0.3f);
                isReadyForRotation = false;
            }
            else if (VectorExtensions.AlmostEqual(headVecUp, cameraPivot.transform.up, 0.1f) && direction == Direction.Upward)
            {
                //Debug.Log($"Forward vector {GetForward()} Down vector {groundSensor.transform.position - insideSensor.transform.position}");
                cameraPivot.Rotate(Vector3.Cross(-GetForward(), groundSensor.transform.position - insideSensor.transform.position), 0.3f);
                isReadyForRotation = false;
            }
        }

        private void MoveForward()
        {
            nextPositions = new Vector3[bodyCubes.Count];
            nextPositions[0] = bodyCubes[0].transform.position + GetForward();

            RaycastHit hit;
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            //if (spawner.takenSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(nextPositions[0], pos.position, 0.1f)))
            if (Physics.Raycast(bodyCubes[0].transform.position, GetForward(), out hit, 1f, layerMask))
            {
                var hitCube = hit.collider.gameObject.GetComponent<BodyCube>();
                if (hitCube)
                {
                    if (hitCube != bodyCubes[bodyCubes.Count - 1])
                    {
                        PlayManager.instance.GameOver();
                        isMoving = false;
                        OnDeath.Invoke();
                        return;
                    }
                }
            }

            for (int i = 0; i < bodyCubes.Count - 1; i++)
            {
                //nextPositions[i + 1] = bodyCubes[i].GetComponent<Rigidbody>().position;
                nextPositions[i + 1] = bodyCubes[i].transform.position;
            }
            for (int i = 0; i < bodyCubes.Count; i++)
            {
                bodyCubes[i].GetComponent<Rigidbody>().MovePosition(nextPositions[i]);
                bodyCubes[i].transform.position = nextPositions[i];
                bodyCubes[i].gameObject.SetActive(true);
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
            Transform cubePos = bodyCubes[bodyCubes.Count - 1].transform;
            BodyCube cube = Instantiate(bodyCubePrefab, cubePos.position, Quaternion.identity, body.transform);
            bodyCubes.Add(cube);
            //Transform found = spawner.takenSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));
            cube.gameObject.SetActive(false);
            //spawner.takenSpawnPositions.Add(found);
            //spawner.freeSpawnPositions.Remove(found);
        }

        private void ReleaseTakenSpaces()
        {
            int released = 0;
            foreach (var cube in bodyCubes)
            {
                Transform cubePos = cube.transform;
                Transform found = null;
                try
                {
                    found = spawner.takenSpawnPositions.Find(pos => VectorExtensions.AlmostEqual(pos.position, cubePos.position, 0.1f));

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
                // TODO
                catch (System.Exception ex)
                {
                    Debug.Log($"EXception {ex.StackTrace}");
                }
                //Debug.Log($"POST {found}\n{spawner}\n{spawner.takenSpawnPositions.Count}");

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
            //Debug.Log($"Free spaces = {spawner.freeSpawnPositions.Count}\nTaken spaces = {spawner.takenSpawnPositions.Count}");
        }
    }
}