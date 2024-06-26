using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DeliveryTruck : MonoBehaviour
{
    private delegate void Win();
    private List<GoalItem> goals;
    private Win win;
    private TMP_Text[] goalTexts;

    void Start()
    {
        GameObject.Find("Title").GetComponent<TMP_Text>().text = "Level " + LevelGenerator.GetCurrentLevel();

        goalTexts = new TMP_Text[4];
        goalTexts[0] = GameObject.Find("Goal1").GetComponent<TMP_Text>();
        goalTexts[1] = GameObject.Find("Goal2").GetComponent<TMP_Text>();
        goalTexts[2] = GameObject.Find("Goal3").GetComponent<TMP_Text>();
        goalTexts[3] = GameObject.Find("Goal4").GetComponent<TMP_Text>();

        goalTexts[0].text = "";
        goalTexts[1].text = "";
        goalTexts[2].text = "";
        goalTexts[3].text = "";

        goals = LevelGenerator.GetCurrentGoals();

        int i = 0;
        goals.ForEach(x => {
            goalTexts[i].text = $"{x.name}: {x.count}";
            i++;
        });


        win += () => {
            PlayerPrefs.SetInt("lastLevelBeat", Math.Max(LevelGenerator.GetCurrentLevel(), PlayerPrefs.GetInt("lastLevelBeat")));
            FindObjectOfType<WinPanel>().MakeActive();
            Debug.Log("Win!");
        };
        win += Reset;
        win += FindObjectOfType<Robot>().Reset;
        win += FindObjectOfType<MixingTable>().Reset;
        win += FindObjectOfType<DecoratingTable>().Reset;
        win += FindObjectOfType<ConveyorBelt>().Reset;
        win += FindObjectOfType<PythonReader>().Reset;
        win += FindObjectOfType<PlayButton>().Reset;
        win += FindObjectOfType<SpeedupButton>().Reset;
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

                //Update the goal counters
                int i = 0;
                goals.ForEach(x => {
                    goalTexts[i].text = $"{x.name}: {x.count}";
                    i++;
                });
                return;
            }
            Debug.LogWarning("Goal for item already completed", gameObject);
            return;
        }
        Debug.LogError("Invalid item turned in");
    }

    //The robot AI uses this to determine if an ingredient is one of the goal ingredients
    public List<string> GetGoals() {
        List<string> goalList = new List<string>();
        goals.ForEach(x => {
            if(x.count > 0) {
                goalList.Add(x.name);
            }
        });
        return goalList;
    }

    public void Reset() {
        goals = LevelGenerator.GetCurrentGoals();
    }
}

public class GoalItem {
    public string name;
    public int count;
}