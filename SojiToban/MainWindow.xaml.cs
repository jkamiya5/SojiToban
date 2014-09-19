using SojiToban.dto;
using SojiToban.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SojiToban
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int maxRowCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataOption.CreateData(this);

        }

        /// <summary>
        /// クリップボードを貼り付けるアクションを検知する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.V)
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    var dataGrid = sender as DataGrid;
                    if (dataGrid != null)
                    {
                        DisplayOption.PasteClipboard(dataGrid);                        
                        //以降のイベントをスキップする
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 割り振りボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = this.dataGrid;
            Queue<Member> Team = new Queue<Member>();
            Queue<Member> RetInfo = new Queue<Member>();
            int i = 0;
            foreach (Member obj in data.Items)
            {
                if(obj.day.Count > 0)
                {
                    obj.Clear();                    
                }
                i++;
                if (obj.Name != string.Empty && obj.No != null)
                {
                    Team.Enqueue(obj);
                }
                if (i == maxRowCount || i == ContractConst.MEMBER_COUNT)
                {
                    MainService service = new MainService();
                    RetInfo = service.MainProc(Team);
                    break;
                }
            }
            DisplayOption.Display(RetInfo, this);
            this.execute.IsEnabled = false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //SojiPlace型の表オブジェクト作成
            var data1 = new ObservableCollection<SojiPlace>(
                Enumerable.Range(0, ContractConst.PLACE_COUNT).Select(i => new SojiPlace
                {
                    PlaceId = ContractConst.PID[i],
                    Place = ContractConst.PLACE[i],
                    day1 = null,
                    day2 = null,
                    day3 = null,
                    day4 = null,
                    day5 = null,
                }));
            this.targetGrid.ItemsSource = data1;
            this.execute.IsEnabled = true;
        }


        //private void targetGrid_LoadedCellPresenter(object sender, DataGridCellEventArgs e)
        //{
        //    if (e.Cell.Row.Index == 0 && e.Cell.Column.Index == 0)
        //    {
        //        e.Cell.Presenter.Background = new SolidColorBrush(Colors.Red);
        //        e.Cell.Presenter.Foreground = new SolidColorBrush(Colors.White);
        //    }
        //    if (e.Cell.Row.Index == 1)
        //    {
        //        e.Cell.Presenter.Background = new SolidColorBrush(Colors.Blue);
        //        e.Cell.Presenter.Foreground = new SolidColorBrush(Colors.White);
        //    }
        //}
    }
}
