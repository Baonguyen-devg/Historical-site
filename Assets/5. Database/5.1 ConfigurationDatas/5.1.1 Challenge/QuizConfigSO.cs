using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizConfigSO", menuName = "DataSO/Quiz")]
public class QuizConfigSO : ScriptableObject
{
    [Header("Content")]
    [SerializeField] private string _id;
    [SerializeField] private string _question;
    [SerializeField] private List<string> _answers = new List<string>();
    [SerializeField] private int _correctAnswerIndex;

    public string Id => _id;
    public string Question => _question;
    public List<string> Answers => _answers;
    public int CorrectAnswerIndex => _correctAnswerIndex;
}
