using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SiteInformationConfigSO", menuName = "DataSO/SiteInformation")]
public class SiteInformationConfigSO : ScriptableObject
{
    [Header("Coordinate")]
    [SerializeField] private float _xCoordinate;
    [SerializeField] private float _yCoordinate;
    [SerializeField] private float _zCoordinate;

    public float XCoordinate => _xCoordinate;
    public float YCoordinate => _yCoordinate;
    public float ZCoordinate => _zCoordinate;

    [Header("Informations")]
    [SerializeField] private string _name;
    [SerializeField] private string _address;
    [SerializeField, TextArea] private string _sescription;
    [SerializeField, TextArea] private string _story;

    [SerializeField] private List<Sprite> _pictures = new List<Sprite>();
    [SerializeField] private List<NPCConversationConfigSO> _conversations = new List<NPCConversationConfigSO>();
    [SerializeField] private List<ChallengeConfigSO> _challenges = new List<ChallengeConfigSO>();

    public List<Sprite> Pictures => _pictures;
    public List<NPCConversationConfigSO> Conversations => _conversations;
    public List<ChallengeConfigSO> Challenges => _challenges;
}
