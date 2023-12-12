using UnityEngine;
using System.Collections.Generic;
using System;
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
    private DeliveryTruck truck;
    private SpeedupButton speedup;
    private GameObject ingredient;

    private Predicate<GameObject> frostingQuery;
    private Predicate<GameObject> cakeQuery;
    private Predicate<GameObject> sprinklesQuery;

    void Start() 
    {
        moveSpeed = 5f;
        state = State.Idle;
        conveyor = FindObjectOfType<ConveyorBelt>();
        oven = FindObjectOfType<Oven>();
        table = FindObjectOfType<MixingTable>();
        truck = FindObjectOfType<DeliveryTruck>();
        speedup = FindObjectOfType<SpeedupButton>();

        frostingQuery = x => x.GetComponent<Ingredient>().ingredientName == "Frosting"
            || x.GetComponent<Ingredient>().ingredientName == "Chocolate Frosting";
        cakeQuery = x => x.GetComponent<Ingredient>().ingredientName == "Cake (Unfrosted)";
        sprinklesQuery = x => x.GetComponent<Ingredient>().ingredientName == "Sprinkles";
    }

    void Update()
    {
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

    private void Idle() {
        goalPosition = transform.position;

        if(conveyor.IsEmpty()) {
            // Debug.Log("Conveyor empty", gameObject);
            return;
        }

        state = State.Retrieving;
    }

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

    private void Moving() {
        Ingredient ingComp = ingredient.GetComponent<Ingredient>();
        ingComp.isGrabbed = true;

        ingredient.transform.position = transform.position - new Vector3(0f, 1f, 0f);

        if(truck.GetGoals().Contains(ingComp.ingredientName)) {
            goalPosition = truck.transform.position;
            if(goalPosition == (Vector2)transform.position) {
                truck.TurnIn(ingredient);
                state = State.Idle;
            }
            return;
        }

        switch(ingComp.ingredientName) {
            case "Frosting":
                GameObject unfrostedCake = conveyor.Find(cakeQuery);
                if(unfrostedCake != null) {
                    goalPosition = new Vector2(unfrostedCake.transform.position.x, unfrostedCake.transform.position.y + 1);
                    if(goalPosition == (Vector2)transform.position) {
                        ingredient = conveyor.Combine(ingredient, conveyor.IndexOf(unfrostedCake));
                        return;
                    }
                }
                goto case "Chocolate";
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
            case "Chocolate Frosting":
                GameObject cake = conveyor.Find(cakeQuery);
                if(conveyor.IsEmpty()) {
                    goalPosition = new Vector2(conveyor.transform.position.x, conveyor.transform.position.y + 1);
                } else if(cake != null) {
                    goalPosition = new Vector2(cake.transform.position.x, cake.transform.position.y + 1);
                    if(goalPosition == (Vector2)transform.position) {
                        ingredient = conveyor.Combine(ingredient, conveyor.IndexOf(cake));
                        return;
                    }
                } else {
                    Vector2 conveyorLead = conveyor.GetAt(0).transform.position;
                    goalPosition = new Vector2(conveyorLead.x + 1, conveyorLead.y + 1);
                }
                if(goalPosition == (Vector2)transform.position) {
                    conveyor.AddToFront(ingredient);
                    state = State.Idle;
                }
                break;
            case "Cake (Unfrosted)":
                GameObject frosting = conveyor.Find(frostingQuery);
                if(conveyor.IsEmpty()) {
                    goalPosition = new Vector2(conveyor.transform.position.x, conveyor.transform.position.y + 1);
                } else if(frosting != null) {
                    goalPosition = new Vector2(frosting.transform.position.x, frosting.transform.position.y + 1);
                    if(goalPosition == (Vector2)transform.position) {
                        ingredient = conveyor.Combine(ingredient, conveyor.IndexOf(frosting));
                        return;
                    }
                } else {
                    Vector2 conveyorLead = conveyor.GetAt(0).transform.position;
                    goalPosition = new Vector2(conveyorLead.x + 1, conveyorLead.y + 1);
                }
                if(goalPosition == (Vector2)transform.position) {
                    conveyor.AddToFront(ingredient);
                    state = State.Idle;
                }
                break;
            case "Chocolate Cake":
            case "Cake":
                GameObject sprinkles = conveyor.Find(sprinklesQuery);
                if(conveyor.IsEmpty()) {
                    goalPosition = new Vector2(conveyor.transform.position.x, conveyor.transform.position.y + 1);
                } else if(sprinkles != null) {
                    goalPosition = new Vector2(sprinkles.transform.position.x, sprinkles.transform.position.y + 1);
                    if(goalPosition == (Vector2)transform.position) {
                        ingredient = conveyor.Combine(ingredient, conveyor.IndexOf(sprinkles));
                        return;
                    }
                } else {
                    Vector2 conveyorLead = conveyor.GetAt(0).transform.position;
                    goalPosition = new Vector2(conveyorLead.x + 1, conveyorLead.y + 1);
                }
                if(goalPosition == (Vector2)transform.position) {
                    conveyor.AddToFront(ingredient);
                    state = State.Idle;
                }
                break;
            default:
                state = State.Idle;
                break;
        }
    }

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