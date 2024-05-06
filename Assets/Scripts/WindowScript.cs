using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindowScript : MonoBehaviour
{

    public GameObject questionWindow;
    public TMP_Text questionText;

    public Button button1;
    public Button button2;

    // public void ShowQuestion(string question)
    // {
    //     questionText.text = question;
    //     questionWindow.SetActive(true);
    // }

    public void OnResponse(bool response)
    {
        if (response == true)
        {
            Debug.Log("User responded YES");
        }
        else
        {
            Debug.Log("User responded NO");
        }
        questionWindow.SetActive(false);
    }

    public void Show()
    {
        questionWindow.SetActive(true);
    }


    // // Start is called before the first frame update
    void Start()
    {
        // questionWindow.SetActive(false);
        Debug.Log("WindowScript Start...");
        // try
        // {
            StreamReader reader = new StreamReader("./Assets/Scripts/questions.txt");
            Question q = new Question
            {
                question = reader.ReadLine(),
                answer1 = reader.ReadLine(),
                answer2 = reader.ReadLine()
            };

            Debug.Log("Question: " + q.question);
            Debug.Log(q.question);

            questionText.text = q.question;
            button1.GetComponentInChildren<TMP_Text>().text = q.answer1;
            button2.GetComponentInChildren<TMP_Text>().text = q.answer2;
        // }
        // catch (Exception e)
        // {
        //     Debug.Log("Error reading file: " + e.Message);
        // }
    }

    private struct Question
    {
        public string question;
        public string answer1;
        public string answer2;
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
