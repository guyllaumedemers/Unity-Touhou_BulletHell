using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public static class DatabaseHandler
{
    private static readonly string db_name = "URI=file:TouhouDatabase.db";

    #region public functions

    public static T[] RetrieveTableEntries<T>(string table, string extendQuery = null) where T : class
    {
        List<T> myOBJ = new List<T>();
        using (var connection = new SqliteConnection(db_name))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM {table} {extendQuery};";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        switch (System.Enum.Parse(typeof(SQLTableEnum), table))
                        {
                            case SQLTableEnum.Bullet:
                                BulletDataContainer bdc = new BulletDataContainer((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), reader.GetString(0)), reader.GetFloat(1), reader.GetFloat(2));
                                myOBJ.Add(bdc as T);
                                break;
                            case SQLTableEnum.UnitData:
                                UnitDataContainer udc = new UnitDataContainer((UnitTypeEnum)System.Enum.Parse(typeof(UnitTypeEnum), reader.GetString(0)), reader.GetFloat(1), reader.GetFloat(2), reader.GetFloat(3));
                                myOBJ.Add(udc as T);
                                break;
                            case SQLTableEnum.Wave:
                                break;
                            case SQLTableEnum.Waypoint:
                                Tool.Vector3Wrapper vwrapper = new Tool.Vector3Wrapper(reader.GetFloat(2), reader.GetFloat(3), 0.0f);
                                myOBJ.Add(vwrapper as T);
                                break;
                            default:
                                throw new System.InvalidOperationException();
                        }
                    }
                    reader.Close();
                }
            }
            connection.Close();
        }
        return myOBJ.ToArray();
    }

    #endregion

    private static void LogWarning(string msg) => Debug.LogWarning("[Database Handler] " + msg);
}
