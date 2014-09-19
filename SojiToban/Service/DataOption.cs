using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Service
{
    public static class DataOption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        internal static void CreateData(MainWindow mainWindow)
        {
            //Member型の表オブジェクト作成
            var data = new ObservableCollection<Member>(
                Enumerable.Range(1, ContractConst.MEMBER_COUNT).Select(i => new Member
                {
                    Name = string.Empty,
                    No = null,
                    Gender = null,
                    Kbn1 = null
                }));
            //バインド
            mainWindow.dataGrid.ItemsSource = data;

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
            mainWindow.targetGrid.ItemsSource = data1;
        }
    }
}
