using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]private GameObject flourPrefab;
    [SerializeField]private GameObject sugarPrefab;
    [SerializeField]private GameObject milkPrefab;
    [SerializeField]private GameObject eggPrefab;
    [SerializeField]private GameObject doughPrefab; 
    [SerializeField]private GameObject frostingPrefab;
    [SerializeField]private GameObject chocolateFrostingPrefab; 
    [SerializeField]private GameObject chocolatePrefab;
    [SerializeField]private GameObject sprinklesPrefab;
    [SerializeField]private GameObject cakeUnfrostedPrefab; 
    [SerializeField]private GameObject cakePrefab;
    [SerializeField]private GameObject cakeSprinklesPrefab;
    [SerializeField]private GameObject chocolateCakePrefab;
    [SerializeField]private GameObject chocolateCakeSprinklesPrefab;

    private Queue<string> ingredientsToAdd;
    private List<GameObject> ingredientList;
    private SpeedupButton speedup;
    private float cooldown;

    void Start(){
        ingredientsToAdd = new Queue<string>();
        ingredientList = new List<GameObject>();
        speedup = FindObjectOfType<SpeedupButton>();
        cooldown = 0f;
    }

    void Update(){
        if(cooldown > 0f) {
            cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0f, 1f / speedup.speedUpFactor);
            return;
        }

        if(ingredientList.Count > 10) {
            return;
        }

        if(ingredientsToAdd.TryDequeue(out string ingredient)) {
            cooldown = 1f / speedup.speedUpFactor;
            Add(ingredient);
        }
    }

    public void AddToQueue(string ingredient) {
        ingredientsToAdd.Enqueue(ingredient);
    }

    public void AddToFront(GameObject ingredient) {
        ingredientList.Insert(0, ingredient);
    }

    public bool IsEmpty() {
        return ingredientList.Count == 0;
    }

    public int IndexOf(GameObject ingredient) {
        return ingredientList.IndexOf(ingredient);
    }

    public GameObject GetAt(int index) {
        return ingredientList[index];
    }

    public void RemoveAt(int index) {
        ingredientList.RemoveAt(index);
    }

    public bool Exists(Predicate<GameObject> predicate) {
        return ingredientList.Exists(predicate);
    }

    public GameObject Find(Predicate<GameObject> predicate) {
        return ingredientList.Find(predicate);
    }

    public void Remove(GameObject ingredient) {
        ingredientList.Remove(ingredient);
    }

    public GameObject Combine(GameObject ingredient, int ingIndex) {
        Ingredient ingComp = ingredient.GetComponent<Ingredient>();
        Ingredient ingInListComp = ingredientList[ingIndex].GetComponent<Ingredient>();
        GameObject combinedIng = null;
        
        switch(ingComp.ingredientName) {
            case "Cake (Unfrosted)":
                if(ingInListComp.ingredientName == "Frosting") {
                    combinedIng = Instantiate(cakePrefab, ingredient.transform.position, Quaternion.identity);
                }
                if(ingInListComp.ingredientName == "Chocolate Frosting") {
                    combinedIng = Instantiate(chocolateCakePrefab, ingredient.transform.position, Quaternion.identity);
                }
                break;
            case "Frosting":
                if(ingInListComp.ingredientName == "Cake (Unfrosted)") {
                    combinedIng = Instantiate(cakePrefab, ingredient.transform.position, Quaternion.identity);
                }
                break;
            case "Chocolate Frosting":
                if(ingInListComp.ingredientName == "Cake (Unfrosted)") {
                    combinedIng = Instantiate(chocolateCakePrefab, ingredient.transform.position, Quaternion.identity);
                }
                break;
            case "Cake":
                if(ingInListComp.ingredientName == "Sprinkles") {
                    combinedIng = Instantiate(cakeSprinklesPrefab, ingredient.transform.position, Quaternion.identity);
                }
                break;
            case "Chocolate Cake":
                if(ingInListComp.ingredientName == "Sprinkles") {
                    combinedIng = Instantiate(chocolateCakeSprinklesPrefab, ingredient.transform.position, Quaternion.identity);
                }
                break;
            default:
                Debug.LogError("Conveyor cannot combine this ingredient", gameObject);
                return ingredient;
        }

        if(combinedIng != null) {
            Destroy(ingredient);
            Destroy(ingredientList[ingIndex]);
            ingredientList.RemoveAt(ingIndex);
            return combinedIng;
        }

        Debug.LogError("Invalid ingredient chosen from conveyor");
        return ingredient;

    }

    public void Reset() {
        ingredientList.ForEach(x => Destroy(x));
        ingredientList.Clear();
    }

    private void Add(string name)
    {

        Debug.Log("Adding to conveyor: " + name, gameObject);
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
            case "Sugar":
                newIngredient = Instantiate(sugarPrefab, (Vector2)transform.position
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
            case "Cake (Unfrosted)": 
            newIngredient = Instantiate(cakeUnfrostedPrefab, (Vector2)transform.position 
            + new Vector2(2, 0), Quaternion.identity); 
            break; 
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
                Debug.LogWarning("Invalid ingredient type", gameObject);
                break;
        }

        if(newIngredient != null) {
            var ingredientComp = newIngredient.GetComponent<Ingredient>(); 
            ingredientComp.ingredientName = name;
            ingredientComp.goalPosition = transform.position;
            ingredientList.ForEach(ingredient => {
                    var ing = ingredient.GetComponent<Ingredient>();
                    if(ing != null) {
                        /* Debug.Log("Moving: " + ing.ingredientName); */
                        ing.goalPosition.x -= 1;
                    } else {
                        Debug.LogWarning("Ingredient component not found", gameObject);
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
