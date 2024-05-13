using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveAgent : Agent
{
    [SerializeField] private Board board;
    public Mark teamMark;
    private float mark;
    public Vector3 pos;
    public bool playing;

    public override void OnEpisodeBegin()
    {
        mark = MarkIndex(teamMark);
        pos = new Vector3(0, -3);
        if (teamMark == Mark.X) {
            playing = true;
        } else {
            playing = false;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        foreach (var mark in board.marks)
        {
            sensor.AddObservation(MarkIndex(mark));
        }
        sensor.AddObservation(mark);
        sensor.AddObservation(MarkIndex(board.currentMark));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (MarkIndex(board.currentMark) == mark) {
            int move = actions.DiscreteActions[0];
            if (move == 0) {
                pos = new Vector3(-2f, 3f);
                playing = true;
            } else if (move == 1) {
                pos = new Vector3(0f, 3f);
                playing = true;
            } else if (move == 2) {
                pos = new Vector3(2f, 3f);
                playing = true;
            } else if (move == 3) {
                pos = new Vector3(-2f, 1f);
                playing = true;
            } else if (move == 4) {
                pos = new Vector3(0f, 1f);
                playing = true;
            } else if (move == 5) {
                pos = new Vector3(2f, 1f);
                playing = true;
            } else if (move == 6) {
                pos = new Vector3(-2f, -1f);
                playing = true;
            } else if (move == 7) {
                pos = new Vector3(0f, -1f);
                playing = true;
            } else if (move == 8) {
                pos = new Vector3(2f, -1f);
                playing = true;
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> DiscreteActions = actionsOut.DiscreteActions;
        if (Input.GetKeyDown(KeyCode.Q)) { 
            DiscreteActions[0] = 0;
        } else if (Input.GetKeyDown(KeyCode.W)) {
            DiscreteActions[0] = 1;
        } else if (Input.GetKeyDown(KeyCode.E)) {
            DiscreteActions[0] = 2;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            DiscreteActions[0] = 3;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            DiscreteActions[0] = 4;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            DiscreteActions[0] = 5;
        } else if (Input.GetKeyDown(KeyCode.Z)) {
            DiscreteActions[0] = 6;
        } else if (Input.GetKeyDown(KeyCode.X)) {
            DiscreteActions[0] = 7;
        } else if (Input.GetKeyDown(KeyCode.C)) {
            DiscreteActions[0] = 8;
        }
    }

    public void CheckWin(float winner)
    {
        if (winner == mark) {
            AgentReward(10f);
            Ending();
        } else {
            AgentReward(-30f);
            Ending();
        }
    }

    public void AgentReward(float reward)
    {
        AddReward(reward);
    }

    public void Ending()
    {
        EndEpisode();
    }

    private float MarkIndex(Mark mark)
    {
        if (mark == Mark.None) {
            return 0f;
        } else if (mark == Mark.X) {
            return 1f;
        } else {
            return 2f;
        }
    }
}
