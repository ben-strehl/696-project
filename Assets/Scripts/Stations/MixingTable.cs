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

    public void Add(GameObject ingredient) {
        string name = ingredient.GetComponent<Ingredient>().ingredientName;

        //We hide the ingredients once they're on the table
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

    public bool HasChocolate() {
        return chocolates.Count > 0;
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

    //Two recipes: chocolate frosting and dough
    private void Combine() {
        if(frostings.Count > 0 && chocolates.Count > 0) {
            handToRobot(Instantiate(chocolateFrostingPrefab, transform.position, Quaternion.identity));

            Destroy(frostings[0]);
            frostings.RemoveAt(0);
            Destroy(chocolates[0]);
            chocolates.RemoveAt(0);

            Debug.Log("Made chocolate frosting", gameObject);
            return;

        } else if(flours.Count > 0 && sugars.Count > 0 && milks.Count > 0 && eggs.Count > 0) {
            handToRobot(Instantiate(doughPrefab, transform.position, Quaternion.identity));

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
