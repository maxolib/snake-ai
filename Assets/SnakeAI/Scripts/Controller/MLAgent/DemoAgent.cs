using Unity.MLAgents.Sensors;

// ReSharper disable once CheckNamespace
namespace SnakeAI.Controller
{
	// ReSharper disable once InconsistentNaming
	public class DemoAgent : SnakeAgent
    {
        protected override void AddState(VectorSensor _sensor)
        {
	        // Add State
        }

        protected override void GameOverAction() => SetReward(-1f);
        protected override void EatAction() => SetReward(1f);
        protected override void WinAction() => SetReward(10f);
    }
}



