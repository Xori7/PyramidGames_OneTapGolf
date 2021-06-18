using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OneTapGolf.Ball {
    public class BallObject : MonoBehaviour {
        public GameObject routePointPrefab;
        private Ball ball;
        private List<GameObject> routePoints = new List<GameObject>();
        private BallRoutePointsGenerator routePointsGenerator;
        private float timeElapsed = 3;

        public float xVelocity, yVelocity;
        private bool throwed;

        private void Start() {
            ball = new Ball(transform.position, Physics2D.gravity, -1.68f, -0.1f, 0.4f);
            routePointsGenerator = new BallRoutePointsGenerator(ball);
        }
        private void FixedUpdate() {
            if (Input.GetButtonDown("Fire1")) {
                Throw();
            }

            if (throwed) {
                ball.OnUpdate(Time.fixedDeltaTime);
                transform.position = ball.physicalObject.position;
            }
            else {
                WhileWaiting();
            }
        }

        private void Throw() {
            throwed = true;
            ball.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            foreach (GameObject point in routePoints) {
                point.gameObject.SetActive(false);
            }
        }

        private void WhileWaiting() {
            ball.Throw(new Vector2(xVelocity, yVelocity) * timeElapsed);
            Vector2[] points = routePointsGenerator.GetPoints(0.01f / timeElapsed);
            for (int i = 0; i < points.Length; i++) {
                if (routePoints.Count <= i) {
                    GameObject point = Instantiate(routePointPrefab, points[i], new Quaternion());
                    routePoints.Add(point);
                }
                else {
                    routePoints[i].transform.position = points[i];
                }
            }

            ball.physicalObject.position = transform.position;
            ball.physicalObject.velocity = new Vector2(0, 0);
            timeElapsed += Time.fixedDeltaTime;

            if (points.Last().x > 10) {
                Throw();
            }
        }
    }
}