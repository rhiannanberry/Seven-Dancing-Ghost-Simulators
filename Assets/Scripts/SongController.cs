using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SongController : MonoBehaviour {
    public struct Metadata {
        public bool valid;

        public string title;
        public string subtitle;
        public string artist;

        public string musicPath; //gonna use resources load for this probably like Resources.Load("music/0000")
        //likely will be necessary to import the succes and failure sounds as well

        public float offset;

        public float bpm;

        public NoteData noteData;
    }

    public struct NoteData {
        public List<List<NoteData>> bars;
    }

    public struct Notes {
        public bool left;
        public bool right;
        public bool up;
        public bool down;
    }

	// Use this for initialization
	void Start () {
		
	}

    void setSong(string songName) {
        //TODO: introduce error handling for invalid sm

        bool inNotes = false; //are we parsing info metadata or notes

        Metadata songData = new Metadata();
        songData.valid = true;
        songData.musicPath = songName; //does this need extension or?

        string[] fileData = File.ReadAllLines("SMFiles/" + songName + ".sm");
        foreach (string line in fileData) {
            if (line.StartsWith("//")) {
                continue;
            } else if (!inNotes && line.StartsWith("#")) {
                string key = line.Substring(0, line.IndexOf(":")).Trim('#').Trim(':');
                switch (key.ToUpper()) {
                    case "TITLE":
                        songData.title = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "SUBTITLE":
                        songData.subtitle = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "OFFSET":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.offset)) {
                            //Error Parsing
                            songData.offset = 0.0f;
                        }
                        break;
                    case "DISPLAYBPM":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.bpm) || songData.bpm == 0) {
                            //Error Parsing - BPM not valid
                            songData.valid = false;
                            songData.bpm = 0.0f;
                        }
                        break;
                    case "NOTES":
                        inNotes = true;
                        break;
                    default:
                        break;
                }
            }
            if (inNotes)
            {

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
