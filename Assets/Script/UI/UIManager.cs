using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    private FadeManager fadeManager;

    public GameObject CameraUI;
    public GameObject Pause_UI;
    public GameObject SettingUI;

    public Image fadeImage;

    //카메라 UI 효과
    public Animator Camera_Effect_Animation;

    //튜토리얼 맵 변수
    public static bool tutorialTrigger = false;
    public static bool is_bear = false;
    public static bool is_cake = false;
    public static bool is_playerPos = false;

    //Stage1 변수
    public static bool is_NPC = false;

    public void CameraEffect(bool isCameraEffect)
    {
        if (Camera_Effect_Animation.gameObject.activeInHierarchy)
        {
            switch (isCameraEffect)
            {
                case false: Camera_Effect_Animation.Play("Camera_Effect_false"); break;
                case true: Camera_Effect_Animation.Play("Camera_Effect_true"); break;
            }
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pause_UI.activeSelf)
            {
                Pause_UI.SetActive(true);
                Time.timeScale = 0;
            }

            else if (Pause_UI.activeSelf)
            {
                Pause_UI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void SettingButton()
    {
        SettingUI.SetActive(true);
    }

    public void GameExit()
    {
        Pause_UI.SetActive(false);
        StartCoroutine(fadeManager.ChangeSceneFade("MainScene"));
        Time.timeScale = 1;
    }

    public void Close()
    {
        Pause_UI.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        Pause();

        if (is_bear && is_cake && is_playerPos) tutorialTrigger = true;
        else tutorialTrigger = false;

        if (tutorialTrigger || is_NPC) //카메라 UI 반짝임 애니메이션을 제어하는 조건문 , 스테이지 추가 할 때 마다 || 연산자를 사용하여 조건식에 추가 해 줄것.
        {
            CameraEffect(true);
            MiniGame.is_take_photo = true;
        }

        else
        {
            CameraEffect(false);
            MiniGame.is_take_photo = false;
        }
    }

    void Start()
    {
        fadeManager = FindFirstObjectByType<FadeManager>();

        Time.timeScale = 1;
        fadeImage.gameObject.SetActive(true);
        CameraEffect(false);
    }
}