using Unity.MLAgents.Sensors;
using System;

// ReSharper disable once CheckNamespace
namespace SnakeAI.Controller
{
	// ReSharper disable once InconsistentNaming
	public class ML1Agent : SnakeAgent
    {
        protected override void AddState(VectorSensor _sensor)
        {
	        // Add State
	        _sensor.AddObservation(m_Controller.State.Head);
	        _sensor.AddObservation(m_Controller.State.Food);
        }

        protected override void GameOverAction() => SetReward(-1f);
        protected override void WinAction() => SetReward(10f);
        protected override void EatAction() => SetReward(1f);
    }
}



