using UnityEngine;

public class MixingTableTester : MonoBehaviour
{
    [SerializeField]private GameObject eggPrefab;
    [SerializeField]private GameObject milkPrefab;
    [SerializeField]private GameObject sugarPrefab;
    [SerializeField]private GameObject flourPrefab;
    [SerializeField]private GameObject frostingPrefab;
    [SerializeField]private GameObject chocolatePrefab;

    //Add ingredients to mixingTable to test it
    void Start()
    {
        GameObject egg = Instantiate(eggPrefab);
        GameObject milk = Instantiate(milkPrefab);
        GameObject sugar = Instantiate(sugarPrefab);
        GameObject flour = Instantiate(flourPrefab);

        var table = (MixingTable)FindObjectOfType(typeof(MixingTable));

        table.Add(egg);
        table.Add(milk);
        table.Add(sugar);
        table.Add(flour);
    }
}
