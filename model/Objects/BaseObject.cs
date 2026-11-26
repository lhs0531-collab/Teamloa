using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.model
{
    public class BaseObject
    {
        public int m_Channel;
        public Serial m_Serial;
        public string m_Map;
        public Point2D m_Loc;

        public short Face;


        public byte WalkingMode, Transparancy, LightRad = 0x02;
        public ConcurrentDictionary<long, script.spells.Spell> m_Buffs = new ConcurrentDictionary<long, script.spells.Spell>();


        public BaseObject(Serial Serial)
        {
            m_Serial = Serial;
        }

        public byte[] BuffArray
        {
            get
            {
                byte[] buffs = new byte[4] { 0, 0, 0, 0 };
                int itrx = 0;
                foreach (var buff in m_Buffs.Values)
                {
                    if (buff.Name == "MENTAL SWORD")
                        continue;
                    if (buff.Seq.OnCastSprite == 0)
                        continue;
                    if (itrx > 3)
                        break;

                    buffs[itrx++] = (byte)buff.Seq.OnCastSprite;
                }
                if (this is model.Player)
                {
                    var aura = (this as model.Player).Equipped2.Where(xe => (xe.Value is script.item.IAura)).FirstOrDefault();
                    if (aura.Value != null)
                    {
                        buffs[3] = (byte)(aura.Value as script.item.IAura).Aura();
                    }
                }

                return buffs;
            }
        }
        public bool AddBuff(script.spells.Spell buff)
        {
            lock (m_Buffs)
            {
                if (!m_Buffs.Values.Where(xe => xe.Name == buff.Name).Any())
                {
                    m_Buffs.TryAdd(Server.tickcount.ElapsedMilliseconds + (buff.BuffEffect.Time + ((buff.Level - 1) * buff.BuffEffect.TimePI)), buff);

                    if (this is Player)
                        (this as Player).client.SendPacket(new UpdateCharStats((this as Player)).Compile());
                    World.SendToAll(new QueDele(this.m_Channel, this.m_Map, new LKCamelot.model.SetObjectEffects(this).Compile()));
                    return true;
                }
                else
                    return false;
            }
        }
    }





    public class BaseGuild
    {
        public string Name;                             // 길드 이름
        public string[] PostionName = new string[6]{ "수장", "부수장", "행동대장", "특수부", "행동대", "맴버" };    // 길드 직책명

        //길드맵 관련
        public bool GuildMapEnabled = true;
        public script.npc.BoardSign Board;

        //길드원 정보
        public Dictionary<Serial, int> Member = new Dictionary<Serial, int>();


        public BaseGuild()
        {
        }
        public BaseGuild(string[] line)
        {
            this.Name = line[0].ToUpper();
            this.GuildMapEnabled = Convert.ToBoolean(line[1]);


            int Channel = Convert.ToInt32(line[2]);
            string Map = line[3];
            Point2D Loc = new Point2D(Convert.ToInt32(line[4]), Convert.ToInt32(line[5]));
            string Message = line[6];

            if (!this.GuildMapEnabled)
            {
                //길드 표지판 생성            
                Board = new script.npc.BoardSign(model.Spawnere.SignSerial++, Channel, Settings.VillageName.ElementAt(Channel) + "," + Map, Loc, 2, Message);
                World.NewNpcs[Channel].TryAdd(Board.m_Serial, Board);
            }

            for (int i = 0; i < 6; i++)
            {
                PostionName[i] = line[7 + i];
            }

            foreach (var p in line.Skip(13))
            {
                string[] temp = p.Split('-');
                Member.Add(Convert.ToInt32(temp[0]), Convert.ToInt32(temp[1]));
            }
            
        }


        public string ToStringPostionName()
        {
            string result = "";
            foreach (var p in PostionName)
            {
                result += "," + p;
            }

            result = result.Remove(0, 1);

            return result;
        }

        public string ToStringNember(List<Serial> list)
        {
            string result = "";
            foreach (var m in Member)
            {
                if(!list.Contains(m.Key) && PlayerHandler.getSingleton().Total.Any(xe=>xe.Value.m_Serial == m.Key) )
                    result += "," + (int)m.Key + "-" + m.Value;
            }

            result = result.Remove(0, 1);

            return result;
        }

        /// <summary>
        /// 직책명으로 해당 직책의 번호를 얻는다
        /// </summary>
        /// <param name="Potion">직책명</param>
        /// <returns>직책번호</returns>
        public int FindPosition(string Potion)
        {
            if (!PostionName.Where(xe => xe.Equals(Potion)).Any())
                return -1;

            int i = 5;
            foreach (var p in PostionName.Reverse())
            {
                if (p == Potion)
                    return i;
                --i;
            }
            return i;
        }

        /// <summary>
        /// 해당유저의 직책을 검색한다.
        /// </summary>
        /// <param name="PlayerName">유저명</param>
        /// <returns>직책명</returns>
        public string GetPositionString(string PlayerName)
        {
            var player = PlayerHandler.getSingleton().Total.Where(xe => xe.Value.Name == PlayerName).FirstOrDefault();
            if (player.Value == null)
                return "";
            int position;

            Member.TryGetValue(player.Value.m_Serial, out position);

            return PostionName[position];
        }
    }
}
