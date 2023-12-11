using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MixingTable : MonoBehaviour
{
    private delegate void HandToRobot(GameObject ingredientObj);
    [SerializeField] private GameObject chocolateFrostingPrefab;
    [SerializeField] private GameObject doughPrefab;
    private List<GameObject> flours;
    private List<GameObject> sugars;
    private List<GameObject> frostings;
    private List<GameObject> milks;
    private List<GameObject> eggs;
    private List<GameObject> chocolates;

    private HandToRobot handToRobot;

    void Start()
    {
        flours = new List<GameObject>();
        sugars = new List<GameObject>();
        frostings = new List<GameObject>();
        milks = new List<GameObject>();
        eggs = new List<GameObject>();
        chocolates = new List<GameObject>();
        handToRobot = FindObjectOfType<Robot>().WaitingForTable;
    }

    void Update()
    {
        
    }

    public void Add(GameObject ingredient) {
        string name = ingredient.GetComponent<Ingredient>().ingredientName;
        switch(name) {
            case "Flour":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                flours.Add(ingredient);
                break;
            case "Sugar":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                sugars.Add(ingredient);
                break;
            case "Frosting":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                frostings.Add(ingredient);
                break;
            case "Milk":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                milks.Add(ingredient);
                break;
            case "Egg":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                eggs.Add(ingredient);
                break;
            case "Chocolate":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                chocolates.Add(ingredient);
                break;
            default:
                Debug.LogWarning("Attempt to add invalid ingredient to mixer: " + name, gameObject);
                return;
        }
        Debug.Log("Added to mixer: " + name, gameObject);
        Combine();
    }

    public void Reset() {
        flours.ForEach(x => Destroy(x));
        flours.Clear();
        sugars.ForEach(x => Destroy(x));
        sugars.Clear();
        frostings.ForEach(x => Destroy(x));
        frostings.Clear();
        milks.ForEach(x => Destroy(x));
        milks.Clear();
        eggs.ForEach(x => Destroy(x));
        eggs.Clear();
        chocolates.ForEach(x => Destroy(x));
        chocolates.Clear();
    }

    private void Combine() {
        // var flours = new List<int>();
        // var sugars = new List<int>();
        // var frostings = new List<int>();
        // var milks = new List<int>();
        // var eggs = new List<int>();
        // var chocolates = new List<int>();

        // for(int i = 0; i < ingredients.Count; i++) {

        //     string name = ingredients[i].GetComponent<Ingredient>().ingredientName;
        //     switch(name) {
        //         case "Flour":
        //             flours.Add(i);
        //             break;
        //         case "Sugar":
        //             sugars.Add(i);
        //             break;
        //         case "Frosting":
        //             frostings.Add(i);
        //             break;
        //         case "Milk":
        //             milks.Add(i);
        //             break;
        //         case "Egg":
        //             eggs.Add(i);
        //             break;
        //         case "Chocolate":
        //             chocolates.Add(i);
        //             break;
        //         default:
        //             Debug.LogError("Invalid ingredient in mixer", gameObject);
        //             break;
        //     }
        // }

        if(frostings.Count > 0 && chocolates.Count > 0) {
            handToRobot(Instantiate(chocolateFrostingPrefab, transform.position, Quaternion.identity));

            // Destroy(ingredients[frostings[0]]);
            // ingredients.RemoveAt(frostings[0]);
            // Destroy(ingredients[chocolates[0]]);
            // ingredients.RemoveAt(chocolates[0]);

            Destroy(frostings[0]);
            frostings.RemoveAt(0);
            Destroy(chocolates[0]);
            chocolates.RemoveAt(0);

            Debug.Log("Made chocolate frosting", gameObject);
            return;

        } else if(flours.Count > 0 && sugars.Count > 0 && milks.Count > 0 && eggs.Count > 0) {
            handToRobot(Instantiate(doughPrefab, transform.position, Quaternion.identity));

            // Destroy(ingredients[flours[0]]);
            // Destroy(ingredients[sugars[0]]);
            // Destroy(ingredients[milks[0]]);
            // Destroy(ingredients[eggs[0]]);
            // ingredients.RemoveAt(milks[0]);
            // ingredients.RemoveAt(sugars[0]);
            // ingredients.RemoveAt(flours[0]);
            // ingredients.RemoveAt(eggs[0]);

            Destroy(flours[0]);
            flours.RemoveAt(0);
            Destroy(sugars[0]);
            sugars.RemoveAt(0);
            Destroy(eggs[0]);
            eggs.RemoveAt(0);
            Destroy(milks[0]);
            milks.RemoveAt(0);

            Debug.Log("Made dough", gameObject);
            return;
        }

        handToRobot(null);
    }
}
