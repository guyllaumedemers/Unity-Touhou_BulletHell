using System.Collections.Generic;
using System.Data;
using System.Linq;
using Mono.Data.Sqlite;
using UnityEngine;

public enum SQLTable
{
    Bullet,
    UnitData,
}

public static class DatabaseHandler
{
    private static readonly string db_name = "URI=file:TouhouDatabase.db";

    #region public functions

    public static T[] RetrieveTableEntries<T>(string table) where T : class
    {
        //System.Type myType = typeof(T);
        //if (myType != typeof(BulletDataContainer) || myType != typeof(UnitDataContainer))
        //{
        //    LogWarning($"Invalid Type - class type can only be {typeof(BulletDataContainer)} OR {typeof(UnitDataContainer)}");
        //    return null;
        //}

        List<T> myOBJ = new List<T>();
        using (var connection = new SqliteConnection(db_name))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT * FROM {table};";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        switch (System.Enum.Parse(typeof(SQLTable), table))
                        {
                            case SQLTable.Bullet:
                                BulletDataContainer bdc = new BulletDataContainer((BulletTypeEnum)System.Enum.Parse(typeof(BulletTypeEnum), reader.GetString(0)), reader.GetFloat(1), reader.GetFloat(2));
                                myOBJ.Add(bdc as T);
                                break;
                            case SQLTable.UnitData:
                                UnitDataContainer udc = new UnitDataContainer((UnitTypeEnum)System.Enum.Parse(typeof(UnitTypeEnum), reader.GetString(0)), reader.GetFloat(1), reader.GetFloat(2), reader.GetFloat(3));
                                myOBJ.Add(udc as T);
                                break;
                            default:
                                break;
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
