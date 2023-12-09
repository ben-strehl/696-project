using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BakeEvent : UnityEvent<GameObject>
{}

public class Oven : MonoBehaviour
{
    [SerializeField] private BakeEvent bakedEvent;
    [SerializeField] private GameObject cakeUnfrostedPrefab;
    /* private GameObject ingredient; */

    void Start()
    {
        bakedEvent = new BakeEvent();
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
                bakedEvent.Invoke(Instantiate(cakeUnfrostedPrefab, transform.position, Quaternion.identity));
                Debug.Log("Baked dough", gameObject);
                break;
            default:
                Debug.LogWarning("Attempt to add invalid ingredient to oven", gameObject);
                break;
        }
    }
}
