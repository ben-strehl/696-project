using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]private GameObject flourPrefab;
    [SerializeField]private GameObject sugarPrefab;
    [SerializeField]private GameObject milkPrefab;
    [SerializeField]private GameObject eggPrefab;
    /* [SerializeField]private GameObject doughPrefab; */
    [SerializeField]private GameObject frostingPrefab;
    /* [SerializeField]private GameObject chocolateFrostingPrefab; */
    [SerializeField]private GameObject chocolatePrefab;
    [SerializeField]private GameObject sprinklesPrefab;
    /* [SerializeField]private GameObject cakeUnfrostedPrefab; */
    [SerializeField]private GameObject cakePrefab;
    [SerializeField]private GameObject cakeSprinklesPrefab;
    [SerializeField]private GameObject chocolateCakePrefab;
    [SerializeField]private GameObject chocolateCakeSprinklesPrefab;

    private Queue<string> ingredientsToAdd;
    private List<GameObject> ingredientList;

    void Start(){
        ingredientsToAdd = new Queue<string>();
        ingredientList = new List<GameObject>();
    }

    void Update(){
        if(ingredientsToAdd.TryDequeue(out string ingredient)) {
            /* Debug.Log("Queueing: " + ingredient); */
            Add(ingredient);
        }
    }

    public void AddToQueue(string ingredient) {
        ingredientsToAdd.Enqueue(ingredient);
    }

    private void Add(string name)
    {
        Debug.Log("Adding: " + name);
        GameObject newIngredient = null;

        switch(name) {
            case "Flour":
                newIngredient = Instantiate(flourPrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            case "Milk":
                newIngredient = Instantiate(milkPrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            case "Egg":
                newIngredient = Instantiate(eggPrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            case "Frosting":
                newIngredient = Instantiate(frostingPrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            case "Chocolate":
                newIngredient = Instantiate(chocolatePrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            case "Sprinkles":
                newIngredient = Instantiate(sprinklesPrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            /* case "Cake (Unfrosted)": */
            /*     newIngredient = Instantiate(cakeUnfrostedPrefab, (Vector2)transform.position */
            /*             + new Vector2(2, 0), Quaternion.identity); */
            /*      break; */
            case "Cake":
                newIngredient = Instantiate(cakePrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                 break;
            case "Cake (Sprinkles)":
                newIngredient = Instantiate(cakeSprinklesPrefab,(Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                break;
            case "Chocolate Cake":
                newIngredient = Instantiate(chocolateCakePrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                break;
            case "Chocolate Cake (sprinkles)":
                newIngredient = Instantiate(chocolateCakeSprinklesPrefab, (Vector2)transform.position
                        + new Vector2(2, 0), Quaternion.identity);
                break;
            default:
                Debug.LogWarning("Invalid ingredient type");
                break;
        }

        if(newIngredient != null) {
            var ingredientComp = newIngredient.GetComponent<Ingredient>(); 
            ingredientComp.ingredientName = name;
            ingredientComp.goalPosition = transform.position;
            ingredientList.ForEach(ingredient => {
                    var ing = ingredient.GetComponent<Ingredient>();
                    if(ing != null) {
                        Debug.Log("Moving: " + ing.ingredientName);
                        ing.goalPosition.x -= 1;
                    } else {
                        Debug.LogWarning("Ingredient component not found");
                    }
                });
            /* newIngredient.transform.position = transform.position; */
            /* StartCoroutine(MoveIngredient(newIngredient)); */
            ingredientList.Add(newIngredient);
        }
    }

    IEnumerator MoveIngredient(GameObject ingredient) {
        var step = Vector2.Distance(transform.position, ingredient.transform.position)
            * Time.deltaTime;
        while(Vector2.Distance(transform.position, ingredient.transform.position) > 0.1) {
            /* ingredient.GetComponent<Ingredient>().goalPosition = transform.position; */
            ingredient.transform.position = Vector2.MoveTowards(ingredient.transform.position,
                transform.position, step);
            Debug.Log($"Ingredient position: {ingredient.transform.position}" + 
                    $"Goal Position: {ingredient.GetComponent<Ingredient>().goalPosition}", ingredient);
            yield return null;
        }

    }
}
