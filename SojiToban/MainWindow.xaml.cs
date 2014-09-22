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
            DataOption dataOption = new DataOption();
            dataOption.CreateData(this);          

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
                        DisplayOption displayOption = new DisplayOption();
                        displayOption.PasteClipboard(dataGrid);                        
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
            DataOption.s_inData = this.inDataGrid;
            Queue<Member> team = new Queue<Member>();
            Queue<Member> retInfo = new Queue<Member>();
            int i = 0;
            foreach (Member obj in DataOption.s_inData.Items)
            {
                obj.Score = 0;
                obj.Info = string.Empty;
                if(obj.day.Count > 0)
                {
                    obj.Clear();                    
                }
                i++;
                if (obj.Name != string.Empty && obj.No != null)
                {
                    team.Enqueue(obj);
                }
                if (i == maxRowCount || i == ContractConst.MEMBER_COUNT)
                {
                    MainService service = new MainService();
                    retInfo = service.MainProc(team, this);
                    break;
                }
            }
            DisplayOption displayOption = new DisplayOption();
            displayOption.Display(retInfo, this);
            this.execute.IsEnabled = false;

        }

        /// <summary>
        /// 出力結果をクリアする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            //SojiPlace型の表オブジェクト作成
            var data = new ObservableCollection<SojiPlace>(
                Enumerable.Range(0, ContractConst.PLACE_COUNT).Select(i => new SojiPlace
                {
                    m_placeId = ContractConst.PID[i],
                    m_place = ContractConst.PLACE[i],
                    m_day1 = null,
                    m_day2 = null,
                    m_day3 = null,
                    m_day4 = null,
                    m_day5 = null,
                }));
            this.targetGrid.ItemsSource = data;
            this.execute.IsEnabled = true;
        }



        /// <summary>
        /// 入力内容をクリアする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputClearButton_Click(object sender, RoutedEventArgs e)
        {
            DataOption dataOption = new DataOption();
            this.inDataGrid.ItemsSource = dataOption.CreateDefaultMemberObject();
        }
    }
}
