using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckpointConfigSO", menuName = "DataSO/Checkpoint")]
public class CheckpointConfigSO : ScriptableObject
{
    [Header("Informations")]
    [SerializeField] private string _id;
    [SerializeField] private string _name;
    [SerializeField] private string _address;
    [SerializeField, TextArea] private string _description;
    [SerializeField, TextArea] private string _story;

    public string Id => _id;
    public string Name => _name;
    public string Address => _address;
    public string Description => _description;
    public string Story => _story;
   
    [Header("Coordinate")]
    [SerializeField] private float _xCoordinate;
    [SerializeField] private float _yCoordinate;
    [SerializeField] private float _zCoordinate;

    public float XCoordinate => _xCoordinate;
    public float YCoordinate => _yCoordinate;
    public float ZCoordinate => _zCoordinate;

    [SerializeField] private List<Sprite> _pictures = new List<Sprite>();
    [SerializeField] private List<NPCConversationConfigSO> _conversations = new List<NPCConversationConfigSO>();
    [SerializeField] private List<ChallengeConfigSO> _challenges = new List<ChallengeConfigSO>();

    public List<Sprite> Pictures => _pictures;
    public List<NPCConversationConfigSO> Conversations => _conversations;
    public List<ChallengeConfigSO> Challenges => _challenges;
}
