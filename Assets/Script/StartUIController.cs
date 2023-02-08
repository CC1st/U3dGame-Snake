using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour {

    public Text lastText;
    public Text bestText;
    public Toggle blue;
    public Toggle green;
    public Toggle border;
    public Toggle free;

    public GameObject Manual;
    public GameObject ManualImage;
    private int count=0;

    void Awake()
    {
        lastText.text = "上局：长度：" + PlayerPrefs.GetInt("lastl", 0) + " 分数：" + PlayerPrefs.GetInt("lasts", 0);
        bestText.text = "记录：长度：" + PlayerPrefs.GetInt("bestl", 0) + " 分数：" + PlayerPrefs.GetInt("bests", 0);
    }

    void Start()
    {
        if (PlayerPrefs.GetString("sh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
        else
        {
            green.isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");

        }
        if (PlayerPrefs.GetInt("border", 1) == 1)
        {
            border.isOn = true;
            PlayerPrefs.SetInt("border", 1);
            Debug.Log(1);
        }
        else
        {
            free.isOn = true;
            PlayerPrefs.SetInt("border", 0);
        }
    }

    public void BlueSelected(bool isOn)
    {
        PlayerPrefs.SetString("sh", "sh01");
        PlayerPrefs.SetString("sb01", "sb0101");
        PlayerPrefs.SetString("sb02", "sb0102");
    }
    public void GreenSelected(bool isOn)
    {
        PlayerPrefs.SetString("sh", "sh02");
        PlayerPrefs.SetString("sb01", "sb0201");
        PlayerPrefs.SetString("sb02", "sb0202");
    }
    public void BorderSelected(bool isOn)
    {
        PlayerPrefs.SetInt("border", 1);
    }
    public void FreeSelected(bool isOn)
    {
        PlayerPrefs.SetInt("border", 0);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void InstructionShow()
    {
        if (count % 2==0)
        {
            ManualImage.GetComponent<Image>().enabled = true;
            count++;
            return;
        }
        else
        {
            ManualImage.GetComponent<Image>().enabled = false;
            count--;
            return;
        }
    }
}
