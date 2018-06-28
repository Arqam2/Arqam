using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	public int maxSize;
	public int currentSize;
	public int xBound;
	public int yBound;
	public int score;
	public GameObject foodPrefab;
	public GameObject currentFood;
    public GameObject snakePrefab;
    public Snake head;
    public Snake tail;
    public int URDL;
    public Vector2 nextPos;
	 //Use this for initialization
	void OnEnable()
	{
		Snake.hit += hit;
	}
	void Start () {
        InvokeRepeating("TimerInvoke", 0, 0.2f);
		FoodFunction ();
	
	}
	//void OnDisable()
	//{
	//	Snake.hit -= hit;
	//}
	// Update is called once per frame
	void Update () {
        ComChangeD();
       // InvokeRepeating("TimerInvoke", 0, 0.5f);
	}
    void TimerInvoke()
    {
        Movement();
		if (currentSize >= maxSize) {
			TailFunction ();
		} else {
			currentSize++;
		}
    }
    void Movement()
    {
        GameObject temp;
        nextPos = head.transform.position;
        switch(URDL)
        {
            case 0:
                nextPos = new Vector2(nextPos.x, nextPos.y + 1);
                break;
            case 1:
                nextPos = new Vector2(nextPos.x+1, nextPos.y);
                break;
            case 2:
                nextPos = new Vector2(nextPos.x, nextPos.y - 1);
                break;
            case 3:
                nextPos = new Vector2(nextPos.x-1, nextPos.y );
                break;
        }
        temp = (GameObject)Instantiate( snakePrefab,nextPos, transform.rotation);
        head.Setnext(temp.GetComponent<Snake>());
        head = temp.GetComponent<Snake>();
        return;
    }
    void ComChangeD()
    {
        if(URDL!=2 &&Input.GetKey(KeyCode.W))
        {
            URDL = 0;
        }
        if (URDL != 3 && Input.GetKey(KeyCode.D))
        {
            URDL = 1;
        }
        if (URDL != 0 && Input.GetKey(KeyCode.S))
        {
            URDL = 2;
        }
        if (URDL != 1 && Input.GetKey(KeyCode.A))
        {
            URDL = 3;
        }


    }
	void TailFunction()
	{
		Snake tempSnake = tail;
		tail = tail.GetNext ();
		tempSnake.RemoveTail ();
	}
	void FoodFunction()
	{
		int xPos = Random.Range (-xBound, xBound);
		int yPos = Random.Range (-yBound, yBound);

		currentFood = (GameObject)Instantiate(foodPrefab,new Vector2(xPos, yPos),transform.rotation);
		StartCoroutine(CheckRender (currentFood));
	}
	IEnumerator CheckRender(GameObject INi)
	{
		yield return new WaitForEndOfFrame ();
		if (INi.GetComponent<Renderer> ().isVisible == false) 
		{
			if(INi.tag=="Food")
			{
				Destroy(INi);
				FoodFunction();
			}
		}
	}
	void hit(string WhatWasSent)
	{
		if (WhatWasSent == "Food") {
			FoodFunction();
			maxSize++;
			score++;
		}
	}
}
