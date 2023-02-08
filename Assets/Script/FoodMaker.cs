using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour {

    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }
    public int xlimit = 11;
    public int ylimit = 7;
    public int xoffset = 6;
    public GameObject FoodPrefab;
    public GameObject RewardPrefab;
    public Sprite[] foodSprite;
    private Transform foodHolder;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        foodHolder = GameObject.FindGameObjectWithTag("FoodHolder").transform;
        MakeFood(false);
    }

    public void MakeFood(bool isreward)
    {
        int index = Random.Range(0, foodSprite.Length);
        GameObject food = Instantiate(FoodPrefab);
        food.GetComponent<Image>().sprite= foodSprite[index];
        food.transform.SetParent(foodHolder, false);  //继承父级的位置，以及是否重置大小？     
        int x = Random.Range(-196, 353);
        int y = Random.Range(-146, 142);
        food.transform.localPosition = new Vector3(x, y, 0);
        if (isreward)
        {
            GameObject reward = Instantiate(RewardPrefab);
            reward.transform.SetParent(foodHolder, false);  //继承父级的位置，以及是否重置大小？     
             x = Random.Range(-196, 353);
             y = Random.Range(-146, 142);
            reward.transform.localPosition = new Vector3(x, y, 0);
        }
    }   
}
