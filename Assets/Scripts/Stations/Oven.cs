using UnityEngine;

public class Oven : MonoBehaviour
{
    private delegate void HandToRobot(GameObject ingredientObj);
    [SerializeField] private GameObject cakeUnfrostedPrefab;
    private HandToRobot handToRobot;

    void Start()
    {
        handToRobot = FindObjectOfType<Robot>().WaitingForTable;
    }

    //Only action is to bake dough
    public void Add(GameObject ingredient) {
        string name = ingredient.GetComponent<Ingredient>().ingredientName;
        switch(name) {
            case "Dough":
                ingredient.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(ingredient);
                handToRobot(Instantiate(cakeUnfrostedPrefab, transform.position, Quaternion.identity));
                Debug.Log("Baked dough", gameObject);
                break;
            default:
                Debug.LogWarning("Attempt to add invalid ingredient to oven", gameObject);
                break;
        }
    }
}
