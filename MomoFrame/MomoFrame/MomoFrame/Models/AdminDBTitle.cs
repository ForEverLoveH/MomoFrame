using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 用户登录数据表模板
/// </summary>
public class AdminDBTitle
{
    private int _id;
    private string _user;
    private string _password;

    [ModeHelp(true,"Id", "integer", false, true, true)]
    public int Id { get { return _id; } set { _id = value; } }

    [ModeHelp(true, "User", "string", false,false)]
    public string User { get { return _user; } set { _user = value; } }

    [ModeHelp(true, "Password", "string", false,false)]
    public string Password { get { return _password; } set { _password = value; } }
}
