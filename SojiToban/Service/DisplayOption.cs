using SojiToban.dto;
using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SojiToban.Service
{
    /// <summary>
    /// 画面描画に関することを行う
    /// </summary>
    public class DisplayOption : MainWindow
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        public void PasteClipboard(DataGrid dataGrid)
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

                StaticObject.maxRowCount = pasteRows.Count();
                for (int rowCount = 0; rowCount < StaticObject.maxRowCount; rowCount++)
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
        /// 各曜日の出力結果を作成
        /// </summary>
        /// <param name="RetInfo"></param>
        /// <param name="dAYS"></param>
        /// <returns></returns>
        private int?[] GetDayRowVal(Queue<Member> RetInfo, ContractConst.DAYS dAYS)
        {
            Dictionary<int?, int?> D = new Dictionary<int?, int?>();
            foreach (var member in RetInfo)
            {
                foreach (var thisDay in member.day)
                {
                    if (thisDay.days == dAYS)
                    {
                        foreach (var v in thisDay.place)
                        {
                            if (v.value.Count == 0)
                            {
                                break;
                            }
                            D.Add(v.value.Dequeue(), member.No);
                        }
                    }
                }
            }
            int?[] Day = new int?[ContractConst.PLACE_COUNT + 1];
            int i = 0;
            foreach (var v in D)
            {
                Day[(int)v.Key] = (int)v.Value;
                i++;
            }
            return Day;
        }


        /// <summary>
        /// 表示を行う
        /// </summary>
        /// <param name="RetInfo"></param>
        /// <param name="mainWindow"></param>
        internal void Display(Queue<Member> RetInfo, MainWindow mainWindow)
        {
            //割り振り結果を出力する
            System.Diagnostics.Debug.WriteLine(RetInfo);
            int?[] day1 = new int?[ContractConst.PLACE_COUNT + 1];
            int?[] day2 = new int?[ContractConst.PLACE_COUNT + 1];
            int?[] day3 = new int?[ContractConst.PLACE_COUNT + 1];
            int?[] day4 = new int?[ContractConst.PLACE_COUNT + 1];
            int?[] day5 = new int?[ContractConst.PLACE_COUNT + 1];
            day1 = GetDayRowVal(RetInfo, ContractConst.DAYS.月);
            day2 = GetDayRowVal(RetInfo, ContractConst.DAYS.火);
            day3 = GetDayRowVal(RetInfo, ContractConst.DAYS.水);
            day4 = GetDayRowVal(RetInfo, ContractConst.DAYS.木);
            day5 = GetDayRowVal(RetInfo, ContractConst.DAYS.金);            

            var data = new ObservableCollection<SojiPlace>(
                Enumerable.Range(1, ContractConst.PLACE_COUNT).Select(j => new SojiPlace
                {
                    m_placeId = ContractConst.PID[j - 1],
                    m_place = ContractConst.PLACE[j - 1],
                    m_afflictionDegree = ContractConst.COEFFICIENT[j],
                    m_day1 = day1[j],
                    m_day2 = day2[j],
                    m_day3 = day3[j],
                    m_day4 = day4[j],
                    m_day5 = day5[j],
                    m_day1_Color = day1[j] == null ? true : false,
                    m_day2_Color = day2[j] == null ? true : false,
                    m_day3_Color = day3[j] == null ? true : false,
                    m_day4_Color = day4[j] == null ? true : false,
                    m_day5_Color = day5[j] == null ? true : false
                }));
            mainWindow.targetGrid.ItemsSource = data;


            //得点を左ウィンドウに反映
            DataOption option = new DataOption();
            mainWindow.inDataGrid.ItemsSource = option.CreateScoreObject(RetInfo);
        }
    }
}
