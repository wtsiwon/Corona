using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Ranking : MonoBehaviour
{
    int _Score;
    string names;
    [SerializeField]
    private GameObject rankbord;
    private void Start()
    {
        var stringReader = new StringReader(Resources.Load<TextAsset>("RanKing").text);
        string[] textlist = new string[5];
        for (int i = 0; i < 5; i++)
        {
            string textLine = stringReader.ReadLine();
            textlist[i] = (textLine == null) ? "NONE,0" : textLine;
        }
        for (int i = 0; i < 5; i++)
        {
            string[] txtscore = textlist[i].Split(',');
            if (int.Parse(txtscore[1]) < _Score)
            {
                for (int j = 3; j >= i; j--)
                {
                    textlist[j + 1] = textlist[j];
                }
                textlist[i] = names + "," + _Score;
                break;
            }
        }
        StreamWriter sw = new StreamWriter("Assets/Resources/RanKing.txt", false);
        rankbord.SetActive(true);
        for (int k = 0; k < 5; k++)
        {
            string[] name = new string[5];
            string[] score = new string[5];
            for (int i = 0; i < 5; i++)
            {
                string[] txt = textlist[i].Split(',');
                name[i] = txt[0];
                score[i] = txt[1];
            }
            sw.WriteLine(textlist[k]);
            rankbord.transform.GetChild(k).GetComponent<Text>().text = ($"{k + 1}µî");
            rankbord.transform.GetChild(k).transform.GetChild(0).GetComponent<Text>().text = name[k];
            rankbord.transform.GetChild(k).transform.GetChild(1).GetComponent<Text>().text = score[k];
        }
        sw.Close();
    }
}
