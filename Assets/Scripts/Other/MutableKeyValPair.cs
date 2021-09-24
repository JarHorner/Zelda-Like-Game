using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutableKeyValPair<Key, Value>
{
    public Key key { get; set; }
    public Value value { get; set; }

    public MutableKeyValPair() { }

    public MutableKeyValPair(Key key, Value value)
    {
        this.key = key;
        this.value = value;
    }
}
