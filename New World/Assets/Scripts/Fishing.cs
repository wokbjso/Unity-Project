using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    // Animator animator;

    public Slider slider;
    public float speed;
    public float minPosition;
    public float maxPosition;
    public RectTransform pass;
    public Image hitImage;
    private int hit = 0;    // 0 : space bar 누르기 전, 1 : 낚시 게임 성공, 2 : 낚시 게임 실패
    private bool isIncreased = true;

    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
        
        slider.value = 0;
        minPosition = pass.anchoredPosition.x;
        maxPosition = pass.sizeDelta.x + minPosition;

        coroutine = FishingAttack();
    }

    IEnumerator FishingAttack()
    {
        yield return null;
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            if (isIncreased)
            {          
                slider.value += speed;

                if (slider.value == slider.maxValue)
                {
                    isIncreased = false;
                }
                yield return null;
            }
            else
            {
                slider.value -=  speed;
                
                if (slider.value == 0)
                {
                    isIncreased = true;
                }
                yield return null;
            }
            

        }
        // 낚시 게임 성공
        if (Input.GetKeyDown(KeyCode.Space) && (slider.value >= minPosition && slider.value <= maxPosition))
        {
            hitImage.gameObject.SetActive(true);
            hit = 1;
            yield break;
        }
        // 낚시 게임 실패
        if (Input.GetKeyDown(KeyCode.Space) && (slider.value <= minPosition && slider.value >= maxPosition))
        {
            // hitImage.gameObject.SetActive(true);
            hit = 2;
            yield break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(coroutine);

        // 성공하면 포인트 오르고 씬 이동
        if (hit == 1)
        {
            StopCoroutine(coroutine);
        }
        // 실패하면 퀘스트 종료?
        else if(hit == 2)
        {
            StopCoroutine(coroutine);
        }
     
    }

}


