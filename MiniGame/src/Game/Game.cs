using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
namespace MiniGame.src.Game
{
	public class Game
	{
		private int m_Rows;
		private int m_Columns;
		private Label[,] m_Labels;
		private Shape m_Shape;
		private bool m_isGameOver;
		private int m_Score;
		private int m_Level;
		private int m_Lines;

		public bool IsGameOver { get => m_isGameOver; set => m_isGameOver = value; }
		public int Score { get => m_Score; set => m_Score = value; }
		public int Level { get => m_Level; set => m_Level = value; }
		public int Lines { get => m_Lines; set => m_Lines = value; }
		public Game(Grid gridLayout)
		{
			m_Columns = gridLayout.ColumnDefinitions.Count;
			m_Rows = gridLayout.RowDefinitions.Count;
			m_Score = 0;
			m_Level = 1;
			m_Lines = 0;

			m_isGameOver = false;

			m_Labels = new Label[m_Columns, m_Rows];
			for (int i = 0; i < m_Columns; i++)
			{
				for (int j = 0; j < m_Rows; j++)
				{
					m_Labels[i, j] = new Label
					{
						Background = Brushes.Black,
						BorderBrush = Brushes.Gray,
						BorderThickness = new Thickness(1, 1, 1, 1)
					};
					Grid.SetRow(m_Labels[i, j], j);
					Grid.SetColumn(m_Labels[i, j], i);
					gridLayout.Children.Add(m_Labels[i, j]);
				}
			}
			m_Shape = new Shape();
			m_Shape.Draw(m_Columns, m_Labels);
		}
		/// <summary>
		/// Update the game, shape, score, level...
		/// </summary>
		/// <param name="k"></param>
		public void OnGameUpdate(int k)
		{	
			// Delete current shape first before updating
			m_Shape.Remove(m_Columns, m_Labels);
			switch (k)
			{
				case (int)Key.Right:
					if (CheckRightMove()) m_Shape.MoveRight();
					m_Shape.Draw(m_Columns, m_Labels);
					break;
				case (int)Key.Left:
					if(CheckLeftMove()) m_Shape.MoveLeft();	
					m_Shape.Draw(m_Columns, m_Labels);
					break;
				case (int)Key.Up:
					if (CheckRotateMove())
					{
						m_Shape.Rotate();
						m_Shape.Draw(m_Columns, m_Labels);
					}
					m_Shape.Draw(m_Columns, m_Labels);
					break;
				default:
					if (CheckDownMove(out bool state))
					{
						m_Shape.MoveDown();
						m_Shape.Draw(m_Columns, m_Labels);
					}
					else
					{
						m_Shape.Draw(m_Columns, m_Labels);
						CheckRows();
						m_Shape = new Shape();
					}
					m_isGameOver = state; // shape is moving now
					break;
			}
		}	
		/// <summary>
		/// Determine whether the current shape can move down
		/// </summary>
		/// <param name="gameOver"></param>
		/// <returns></returns>
		public bool CheckDownMove(out bool gameOver)
		{
			gameOver = false;
			foreach (Point sh in m_Shape.CurrentShape)
			{
				// when new shape spawns, current position member is as below:
				Point temp = new Point(0, -1); 
				if (((int)(sh.Y + m_Shape.CurrentPosition.Y) + 2 + 1) >= m_Rows)
				{
					return false;
				}
				else if (m_Labels[((int)(sh.X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1)), 
					(int)(sh.Y + m_Shape.CurrentPosition.Y) + 2 + 1].Background != Brushes.Black 
					&& m_Shape.CurrentPosition == temp)
				{
					//TODO: CHECK LOSE CONDITION AND END GAME -> GIVE OPTION TO RESTART GAME
					gameOver = true;
				}
				else if (m_Labels[((int)(sh.X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1)), 
					(int)(sh.Y + m_Shape.CurrentPosition.Y) + 2 + 1].Background != Brushes.Black)
				{
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// To check whether moving LEFT is possible with the current shape 
		/// </summary>
		/// <returns></returns>
		public bool CheckLeftMove()
		{
			foreach (Point S in m_Shape.CurrentShape)
			{
				if (((int)(S.X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1) - 1) < 0)
				{
					return false;
				}
				else if (m_Labels[((int)(S.X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1) - 1), 
					(int)(S.Y + m_Shape.CurrentPosition.Y) + 2].Background != Brushes.Black)
				{
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// To check whether moving RIGHT is possible with the current shape
		/// </summary>
		/// <returns></returns>
		public bool CheckRightMove()
		{
			foreach (Point sh in m_Shape.CurrentShape)
			{
				if (((int)(sh.X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1) + 1) >= m_Columns)
				{
					return false;
				}
				else if (m_Labels[((int)(sh.X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1) + 1),
					(int)(sh.Y + m_Shape.CurrentPosition.Y) + 2].Background != Brushes.Black)
				{
					return false;
				}
			}
			return true;
		}
		/// <summary>
		/// To check whether rotation is possible with the current shape
		/// </summary>
		/// <returns></returns>
		public bool CheckRotateMove()
		{
			// To store a copy of the current shape data to check if rotation is possible
			Point[] S = new Point[4];

			// Copy current shape data to temp array
			m_Shape.CurrentShape.CopyTo(S, 0);

			// don't use ForEach because it is read-only
			for (int i = 0; i < S.Length; i++)
			{
				double x = S[i].X;
				S[i].X = S[i].Y * -1;
				S[i].Y = x;
				if (((int)((S[i].Y + m_Shape.CurrentPosition.Y) + 2)) >= m_Rows)
				{
					return false;
				}
				else if (((int)(S[i].X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1)) < 0)
				{
					return false;
				}
				else if (((int)(S[i].X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1)) >= m_Columns)
				{
					return false;
				}
				else if (m_Labels[((int)(S[i].X + m_Shape.CurrentPosition.X) + ((m_Columns / 2) - 1)), 
					(int)(S[i].Y + m_Shape.CurrentPosition.Y) + 2].Background != Brushes.Black)
				{
					return false;
				}
			}
			
			return true;
		}
		/// <summary>
		/// To determine which rows are full and remove them accordingly
		/// </summary>
		public void CheckRows()
		{
			bool full;
			for (int row = m_Rows - 1; row > 0; row--)
			{
				full = true;
				for (int col = 0; col < m_Columns; col++)
				{
					if (m_Labels[col, row].Background == Brushes.Black)
					{
						full = false; // if one label is transparent, cant remove row
					}
				}
				if (full)
				{
					m_Lines++;
					RemoveRow(row);
					m_Score += 150;
					if (m_Score % 100 == 0)
					{
						m_Level++;
					}
					// Call again, otherwise consecutive full rows wont be deleted
					// because the row above the full row is moved down.
					// So in the next loop iteration, the row (may be full too) that was moved
					// down won't be detected as full
					CheckRows();
				}
			}
			//return true;
		}
		/// <summary>
		/// Removes a full row in the game grid
		/// </summary>
		/// <param name="row"></param>
		private void RemoveRow(int row)
		{
			for (int r = row; r > 0; r--)
			{
				for (int c = 0; c < m_Columns; c++)
				{
					m_Labels[c, r].Background = m_Labels[c, r - 1].Background;
				}
			}
		}
	}
}
