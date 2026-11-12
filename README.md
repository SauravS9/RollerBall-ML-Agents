# RollerBall – Unity ML-Agents

A ball (the agent) learns to roll to a cube (the target) on a floating platform without falling off.
Built in Unity using the ML-Agents Toolkit and trained with PPO for the AR/VR lab.

<p>
  <b>Training video:</b> <a href="Videos/RollerBall_training.mp4">Videos/RollerBall_training.mp4</a> (15 s)<br>
  <b>Trained model output:</b> <a href="Videos/RollerBall_output.mp4">Videos/RollerBall_output.mp4</a> (15 s)
</p>

## Environment

| | |
|---|---|
| **Agent** | Blue sphere with a Rigidbody (`RollerAgent.cs`) |
| **Goal** | Touch the orange cube (distance < 1.42) |
| **Observations (8)** | Target position (3), agent position (3), agent velocity x/z (2) |
| **Actions** | 2 continuous – force along x and z (`forceMultiplier = 10`) |
| **Rewards** | +1 for reaching the target; episode ends if the ball falls off |
| **Decision period** | 10 |
| **Training areas** | 4 copies of the `TrainingArea` prefab to speed up training |

There's also a small on-screen scoreboard (`ScoreBoard.cs`) that shows the step count,
targets reached, falls and the success rate over the last 100 episodes.

## Results

Trained with `config/rollerball_config.yaml` (PPO, 128×2 network, lr 3e-4).

| Trainer step | Mean reward |
|---|---|
| 10k | 0.40 |
| 50k | 0.88 |
| 100k | 0.91 |
| 200k | 0.91 |
| 250k | 0.97 |

The final model (`Assets/Models/RollerBall.onnx`, also in `results/`) reaches the target ~94–100% of the time.

## Versions

- Unity **6000.0 LTS** (6000.0.85f2)
- ML-Agents Unity package **4.0.0** (release 23)
- Python **3.10.12**, `mlagents` from release 23, PyTorch 2.2

## How to run

**Watch the trained model**
1. Open this folder in Unity Hub (Add → Add project from disk) with Unity 6000.0.x.
2. Open `Assets/Scenes/RollerBall.unity` and press **Play**.

**Train it again**
```bash
conda activate mlagents
mlagents-learn config/rollerball_config.yaml --run-id=RollerBall --timeout-wait=600
```
Press **Play** in Unity when it says *Listening on port 5004*. To train, set **Behavior Type** to
*Default* in the agent's Behavior Parameters (it's on *Inference Only* with the trained model right now).

`train_rollerball.bat` does the same thing on Windows, assuming the ml-agents repo is cloned at `%USERPROFILE%\ml-agents`.

> If `mlagents-learn` fails with `No module named 'pkg_resources'`, run `pip install "setuptools<70"`.

## Project structure

```
Assets/
  Scenes/RollerBall.unity
  Scripts/RollerAgent.cs, ScoreBoard.cs
  Prefabs/TrainingArea.prefab
  Materials/
  Models/RollerBall.onnx
config/rollerball_config.yaml
results/                trained model + training configuration
Videos/                 15 s training and output videos
```

Based on the "Making a New Learning Environment" guide in the
[Unity ML-Agents docs](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.0/manual/Learning-Environment-Create-New.html).
