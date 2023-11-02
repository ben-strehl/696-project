using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PocketPython;
using static PlayerController;

public class PlayerConnector : MonoBehaviour
{
    private static PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Debug.Log("Found player");
    }

    public void Forward(string count) {
        player.EnqueueAction(new Action(ActionType.Forward, new string[] {count}));
    }

    public void Turn(string direction) {
        player.EnqueueAction(new Action(ActionType.Forward, new string[] {direction}));
    }

    public void Interact() {
        player.EnqueueAction(new Action(ActionType.Interact, new string[] {""}));
    }

}

public class PyPlayerConnector : PyTypeObject 
{
    public override string Name => "player_connector";
    public override System.Type CSType => typeof(PlayerConnector);

    [PythonBinding]
    public void forward(PlayerConnector connector, object count) {
        if(count is string) 
            connector.Forward(count.ToString());
    }

    [PythonBinding]
    public void turn(PlayerConnector connector, object count) {
        if(count is string) 
            connector.Turn(count.ToString());
    }

    [PythonBinding]
    public void interact(PlayerConnector connector) {
        connector.Interact();
    }
}