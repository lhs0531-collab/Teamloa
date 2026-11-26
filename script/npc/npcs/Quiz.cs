using LKCamelot.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.npc
{
    public class Quiz : BaseNPC
    {
        public string Answer = string.Empty;
        public string Question = string.Empty;
        public string Explanation = string.Empty;
        public long LimiteTime = 0;

        public override string Name { get { return "초성퀴즈"; } }
        public override List<string> ChatString
        {
            get
            {
                return new List<string>()
                {
                    "초성퀴즈 문제를 제출해보세요",
                    "초성퀴즈 답을 맞춰보세요",
                    "퀴즈를 보시려면 \'보기\'를 치세요",
                    "문제를 제출하려면 \'@초성\'입니다."
                };
            }
        }

        public override byte Sprite { get { return 8; } }
        public override byte aSpeed { get { return 4; } }
        public override byte aFrames { get { return 4; } }
        public override int MessageGap { get { return 3 * Settings.Second; } }

        public Quiz(Serial serial, int Channel, string Map, Point2D Loc)
            : base(serial)
        {
            base.m_Channel = Channel;
            m_Map = Map;
            base.m_Loc = new Point2D(Loc.X, Loc.Y);
        }
        public override void Event_Message(Player player, string[] str2, string str)
        {
            if (str == Answer)
            {
                this.Event_Chat(Answer + ", " + player.Name + "님 정답입니다.", true);
                World.SendToSoundRange(new QueDele(player.Map, new EffectSound(library.EffectSound.Congratulation).Compile()), player);
                Answer = string.Empty;
                Question = string.Empty;
                Explanation = string.Empty;
                LimiteTime = 0;
            }
            else if (str == "보기")
            {
                if (Answer == string.Empty)
                    this.Event_Chat("문제가 없습니다.");
                else
                    this.Event_Chat(string.Format("[{0}]-{1}", Question, Explanation), true);
            }
            
        }

        public string ChangIC(string str)
        {
            StringBuilder returnstring = new StringBuilder();

            const int KOREAN_START = 44032;
            const int KOREAN_IC_INTERVAL = 588;
            const string Table = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
            int idx;
            for (int temp = 0; temp < str.Length; temp++)
            {
                idx = (str[temp] - KOREAN_START) / KOREAN_IC_INTERVAL;
                if (idx < 0)
                {
                    for (int i = 0; i < Table.Length; i++)
                    {
                        if (str[temp] == Table[i])
                        {
                            returnstring.Append(Table[i]);
                            break;
                        }
                    }
                }
                else
                    returnstring.Append(Table.Substring(idx, 1));
            }

            return returnstring.ToString();
        }


    }
}
