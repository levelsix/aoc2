//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: SpellProto.proto
// Note: requires additional types generated from: JobType.proto
// Note: requires additional types generated from: ResourceType.proto
namespace com.lvl6.aoc2.proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SpellProto")]
  public partial class SpellProto : global::ProtoBuf.IExtensible
  {
    public SpellProto() {}
    

    private int _spellID = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"spellID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int spellID
    {
      get { return _spellID; }
      set { _spellID = value; }
    }
    private com.lvl6.aoc2.proto.JobType _class;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"class", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public com.lvl6.aoc2.proto.JobType @class
    {
      get { return _class; }
      set { _class = value; }
    }

    private string _name = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }

    private com.lvl6.aoc2.proto.SpellProto.FunctionType _function = com.lvl6.aoc2.proto.SpellProto.FunctionType.ATTACK;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"function", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.aoc2.proto.SpellProto.FunctionType.ATTACK)]
    public com.lvl6.aoc2.proto.SpellProto.FunctionType function
    {
      get { return _function; }
      set { _function = value; }
    }

    private int _strength = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"strength", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int strength
    {
      get { return _strength; }
      set { _strength = value; }
    }

    private int _manaCost = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"manaCost", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int manaCost
    {
      get { return _manaCost; }
      set { _manaCost = value; }
    }

    private int _duration = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"duration", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int duration
    {
      get { return _duration; }
      set { _duration = value; }
    }

    private int _speed = default(int);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"speed", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int speed
    {
      get { return _speed; }
      set { _speed = value; }
    }

    private bool _targetted = default(bool);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"targetted", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool targetted
    {
      get { return _targetted; }
      set { _targetted = value; }
    }

    private float _size = default(float);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"size", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float size
    {
      get { return _size; }
      set { _size = value; }
    }

    private float _castTime = default(float);
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"castTime", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float castTime
    {
      get { return _castTime; }
      set { _castTime = value; }
    }

    private float _coolDown = default(float);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"coolDown", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float coolDown
    {
      get { return _coolDown; }
      set { _coolDown = value; }
    }

    private float _range = default(float);
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"range", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float range
    {
      get { return _range; }
      set { _range = value; }
    }

    private int _levelReq = default(int);
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"levelReq", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int levelReq
    {
      get { return _levelReq; }
      set { _levelReq = value; }
    }

    private int _spellLvl = default(int);
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"spellLvl", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int spellLvl
    {
      get { return _spellLvl; }
      set { _spellLvl = value; }
    }

    private int _researchCost = default(int);
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"researchCost", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int researchCost
    {
      get { return _researchCost; }
      set { _researchCost = value; }
    }

    private int _researchTime = default(int);
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"researchTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int researchTime
    {
      get { return _researchTime; }
      set { _researchTime = value; }
    }
    private com.lvl6.aoc2.proto.ResourceType _researchResource;
    [global::ProtoBuf.ProtoMember(18, IsRequired = true, Name=@"researchResource", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public com.lvl6.aoc2.proto.ResourceType researchResource
    {
      get { return _researchResource; }
      set { _researchResource = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"FunctionType")]
    public enum FunctionType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"ATTACK", Value=0)]
      ATTACK = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FLAT_BUFF", Value=1)]
      FLAT_BUFF = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SCALE_BUFF", Value=2)]
      SCALE_BUFF = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"HEAL_SELF", Value=3)]
      HEAL_SELF = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"HEAL_OTHER", Value=4)]
      HEAL_OTHER = 4
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}