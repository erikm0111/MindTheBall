using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
        NetworkServer.RegisterHandler(9002, Lost);
        started = true;
        isClient = false;
        Debug.Log("server started");
        scenes.shouldStartServer = false;
    }

    public void SetupClient()
    {
        isClient = true;
        myClient = new NetworkClient();
        string ipaddress = scenes.ipaddress;
        Debug.Log(ipaddress);
        myClient.Connect(ipaddress, 4444);
        myClient.RegisterHandler(9001, ClientSync);
        myClient.RegisterHandler(9002, Lost);
        started = true;
        scenes.shouldStartClient = false;
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

    public void Lost(NetworkMessage msg) {
        //Debug.Log("show lost screen");
        SceneManager.LoadScene("loss_screen");
    }

    // Use this for initialization
    void Start()
    {
        bean = new SyncBean();
    }

    void Update()
    {
        if (scenes.shouldStartServer)
        {
            Debug.Log("server start");
            SetupServer();
        }
        if (scenes.shouldStartClient)
        {
            Debug.Log("client start");
            SetupClient();
        }
        if(started)
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

    public void NotifyWin() {
      if (isClient) {
        myClient.Send(9002, new WinNotification());
      } else {
        NetworkServer.SendToAll(9002, new WinNotification());
      }

        //Debug.Log("prikazi win screen");
        SceneManager.LoadScene("win_screen");
    }

    class WinNotification : MessageBase {
      public bool information;
    }

    class SyncBean : MessageBase
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
