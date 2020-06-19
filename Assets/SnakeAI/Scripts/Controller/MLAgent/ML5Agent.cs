using Unity.MLAgents.Sensors;
using System;
using SnakeAI.Scripts.Entities;

// ReSharper disable once CheckNamespace
namespace SnakeAI.Controller
{
	// ReSharper disable once InconsistentNaming
	public class ML5Agent : SnakeAgent
    {
        protected override void AddState(VectorSensor _sensor)
        {
	        // Add State
            _sensor.AddObservation(m_Controller.State.Head);
            _sensor.AddObservation(m_Controller.State.Food);
            _sensor.AddObservation(m_Controller.State.Tails.Count);

	        // Obs State
	        for(var i = 0; i < m_Controller.State.Width; i++)
	        {
		        for(var j = 0; j < m_Controller.State.Height; j++)
		        {
			        var blockType = m_Controller.State.StateBlock[i, j];
			        switch(blockType)
			        {
				        case BlockType.Blank:
					        _sensor.AddObservation(1); 
					        break;
				        case BlockType.Head:
					        _sensor.AddObservation(1);
					        break;
				        case BlockType.Tail:
					        _sensor.AddObservation(-1);
					        break;
				        case BlockType.Food:
					        _sensor.AddObservation(1);
					        break;
				        case BlockType.Wall:
					        _sensor.AddObservation(-1);
					        break;
				        default:
					        _sensor.AddObservation(0);
					        break;
			        }
		        }
	        }
        }
        
        protected override void GameOverAction() => SetReward(-1f);
        protected override void WinAction() => SetReward(10f);
        protected override void EatAction() => SetReward(1f);
    }
}