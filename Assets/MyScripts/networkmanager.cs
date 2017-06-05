using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class networkmanager : MonoBehaviour
{

    NetworkClient myClient;
    bool started;
    Vector3 pos;

    public Transform t;

    public void SetupServer()
    {
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(9000, Synced);
        started = true;
    }

    public void SetupClient()
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect("127.0.0.1", 4444);

    }

    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("connected");
    }

    public void Synced(NetworkMessage msg)
    {
        t.position = msg.reader.ReadVector3();
    }

    // Use this for initialization
    void Start()
    {
        pos = new Vector3(0f, 0f, 0f);
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
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pos += Vector3.up;
                Position p = new Position();
                p.position = pos;
                myClient.Send(9000, p);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                pos += Vector3.down;
                Position p = new Position();
                p.position = pos;
                myClient.Send(9000, p);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pos += Vector3.left;
                Position p = new Position();
                p.position = pos;
                myClient.Send(9000, p);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pos += Vector3.right;
                Position p = new Position();
                p.position = pos;
                myClient.Send(9000, p);
            }
        }
    }

    class Position : MessageBase
    {
        public Vector3 position;
    }
}
