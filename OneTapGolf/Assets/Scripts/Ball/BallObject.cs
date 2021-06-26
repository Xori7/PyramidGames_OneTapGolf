using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneTapGolf.Ball {
    enum BallState {
        Static,
        Wait,
        Throwed
    }

    public class BallObject : MonoBehaviour {
        public float xVelocity, yVelocity;
        [SerializeField] private GameObject routePointPrefab;
        [SerializeField] private BallController ballController;
        private BallState state;
        private List<GameObject> routePoints = new List<GameObject>();
        private BallRoutePointsGenerator routePointsGenerator;
        private float timeElapsed;

        public void Initialize() {
            ballController = new BallController(transform.position, Physics2D.gravity, -1.68f, -0.1f, 0.4f);
            routePointsGenerator = new BallRoutePointsGenerator(ballController);
            timeElapsed = 0;
            state = BallState.Static;
        }

        private void Start() {
            Initialize();
        }

        private void Update() {
            UpdateState();
        }

        private void FixedUpdate() {
            if (state == BallState.Throwed) {
                ballController.OnUpdate(Time.fixedDeltaTime);
                transform.position = ballController.physicalObject.position;
            }
        }

        private void UpdateState() {
            switch (state) {
                case BallState.Static:
                    if (Input.GetKeyDown(KeyCode.Mouse0)) {
                        state = BallState.Wait;
                    }
                    break;
                case BallState.Throwed:
                    if (ballController.physicalObject.velocity.x == 0
                        || ballController.physicalObject.position.x > GameManager.Singleton.CameraHorizontalRange) {
                        GameManager.Singleton.Lost();
                    }
                    break;
                case BallState.Wait:
                    WhileWaiting();
                    if (Input.GetKeyUp(KeyCode.Mouse0)) {
                        Throw();
                    }
                    break;
            }
        }

        private void Throw() {
            ballController.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            foreach (GameObject point in routePoints) {
                point.gameObject.SetActive(false);
            }
            state = BallState.Throwed;
        }

        private void WhileWaiting() {
            ballController.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            Vector2[] points = routePointsGenerator.GetPoints(0.01f / timeElapsed);
            for (int i = 0; i < points.Length; i++) {
                if (routePoints.Count <= i) {
                    GameObject point = Instantiate(routePointPrefab, points[i], new Quaternion());
                    routePoints.Add(point);
                }
                else {
                    routePoints[i].transform.position = points[i];
                    routePoints[i].SetActive(true);
                }
            }

            ballController.physicalObject.position = transform.position;
            ballController.physicalObject.velocity = new Vector2(0, 0);
            timeElapsed += Time.fixedDeltaTime;

            if (points.Last().x > GameManager.Singleton.CameraHorizontalRange) {
                Throw();
            }
        }
    }
}