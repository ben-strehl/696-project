using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
    public Vector2 goalPosition; 
    public bool isGrabbed;
    private float moveSpeed;  

    void Start()
    {
        moveSpeed = 1f;
    }

    void Update()
    {
        if(goalPosition != (Vector2)transform.position && !isGrabbed){
            var step =  moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);
        }
    }
}
