using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {

    public List<Transform> bodyList = new List<Transform>();
    public float velocity=0.35f;
    public int step;  //步长
    private int x;   //x轴移动量
    private int y;  //y轴移动量
    private Vector3 HeadPos;
    private Transform canvas;
    private bool isDie = false;
    public GameObject DieEffect;

    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];

    public AudioClip EatClip;
    public AudioClip DieClip;
    private Vector3 Clippoint=new Vector3(0,0,-20);

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas").transform;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh", "sh01"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01", "sb0101"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0102"));
    }
	void Start () {
        InvokeRepeating("Move", 0, velocity);
        x = step;y = 0;
	}
	
	
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space) && MainUIController.Instance.isPause == false&&isDie==false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }
        if(Input.GetKeyUp(KeyCode.Space)&& MainUIController.Instance.isPause == false&&isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }
        if (Input.GetKey(KeyCode.W)&& y!=step&& MainUIController.Instance.isPause == false&&isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            x = 0;y = step;
        }
        if (Input.GetKey(KeyCode.A) &&x!=-step&& MainUIController.Instance.isPause == false&&isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step; y = 0;
        }
        if (Input.GetKey(KeyCode.D) &&x!=step&& MainUIController.Instance.isPause == false&&isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step; y = 0;
        }
        if (Input.GetKey(KeyCode.S)&&y!=-step && MainUIController.Instance.isPause == false&&isDie==false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0; y = -step;
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(DieClip, Clippoint);
        CancelInvoke();
        isDie = true;
        GameObject Dieeffect=Instantiate(DieEffect);
        Dieeffect.transform.SetParent(canvas, false);
        Dieeffect.transform.localPosition = gameObject.transform.localPosition;
        PlayerPrefs.SetInt("lastl", MainUIController.Instance.length);
        PlayerPrefs.SetInt("lasts", MainUIController.Instance.score);
        if (PlayerPrefs.GetInt("bests", 0) < MainUIController.Instance.score)
        {
            PlayerPrefs.SetInt("bestl", MainUIController.Instance.length);
            PlayerPrefs.SetInt("bests", MainUIController.Instance.score);
        }
        gameObject.GetComponent<Image>().enabled = false;
        StartCoroutine(GameOver(2f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    void Move()
    {
        HeadPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(HeadPos.x + x, HeadPos.y + y, HeadPos.z);
        if (bodyList.Count > 0)
        {

          for(int i = bodyList.Count - 2; i>=0; i--)
         {
                bodyList[i + 1].localPosition= bodyList[i].localPosition;  //前面覆盖后面
          }
            bodyList[0].localPosition = HeadPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "food")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            FoodMaker.Instance.MakeFood((Random.Range(0, 100 )< 20)?true:false);
        }
        else if (collision.tag == "reward")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(5,15)*10);
            Grow();
        }
        else if (collision.tag == "body")
        {
            Die();
        }
        else
        {
            if (MainUIController.Instance.Hasborder)
            {     
                Die();
            }
            else
            {
                Debug.Log(MainUIController.Instance.Hasborder);
                switch (collision.gameObject.name)
                {
                    case "Up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 20, transform.localPosition.z);
                        break;
                    case "Down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 20, transform.localPosition.z);
                        break;
                    case "Right":
                        transform.localPosition = new Vector3(-196, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "Left":
                        transform.localPosition = new Vector3(353, transform.localPosition.y, transform.localPosition.z);
                        break;
                }
            }
        }
    }

    void Grow()
    {
        AudioSource.PlayClipAtPoint(EatClip, Clippoint);
        int index = (bodyList.Count % 2 == 0) ? 0 : 1;
        GameObject body = Instantiate(bodyPrefab, new Vector3(2000, 2000, 0), Quaternion.identity);
        body.GetComponent<Image>().sprite = bodySprites[index];
        body.transform.SetParent(canvas, false);
        bodyList.Add(body.transform);
    }

}
