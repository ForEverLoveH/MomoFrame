using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ��¼����ģ��
/// </summary>
public class Admin
{
    private string _user;
    private string _password;

    [ModeHelp(true, "User", "string", false, false)]
    public string User { get { return _user; } set { _user = value; } }

    [ModeHelp(true, "Password", "string", false, false)]
    public string Password { get { return _password; } set { _password = value; } }
}
