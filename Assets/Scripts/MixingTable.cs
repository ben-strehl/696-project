using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MixingTable : MonoBehaviour
{
    private delegate void HandToRobot(GameObject ingredientObj);
    [SerializeField] private GameObject chocolateFrostingPrefab;
    [SerializeField] private GameObject doughPrefab;
    private List<GameObject> ingredients;
    private HandToRobot handToRobot;

    void Start()
    {
        ingredients = new List<GameObject>();
        /* combinedEvent = new CombineEvent(); */
        /* combinedEvent.AddListener(FindObjectOfType<Robot>().WaitingForTable); */
        handToRobot = FindObjectOfType<Robot>().WaitingForTable;
    }

    void Update()
    {
        
    }

    public void Add(GameObject ingredient) {
        string name = ingredient.GetComponent<Ingredient>().ingredientName;
        switch(name) {
            case "Flour":
            case "Sugar":
            case "Frosting":
            case "Milk":
            case "Egg":
            case "Chocolate":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                ingredients.Add(ingredient);
                Debug.Log("Added to mixer: " + name, gameObject);
                Combine();
                break;
            default:
                Debug.LogWarning("Attempt to add invalid ingredient to mixer: " + name, gameObject);
                break;
        }
    }

    private void Combine() {
        var flours = new List<int>();
        var sugars = new List<int>();
        var frostings = new List<int>();
        var milks = new List<int>();
        var eggs = new List<int>();
        var chocolates = new List<int>();

        for(int i = 0; i < ingredients.Count; i++) {

            string name = ingredients[i].GetComponent<Ingredient>().ingredientName;
            switch(name) {
                case "Flour":
                    flours.Add(i);
                    break;
                case "Sugar":
                    sugars.Add(i);
                    break;
                case "Frosting":
                    frostings.Add(i);
                    break;
                case "Milk":
                    milks.Add(i);
                    break;
                case "Egg":
                    eggs.Add(i);
                    break;
                case "Chocolate":
                    chocolates.Add(i);
                    break;
                default:
                    Debug.LogError("Invalid ingredient in mixer", gameObject);
                    break;
            }
        }

        if(frostings.Count > 0 && chocolates.Count > 0) {
            handToRobot(Instantiate(chocolateFrostingPrefab, transform.position, Quaternion.identity));

            Destroy(ingredients[frostings[0]]);
            ingredients.RemoveAt(frostings[0]);
            Destroy(ingredients[chocolates[0]]);
            ingredients.RemoveAt(chocolates[0]);

            Debug.Log("Made chocolate frosting", gameObject);
            return;

        } else if(flours.Count > 0 && sugars.Count > 0 && milks.Count > 0 && eggs.Count > 0) {
            handToRobot(Instantiate(doughPrefab, transform.position, Quaternion.identity));

            Destroy(ingredients[flours[0]]);
            ingredients.RemoveAt(flours[0]);
            Destroy(ingredients[sugars[0]]);
            ingredients.RemoveAt(sugars[0]);
            Destroy(ingredients[milks[0]]);
            ingredients.RemoveAt(milks[0]);
            Destroy(ingredients[eggs[0]]);
            ingredients.RemoveAt(eggs[0]);

            Debug.Log("Made dough", gameObject);
            return;
        }

        handToRobot(null);
    }
}
