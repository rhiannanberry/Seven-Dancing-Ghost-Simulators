using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public enum VertexType {Wall, Door, Window};

public class Vertex {
    public Vector3 position;
    public bool wall = false, door = false, window = false;

    public int id;

    public List<int> idList;

    public Vertex(Vector3 position, int id = 0, params VertexType[] vTypes) {
        this.id = id;
        idList = new List<int>();
        idList.Add(id);
        this.position = position;
        if (vTypes.Length == 0) {
            wall = true;
        } else {
            foreach(VertexType vType in vTypes) {
                switch (vType) {
                    case VertexType.Wall:
                        wall = true;
                        break;
                    case VertexType.Door:
                        door = true;
                        break;
                    case VertexType.Window:
                        window = true;
                        break;
                }
            }
        }
    }

    public void AddVertexType(params VertexType[] vs) {
        foreach(VertexType v in vs) {
            switch (v) {
                case VertexType.Wall:
                    wall = true;
                    break;
                case VertexType.Door:
                    door = true;
                    break;
                case VertexType.Window:
                    window = true;
                    break;
            }
        }
    }

    public VertexType[] GetVertexTypes() {
        List<VertexType> vt = new List<VertexType>();
        if (wall) vt.Add(VertexType.Wall);
        if (door) vt.Add(VertexType.Door);
        if (window) vt.Add(VertexType.Window);
        return vt.ToArray();
    }

    public static Vertex[] MergeDistinct(List<Vertex> xList, List<Vertex> yList, List<Vertex> origVertList) {
        Vertex[] xDistinct = Vertex.GetDistinct(xList);
        
        string xstr = "X Sorted: \n";
        foreach(Vertex vx in xDistinct) {
            xstr += vx.ToString() + "\n";
        }
        Debug.Log(xstr);
        Vertex[] yDistinct = Vertex.GetDistinct(yList);

        
        string ystr = "Y Sorted: \n";
        foreach(Vertex vy in yDistinct) {
            ystr += vy.ToString() + "\n";
        }
        Debug.Log(ystr);
        return Vertex.GetMerged(xDistinct, yDistinct, origVertList);
    }

    public static Vertex[] GetDistinct(List<Vertex> list) {
        List<Vertex> distinctList = new List<Vertex>();
        distinctList.Add(list[0]);
        list.RemoveAt(0);


        foreach(Vertex v in list) {
            bool distinct = true;
            foreach(Vertex vd in distinctList) {
                if (vd.position.Equals(v.position)) {
                    distinct = false;
                    vd.AddVertexType(v.GetVertexTypes());
                    break;
                }
            }
            if (distinct) distinctList.Add(v);
        }
        return Vertex.Sort(distinctList);
    }

    public static Vertex[] GetMerged(Vertex[] x, Vertex[] y, List<Vertex> origVertList) {
        List<Vertex> merged = new List<Vertex>();
        foreach (Vertex yv in y) {
            foreach (Vertex xv in x) {
                Vertex origUsed = null;
                Vertex newV = new Vertex(new Vector3(xv.position.x, yv.position.y), 0, xv.GetVertexTypes());
                foreach(Vertex v in origVertList) {
                    if (v.position == newV.position) {
                        origUsed = v;
                        break;
                    }
                }
                if (origUsed == null) {
                    newV.wall = (newV.wall && yv.wall);
                    newV.door = (newV.door && yv.door);
                    newV.window = (newV.window && yv.window);
                    merged.Add(newV);
                } else {
                    origVertList.Remove(origUsed);
                    merged.Add(origUsed);

                }
                
            }
        }
        return merged.ToArray();
    }

    public static Vertex[] Sort(List<Vertex> vu) {
        List<Vertex> sorted = new List<Vertex>();
        foreach(Vertex v in vu) {
            bool first = true;
            for (int i = 0; i < sorted.Count; i++) {
                if (Vector3.Magnitude(v.position) <= Vector3.Magnitude(sorted[i].position)) {
                    
                    sorted.Insert(i, v);
                    
                    first = false;
                    break;
                }
            }
            if (first) {
                sorted.Add(v);
            }
        }
        return sorted.ToArray();
    }

    public string ToString() {
        string str = position.ToString();
        str += " {";
        if (wall) str += " Wall";
        if (door) str += " Door";
        if (window) str += " Window";
        str += " }";
        return str;
    }

    public static Vector3[] GetPositionArray(Vertex[] vertArr) {
        Vector3[] vArr = new Vector3[vertArr.Length];
        for(int i = 0; i < vertArr.Length; i++) {
            vArr[i] = vertArr[i].position;
        }
        return vArr;
    }
}