//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: FullUserProto.proto
// Note: requires additional types generated from: JobType.proto
namespace com.lvl6.aoc2.proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FullUserProto")]
  public partial class FullUserProto : global::ProtoBuf.IExtensible
  {
    public FullUserProto() {}
    

    private int _userID = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }

    private int _level = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }

    private string _name = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }

    private int _exp = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"exp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int exp
    {
      get { return _exp; }
      set { _exp = value; }
    }

    private int _gold = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int gold
    {
      get { return _gold; }
      set { _gold = value; }
    }

    private int _tonic = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"tonic", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int tonic
    {
      get { return _tonic; }
      set { _tonic = value; }
    }

    private int _gems = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"gems", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int gems
    {
      get { return _gems; }
      set { _gems = value; }
    }
    private com.lvl6.aoc2.proto.JobType _job;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"job", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public com.lvl6.aoc2.proto.JobType job
    {
      get { return _job; }
      set { _job = value; }
    }

    private int _lastHealth = default(int);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"lastHealth", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int lastHealth
    {
      get { return _lastHealth; }
      set { _lastHealth = value; }
    }

    private int _maxHealth = default(int);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"maxHealth", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int maxHealth
    {
      get { return _maxHealth; }
      set { _maxHealth = value; }
    }

    private long _lastHealthRegen = default(long);
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"lastHealthRegen", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long lastHealthRegen
    {
      get { return _lastHealthRegen; }
      set { _lastHealthRegen = value; }
    }

    private int _lastMana = default(int);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"lastMana", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int lastMana
    {
      get { return _lastMana; }
      set { _lastMana = value; }
    }

    private int _maxMana = default(int);
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"maxMana", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int maxMana
    {
      get { return _maxMana; }
      set { _maxMana = value; }
    }

    private long _lastManaRecovery = default(long);
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"lastManaRecovery", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long lastManaRecovery
    {
      get { return _lastManaRecovery; }
      set { _lastManaRecovery = value; }
    }

    private int _gameCenterID = default(int);
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"gameCenterID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int gameCenterID
    {
      get { return _gameCenterID; }
      set { _gameCenterID = value; }
    }

    private long _dateCreated = default(long);
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"dateCreated", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long dateCreated
    {
      get { return _dateCreated; }
      set { _dateCreated = value; }
    }

    private int _clanID = default(int);
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"clanID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int clanID
    {
      get { return _clanID; }
      set { _clanID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}