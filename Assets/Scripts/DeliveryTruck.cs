using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class DeliveryTruck : MonoBehaviour
{
    private delegate void Win();
    private List<GoalItem> goals;
    private List<GoalItem> savedGoals;
    private Win win;

    void Start()
    {
        goals = new List<GoalItem>();
        goals.Add(new GoalItem { name = "Egg", count = 1});
        savedGoals = new List<GoalItem>();
        goals.ForEach(x => savedGoals.Add(new GoalItem {name = x.name, count = x.count}));

        win += () => Debug.Log("Win!");
        win += Reset;
        win += FindObjectOfType<Robot>().Reset;
        win += FindObjectOfType<Oven>().Reset;
        win += FindObjectOfType<MixingTable>().Reset;
        win += FindObjectOfType<ConveyorBelt>().Reset;
    }

    void LateUpdate() {
        if(!goals.Exists(x => x.count > 0)) {
            win();
        }
    }

    public void TurnIn(GameObject ingredient) {
        Ingredient ingComp = ingredient.GetComponent<Ingredient>();
        GoalItem item = goals.Find(x => x.name == ingComp.ingredientName);
        if(item != null) {
            if(item.count != 0) {
                Destroy(ingredient);
                item.count--;
                return;
            }
            Debug.LogWarning("Goal for item already completed", gameObject);
            return;
        }
        Debug.LogError("Invalid item turned in");
    }

    public List<string> GetGoals() {
        List<string> goalList = new List<string>();
        goals.ForEach(x => goalList.Add(x.name));
        return goalList;
    }

    public void Reset() {
        goals.Clear();
        savedGoals.ForEach(x => goals.Add(new GoalItem {name = x.name, count = x.count}));
    }
}

public class GoalItem {
    public string name;
    public int count;
}