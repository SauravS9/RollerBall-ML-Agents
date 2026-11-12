using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;

// Little HUD in the corner so you can actually see how the agents are doing
// (counts are shared by all four training areas).
public class ScoreBoard : MonoBehaviour
{
    public Text label;

    static int hits;
    static int falls;
    static readonly Queue<int> lastHundred = new Queue<int>();

    public static void Report(bool reachedTarget)
    {
        if (reachedTarget) hits++; else falls++;
        lastHundred.Enqueue(reachedTarget ? 1 : 0);
        if (lastHundred.Count > 100) lastHundred.Dequeue();
    }

    void Awake()
    {
        hits = 0;
        falls = 0;
        lastHundred.Clear();
    }

    void Update()
    {
        if (label == null) return;

        int sum = 0;
        foreach (var r in lastHundred) sum += r;
        float rate = lastHundred.Count == 0 ? 0f : 100f * sum / lastHundred.Count;

        string mode = Academy.Instance.IsCommunicatorOn ? "TRAINING  (PPO, mlagents-learn)" : "TRAINED MODEL  (inference)";
        label.text =
            "<b>RollerBall</b>   " + mode + "\n" +
            "Steps: " + Academy.Instance.StepCount +
            "     Targets reached: " + hits +
            "     Fell off: " + falls + "\n" +
            "Success rate (last 100 episodes): " + rate.ToString("0") + "%";
    }
}
