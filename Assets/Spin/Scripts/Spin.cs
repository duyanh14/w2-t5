using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spin : MonoBehaviour
{
    [SerializeField] private GameObject Background;
    
    private string[] Item = { "Tim", "Vàng", "Máu", "Gậy","Áo","Quần","Tóc","Mũ","Thắt lưng"};

    [SerializeField] public int[] SpinRoundRange = {2, 5 };

    [SerializeField] public int SpinTime = 3;

    [SerializeField]  public AnimationCurve Curve;

    private int BackgroundDefaultRotage = 10;

    AudioSource Audio;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

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

        Audio.Stop();

        SelectItem(Index);

        yield break;
    }

    void SelectItem(int Index)
    {
        Debug.Log("Bạn đã nhận được " + Item[Index-1] + "*" + Index);

        SceneManager.LoadScene("Level "+Index, LoadSceneMode.Single);

    }
}
