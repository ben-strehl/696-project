using UnityEngine;

public class OvenTester : MonoBehaviour
{
    [SerializeField]private GameObject doughPrefab;

    //Add ingredients to mixingTable to test it
    void Start()
    {
        GameObject dough = Instantiate(doughPrefab);

        var oven = (Oven)FindObjectOfType(typeof(Oven));

        oven.Add(dough);
    }

    void Update()
    {
        
    }
}
