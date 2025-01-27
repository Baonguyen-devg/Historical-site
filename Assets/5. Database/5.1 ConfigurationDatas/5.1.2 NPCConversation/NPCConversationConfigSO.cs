using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCConversationConfigSO", menuName = "DataSO/NPCConversation")]
public class NPCConversationConfigSO : ScriptableObject
{
    [SerializeField, TextArea] private List<string> _contents = new List<string>();
    public  List<string> Contents => _contents;
}
