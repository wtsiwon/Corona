using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class RankingManager : MonoBehaviour
{
    private const int MAX_RANKING_PLAYER = 5;

    [SerializeField] private List<Text> rankText;

    private void Start()
    {
        ReadRankingFile();

        MakeNewRankPlayer();
    }

    private void ReadRankingFile()
    {
        if(File.Exists("Asset/Recources/Ranking.txt") == false)
        {
            File.Exists("Asset/Recources/Ranking.txt");
        }

        var stringReader = new StringReader(Resources.Load<TextAsset>("Ranking").text);


        string name;
        int score;

        int rankPlayerCnt = 0;
        while(rankPlayerCnt < MAX_RANKING_PLAYER)
        {
            string textLine = stringReader.ReadLine();

            string[] splitText;
            splitText = (textLine == null) ? new string[2] { "NONE", "0" } : textLine.Split(',');

            name = splitText[0];
            score = int.Parse(splitText[1]);

            rankText[rankPlayerCnt].text = $"{rankPlayerCnt + 1}µî : {name} -> {score}";
            rankPlayerCnt++;
        }

    }

    private void MakeNewRankPlayer()
    {
        int dumpScore = 123;

        if (File.Exists("Assets/Resources/Ranking.txt") == false)
        {
            File.Exists("Assets/Resources/Ranking.txt");
        }

        var stringReader = new StringReader(Resources.Load<TextAsset>("Ranking").text);
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
