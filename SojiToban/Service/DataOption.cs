using SojiToban.dto;
using SojiToban.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SojiToban.Service
{

    /// <summary>
    /// データ生成を行う
    /// </summary>
    public class DataOption
    {
        public static DataGrid s_inData { get; set; }


        /// <summary>
        /// 初期データ作成
        /// </summary>
        /// <param name="mainWindow"></param>
        internal void CreateData(MainWindow mainWindow)
        {
            //Member型の表オブジェクト作成
            mainWindow.inDataGrid.ItemsSource = CreateDefaultMemberObject();
            //SojiPlace型の表オブジェクト作成
            mainWindow.targetGrid.ItemsSource = CreateSojiPlaceObject(mainWindow);

        }


        /// <summary>
        /// SojiPlace型のオブジェクト生成
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <returns></returns>
        public IEnumerable CreateSojiPlaceObject(MainWindow mainWindow)
        {
            var data = new ObservableCollection<SojiPlace>(
                Enumerable.Range(0, ContractConst.PLACE_COUNT).Select(i => new SojiPlace
                {
                    m_placeId = ContractConst.PID[i],
                    m_place = ContractConst.PLACE[i],
                    m_afflictionDegree = ContractConst.COEFFICIENT[i + 1] != null ? (int)ContractConst.COEFFICIENT[i + 1] : 0,
                    m_day1 = null,
                    m_day2 = null,
                    m_day3 = null,
                    m_day4 = null,
                    m_day5 = null,
                }));
            return data;
        }


        /// <summary>
        /// Member型のデフォルトオブジェクト生成
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <returns></returns>
        public IEnumerable CreateDefaultMemberObject()
        {
            var data = new ObservableCollection<Member>(
                Enumerable.Range(1, ContractConst.MEMBER_COUNT).Select(i => new Member
                {
                    Name = string.Empty,
                    No = null,
                    Gender = null,
                    Score = null
                }));
            return data;
        }


        /// <summary>
        /// 一週間分のデータを作成
        /// </summary>
        /// <returns></returns>
        public RandamWeekMap CreateNumMap()
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
            return RandamWeekMap;
        }


        /// <summary>
        /// 曜日単位のデータを作成
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Day DayLoccation(int[] p)
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


        /// <summary>
        /// 割り振り結果の得点を表示する
        /// </summary>
        /// <param name="RetInfo"></param>
        /// <returns></returns>
        internal IEnumerable CreateScoreObject(Queue<Member> RetInfo)
        {
            RandamSortByNo(ref RetInfo);
            string[] Name = new string[RetInfo.Count];
            int?[] No = new int?[RetInfo.Count];
            ContractConst.GENDER?[] Gender = new ContractConst.GENDER?[RetInfo.Count];
            int?[] Score = new int?[RetInfo.Count];
            string[] Info = new string[RetInfo.Count];
            int j = 0;
            foreach (var member in RetInfo)
            {
                Name[j] = member.Name;
                No[j] = member.No;
                Gender[j] = member.Gender;
                Score[j] = member.Score;
                Info[j] = member.Info;
                j++;
            }

            var data = new ObservableCollection<Member>(
                Enumerable.Range(0, RetInfo.Count + 5).Select(i => new Member
                {
                    Name = i > RetInfo.Count - 1 ? String.Empty : Name[i],
                    No = i > RetInfo.Count - 1 ? null : No[i],
                    Gender = i > RetInfo.Count - 1 ? null : Gender[i],
                    Score = i > RetInfo.Count - 1 ? null : Score[i],
                    Info = i > RetInfo.Count - 1 ? null : Info[i],
                })
                );
            return data;
        }

        /// <summary>
        /// 得点順にランダムソートを行う
        /// </summary>
        /// <param name="Team"></param>
        public void RandamSortByScore(ref Queue<Member> Team)
        {
            IEnumerable<Member> query = Team.OrderBy(member => member.Score).ThenBy(member => member.day.Count);
            foreach (Member member in query)
            {
                Team.Dequeue();
                Team.Enqueue(member);
            }
        }

        /// <summary>
        /// 番号順にランダムソートを行う
        /// </summary>
        /// <param name="Team"></param>
        public void RandamSortByNo(ref Queue<Member> Team)
        {
            IEnumerable<Member> query = Team.OrderBy(member => member.No);
            foreach (Member member in query)
            {
                Team.Dequeue();
                Team.Enqueue(member);
            }
        }


        /// <summary>
        /// 回数順にランダムソートを行う
        /// </summary>
        /// <param name="Team"></param>
        public void RandamSortByCount(ref Queue<Member> Team)
        {
            IEnumerable<Member> query = Team.OrderBy(member => member.day.Count).ThenBy(member => member.Score);
            foreach (Member member in query)
            {
                Team.Dequeue();
                Team.Enqueue(member);
            }
        }


        /// <summary>
        /// 画面から入力したチーム情報を返す
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <returns></returns>
        public Queue<Member> getTeamData(MainWindow mainWindow)
        {
            DataOption.s_inData = mainWindow.inDataGrid;
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
                        //エラー処理を行う
                        ErrorOption errorOption = new ErrorOption();
                        errorOption.ErrorProc(ContractConst.ERROR_MESSAGE_001);
                        return null;
                    }
                    return team;
                }
            }
            return null;
        }
    }
}
