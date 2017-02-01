using System;
using System.Collections.Generic;

public class Authentificator
{
    private static Authentificator instance;
    private Authentificator() {
        Cookie = new Dictionary<string, string>();
    }
    public static Authentificator Instance {
        get
        {
            if (instance == null)
                instance = new Authentificator();
            return instance;
        }
                
    }
    public Dictionary<string, string>  Cookie {get; set; }
    public void setCookie(string token)
    {
        if (token != null)
        {
            String tmp = token.Split(';')[0];
            if (tmp != null)
            {
                String key = tmp.Split('=')[0];
                String value = tmp.Split('=')[1];
                Cookie.Add(key, value);
            }
        }
    }
}
