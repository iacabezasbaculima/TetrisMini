using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;
using MiniGame.src.Game;
using System.Collections.ObjectModel;

namespace MiniGame
{
	/// <summary>
	/// Interaction logic for ScoreboardWindow.xaml
	/// </summary>
	public partial class ScoreboardWindow : Window
	{

		public ObservableCollection<TetrisHighScore> HighscoreList
		{
			get; set;
		} = new ObservableCollection<TetrisHighScore>();

		public ScoreboardWindow()
		{
			InitializeComponent();
			// Load high scores from XML file in ./bin/debug/
			LoadHighscoreList();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LoadHighscoreList();
			
		}
		private void LoadHighscoreList()
		{
			if(File.Exists("tetris_highscorelist.xml"))
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
		private void SaveHighscoreList()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<TetrisHighScore>));
			using (Stream writer = new FileStream("tetris_highscorelist.xml", FileMode.Create))
			{
				serializer.Serialize(writer, this.HighscoreList);
			}
		}
		/*
		 
		 */
	}
}
