//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: RetrieveStaticDataProto.proto
// Note: requires additional types generated from: FullUserProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RetrieveStaticDataRequestProto")]
  public partial class RetrieveStaticDataRequestProto : global::ProtoBuf.IExtensible
  {
    public RetrieveStaticDataRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private int _hp = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"hp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int hp
    {
      get { return _hp; }
      set { _hp = value; }
    }

    private int _mana = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"mana", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int mana
    {
      get { return _mana; }
      set { _mana = value; }
    }

    private int _attack = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"attack", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int attack
    {
      get { return _attack; }
      set { _attack = value; }
    }

    private int _defense = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"defense", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int defense
    {
      get { return _defense; }
      set { _defense = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RetrieveStaticDataResponseProto")]
  public partial class RetrieveStaticDataResponseProto : global::ProtoBuf.IExtensible
  {
    public RetrieveStaticDataResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private int _userHp = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"userHp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userHp
    {
      get { return _userHp; }
      set { _userHp = value; }
    }

    private int _userMana = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"userMana", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userMana
    {
      get { return _userMana; }
      set { _userMana = value; }
    }

    private int _userAttack = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"userAttack", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userAttack
    {
      get { return _userAttack; }
      set { _userAttack = value; }
    }

    private int _userDefense = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"userDefense", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userDefense
    {
      get { return _userDefense; }
      set { _userDefense = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}