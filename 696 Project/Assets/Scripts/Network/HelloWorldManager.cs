using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        void Start() {
            NetworkManager.Singleton.StartServer();
        }
    }
}