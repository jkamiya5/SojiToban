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
using System.Xml;
using System.Runtime.Serialization;

namespace SojiToban.Service
{

    /// <summary>
    /// データ生成を行う
    /// </summary>
    public class DataOption
    {
        public static DataGrid s_inData { get; set; }
        public static DataGrid s_targetGrid { get; set; }


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
        /// 割り振り結果の得点表示文字列を作成
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
        /// 画面から入力したチーム情報を返す
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <returns></returns>
        public Queue<Member> GetTeamInfoEnteredFromScreen(MainWindow mainWindow)
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
                if (i == StaticObject.MaxRowCount || i == ContractConst.MEMBER_COUNT)
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



        /// <summary>
        /// 割り振り結果をシリアル化して外部のxmlファイルに保存
        /// </summary>
        /// <param name="mainWindow"></param>
        public void SerializePlacementResults(MainWindow mainWindow)
        {            
            //XMLシリアル化するオブジェクト
            SojiPlaceClass sojiPlaceClass = new SojiPlaceClass();
            sojiPlaceClass.Items = new List<SojiPlace>();
            int rowCnt = 1;
            foreach (SojiPlace obj in mainWindow.targetGrid.Items)
            {
                SojiPlace sojiPlace = new SojiPlace();
                System.Diagnostics.Debug.Write(obj);
                sojiPlace.m_placeId = obj.m_placeId;
                sojiPlace.m_place = obj.m_place;
                sojiPlace.m_afflictionDegree = obj.m_afflictionDegree;
                sojiPlace.m_day1 = obj.m_day1;
                sojiPlace.m_day2 = obj.m_day2;
                sojiPlace.m_day3 = obj.m_day3;
                sojiPlace.m_day4 = obj.m_day4;
                sojiPlace.m_day5 = obj.m_day5;
                sojiPlace.m_day1_Color = obj.m_day1_Color;
                sojiPlace.m_day2_Color = obj.m_day2_Color;
                sojiPlace.m_day3_Color = obj.m_day3_Color;
                sojiPlace.m_day4_Color = obj.m_day4_Color;
                sojiPlace.m_day5_Color = obj.m_day5_Color;
                sojiPlaceClass.Items.Add(sojiPlace);
                if (rowCnt == ContractConst.PLACE_COUNT)
                {
                    break;
                }
                rowCnt++;
            }
            //出力先XMLのストリーム
            System.IO.FileStream stream = new System.IO.FileStream(ContractConst.XMLFILE_PATH1 + sojiPlaceClass.GetType().Name + ".xml", System.IO.FileMode.Create);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8);
            //シリアライズ
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SojiPlaceClass));
            serializer.Serialize(writer, sojiPlaceClass);
            writer.Flush();
            writer.Close();
        }


        /// <summary>
        /// チーム情報をシリアル化して外部のxmlファイルに保存
        /// </summary>
        /// <param name="teamData"></param>
        public void SerializeTeamData(Queue<Member> teamData)
        {
            //XMLシリアル化するオブジェクト
            TestMemberClass obj = new TestMemberClass();
            obj.Items = new List<TestMember>();

            foreach(var v in teamData)
            {
                TestMember member = new TestMember();
                member.No = v.No;
                member.Name = v.Name;                
                member.Score = v.Score;
                member.Info = v.Info;
                member.Gender = v.Gender == ContractConst.GENDER.男 ? 1 : 2;
                obj.Items.Add(member);    
            }

            //出力先XMLのストリーム
            System.IO.FileStream stream = new System.IO.FileStream(ContractConst.XMLFILE_PATH, System.IO.FileMode.Create);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8);
            //シリアライズ
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(TestMemberClass));
            serializer.Serialize(writer, obj);
            writer.Flush();
            writer.Close();

        }


        /// <summary>
        /// シリアライズ化されたXMLファイルからチーム情報を復元
        /// </summary>
        /// <param name="teamData"></param>
        public Queue<Member> DeSerializeTeamData()
        {
            //サンプルコード
            System.IO.FileStream fs = new System.IO.FileStream(ContractConst.XMLFILE_PATH, System.IO.FileMode.Open);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(TestMemberClass));
            TestMemberClass target = (TestMemberClass)serializer.Deserialize(fs);
            Queue<Member> Team = new Queue<Member>();
            foreach (TestMember merber in target.Items)
            {
                Member obj = new Member();
                obj.No = merber.No;
                obj.Name = merber.Name;
                obj.Score = merber.Score;
                obj.Gender = merber.Gender == 1 ? ContractConst.GENDER.男 : ContractConst.GENDER.女;
                obj.Info = merber.Info;

                Team.Enqueue(obj);
            }
            return Team;
        }
    }
}
