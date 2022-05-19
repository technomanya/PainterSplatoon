using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float touchSpeed;
        public float forwardSpeed;

        public float maxX, minX;

        private Vector2 prevPos;
        private float deltaPos = 0;

        protected void Update()
        {
            if (deltaPos > 0.025) // Dampen the Input
                deltaPos = Mathf.Lerp(deltaPos, 0, .5f);
            else if (deltaPos < -0.025)
                deltaPos = Mathf.Lerp(deltaPos, 0, .5f);
            else
                deltaPos = 0;

            if (Input.GetMouseButtonDown(0))
            {
                prevPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0)) // Get player input
            {
                deltaPos = Input.GetTouch(0).position.x - prevPos.x;
                deltaPos *= touchSpeed * Time.deltaTime; // SpeedOffset

                prevPos = Input.GetTouch(0).position; // Set for next frame
            }

            GapCheck();

        }

        private void FixedUpdate()
        {
            PlayerMove();
        }

        public void PlayerMove()
        {

            if ((deltaPos < 0 && transform.position.x < minX) || (deltaPos > 0 && transform.position.x > maxX))
                transform.Translate(0, 0, forwardSpeed * Time.deltaTime);
            else
                transform.Translate(Mathf.Lerp(0, deltaPos, .5f), 0, forwardSpeed * Time.deltaTime);
        }

        public void GapCheck()
        {
            if (transform.position.x < minX)
                //transform.position.Set(minX, transform.position.y, transform.position.z);
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            else if (transform.position.x > maxX)
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }
}
