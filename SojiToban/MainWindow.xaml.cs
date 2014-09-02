﻿using SojiToban.dto;
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
            //空のセルを作成する
            var data = new ObservableCollection<Person>(
                Enumerable.Range(1, 100).Select(i => new Person
                {
                    Name = string.Empty,
                    No = null,
                    Gender = null,
                    Kbn1 = null
                }));
            //DataGridに設定する
            this.dataGrid.ItemsSource = data;
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
                        pasteClipboard(dataGrid);

                        e.Handled = true;
                    }
                }
            }
        }


        /// <summary>
        /// クリップボード貼り付け
        /// </summary>
        /// <param name="dataGrid"></param>
        private void pasteClipboard(DataGrid dataGrid)
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

                var maxRowCount = pasteRows.Count();
                String firstCell = firstCellValue(pasteRows);
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
                dataGrid.Items[startRowIndex], dataGrid.Columns[2]);
                

            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pasteRows"></param>
        /// <returns></returns>
        private string firstCellValue(string[] pasteRows)
        {
            string str = pasteRows[0];
            str = str.Replace("※", "");
            string pattern = "(\t[0-9]*)";
            str = Regex.Replace(str, pattern, String.Empty);
            return str;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable DataTable = (DataTable)this.dataGrid.DataContext;
        }
    }
}
    