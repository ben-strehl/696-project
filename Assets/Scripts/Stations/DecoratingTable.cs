using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoratingTable : MonoBehaviour
{
    private delegate void HandToRobot(GameObject ingredientObj);
    [SerializeField] private GameObject cakePrefab;
    [SerializeField] private GameObject cakeSprinklesPrefab;
    [SerializeField] private GameObject chocolateCakePrefab;
    [SerializeField] private GameObject chocolateCakeSprinklesPrefab;
    private List<GameObject> frostings;
    private List<GameObject> chocolateFrostings;
    private List<GameObject> sprinkles;
    private List<GameObject> unfrostedCakes;
    private List<GameObject> cakes;
    private List<GameObject> chocolateCakes;
    private HandToRobot handToRobot;
    void Start()
    {
        chocolateFrostings = new List<GameObject>();
        frostings = new List<GameObject>();
        sprinkles = new List<GameObject>();
        unfrostedCakes = new List<GameObject>();
        cakes = new List<GameObject>();
        chocolateCakes = new List<GameObject>();

        handToRobot = FindObjectOfType<Robot>().WaitingForTable;
    }

    public void Add(GameObject ingredient) {
        string name = ingredient.GetComponent<Ingredient>().ingredientName;

        //We hide the ingredients once they're on the table
        switch(name) {
            case "Frosting":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                frostings.Add(ingredient);
                break;
            case "Chocolate Frosting":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                chocolateFrostings.Add(ingredient);
                break;
            case "Cake (Unfrosted)":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                unfrostedCakes.Add(ingredient);
                break;
            case "Sprinkles":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                sprinkles.Add(ingredient);
                break;
            case "Cake":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                cakes.Add(ingredient);
                break;
            case "Chocolate Cake":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                chocolateCakes.Add(ingredient);
                break;
            default:
                Debug.LogWarning("Attempt to add invalid ingredient to decorator: " + name, gameObject);
                return;
        }
        Debug.Log("Added to decorator: " + name, gameObject);
        Combine();
    }

    public void Reset() {
        unfrostedCakes.ForEach(x => Destroy(x));
        unfrostedCakes.Clear();
        sprinkles.ForEach(x => Destroy(x));
        sprinkles.Clear();
        frostings.ForEach(x => Destroy(x));
        frostings.Clear();
        chocolateFrostings.ForEach(x => Destroy(x));
        chocolateFrostings.Clear();
        cakes.ForEach(x => Destroy(x));
        cakes.Clear();
        chocolateCakes.ForEach(x => Destroy(x));
        chocolateCakes.Clear();
    }

    //We can frost and sprinkle at the same time, just frost, or just sprinkle
    private void Combine() {
        if(frostings.Count > 0 && unfrostedCakes.Count > 0 && sprinkles.Count > 0) {
            handToRobot(Instantiate(cakeSprinklesPrefab, transform.position, Quaternion.identity));

            Destroy(frostings[0]);
            frostings.RemoveAt(0);
            Destroy(unfrostedCakes[0]);
            unfrostedCakes.RemoveAt(0);
            Destroy(sprinkles[0]);
            sprinkles.RemoveAt(0);

            Debug.Log("Made cake with sprinkles", gameObject);
            return;

        } else if(chocolateFrostings.Count > 0 && unfrostedCakes.Count > 0 && sprinkles.Count > 0) {
            handToRobot(Instantiate(chocolateCakeSprinklesPrefab, transform.position, Quaternion.identity));

            Destroy(chocolateFrostings[0]);
            chocolateFrostings.RemoveAt(0);
            Destroy(unfrostedCakes[0]);
            unfrostedCakes.RemoveAt(0);
            Destroy(sprinkles[0]);
            sprinkles.RemoveAt(0);

            Debug.Log("Made chocolate cake with sprinkles", gameObject);
            return;

        } else if(sprinkles.Count > 0 && cakes.Count > 0) {
            handToRobot(Instantiate(cakeSprinklesPrefab, transform.position, Quaternion.identity));

            Destroy(sprinkles[0]);
            sprinkles.RemoveAt(0);
            Destroy(cakes[0]);
            cakes.RemoveAt(0);


            Debug.Log("Made cake with sprinkles", gameObject);
            return;
        } else if(sprinkles.Count > 0 && chocolateCakes.Count > 0) {
            handToRobot(Instantiate(chocolateCakeSprinklesPrefab, transform.position, Quaternion.identity));

            Destroy(sprinkles[0]);
            sprinkles.RemoveAt(0);
            Destroy(chocolateCakes[0]);
            chocolateCakes.RemoveAt(0);


            Debug.Log("Made chocolate cake with sprinkles", gameObject);
            return;
        } else if(chocolateFrostings.Count > 0 && unfrostedCakes.Count > 0) {
            handToRobot(Instantiate(chocolateCakePrefab, transform.position, Quaternion.identity));

            Destroy(chocolateFrostings[0]);
            chocolateFrostings.RemoveAt(0);
            Destroy(unfrostedCakes[0]);
            unfrostedCakes.RemoveAt(0);

            Debug.Log("Made chocolate cake", gameObject);
            return;

        } else if(frostings.Count > 0 && unfrostedCakes.Count > 0) {
            handToRobot(Instantiate(cakePrefab, transform.position, Quaternion.identity));

            Destroy(frostings[0]);
            frostings.RemoveAt(0);
            Destroy(unfrostedCakes[0]);
            unfrostedCakes.RemoveAt(0);


            Debug.Log("Made cake", gameObject);
            return;
        } 

        handToRobot(null);
    }
}
