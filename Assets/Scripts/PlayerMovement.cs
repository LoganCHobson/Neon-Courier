using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float laneDistance = 1.5f;
    public float laneSwitchSpeed = 10f;

    private int currentLane = 1;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f;

    void Update()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                startTouchPosition = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                HandleSwipe();
            }
        }


        float targetX = (currentLane - 1) * laneDistance;
        Vector3 moveTarget = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, laneSwitchSpeed * Time.deltaTime);
    }

    void HandleSwipe()
    {
        Vector2 delta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(delta.x) > swipeThreshold && Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0 && currentLane < 2) currentLane++;
            else if (delta.x < 0 && currentLane > 0) currentLane--;
        }
    }
}
