using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MiniGame.src.Game
{
	public class Shape
	{
		private Point[] m_CurrentShape;
		private Point m_CurrentPosition;
		private Brush m_CurrentColor;
		private bool m_canRotate;
		public Point[] CurrentShape { get => m_CurrentShape; set => m_CurrentShape = value; }
		public Point CurrentPosition { get => m_CurrentPosition; set => m_CurrentPosition = value; }
		public Brush CurrentColor { get => m_CurrentColor; set => m_CurrentColor = value; }
		public bool CanRotate { get => m_canRotate; set => m_canRotate = value; }
		public Shape()
		{
			m_CurrentPosition = new Point(0, -1);
			m_CurrentColor = Brushes.Black;
			m_CurrentShape = SetShape();
		}
		/// <summary>
		/// Returns a random shape data
		/// </summary>
		/// <returns></returns>
		public Point[] SetShape()
		{
			Random r = new Random();
			// r.Next() % 7
			switch (r.Next() % 7)
			{
				case 0:
					m_canRotate = true;
					m_CurrentColor = Brushes.Red;
					return new Point[]
					{
						new Point(0,-1),
						new Point(-1,-1),
						new Point(1,-1),
						new Point(2,-1)
					};
				case 1:
					m_canRotate = true;
					m_CurrentColor = Brushes.Green;
					return new Point[]
					{
						new Point(1,-1),
						new Point(-1,0),
						new Point(0,0),
						new Point(1,0)
					};
				case 2:
					m_canRotate = true;
					m_CurrentColor = Brushes.Blue;
					return new Point[]
					{
						new Point(0,0),
						new Point(-1,0),
						new Point(1,0),
						new Point(1,-1)
					};
				case 3:
					m_canRotate = false;
					m_CurrentColor = Brushes.Yellow;
					return new Point[]
					{
						new Point(0,0),
						new Point(0,-1),
						new Point(1,0),
						new Point(1,-1)
					};
				case 4:
					m_canRotate = true;
					m_CurrentColor = Brushes.Orange;
					return new Point[]
					{
						new Point(0,0),
						new Point(-1,0),
						new Point(0,-1),
						new Point(1,0)
					};
				case 5:
					m_canRotate = true;
					m_CurrentColor = Brushes.DarkCyan;
					return new Point[]
					{
						new Point(0,0),
						new Point(-1,0),
						new Point(0,-1),
						new Point(1,-1)
					};
				case 6:
					m_canRotate = true;
					m_CurrentColor = Brushes.Purple;
					return new Point[]
					{
						new Point(0,-1),
						new Point(-1,-1),
						new Point(0,0),
						new Point(1,0)
					};
				default:
					return null;
			}
		}
		public void Draw(int columns, Label[,] labels)
		{
			foreach (Point sh in m_CurrentShape)
			{
				// first: [0+0+5-1=4,-1-1+2=0]
				labels[(int)(sh.X + m_CurrentPosition.X) + ((columns / 2) - 1),
					(int)(sh.Y + m_CurrentPosition.Y) + 2].Background =
					m_CurrentColor;
			}
		}
		/// <summary>
		/// Draw NOT current shape in the game grid
		/// </summary>
		/// <param name="columns"></param>
		/// <param name="labels"></param>
		public void Remove(int columns, Label[,] labels)
		{
			foreach (Point sh in m_CurrentShape)
			{
				labels[(int)(sh.X + m_CurrentPosition.X) + ((columns / 2) - 1),
					(int)(sh.Y + m_CurrentPosition.Y) + 2].Background =
					Brushes.Black;
			}
		}
		public void MoveLeft()
		{
			m_CurrentPosition.X -= 1;
		}
		public void MoveRight()
		{
			m_CurrentPosition.X += 1;
		}
		public void MoveDown()
		{
			m_CurrentPosition.Y += 1;
		}
		public void Rotate()
		{
			if (m_canRotate)
			{
				for (int i = 0; i < m_CurrentShape.Length; i++)
				{
					double previousX = m_CurrentShape[i].X;
					m_CurrentShape[i].X = m_CurrentShape[i].Y * -1;
					m_CurrentShape[i].Y = previousX;
				}
			}
		}
	}
}
