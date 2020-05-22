using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace SnakeAI.Game
{
    public class State
	{
		public const DirectionType DEFAULT_DIRECTION = DirectionType.Right;
		public int Height;
		public int Width;
		public BlockType[,] StateBlock;
		public Vector2 Head;
		public Vector2 Food;
		public List<Vector2> Tails;
		public DirectionType Direction;
		public bool GameOver;
		public bool Eat;

		public enum BlockType
		{
			Blank,
			Head,
			Tail,
			Food,
			Wall
		}
		public enum DirectionType
		{
			Up,
			Down,
			Right,
			Left
		}
		public State(int _width = 10, int _height = 10)
		{
			// State Size
			Width = _width;
			Height = _height;
			var halfWidth = (int) Width / 2;
			var halfHeight = (int) Height / 2;
			Tails = new List<Vector2>();

			// Create StateBlock
			StateBlock = new BlockType[_width, _height];
			Direction = DEFAULT_DIRECTION;

			// Set Snake and Food
			SetHead(new Vector2(halfWidth, halfHeight));
			var availableFood = GetAvailableFood();
			SetFood(availableFood[Random.Range(0, availableFood.Count)]);
			
		}
		public void Next(DirectionType _direction)
		{
			// Wrong Direction
			if(GetNegativeDirection(_direction) == Direction)
			{
				_direction = Direction;
			}

			// Check GameOver
			var headNext = GetPositionFromDirection(Head, _direction);
			if (headNext.x >= Width || headNext.x < 0 || 
				headNext.y >= Height || headNext.y < 0 || 
				StateBlock[(int)headNext.x, (int)headNext.y] == BlockType.Tail)
			{
				GameOver = true;
				return;
			}

			// Check Eat
			Eat = headNext == Food ? true : false;

			// Set Snake
			SetHead(headNext);
			Direction = _direction;

			// Generate food
			if(Eat)
			{
				var availableFood = GetAvailableFood();
				SetFood(availableFood[Random.Range(0, availableFood.Count)]);
			}
		}
		public List<Vector2> GetAvailableFood()
		{
			var list = new List<Vector2>();
			for(int i = Width - 1; i > 0; i--)
			{
				for(int j = Height - 1; j > 0; j--)
				{
					if (StateBlock[i, j] == BlockType.Blank)
					{
						list.Add(new Vector2(i, j));
					}
				}
			}
			return list;
		}
		public void SetHead(Vector2 _index)
		{
			var oldHead = Head;
			if(Eat)
			{
				Tails.Add(oldHead);
				StateBlock[(int) oldHead.x, (int) oldHead.y] = BlockType.Tail;
			}
			else
			{
				if (Tails.Count > 0)
				{
					Tails.Add(oldHead);
					StateBlock[(int) oldHead.x, (int) oldHead.y] = BlockType.Tail;
					StateBlock[(int) Tails[0].x, (int) Tails[0].y] = BlockType.Blank;
					Tails.RemoveAt(0);
				}
				else
				{
					StateBlock[(int) oldHead.x, (int) oldHead.y] = BlockType.Blank;
				}
			}
			Head = _index;
			StateBlock[(int) Head.x, (int) Head.y] = BlockType.Head;
		}
		public void SetFood(Vector2 _index)
		{
			StateBlock[(int) Food.x, (int) Food.y] = BlockType.Blank;
			StateBlock[(int) _index.x, (int) _index.y] = BlockType.Food;
			Food = _index;
		}

		public void PrintDebug()
		{
			Debug.Log($"Head: {Head}");		
			Debug.Log($"Food: {Food}");		
			for(int i = 0; i < Width; i++)
			{
				var line = "";
				for(int j = 0; j < Height; j++)
				{
					if (StateBlock[i, j] == BlockType.Head)
					{
						line += $" H ";
					}
					else if (StateBlock[i, j] == BlockType.Food)
					{
						line += $" F ";
					}
					else if (StateBlock[i, j] == BlockType.Tail)
					{
						line += $" T ";
					}
					else
					{
						line += " _ ";
					}
				}
				Debug.Log(line + "\n");
			}		
		}

		public Vector2 GetPositionFromDirection(Vector2 _vector, DirectionType _direction)
		{
			if(_direction == DirectionType.Up) return _vector + new Vector2(0, 1);
			else if(_direction == DirectionType.Down) return _vector + new Vector2(0, -1);
			else if(_direction == DirectionType.Right) return _vector + new Vector2(1, 0);
			else if(_direction == DirectionType.Left) return _vector + new Vector2(-1, 0);
			return Vector2.zero;
		}
		private DirectionType GetNegativeDirection(DirectionType _direction)
		{
			if(_direction == DirectionType.Up) return DirectionType.Down;
			else if(_direction == DirectionType.Down) return DirectionType.Up;
			else if(_direction == DirectionType.Right) return DirectionType.Left;
			else if(_direction == DirectionType.Left) return DirectionType.Right;
			else
			{
				return DirectionType.Up;
			}
		}
	}
}


