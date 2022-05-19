using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private GameObject Background;
    
    private string[] Item = { "Tim", "Vàng", "Máu", "Gậy","Áo","Quần","Tóc","Mũ","Thắt lưng"};

    [SerializeField] public int[] SpinRoundRange = {2, 5 };

    [SerializeField] public int SpinTime = 3;

    [SerializeField]  public AnimationCurve Curve;

    private int BackgroundDefaultRotage = 10;

    AudioSource Audio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10)
            {
                Debug.Log(number);
                StartCoroutine(RotateToItem(number));
            }
        }
    }

    IEnumerator RotateToItem(int Index)
    {
        float Start = Background.transform.eulerAngles.z;
        float CurrentTime = 0;

        int ItemSize = 360 / Item.Length;
        int SpinRound = UnityEngine.Random.Range(SpinRoundRange[0], SpinRoundRange[1]);

        Debug.Log(SpinRound);

        float Target = (SpinRound * 360) + ((ItemSize * Index) - (ItemSize / 2) + BackgroundDefaultRotage) - Start;

        while (CurrentTime < SpinTime)
        {
            yield return new WaitForEndOfFrame();

            CurrentTime += Time.deltaTime;

            float Current = Target * Curve.Evaluate(CurrentTime / SpinTime);

            Background.transform.eulerAngles = new Vector3(0, 0, Current + Start);
            
            if (!Audio.isPlaying)
            {
                Audio.Play(0);
            }
        }

    }
}
