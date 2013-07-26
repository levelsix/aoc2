using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AOC2DataList<T> {
 
    private Dictionary<int, T> dict;
    
    public T this[int i]{
        get
        {
            return dict[i];
        }
    }
    
}
