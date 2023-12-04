using UnityEngine;

public class Ingredient:MonoBehaviour
{
    /* public IngredientType type; */
    public string IngredientName;
    private float moveSpeed = 5f;
    public Vector2 goalPosition { get; set; }

    void Start()
    {
        goalPosition = (Vector2)transform.position;
    }

    void Update()
    {
        var step =  moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards((Vector2)transform.position, goalPosition, step);
    }

    public override string ToString()
    {
        return IngredientName;
    }
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

