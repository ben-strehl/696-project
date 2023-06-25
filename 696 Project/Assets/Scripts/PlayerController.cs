using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int[] facing = {1, 0};
    Vector2 goalPosition;
    private static Queue<Action> actionQueue = new Queue<Action>();
    private bool isMoving;
    private int[,] transformMatrix = {{1, -1}, {1, 1}};

    void Start() 
    {
        isMoving = false;
    }
    void Update()
    {
        // if(isMoving){
        //     var step =  moveSpeed * Time.deltaTime;

        //     transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);

        //     if(Vector2.Distance((Vector2)transform.position, goalPosition) == 0f){
        //         isMoving = false;
        //     }
        // } else {
        //     float horizontalInput = Input.GetAxis("Horizontal");
        //     float verticalInput = Input.GetAxis("Vertical");

        //     if(horizontalInput != 0) {
        //         goalPosition.x = transform.position.x + ((horizontalInput > 0) ? 0.5f : -0.5f);
        //         goalPosition.y = transform.position.y + ((horizontalInput > 0) ? 0.25f : -0.25f);
        //     } else if(verticalInput != 0) {
        //         goalPosition.x = transform.position.x + ((verticalInput > 0) ? -0.5f : 0.5f);
        //         goalPosition.y = transform.position.y + ((verticalInput > 0) ? 0.25f : -0.25f);
        //     } else {
        //         goalPosition = (Vector2)transform.position;
        //     }

        //     isMoving = true;

        //     // Debug.Log($"Moving player with goal postion ({goalPosition.x}, {goalPosition.y})");
        // }

        if(isMoving){
            var step =  moveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);

            if(Vector2.Distance((Vector2)transform.position, goalPosition) == 0f){
                isMoving = false;
            }
        } else {
            Action action;

            if(actionQueue.TryDequeue(out action)) {
                switch(action.actionType){
                    case ActionType.Forward:
                        Forward(int.Parse(action.args[0]));
                        break;
                    case ActionType.Turn:
                        Turn(action.args[0]);
                        break;
                    case ActionType.Interact:
                        Interact();
                        break;
                    default:
                        //Shouldn't be possible to reach this
                        Debug.Log("Invalid command at player");
                        break;
                }
            }
        }
    }

    void Forward(int steps) {
        int xDirection = facing[0] * transformMatrix[0,0] + facing[1] * transformMatrix[0,1];
        int yDirection = facing[0] * transformMatrix[1,0] + facing[1] * transformMatrix[1,1];
        goalPosition.x = transform.position.x + (steps * xDirection * 0.5f);
        goalPosition.y = transform.position.y + (steps * yDirection * 0.25f);

        isMoving = true;
    }

    void Turn(string direction) {
        if(direction == "left"){
            if(facing[0] == 0){
                facing[0] = -facing[1];
                facing[1] = 0;
            } else {
                facing[1] = facing[0];
                facing[0] = 0;
            }
        } else {
            if(facing[0] == 0){
                facing[0] = facing[1];
                facing[1] = 0;
            } else {
                facing[1] = -facing[0];
                facing[0] = 0;
            }
        }
    }

    void Interact() {
        Debug.Log("Interacting");
    }

    public void EnqueueAction(Action action){
        actionQueue.Enqueue(action);
    }

    public class Action {
        public Action(ActionType actionType, string[] args) {
            this.actionType = actionType;
            this.args = args;
        }

        public ActionType actionType;
        public string[] args;
    }

    public enum ActionType {
        Forward,
        Turn,
        Interact,
    }
}