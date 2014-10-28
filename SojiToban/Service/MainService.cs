using SojiToban.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using MathNet.Numerics.Statistics;
using SojiToban.CommonModule;
using System.Security.Cryptography;
using System.Xml;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace SojiToban.Service
{


    /// <summary>
    /// メイン処理を行うクラス
    /// </summary>
    public class MainService
    {
        /// <summary>
        /// メイン処理
        /// </summary>
        /// <param name="Team"></param>
        /// <returns></returns>
        public Queue<Member> MainProc(Queue<Member> Team, MainWindow mainWindow)
        {           

            DataOption dataOption = new DataOption();
            LoccateOption locateOption = new LoccateOption();

            //SerializeTeamData(Team);
            SerializeTeamData2(Team);

            //清掃箇所をランダムに割り振った数字列作成
            RandamWeekMap RandamWeekMap = dataOption.CreateNumMap();

            //曜日毎に割り振りを行う
            foreach (Day EachDay in RandamWeekMap.day)
            {
                //休日判定を行う
                HolidayJudge HolidayJudge = new HolidayJudge();
                bool isHoliday = HolidayJudge.Judge(mainWindow, EachDay);

                //休日なら次の曜日へ
                if(isHoliday)
                {
                    continue;
                }


                //割り振り処理フラグ初期化
                bool ret = true;

                //メンバー全員に対して割り振り処理を行う
                while (ret == true)
                {

                    foreach (Member member in Team)
                    {
                        //個人割り振り初回時
                        if (member.day.Count() == 0)
                        {                            
                            ret = locateOption.AllocationFirstTime(EachDay, member);
                            if (ret == false)
                            {
                                break;
                            }
                        }
                        else
                        {
                            ret = locateOption.AssignmentEachDay(EachDay, member);
                            if (ret == false)
                            {
                                break;
                            }
                        }
                    }

                    //メンバーのランダムソートを行う
                    if (mainWindow.countRbt.IsChecked == true)
                    {
                        dataOption.RandamSortByCount(ref Team);
                    }
                    else if (mainWindow.scoreRbt.IsChecked == true)
                    {
                        dataOption.RandamSortByScore(ref Team);
                    }
                }
            }
            return Team;
        }


        /// <summary>
        /// 入力データをXML化して退避する
        /// </summary>
        /// <param name="Team"></param>
        private void SerializeTeamData(Queue<Member> Team)
        {
            int i = 0;
            //保存する配列を作成
            TestMemberModel[] ary = new TestMemberModel[Team.Count];
            foreach (var obj in Team)
            {
                ary[i] = new TestMemberModel();
                ary[i].No = obj.No != null ? Convert.ToString(obj.No) : string.Empty;
                ary[i].Name = obj.Name == null ? "" : obj.Name;
                ary[i].Score = obj.Score;
                ary[i].Info = obj.Info == null ? "" : obj.Info;
                ary[i].Gender = obj.Gender == ContractConst.GENDER.男 ? 0 : 1;
                i++;
            }

            //XMLファイルに保存する
            System.Xml.Serialization.XmlSerializer serializer1 =
                new System.Xml.Serialization.XmlSerializer(typeof(TestMemberModel[]));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
                @"C:\sample\sample.xml", false, new System.Text.UTF8Encoding(false));
            serializer1.Serialize(sw, ary);
            sw.Close();
        }

        /// <summary>
        /// 入力データをXML化して退避する
        /// </summary>
        /// <param name="Team"></param>
        private void SerializeTeamData2(Queue<Member> Team)
        {
            //サンプルコード
            //シリアライズする為のPersonsインスタンスを生成
            TestMembersModel testMembers = new TestMembersModel();
            testMembers.Members = new List<TestMemberModel>();
            //インスタンスに値を設定
            TestMemberModel members = null;
            foreach (var obj in Team)
            {
                members = new TestMemberModel();
                members.No = obj.No != null ? Convert.ToString(obj.No) : string.Empty;
                members.Name = obj.Name;
                members.Gender = obj.Gender == ContractConst.GENDER.男 ? 0 : 1;
                members.Info = obj.Info != null ? obj.Info : string.Empty;
                members.Score = obj.Score;
                testMembers.Members.Add(members);
            }

            //出力先XMLのストリーム
            FileStream stream = new FileStream(@"C:\sample\TeamInfo.xml", System.IO.FileMode.Create);
            StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);

            //シリアライズ
            XmlSerializer serializer = new XmlSerializer(typeof(TestMembersModel));
            serializer.Serialize(writer, testMembers);

            writer.Flush();
            writer.Close();
        }
    }
}
