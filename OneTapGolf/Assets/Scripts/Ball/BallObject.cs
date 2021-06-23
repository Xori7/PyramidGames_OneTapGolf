using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneTapGolf.Ball {
    public enum BallState {
        Static,
        Wait,
        Throwed
    }

    public class BallObject : MonoBehaviour {
        public GameObject routePointPrefab;
        public BallController BallController { get; private set; }
        public BallState state;
        public float xVelocity, yVelocity;
        private List<GameObject> routePoints = new List<GameObject>();
        private BallRoutePointsGenerator routePointsGenerator;
        private float timeElapsed;


        public void Initialize() {
            StopAllCoroutines();
            BallController = new BallController(transform.position, Physics2D.gravity, -1.68f, -0.1f, 0.4f);
            routePointsGenerator = new BallRoutePointsGenerator(BallController);
            timeElapsed = 3;
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
                BallController.OnUpdate(Time.fixedDeltaTime);
                transform.position = BallController.physicalObject.position;
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
                    if (BallController.physicalObject.velocity.x == 0) {
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
            BallController.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            foreach (GameObject point in routePoints) {
                point.gameObject.SetActive(false);
            }
            state = BallState.Throwed;
        }

        private void WhileWaiting() {
            BallController.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
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

            BallController.physicalObject.position = transform.position;
            BallController.physicalObject.velocity = new Vector2(0, 0);
            timeElapsed += Time.fixedDeltaTime;

            if (points.Last().x > 10) {
                Throw();
            }
        }
    }
}