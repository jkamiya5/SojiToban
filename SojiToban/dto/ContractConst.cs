using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SojiToban.dto
{
    public class ContractConst
    {
        public static String[] PID = { "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩", "⑪", "⑫", "⑬", "⑭", "⑮", "⑯", "⑰" };
        public static int[] MONDAY = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public static int[] TUESDAY = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public static int[] WEDNESDAY = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 18, 19, 20, 21, 22, 23, 24 };
        public static int[] THURSDAY = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public static int[] FRIDAY = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 12, 14, 15, 16, 17, 18, 19, 20, 21, 22, 26, 27, 28 };
        public static Object[] WEEK = { MONDAY, TUESDAY, WEDNESDAY, THURSDAY, FRIDAY, WEEK };
        public enum DAYS { Mon, Tue, Wed, Thu, Fri };
        public static int[] COEFFICIENT = { 999, 1, 1, 1, 1, 2, 2, 2, 3, 2, 3, 3, 3, 3, 3, 3, 3, 3 };
        public enum GENDER { MAN, WOMAN };
        public static int[] MAN_LOCATE = { 10, 12, 16, 23, 27 };
        public static int[] WOMAN_LOCATE = { 11, 17, 24, 28 };

        public static String[] PLACE = {"・給湯室（排水溝・食器の整理等）"
                                , "･拭きそうじ（カウンター）"
                                , "　　　〃　　　（応接室）"
                                , "　　　〃　　　（会議室）"
                                , "･ゴミ捨て 朝"
                                , "（シュレッダー・タバコの 含む）"
                                , "･ゴミ捨て 夕方"
                                , "（シュレッダー・タバコの 含む）"
                                , "・階段（１階～２階）"
                                , "・植木・花壇の水かけ"
                                , "・空気清浄機（水の入替・金曜：洗浄）"
                                , "・男子トイレ掃除（水かけ以外の清掃）"
                                , "・女子トイレ掃除（水かけ以外の清掃）"
                                , "・身障者トイレ掃除"
                                , "・フロア掃き掃除（火・木）※週２回"
                                , "・駐車場（掃き掃除・ゴミ拾い）"
                                , "･ベランダの掃き掃除"
                                , "・男子トイレ掃除（水を流して掃除）"
                                , "・女子トイレ掃除（水を流して掃除）"};

        public static int PERSON_COUNT = 100;
    }
}
