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
            CreateData();

        }


        /// <summary>
        /// ダミーデータを作成する
        /// </summary>
        private void CreateData()
        {            
            //Person型の表オブジェクト作成
            var data = new ObservableCollection<Member>(
                Enumerable.Range(1, ContractConst.MEMBER_COUNT).Select(i => new Member
                {
                    Name = string.Empty,
                    No = null,
                    Gender = null,
                    Kbn1 = null
                }));
            //バインド
            this.dataGrid.ItemsSource = data;

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
                        PasteClipboard(dataGrid);
                        //以降のイベントをスキップする
                        e.Handled = true;
                    }
                }
            }
        }


        /// <summary>
        /// クリップボード貼り付け
        /// </summary>
        /// <param name="dataGrid"></param>
        private void PasteClipboard(DataGrid dataGrid)
        {
            try
            {                                      
                // 張り付け開始位置設定
                var startRowIndex = dataGrid.ItemContainerGenerator.IndexFromContainer(
                    (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromItem
                    (dataGrid.CurrentCell.Item));
                var startColIndex = dataGrid.SelectedCells[0].Column.DisplayIndex;

                // クリップボード文字列から行を取得
                var pasteRows = ((string)Clipboard.GetData(DataFormats.Text)).Replace("\r", "")
                    .Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                maxRowCount = pasteRows.Count();                
                for (int rowCount = 0; rowCount < maxRowCount; rowCount++)
                {
                    var rowIndex = startRowIndex + rowCount;
                    // タブ区切りでセル値を取得
                    var pasteCells = pasteRows[rowCount].Split('\t');
                    // 選択位置から列数繰り返す
                    var maxColCount = Math.Min(pasteCells.Count(), dataGrid.Columns.Count - startColIndex);
                    for (int colCount = 0; colCount < maxColCount; colCount++)
                    {
                        var column = dataGrid.Columns[colCount + startColIndex];
                        // 貼り付け
                        column.OnPastingCellClipboardContent(dataGrid.Items[rowIndex], pasteCells[colCount]);
                    }
                }
                
                // 選択位置復元
                dataGrid.CurrentCell = new DataGridCellInfo(
                dataGrid.Items[startRowIndex], dataGrid.Columns[3]);
                

            }
            catch
            {
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
            Queue<Member> ret = new Queue<Member>();
            int i = 0;
            foreach (Member obj in data.Items)
            {
                i++;
                if (obj.Name != "" && obj.No != null)
                {
                    Team.Enqueue(obj);
                }
                if (i == maxRowCount || i == ContractConst.MEMBER_COUNT)
                {
                    MainService sv = new MainService();
                    ret = sv.MainProc(Team);
                    break;
                }
            }
            //割り振り結果を出力する
            System.Diagnostics.Debug.WriteLine(ret);
            foreach(var v in ret)
            {
                //todo:
            }
            //SojiPlace型の表オブジェクト作成
            var data1 = new ObservableCollection<SojiPlace>(
                Enumerable.Range(0, ContractConst.PLACE_COUNT).Select(j => new SojiPlace
                {
                    PlaceId = ContractConst.PID[j],
                    Place = ContractConst.PLACE[j],
                    day1 = j,
                    day2 = j + 1,
                    day3 = j + 2,
                    day4 = null,
                    day5 = null,
                }));
            this.targetGrid.ItemsSource = data1;
        }

        
    }
}
    