using System.IO;
using System.Linq;
using System.Media;
using System.Windows;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MiniGame.src.Game;

namespace MiniGame
{
	/// <summary>
	/// Interaction logic for StartWindow.xaml
	/// </summary>
	public partial class StartWindow : Window
	{
		public ObservableCollection<TetrisHighScore> HighscoreList
		{
			get; set;
		} = new ObservableCollection<TetrisHighScore>();

		public StartWindow()
		{
			InitializeComponent();
			SoundPlayer SP = new SoundPlayer();
			SP.SoundLocation = @"./resources/sounds/tetriestheme.wav";
			SP.PlayLooping();
			LoadHighscoreList();
		}

		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			// Open Main Window
			MainWindow main = new MainWindow();
			App.Current.MainWindow = main;
			this.Close();
			main.Show();
		}

		private void QuitButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
		private void LoadHighscoreList()
		{
			bool isAlive = false;
			if (isAlive = File.Exists("tetris_highscorelist.xml"))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(List<TetrisHighScore>));

				using (Stream reader = new FileStream("tetris_highscorelist.xml", FileMode.Open))
				{
					List<TetrisHighScore> tempList = (List<TetrisHighScore>)serializer.Deserialize(reader);
					this.HighscoreList.Clear();
					foreach (var item in tempList.OrderByDescending(x => x.PlayerScore))
						this.HighscoreList.Add(item);
				}
			}
		}
		/// <summary>
		/// Handle global keyboard input in the current window context
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Escape) this.Close();
			if (e.Key == System.Windows.Input.Key.Space)
			{
				// Open Main Window
				MainWindow main = new MainWindow();
				App.Current.MainWindow = main;
				this.Close();
				main.Show();
			}
		}
	}
}
