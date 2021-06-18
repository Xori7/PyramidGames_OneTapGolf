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

        private void FixedUpdate() {
            switch (state) {
                case BallState.Static:
                    if (Input.GetButtonDown("Fire1")) {
                        state = BallState.Wait;
                    }
                    break;
                case BallState.Throwed:
                    BallController.OnUpdate(Time.fixedDeltaTime);
                    transform.position = BallController.physicalObject.position;
                    break;
                case BallState.Wait:
                    WhileWaiting();
                    if (Input.GetButtonUp("Fire1")) {
                        Throw();
                        state = BallState.Throwed;
                    }
                    break;
            }
        }

        private void Throw() {
            BallController.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            foreach (GameObject point in routePoints) {
                point.gameObject.SetActive(false);
            }
            StartCoroutine(Wait());
        }

        private IEnumerator Wait() {
            yield return new WaitForSeconds(5);
            if (state == BallState.Throwed) {
                GameManager.Singleton.Lost();
            }
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