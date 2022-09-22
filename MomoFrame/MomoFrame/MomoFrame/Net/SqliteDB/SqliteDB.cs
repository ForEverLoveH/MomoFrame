using PEProtocol;
using PEUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class SqliteDB
{
    public static SqliteDB Instance;

    public void Awake()
    {
        Instance = this;
    }

    //private string DBPath = Application.StartupPath + Constants.DBPath;
    //private string DatabasePath = Application.StartupPath + Constants.DatabasePath;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //if (!Directory.Exists(DBPath))
        //{
        //    Directory.CreateDirectory(DBPath);
        //    Debug.LogError($"Database文件夹已丢失,已重新创建 Database文件夹路径:{DBPath}");
        //}

        //if (File.Exists(DatabasePath))
        //{
        //    Debug.Log("Database.db文件存在");
        //}
        //else
        //{
        //    using (SqlDbCommand sql = new SqlDbCommand(DatabasePath)){}
        //    PELog.Error($"Database.db文件不存在,已重新创建   Database.db文件路径{DatabasePath}");
        //}

        //IsExistenceAdminData("Admin");
    }

    // <summary>
    /// 是否存在Admin表
    /// </summary>
    /// <param name="name"></param>
    public void IsExistenceAdminData(string name)
    {
        
        //using (SqlDbCommand sql = new SqlDbCommand(DatabasePath))
        //{
        //    int isExistenceAdminData = sql.IsCreateTable(name);
        //    if (isExistenceAdminData == 0)
        //    {
        //        sql.CreateTable<AdminDBTitle>(name);

        //        //初始化Admin表
        //        Admin admin = new Admin() { User = "Admin", Password = "123456" };
        //        Admin user = new Admin() { User = "User", Password = "123456" };
        //        List<Admin> admins = new List<Admin>();
        //        admins.Add(admin);
        //        admins.Add(user);
        //        sql.Insert<Admin>(admins, name);
        //        Debug.LogError($"数据库表{name}不存在,已初始化{name}表");

        //    }
        //    if (isExistenceAdminData == 1)
        //    {
        //        Debug.Log($"数据库表{name}存在");
        //    }
        //}
    }


    public void TestAddSqliteDB(string name)
    {
        //using (SqlDbCommand sql = new SqlDbCommand(DatabasePath))
        //{
        //    List<Admin> admins = new List<Admin>();
        //    Admin admin = new Admin() { User = "Admin", Password = "123456" };
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        admins.Add(admin);
        //    }
        //    sql.Insert<Admin>(admins, name);
        //}

    }

    public void TestQuerySqliteDB(string name)
    {
    //    using (SqlDbCommand sql = new SqlDbCommand(DatabasePath))
    //    {
    //        sql.SelectBySql<Admin>(name);
    //    }
    }

}
