using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    private static rifle userRifle;
    private static User usuario;
    private static gun userGun;
 
    public static User Usuario
    {
        get
        {
            return usuario;
        }
        set
        {
            usuario = value;
        }
    }
    public static rifle UserRifle
    {
        get
        {
            return userRifle;
        }
        set
        {
            userRifle = value;
        }
    }
    public static gun UserGun
    {
        get
        {
            return userGun;
        }
        set
        {
            userGun = value;
        }
    }
}
