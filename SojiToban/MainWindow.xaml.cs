﻿using SojiToban.Dto;
using SojiToban.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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


        /// <summary>
        /// MainWindowコンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GetLatestBuildInfo();
            DataOption dataOption = new DataOption();
            dataOption.CreateData(this);

        }


        /// <summary>
        /// ビルド情報を表示する
        /// </summary>
        private void GetLatestBuildInfo()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var version = asm.GetName().Version;
            int days = version.Build;
            int seconds = version.Revision * 2;
            DateTime baseDate = new DateTime(2000, 1, 1);
            DateTime buildDate = baseDate.AddDays(days);
            DateTime buildDateTime = buildDate.AddSeconds(seconds);
            StringBuilder sb = new StringBuilder();
            sb.Append("アセンブリバージョン：");
            sb.AppendLine(version.ToString());
            sb.Append("ビルド日時：");
            sb.AppendLine(buildDateTime.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss"));
            this.LatestBuildDate.Content = sb.ToString();
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
            try
            {
                DataOption.s_inData = this.inDataGrid;
                Queue<Member> team = new Queue<Member>();
                Queue<Member> retInfo = new Queue<Member>();
                int i = 0;
                foreach (Member obj in DataOption.s_inData.Items)
                {
                    obj.Score = 0;
                    obj.Info = string.Empty;
                    if (obj.day.Count > 0)
                    {
                        obj.Clear();
                    }
                    i++;
                    if (obj.Name != string.Empty && obj.No != null)
                    {
                        team.Enqueue(obj);
                    }
                    if (i == StaticObject.maxRowCount || i == ContractConst.MEMBER_COUNT)
                    {
                        if (team.Count == 0)
                        {
                            ErrorProc(ContractConst.ERROR_MESSAGE_001);
                            return;
                        }
                        MainService service = new MainService();
                        retInfo = service.MainProc(team, this);
                        break;
                    }
                }
                DisplayOption displayOption = new DisplayOption();
                displayOption.Display(retInfo, this);
                this.execute.IsEnabled = false;
                this.countRbt.IsEnabled = false;
                this.scoreRbt.IsEnabled = false;
                this.chkAll.IsEnabled = false;
                this.chkMon.IsEnabled = false;
                this.chkTue.IsEnabled = false;
                this.chkWed.IsEnabled = false;
                this.chkThu.IsEnabled = false;
                this.chkFri.IsEnabled = false;
            }
            catch
            {
                return;
            }
        }



        /// <summary>
        /// エラー処理を行う
        /// </summary>
        /// <param name="p"></param>
        private void ErrorProc(string p)
        {
            this.errorInfo.Content = "  " + p;
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
                    m_afflictionDegree = ContractConst.COEFFICIENT[i + 1],
                    m_day1 = null,
                    m_day2 = null,
                    m_day3 = null,
                    m_day4 = null,
                    m_day5 = null,
                }));
            this.targetGrid.ItemsSource = data;
            this.execute.IsEnabled = true;
            this.countRbt.IsEnabled = true;
            this.scoreRbt.IsEnabled = true;
            this.chkAll.IsEnabled = true;
            this.chkMon.IsEnabled = true;
            this.chkTue.IsEnabled = true;
            this.chkWed.IsEnabled = true;
            this.chkThu.IsEnabled = true;
            this.chkFri.IsEnabled = true;

        }

        /// <summary>
        /// 休日設定にチェックが入った曜日をグレーアウトする
        /// </summary>
        /// <param name="mainWindow"></param>
        private void GrayOut(MainWindow mainWindow)
        {
            var data = new ObservableCollection<SojiPlace>(
                Enumerable.Range(1, ContractConst.PLACE_COUNT).Select(j => new SojiPlace
                {
                    m_placeId = ContractConst.PID[j - 1],
                    m_place = ContractConst.PLACE[j - 1],
                    m_day1_Color = mainWindow.chkMon.IsChecked == true ? true : false,
                    m_day2_Color = mainWindow.chkTue.IsChecked == true ? true : false,
                    m_day3_Color = mainWindow.chkWed.IsChecked == true ? true : false,
                    m_day4_Color = mainWindow.chkThu.IsChecked == true ? true : false,
                    m_day5_Color = mainWindow.chkFri.IsChecked == true ? true : false,
                }));
            mainWindow.targetGrid.ItemsSource = data;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMon_Checked(object sender, RoutedEventArgs e)
        {
            GrayOut(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTue_Checked(object sender, RoutedEventArgs e)
        {
            GrayOut(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWed_Checked(object sender, RoutedEventArgs e)
        {
            GrayOut(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkThu_Checked(object sender, RoutedEventArgs e)
        {
            GrayOut(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFri_Checked(object sender, RoutedEventArgs e)
        {
            GrayOut(this);
        }


        /// <summary>
        /// 全選択/全解除イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_Checked(object sender, RoutedEventArgs e)
        {
            if (this.chkAll.IsChecked == true)
            {
                this.chkMon.IsChecked = true;
                this.chkTue.IsChecked = true;
                this.chkWed.IsChecked = true;
                this.chkThu.IsChecked = true;
                this.chkFri.IsChecked = true;
                chkMon_Checked(sender, e);
                chkTue_Checked(sender, e);
                chkWed_Checked(sender, e);
                chkThu_Checked(sender, e);
                chkFri_Checked(sender, e);
            }
            else if (this.chkAll.IsChecked == false)
            {
                this.chkMon.IsChecked = false;
                this.chkTue.IsChecked = false;
                this.chkWed.IsChecked = false;
                this.chkThu.IsChecked = false;
                this.chkFri.IsChecked = false;
                chkMon_Checked(sender, e);
                chkTue_Checked(sender, e);
                chkWed_Checked(sender, e);
                chkThu_Checked(sender, e);
                chkFri_Checked(sender, e);
            }
        }

        /// <summary>
        /// 設定を保存ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
