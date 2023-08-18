using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class album 
{

    public int songPosition;
    public string artistName;
    public string songName;

        public album(int position, string artName, string song)

        {

        songPosition = position;
        artistName = artName;
        songName = song;


        }


    }


