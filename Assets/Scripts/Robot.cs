using UnityEngine;
using Unity.VisualScripting;

public class Robot : MonoBehaviour
{
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    private float moveSpeed;
    private Vector2 goalPosition;
    private State state;
    private ConveyorBelt conveyor;
    private Oven oven;
    private MixingTable table;
    private DecoratingTable decoTable;
    private DeliveryTruck truck;
    private SpeedupButton speedup;
    private GameObject ingredient;

    void Start() 
    {
        moveSpeed = 5f;
        state = State.Idle;
        conveyor = FindObjectOfType<ConveyorBelt>();
        oven = FindObjectOfType<Oven>();
        table = FindObjectOfType<MixingTable>();
        decoTable = FindObjectOfType<DecoratingTable>();
        truck = FindObjectOfType<DeliveryTruck>();
        speedup = FindObjectOfType<SpeedupButton>();
    }

    void Update()
    {
        //This is our finite state machine
        switch(state) {
            case State.Idle:
                Idle();
                break;
            case State.Retrieving:
                Retrieving();
                break;
            case State.Moving:
                Moving();
                break;
            //In these states we just wait for the oven or mixing table
            //to call one of our methods
            case State.WaitingForOven: 
            case State.WaitingForTable:
                return; 
        }

        Vector2 currentPosition = transform.position;

        if(goalPosition != (Vector2)transform.position){
            var step =  speedup.speedUpFactor * moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);
        }

        Vector2 move = (Vector2)transform.position - currentPosition;
        var spriteRend = GetComponent<SpriteRenderer>();

        //Rotate the sprite based on our direction of travel
        if(Mathf.Abs(move.x) > Mathf.Abs(move.y)){
            if(move.x > 0f) {
                spriteRend.sprite = rightSprite;
            } else if(move.x < 0f) {
                spriteRend.sprite = leftSprite;
            }
        } else {
            if(move.y > 0f) {
                spriteRend.sprite = upSprite;
            } else if(move.y < 0f) {
                spriteRend.sprite = downSprite;
            }
        }

    }

    public void Reset() {
        if(ingredient != null) {
            if(!ingredient.IsDestroyed()) {
                Destroy(ingredient);
            }
            ingredient = null;
        }
        state = State.Idle;
        transform.position = new Vector3(0f, -2.5f, 0f);
        GetComponent<SpriteRenderer>().sprite = downSprite;
    }

    //Wait for an ingredient to arrive on the conveyor
    private void Idle() {
        goalPosition = transform.position;

        if(conveyor.IsEmpty()) {
            return;
        }

        state = State.Retrieving;
    }

    //Grab the lead ingredient from the conveyor
    private void Retrieving() {

        if(conveyor.GetAt(0).transform.position.x <= conveyor.transform.position.x){
            ingredient = conveyor.GetAt(0);
            if(ingredient == null) {
                Debug.LogWarning("Null ingredient", gameObject);
            } else {
                goalPosition = new Vector2(ingredient.transform.position.x, ingredient.transform.position.y + 1);

                if(goalPosition == (Vector2)transform.position) {
                    conveyor.RemoveAt(0);
                    state = State.Moving;
                }
            }
        }

    }

    //Move to a station based on the ingredient held and the game state
    private void Moving() {
        Ingredient ingComp = ingredient.GetComponent<Ingredient>();
        ingComp.isGrabbed = true;

        ingredient.transform.position = transform.position - new Vector3(0f, 1f, 0f);

        //Override all other logic and bring the ingredient to the truck if
        //it is one of the goal ingredients
        if(truck.GetGoals().Contains(ingComp.ingredientName)) {
            goalPosition = (Vector2)truck.transform.position + new Vector2(1.5f, 0f);
            if(goalPosition == (Vector2)transform.position) {
                truck.TurnIn(ingredient);
                state = State.Idle;
            }
            return;
        }

        switch(ingComp.ingredientName) {
            case "Flour":
            case "Milk":
            case "Sugar":
            case "Egg":
            case "Chocolate":
                goalPosition = new Vector2(table.transform.position.x, table.transform.position.y - 1);
                if(goalPosition == (Vector2)transform.position) {
                    table.Add(ingredient);
                }
                break;
            case "Dough":
                goalPosition = new Vector2(oven.transform.position.x, oven.transform.position.y - 1);
                if(goalPosition == (Vector2)transform.position) {
                    oven.Add(ingredient);
                }
                break;
            //If there is chocolate in the mixing table, we assume the player is trying to
            //make chocolate frosting
            case "Frosting":
                if(table.HasChocolate()) {
                    goto case "Chocolate";
                } 
                //Otherwise, put it on the decorating table
                goto case "Cake (Unfrosted)";
            case "Chocolate Frosting":
            case "Cake (Unfrosted)":
            case "Sprinkles":
            case "Chocolate Cake":
            case "Cake":
                goalPosition = new Vector2(decoTable.transform.position.x, decoTable.transform.position.y - 1);
                if(goalPosition == (Vector2)transform.position) {
                    decoTable.Add(ingredient);
                }
                break;
            default:
                state = State.Idle;
                break;
        }
    }

    //These two methods will be called by the oven, mixing table, or decorating table
    //when they have something to give the robot
    public void WaitingForOven(GameObject ingredientObj) {
        Debug.Log("WaitingForOven invoked", gameObject);
        ingredient = ingredientObj;
        state = State.Moving;
    }

    public void WaitingForTable(GameObject ingredientObj) {
        Debug.Log("WaitingForTable invoked", gameObject);
        if(ingredientObj != null) {
            ingredient = ingredientObj;
            state = State.Moving;
        } else {
            state = State.Idle;
        }
    }

    private enum State {
        Idle,
        Retrieving,
        Moving,
        WaitingForOven,
        WaitingForTable
    }
}