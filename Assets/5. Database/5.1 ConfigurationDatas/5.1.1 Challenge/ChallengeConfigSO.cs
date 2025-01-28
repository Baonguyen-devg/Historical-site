using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChallengeConfigSO", menuName = "DataSO/Challenge")]
public class ChallengeConfigSO : ScriptableObject
{
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private float _price;
    [SerializeField] private List<QuizConfigSO> _quizConfigSOs= new List<QuizConfigSO>();

    public string Id => _id;
    public string Name => _name;
    public float Price => _price;
    public List<QuizConfigSO> QuizConfigSOs => _quizConfigSOs;
}
