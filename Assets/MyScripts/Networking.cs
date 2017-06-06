﻿using UnityEngine;
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
        NetworkServer.RegisterHandler(9002, Lost);
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
        myClient.RegisterHandler(9002, Lost);
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

    public void Lost(NetworkMessage msg) {
        Debug.Log("show lost screen");
    }

    // Use this for initialization
    void Start()
    {
        bean = new SyncBean();
    }

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

    public void NotifyWin() {
      if (isClient) {
        myClient.Send(9002, new WinNotification());
      } else {
        NetworkServer.SendToAll(9002, new WinNotification());
      }

      Debug.Log("prikazi win screen");
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
