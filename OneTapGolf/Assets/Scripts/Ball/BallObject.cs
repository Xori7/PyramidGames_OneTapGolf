using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneTapGolf.Ball {
    public class BallObject : MonoBehaviour {
        public GameObject routePointPrefab;
        public BallController BallController { get; private set; }
        private List<GameObject> routePoints = new List<GameObject>();
        private BallRoutePointsGenerator routePointsGenerator;
        private float timeElapsed;

        public float xVelocity, yVelocity;
        private bool throwed;

        public void Initialize() {
            StopAllCoroutines();
            BallController = new BallController(transform.position, Physics2D.gravity, -1.68f, -0.1f, 0.4f);
            routePointsGenerator = new BallRoutePointsGenerator(BallController);
            timeElapsed = 3;
            throwed = false;
        }

        private void Start() {
            Initialize();
        }

        private void FixedUpdate() {
            if (Input.GetButtonDown("Fire1")) {
                Throw();
            }

            if (throwed) {
                BallController.OnUpdate(Time.fixedDeltaTime);
                transform.position = BallController.physicalObject.position;
            }
            else {
                WhileWaiting();
            }
        }

        private void Throw() {
            throwed = true;
            BallController.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            foreach (GameObject point in routePoints) {
                point.gameObject.SetActive(false);
            }
            StartCoroutine(Wait());
        }

        private IEnumerator Wait() {
            yield return new WaitForSeconds(5);
            if (throwed) {
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