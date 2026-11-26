using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace LKCamelot
{
    public class GameData
    {
        private DataSet MobData = new DataSet();

        public GameData(){}

        private static GameData _instance;
        public static GameData getInstance()
        {

            if (_instance == null)
                _instance = new GameData();

            return _instance;            
        }

        public void ResetMonster()
        {
            string strConn = Settings.DB_Conn;
            using (MySqlConnection conn = new MySqlConnection(strConn))
            {
                string strQuery = "SELECT * FROM monstermaster";
                conn.Open();
                MySqlDataAdapter adpt = new MySqlDataAdapter(strQuery, conn);
                MobData.Clear();
                adpt.Fill(MobData, "Mob");
            }
        }

        public DataTable GetMobs()
        {
            return MobData.Tables["Mob"];
        }
        public DataRow GetMob(string name)
        {
            DataRow[] data = MobData.Tables["Mob"].Select(string.Format("ENG_NAME = '{0}'", name));

            if (data.Count() == 0)
                return null;
            return data[0];
        }
        public DataRow GetMob(int index)
        {
            DataRow[] data = null;
            try
            {
                data = MobData.Tables["Mob"].Select(string.Format("M_INDEX = '{0}'", index));
            }
            catch
            {
                return null;
            }

            if (data.Count() == 0)
                return null;
            return data[0];
        }
    }
}
