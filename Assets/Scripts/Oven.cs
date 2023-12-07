using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField]private GameObject cakeUnfrostedPrefab;
    /* private GameObject ingredient; */

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Add(GameObject ingredient) {
        string name = ingredient.GetComponent<Ingredient>().ingredientName;
        switch(name) {
            case "Dough":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(ingredient);
                Instantiate(cakeUnfrostedPrefab, transform.position, Quaternion.identity);
                Debug.Log("Baked dough");
                break;
            default:
                Debug.LogWarning("Attempt to add invalid ingredient to oven");
                break;
        }
    }

    /* private void Cook() { */
    /*     Destroy(ingredients[0]); */
    /* } */
}
