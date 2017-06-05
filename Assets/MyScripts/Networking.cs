using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{

    NetworkClient myClient;
    public Client client;
    private SyncBean bean;

    bool started;
    bool isClient;
    public Transform myBall;
    public Transform myPlatform;

    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(9000, Synced);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        started = true;
        isClient = false;
        Debug.Log("server started");
    }

    public void SetupClient()
    {
        isClient = true;
        myClient = new NetworkClient();
        myClient.Connect("127.0.0.1", 4444);
        myClient.RegisterHandler(9001, ClientSync);
        started = true;
    }

    public void OnConnected(NetworkMessage msg) {

    }

    public void SetupLocalClient()
    {
        isClient = true;
        myClient = ClientScene.ConnectLocalServer();
    }

    public void ClientSync(NetworkMessage msg) {
        client.NetworkUpdate(msg.reader.ReadVector3(), msg.reader.ReadQuaternion());
    }

    public void Synced(NetworkMessage msg)
    {
        client.NetworkUpdate(msg.reader.ReadVector3(), msg.reader.ReadQuaternion());
    }

    // Use this for initialization
    void Start()
    {
        bean = new SyncBean();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetupServer();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                SetupClient();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SetupServer();
                SetupLocalClient();
            }
        }
        else
        {
          bean.rotation = myPlatform.rotation;
          bean.position = myBall.localPosition;

          if (isClient && myClient.isConnected) {
            myClient.Send(9000, bean);
          } else {
            NetworkServer.SendToAll(9001, bean);
          }
        }
    }

    class SyncBean : MessageBase
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
