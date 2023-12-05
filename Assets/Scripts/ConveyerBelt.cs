using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    [SerializeField]private GameObject flourPrefab;
    [SerializeField]private GameObject sugarPrefab;
    [SerializeField]private GameObject milkPrefab;
    [SerializeField]private GameObject eggPrefab;
    [SerializeField]private GameObject frostingPrefab;
    [SerializeField]private GameObject chocolatePrefab;
    [SerializeField]private GameObject sprinklesPrefab;
    [SerializeField]private GameObject cakeUnfrostedPrefab;
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
                newIngredient = Instantiate(flourPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Milk":
                newIngredient = Instantiate(milkPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Egg":
                newIngredient = Instantiate(eggPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Frosting":
                newIngredient = Instantiate(frostingPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Chocolate":
                newIngredient = Instantiate(chocolatePrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Sprinkles":
                newIngredient = Instantiate(sprinklesPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Cake (Unfrosted)":
                newIngredient = Instantiate(cakeUnfrostedPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Cake":
                newIngredient = Instantiate(cakePrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Cake (Sprinkles)":
                newIngredient = Instantiate(cakeSprinklesPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Chocolate Cake":
                newIngredient = Instantiate(chocolateCakePrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            case "Chocolate Cake (sprinkles)":
                newIngredient = Instantiate(chocolateCakeSprinklesPrefab, new Vector2(50, 50), Quaternion.identity);
                break;
            default:
                Debug.LogWarning("Invalid ingredient type");
                break;
        }

        if(newIngredient != null) {
            newIngredient.GetComponent<Ingredient>().ingredientName = name;
            ingredientList.ForEach(x => {
                    var ing = x.GetComponent<Ingredient>();
                    if(ing != null) {
                        Debug.Log("Moving: " + ing.ingredientName);
                        ing.goalPosition.x -= 1;
                    } else {
                        Debug.LogWarning("Ingredient component not found");
                    }
                });
            newIngredient.transform.position = transform.position;
            ingredientList.Add(newIngredient);
        }
    }
}
