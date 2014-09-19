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
                    return;
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
            
        }
    }
}
