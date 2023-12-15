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
    private PythonReader reader;

    void Start(){
        ingredientsToAdd = new Queue<string>();
        ingredientList = new List<GameObject>();
        speedup = FindObjectOfType<SpeedupButton>();
        reader = FindObjectOfType<PythonReader>();
        cooldown = 0f;
    }

    void Update(){
        //1 second cooldown for adding new ingredient to the belt
        if(cooldown > 0f) {
            cooldown = Mathf.Clamp(cooldown - Time.deltaTime, 0f, 1f / speedup.speedUpFactor);
            return;
        }

        //Limit of 10 ingredients on the belt at a time
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

    public bool IsEmpty() {
        return ingredientList.Count == 0;
    }

    public GameObject GetAt(int index) {
        return ingredientList[index];
    }

    public void RemoveAt(int index) {
        ingredientList.RemoveAt(index);
    }

    public void Reset() {
        ingredientsToAdd.Clear();
        ingredientList.ForEach(x => Destroy(x));
        ingredientList.Clear();
    }

    private void Add(string name)
    {

        Debug.Log("Adding to conveyor: " + name, gameObject);
        GameObject newIngredient = null;

        //All new ingredient start 2 units to the right of the conveyor
        //so they can transition into the scene smoothly
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
            case "Chocolate Frosting":
                newIngredient = Instantiate(chocolateFrostingPrefab, (Vector2)transform.position
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
                // Debug.LogWarning("Invalid ingredient type: " + name, gameObject);
                // errorDisplay.text = "<color=\"red\">" + "Invalid ingredient type: " + name;
                reader.SetErrorMessage("<color=\"red\">" + "Invalid ingredient type: " + name);
                FindObjectOfType<PlayButton>().Stop();
                break;
        }

        if(newIngredient != null) {
            var ingredientComp = newIngredient.GetComponent<Ingredient>(); 
            ingredientComp.ingredientName = name;
            ingredientComp.goalPosition = transform.position;

            //Move each ingredient over to make space for the new one
            ingredientList.ForEach(ingredient => {
                    var ing = ingredient.GetComponent<Ingredient>();
                    if(ing != null) {
                        ing.goalPosition.x -= 1;
                    } else {
                        Debug.LogWarning("Ingredient component not found", gameObject);
                    }
                });
            ingredientList.Add(newIngredient);
        }
    }
}
