﻿using Photon.Pun;
using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviourPun
{
    private Core Core;
    public string currentPlayerName;
    public int currentPlayerIndex;
    public bool isReverse = false;
    public readonly int[][] PlayOrders = new int[][]
    {
        new int[]{ -1, 0 }, //2p
        new int[]{ -1, 1, 0} , //3p
        new int[]{ -1, 1, 0, 2 }, //4p
        new int[]{ -1, 1, 0, 3, 2 }, //5p
        new int[]{ -1, 1, 4, 0, 3, 2 }, //6p
        new int[]{ -1, 1, 4, 5, 0, 3, 2 } //7p
    };
    //will be one of those above
    public int[] PlayOrder;

    

    private void Start()
    {
        Core = GetComponent<Core>();
    }

    /// <summary>
    /// Makes the play order synced between clients
    /// https://i.imgur.com/hLg8zTn.png
    /// </summary>
    public void SortPlayerList()
    {
        if (Core.playerCount == 1) return;
        List<string> namesBefore = Core.PlayerList.GetRange(0, Core.Stack.myID);
        Core.PlayerList.RemoveRange(0, Core.Stack.myID);
        Core.PlayerList.AddRange(namesBefore);
        GameObject.Find("Canvas/DEBUG").GetComponent<Text>().text = string.Join(", ", Core.PlayerList);
        PlayOrder = PlayOrders[Core.PlayerList.Count - 1];
        for (int i = 1; i < Core.PlayerList.Count; i++)
        {
            GameObject.Find($"OtherCards ({PlayOrder[i]})/Canvas/Text").GetComponent<Text>().text = $"{Core.PlayerList[i]} ({i})";
            GameObject.Find($"OtherCards ({PlayOrder[i]})").GetComponent<Player>().name = Core.PlayerList[i];
            GameObject.Find($"OtherCards ({PlayOrder[i]})").GetComponent<Player>().destination = $"OtherCards ({PlayOrder[i]})";
        }
    }
    /// <summary>
    /// Changes current player 
    /// </summary>
    public void NextPlayer()
    {
        currentPlayerIndex += isReverse ? -1 : 1;
        if (currentPlayerIndex == PlayOrder.Length) currentPlayerIndex = 0;
        if (currentPlayerIndex == -1) currentPlayerIndex = PlayOrder[PlayOrder.Length - 1];
    }
    /// <summary>
    /// Variables controlled and method ran by the Game Master
    /// </summary>
    #region Master

    #endregion
}