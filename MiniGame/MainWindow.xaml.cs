using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MiniGame.src.Game;

namespace MiniGame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Game m_Board;
		private DispatcherTimer m_TimerThread;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Initialized(object sender, EventArgs e)
		{
			m_TimerThread = new DispatcherTimer();
			m_TimerThread.Tick += new EventHandler(Update);
			m_TimerThread.Interval = new TimeSpan(0, 0, 0, 0, 500);
			Start();
		}
		private void Start()
		{
			theGrid.Children.Clear();           // Clear the grid
			m_Board = new Game(theGrid);    // Create a new game
			m_TimerThread.Start();              // Init the game
		}
		private void Update(object o, EventArgs e)
		{
			var score = m_Board.Score;
			var level = m_Board.Level;
			var lines = m_Board.Lines;
			// Increase the speed at which shapes are spawned
			var multiplier = 400 - (level * 50);
			m_TimerThread.Interval = new TimeSpan(0, 0, 0, 0, multiplier);
			tbScore.Text = score.ToString("000000");
			tbLevel.Text = level.ToString("000");
			tbLines.Text = lines.ToString("000");
			m_Board.OnGameUpdate(0);

			if(m_Board.IsGameOver)
			{
				LoseWindow lose = new LoseWindow(score, lines);
				App.Current.MainWindow = lose;
				this.Close();
				lose.Show();
				m_TimerThread.Stop();
			}
		}
		private void Window_KeyDown(object sender, KeyEventArgs args)
		{
			switch (args.Key)
			{
				case Key.Escape:
					if (m_TimerThread.IsEnabled)
						Close();
					break;
				case Key.P:
					Pause();
					break;
				case Key.Down:
					if (m_TimerThread.IsEnabled)
						m_Board.OnGameUpdate(0);
					break;
				case Key.Right:
					if (m_TimerThread.IsEnabled)
						m_Board.OnGameUpdate((int)args.Key);
					break;
				case Key.Left:
					if (m_TimerThread.IsEnabled)
						m_Board.OnGameUpdate((int)args.Key);
					break;
				case Key.Up:
					if (m_TimerThread.IsEnabled)
						m_Board.OnGameUpdate((int)args.Key);
					break;
				case Key.R:
					if (m_TimerThread.IsEnabled)
						Restart();
					break;
			}
		}
		private void Pause()
		{
			if (m_TimerThread.IsEnabled) m_TimerThread.Stop();
			else m_TimerThread.Start();
		}
		private void Restart()
		{
			MainWindow main = new MainWindow();
			App.Current.MainWindow = main;
			Close();
			main.Show();
			if (m_TimerThread.IsEnabled) m_TimerThread.Stop();
		}
	}
}
