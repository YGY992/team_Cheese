using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Text text;
    public SpriteRenderer sprite;
    public Text button_text;

    public List<string> contentsList;
    public List<Sprite> spriteList;

    public GameObject ingameUiPanel;
    public GameObject DialoguePanel;

    public PlayerControl playerControl;

    public int count; // ��ȭ �����Ȳ ǥ�ÿ�, Ȯ�� �� private �� ���� �ʿ�

    public bool dialogue_continue = false;

    public bool is_talking = false;

    public MiniGame minigame;

    private void Start()
    {
        text.text = "";
        count = 0;
    }

    public void ShowDialogue(Dialogue dialogue) // dlalogue�� sprite������ contents ������ �޾ƿ��� �Լ�
    {
        for (int i = 0; i < dialogue.contents.Length; i++)
        {
            contentsList.Add(dialogue.contents[i]);
            spriteList.Add(dialogue.sprites[i]);

        }
        dialogue_continue = true;
        button_text.text = "����";
        ingameUiPanel.SetActive(false);
        DialoguePanel.SetActive(true);
        StartCoroutine(startDialogueCoroutine());
        playerControl.isMove = false;
    }

    public void ExitDialogue()
    {
        DialoguePanel = transform.GetChild(0).gameObject;
        text.text = "";
        contentsList.Clear();
        spriteList.Clear();
        count = 0;
        ingameUiPanel.SetActive(true);
        DialoguePanel.SetActive(false);
        dialogue_continue = false;
        playerControl.isMove = true;
    }

    IEnumerator startDialogueCoroutine()
    {
        if (count == 0)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];
            is_talking = true;
            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(0.03f);
            }
            is_talking = false;
        }

        if (count != 0) //�ε��� ������ ���� 0�϶��� �ƴҶ� ����
        {
            if (spriteList[count] != spriteList[count - 1])
            {
                sprite.GetComponent<SpriteRenderer>().sprite = spriteList[count];
            }
            is_talking = true;
            for (int i = 0; i < contentsList[count].Length; i++)
            {
                text.text += contentsList[count][i];
                yield return new WaitForSeconds(0.03f);
            }
            is_talking = false;
        }


        yield return null;
    }

    public void NextSentence()
    {
        if (dialogue_continue && is_talking == false)
        {
            if (count == contentsList.Count - 2)
            {
                button_text.text = "�ݱ�";
            }
            count++;
            text.text = "";
            if (count == contentsList.Count)
            {
                StopCoroutine(startDialogueCoroutine());
                ExitDialogue();
            }
            else
            {
                StopCoroutine(startDialogueCoroutine());
                StartCoroutine(startDialogueCoroutine());
            }
        }
    }

    private void Update()
    {
        if (dialogue_continue && is_talking == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (count == contentsList.Count - 2)
                {
                    button_text.text = "�ݱ�";
                }
                count++;
                text.text = "";
                if (count == contentsList.Count)
                {
                    StopCoroutine(startDialogueCoroutine());
                    ExitDialogue();
                }
                else
                {
                    StopCoroutine(startDialogueCoroutine());
                    StartCoroutine(startDialogueCoroutine());
                }
            }

           
        }
    }

    //Ȥ�ó� ���׳� �� �����ؼ� ����� �ڵ�...

    //public void NextSentence()
    //{
    //    if (dialogue_continue)
    //    {
    //        count++;
    //        text.text = "";
    //        if (count == contentsList.Count - 1)
    //        {
    //            StopCoroutine(startDialogueCoroutine());
    //        }
    //        else
    //        {
    //            StopCoroutine(startDialogueCoroutine());
    //            StartCoroutine(startDialogueCoroutine());
    //        }
    //    }        
    //}

    //private void Update()
    //{
    //    if (dialogue_continue)
    //    {
    //        if(Input.GetKeyDown(KeyCode.Z))
    //        {
    //            //count++;
    //            //text.text = "";
    //            //if (count == contentsList.Count)
    //            //{
    //            //    StopCoroutine(startDialogueCoroutine());
    //            //    ExitDialogue();

    //            //}
    //            //else
    //            //{
    //            //    StopCoroutine(startDialogueCoroutine());
    //            //    StartCoroutine(startDialogueCoroutine());
    //            //}
    //        }
    //    }
    //}
}