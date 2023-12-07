using UnityEngine;

public class Ingredient : MonoBehaviour
{
    /* public IngredientType type; */
    public string ingredientName;
    public Vector2 goalPosition; 
    private float moveSpeed;  

    /* public Ingredient(string name, Vector2 pos) */
    /* { */
    /*     ingredientName = name; */
    /*     goalPosition = pos; */
    /* } */

    void Start()
    {
        /* goalPosition = (Vector2)transform.position; */
        moveSpeed = 1f;
    }

    void Update()
    {
        if(goalPosition != (Vector2)transform.position){
            /* Debug.Log("Moving to (" + goalPosition.x + ", " + goalPosition.y + ")", gameObject); */
            var step =  moveSpeed * Time.deltaTime;
            /* Debug.Log("step: " + moveSpeed); */
            transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);
        }
    }

    /* public override string ToString() */
    /* { */
    /*     return ingredientName; */
    /* } */
}

/* public enum IngredientType */
/* { */
/*     Flour, */
/*     Sugar, */
/*     Milk, */
/*     Egg, */
/*     Frosting, */
/*     Chocolate, */
/*     Vanilla, */
/*     Sprinkles, */
/*     Cake */
/* } */

