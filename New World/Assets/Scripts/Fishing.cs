using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{
    // Animator animator;

    public Slider slider;
    public int speed;
    public float minPosition;
    public float maxPosition;
    public RectTransform pass;
    public Image hitImage;


    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
        hitImage = GetComponent<Image>();
        slider.value = 0;
        minPosition = pass.anchoredPosition.x;
        maxPosition = pass.sizeDelta.x + minPosition;
    }

    IEnumerator FishingAttack()
    {
        yield return null;
        while (!(Input.GetKeyDown(KeyCode.Space) || (slider.value == slider.maxValue)))
        {
            slider.value += Time.deltaTime * speed;
            yield return null;

        }
        if (Input.GetKeyDown(KeyCode.Space) && (slider.value >= minPosition && slider.value <= maxPosition))
        {
            hitImage.enabled = true;
            yield break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FishingAttack());
  
     
    }

}


