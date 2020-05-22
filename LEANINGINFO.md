## ML-Agents Link

- [*Training ML-Agents Config](https://github.com/Unity-Technologies/ml-agents/blob/release_2_docs/docs/Training-ML-Agents.md#training-with-mlagents-learn)
- [How to train](https://github.com/Unity-Technologies/ml-agents/blob/release_2_docs/docs/Learning-Environment-Executable.md)
- [Installation](https://github.com/Unity-Technologies/ml-agents/blob/release_2_docs/docs/Installation.md)
- [Example Projects](https://github.com/Unity-Technologies/ml-agents/blob/release_2_docs/docs/Learning-Environment-Examples.md)
- [*How to make Agents](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Learning-Environment-Design-Agents.md)
- 

## Python Command

| Name                        | Command                                                      |
| --------------------------- | ------------------------------------------------------------ |
| Start training              | `mlagents-learn config/trainer_config.yaml --run-id=first3DBallRun`<br /><br />`--resume` - continuous old model<br />`--env` - path of unity executable file in case of build |
| Observing Training Progress | `tensorboard --logdir=summaries`                             |
|                             |                                                              |
|                             |                                                              |
|                             |                                                              |

## Training Configurations

| Name                    | Command                                                      |
| ----------------------- | ------------------------------------------------------------ |
| `--curriculum`          | defines the set-up for Curriculum Learning                   |
| `<trainer-config-file>` | defines the training hyperparameters for each Behavior in the scene |
| `--sampler`             | defines the set-up for Environment Parameter Randomization   |
| `--num-envs`            | number of concurrent Unity instances to use during training  |
|                         |                                                              |

## Training Type

| Name | Command                      |
| ---- | ---------------------------- |
| PPO  | Proximal Policy Optimization |
| SAC  | Soft Actor-Critic            |
|      |                              |
|      |                              |
|      |                              |