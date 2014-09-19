using SojiToban.dto;
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
    public class DisplayOption : MainWindow
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void PasteClipboard(DataGrid dataGrid)
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
        /// 
        /// </summary>
        /// <param name="RetInfo"></param>
        /// <param name="dAYS"></param>
        /// <returns></returns>
        private static int[] GetDayRowVal(Queue<Member> RetInfo, ContractConst.DAYS dAYS)
        {
            Dictionary<int, int?> D = new Dictionary<int, int?>();
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
            int[] Day = new int[ContractConst.PLACE_COUNT];
            int i = 0;
            foreach (var v in D)
            {
                Day[i] = (int)v.Value;
                i++;
            }
            return Day;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="RetInfo"></param>
        /// <param name="mainWindow"></param>
        internal static void Display(Queue<Member> RetInfo, MainWindow mainWindow)
        {
            //割り振り結果を出力する
            System.Diagnostics.Debug.WriteLine(RetInfo);
            int[] Day1 = new int[ContractConst.PLACE_COUNT];
            int[] Day2 = new int[ContractConst.PLACE_COUNT];
            int[] Day3 = new int[ContractConst.PLACE_COUNT];
            int[] Day4 = new int[ContractConst.PLACE_COUNT];
            int[] Day5 = new int[ContractConst.PLACE_COUNT];
            Day1 = GetDayRowVal(RetInfo, ContractConst.DAYS.Mon);
            Day2 = GetDayRowVal(RetInfo, ContractConst.DAYS.Tue);
            Day3 = GetDayRowVal(RetInfo, ContractConst.DAYS.Wed);
            Day4 = GetDayRowVal(RetInfo, ContractConst.DAYS.Thu);
            Day5 = GetDayRowVal(RetInfo, ContractConst.DAYS.Fri);

            //day1.OrderBy(c => c);
            ////SojiPlace型の表オブジェクト作成
            var data1 = new ObservableCollection<SojiPlace>(
                Enumerable.Range(0, ContractConst.PLACE_COUNT).Select(j => new SojiPlace
                {
                    PlaceId = ContractConst.PID[j],
                    Place = ContractConst.PLACE[j],
                    day1 = Day1[j],
                    day2 = Day2[j],
                    day3 = Day3[j],
                    day4 = Day4[j],
                    day5 = Day5[j]
                }));
            mainWindow.targetGrid.ItemsSource = data1;
        }
    }
}
