using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Unity.Mathematics;


public class UI_Enemy_Detected_Circle : MonoBehaviour
{

    [SerializeField] private float Animation_Time = 3f;
    [SerializeField] private Ease ease;

    //float randomX, randomY;
    private bool IsBusy = false;

    
    public void Detected_Animation()
    {
        if (IsBusy == false)
        {
            // randomX = UnityEngine.Random.Range(-2f, 2f);
            // randomY = UnityEngine.Random.Range(-2f, 2f);
            //transform.localPosition = new Vector2(-2, 2);

            this.gameObject.SetActive(true);
            IsBusy = true;
            //*動畫顯示
            var sequence = DOTween.Sequence();
            sequence.Join(transform.DORotate(new Vector3(0, 0, 360), Animation_Time, RotateMode.FastBeyond360));
            sequence.OnComplete(() =>
              {
                //*動畫Reset
                  this.gameObject.SetActive(false);
                  Debug.Log("Detected_Animation All Tweens completed!");
                  IsBusy = false;
              });
        }
    }


    private void Start()
    {

        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        //*SetActive(true) 被UI_HUD啟用時

    }
}
