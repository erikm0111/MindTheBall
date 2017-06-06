using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btnstartgame : MonoBehaviour
{

    public Button btnPlay;

    // Use this for initialization
    void Start()
    {
        Button btn = btnPlay.GetComponent<Button>();
        btn.onClick.AddListener(LoadMenu);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("realDeal");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
