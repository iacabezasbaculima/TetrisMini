using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;
using MiniGame.src.Game;
using System.Linq;

namespace MiniGame
{
	/// <summary>
	/// Interaction logic for LoseWindow.xaml
	/// </summary>
	public partial class LoseWindow : Window
	{
		private int _totalScore;
		private int _totalLines;
		private List<TetrisHighScore> _highscoreList = new List<TetrisHighScore>(5);
		private const int MaxHighscoreListEntryCount = 5;

		public LoseWindow() { }
		public LoseWindow(int inTotalScore, int inTotalLines)
		{
			InitializeComponent();
			// Load the highscore list from XML file
			LoadHighscoreList();
			// Check which panel should be displayed
			CheckScore(inTotalScore, inTotalLines);

			_totalScore = inTotalScore;
			_totalLines = inTotalLines;
		}
		/// <summary>
		/// Start a new game
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RestartButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow main = new MainWindow();
			App.Current.MainWindow = main;
			Close();
			main.Show();
		}
		/// <summary>
		/// Open the Start Menu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MenuButton_Click(object sender, RoutedEventArgs e)
		{
			StartWindow sw = new StartWindow();
			App.Current.MainWindow = sw;
			this.Close();
			sw.Show();
		}
		/// <summary>
		/// Close the game
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		/// <summary>
		/// Called when LoseWindow is created to display correct panel
		/// </summary>
		/// <param name="totalScore"></param>
		/// <param name="totalLines"></param>
		private void CheckScore(int totalScore, int totalLines)
		{
			bool isNewHighscore = false;
			if (totalScore > 0)
			{
				int lowestHighscore = (this._highscoreList.Count > 0 ? this._highscoreList.Min(x => x.PlayerScore) : 0);
				if ((totalScore > lowestHighscore) || (this._highscoreList.Count < MaxHighscoreListEntryCount))
				{
					Panel_NewHighScore.Visibility = Visibility.Visible;
					tbxInput.Focus();
					isNewHighscore = true;
				}
			}
			if (!isNewHighscore)
			{
				tbScore.Text = totalScore.ToString();
				tbLines.Text = totalLines.ToString();
				Panel_Lose.Visibility = Visibility.Visible;
				Panel_Buttons.Visibility = Visibility.Visible;
			}
		}
		/// <summary>
		/// Called to determine if a new high score can be submitted and do so if possible
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SubmitButton_Click(object sender, RoutedEventArgs e)
		{
			int newIndex = 0;
			// Where should the new entry be inserted?
			if ((this._highscoreList.Count > 0) && (_totalScore < this._highscoreList.Max(x => x.PlayerScore)))
			{
				TetrisHighScore justAbove = this._highscoreList.OrderByDescending(x => x.PlayerScore).First(x => x.PlayerScore >= _totalScore);
				if (justAbove != null)
					newIndex = this._highscoreList.IndexOf(justAbove) + 1;
			}
			// Create & insert the new entry
			this._highscoreList.Insert(newIndex, new TetrisHighScore()
			{
				PlayerName = tbxInput.Text,
				PlayerScore = _totalScore,
				PlayerLines = _totalLines
			});
			// Make sure that the amount of entries does not exceed the maximum
			while (this._highscoreList.Count > MaxHighscoreListEntryCount)
				this._highscoreList.RemoveAt(MaxHighscoreListEntryCount);

			SaveHighscoreList();

			// Control panels
			Panel_NewHighScore.Visibility = Visibility.Collapsed;
			Panel_Buttons.Visibility = Visibility.Visible;
		}
		/// <summary>
		/// Serializes the current highscore list and writes to an XML document
		/// </summary>
		private void SaveHighscoreList()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(List<TetrisHighScore>));
			using (Stream writer = new FileStream("tetris_highscorelist.xml", FileMode.Create))
			{
				serializer.Serialize(writer, this._highscoreList);
			}
		}
		/// <summary>
		/// Loads the current highscore list from a XML document
		/// </summary>
		private void LoadHighscoreList()
		{
			if (File.Exists("tetris_highscorelist.xml"))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<TetrisHighScore>));

				using (Stream reader = new FileStream("tetris_highscorelist.xml", FileMode.Open))
				{
					List<TetrisHighScore> tempList = (List<TetrisHighScore>)serializer.Deserialize(reader);
					this._highscoreList.Clear();
					foreach (var item in tempList.OrderByDescending(x => x.PlayerScore))
						this._highscoreList.Add(item);
				}
			}
		}
	}
}
