using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour {

    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }

    //计算值
    public int score = 0;
    public int length = 0;
    //显示值
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public Image bgImg;
    private Color tempColor;
    public bool Hasborder = true;

    public Image pauseImage;
    public bool isPause = false;
    public Sprite[] pauseSprites;
    private AudioSource BGM;


    void Awake()
    {
        _instance = this;
    }
    
    void Start()
    {
        BGM=gameObject.GetComponent<AudioSource>();
        BGM.Play();
        if (PlayerPrefs.GetInt("border", 1) == 0)
        {
            Debug.Log(Hasborder);
            Hasborder = false;
            foreach (Transform t in bgImg.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;  //边界不可见
            }
        }
    }
    void Update()
    {
        switch (score / 100)
        {
            case 0:
            case 1:
            case 2:
                break;
            case 3:
            case 4:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tempColor);
                bgImg.color = tempColor;
                msgText.text = "阶段 " + 2;
                break;
            case 5:
            case 6:
                ColorUtility.TryParseHtmlString("#CCFFDBFF", out tempColor);
                bgImg.color = tempColor;
                msgText.text = "阶段 " + 3;
                break;
            case 7:
            case 8:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tempColor);
                bgImg.color = tempColor;
                msgText.text = "阶段 " + 4;
                break;
            case 9:
            case 10:
                ColorUtility.TryParseHtmlString("#FFF3CCFF", out tempColor);
                bgImg.color = tempColor;
                msgText.text = "阶段 " + 5;
                break;
            default:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tempColor);
                bgImg.color = tempColor;
                msgText.text = "无尽模式";
                break;
        }
    }
    public void UpdateUI(int s=5,int l = 1)
    {
        score += s;
        length += l;
        scoreText.text = "得分：\n" + score;
        lengthText.text = "长度：\n" + length;
    }

    public void Pause()
    {
        isPause = !isPause;
        if (isPause)
        {
            BGM.Pause();
            Time.timeScale = 0;  //时间冻结
            pauseImage.sprite = pauseSprites[1];
        }
        else
        {
            BGM.Play();
            Time.timeScale = 1;  //时间运转
            pauseImage.sprite = pauseSprites[0];
        }
    }

    public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
