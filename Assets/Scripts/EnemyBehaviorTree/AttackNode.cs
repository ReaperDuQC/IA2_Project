using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackNode : Node
{
    public AttackNode()
    {

    }
    public override NodeStates Evaluate()
    {
        PlayerPrefs.SetInt("GameOver", 1);
        SceneManager.LoadScene("End");
        return NodeStates.SUCCESS;
    }
}
