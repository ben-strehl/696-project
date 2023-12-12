using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
    public Vector2 goalPosition; 
    public bool isGrabbed;
    private float moveSpeed;  
    private SpeedupButton speedup;

    void Start()
    {
        moveSpeed = 1f;
        speedup = FindObjectOfType<SpeedupButton>();
    }

    void Update()
    {
        if(goalPosition != (Vector2)transform.position && !isGrabbed){
            var step =  speedup.speedUpFactor * moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);
        }
    }
}
