using SojiToban.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.Service
{

    /// <summary>
    /// データ生成を行う
    /// </summary>
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


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static RandamWeekMap CreateNumMap()
        {
            Day day = new Day();
            RandamWeekMap RandamWeekMap = new RandamWeekMap();
            for (int i = 0; i < ContractConst.WEEK.Count() - 1; i++)
            {
                int[] obj = (int[])ContractConst.WEEK[i];
                day = DayLoccation(obj);
                RandamWeekMap.day.Add(day);

            }
            int j = 0;
            foreach (ContractConst.DAYS v in Enum.GetValues(typeof(ContractConst.DAYS)))
            {
                RandamWeekMap.day[j].days = v;
                j++;
            }
            System.Diagnostics.Debug.WriteLine(RandamWeekMap);
            return RandamWeekMap;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static Day DayLoccation(int[] p)
        {
            Day day = new Day();
            Place place = new Place();
            while (place.value.Count() < p.Count())
            {
                System.Random rng = new System.Random();
                int k = rng.Next(p.Count());
                int num = p[k];
                if (!place.value.Contains(num))
                {
                    place.value.Enqueue(p[k]);
                }
            }
            day.place.Add(place);
            return day;
        }
    }
}
