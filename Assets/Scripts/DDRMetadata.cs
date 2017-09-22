using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DDRMetadata
{
    public bool valid;
    public string title;
    public string subtitle;

    public string musicPath;

    public float offset;

    public NoteData noteData;
}

public struct NoteData
{
    public List<List<NoteData>> bars;
}

public struct Notes
{
    public bool left;
    public bool up;
    public bool right;
    public bool down;
}