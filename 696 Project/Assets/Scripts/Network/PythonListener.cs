using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Collections.Generic;
using static PlayerController;

public class PythonListener : MonoBehaviour
{
    [SerializeField] private int connectionPort = 25001;
    private Thread thread;
    private TcpListener server;
    private TcpClient client;
    private bool running;
    private static PlayerController player;
    private static Queue<string> commandQueue = new Queue<string>();

    void Start()
    {
        // Receive on a separate thread so Unity doesn't freeze waiting for data
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void GetData()
    {
        // Create the server
        server = new TcpListener(IPAddress.Any, connectionPort);
        server.Start();

        // Create a client to get the data stream
        client = server.AcceptTcpClient();

        // Start listening
        running = true;
        while (running)
        {
            try{
                Connection();
            } catch(SocketException) {
                Debug.Log("Go fuck yourself");
                client = server.AcceptTcpClient();
            }
        }
        server.Stop();
    }

    void Connection()
    {
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into a string
        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        
        // Make sure we're not getting an empty string
        if (dataReceived != null && dataReceived != "")
        {
            SendToPlayer(dataReceived);
            nwStream.Write(buffer, 0, bytesRead);
        }
    }

    public static void SendToPlayer(string dataString)
    {
        Debug.Log(dataString);

        if (dataString.StartsWith("(") && dataString.EndsWith(")"))
        {
            dataString = dataString.Substring(1, dataString.Length - 2);
        }

        // Split the elements into an array
        string[] stringArray = dataString.Split(' ');
        List<string> args = new List<string>();

        switch(stringArray[0]) {
            case "forward":
                args.Add(stringArray[1]);
                player.EnqueueAction(new Action(ActionType.Forward, args.ToArray()));
                break;
            case "turn":
                args.Add(stringArray[1]);
                player.EnqueueAction(new Action(ActionType.Turn, args.ToArray()));
                break;
            default:
                Debug.Log("Invalid command received");
                break;
        }
    }

    void Update()
    {
        // string dataString = "";
        // if(commandQueue.TryDequeue(out dataString)) {
        //     // Remove the parentheses
        //     if (dataString.StartsWith("(") && dataString.EndsWith(")"))
        //     {
        //         dataString = dataString.Substring(1, dataString.Length - 2);
        //     }

        //     // Split the elements into an array
        //     string[] stringArray = dataString.Split(' ');

        //     switch(stringArray[0]) {
        //         case "forward":
        //             player.Forward(int.Parse(stringArray[1]));
        //             break;
        //         case "turn":
        //             player.Turn(stringArray[1]);
        //             break;
        //         default:
        //             Debug.Log("Invalid command received");
        //             break;
        //     }
        // }
    }
}