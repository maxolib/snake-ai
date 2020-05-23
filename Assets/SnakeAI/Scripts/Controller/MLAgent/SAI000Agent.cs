using Unity.MLAgents.Sensors;
using System;

namespace SnakeAI.Controller
{
    public class SAI000Agent : SnakeAgent
    {
        public override void CollectObservations(VectorSensor sensor)
		{
			// Snake10x10Learning
			var headX = Controller.State.Head.x / Controller.State.Width;
			var headY = Controller.State.Head.y / Controller.State.Height;
			var foodX = Controller.State.Food.x / Controller.State.Width;
			var foodY = Controller.State.Food.y / Controller.State.Height;
			sensor.AddObservation(headX);
			sensor.AddObservation(headY);
			sensor.AddObservation(foodX);
			sensor.AddObservation(foodY);
			sensor.AddObservation(Math.Abs(headX - foodX));
			sensor.AddObservation(Math.Abs(headY - foodY));
			sensor.AddObservation(Controller.State.Tails.Count);

			// Obs State
			for(var i = 0; i < Controller.State.Width; i++)
			{
				for(var j = 0; j < Controller.State.Height; j++)
				{
					sensor.AddObservation(Controller.State.StateBlock[i, j].GetHashCode() / 5f);
				}
			}
		}
    }
}