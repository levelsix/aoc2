//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: TrainOrUpgradeSpellEvent.proto
// Note: requires additional types generated from: FullUserProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TrainOrUpgradeSpellRequestProto")]
  public partial class TrainOrUpgradeSpellRequestProto : global::ProtoBuf.IExtensible
  {
    public TrainOrUpgradeSpellRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private string _spellId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"spellId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string spellId
    {
      get { return _spellId; }
      set { _spellId = value; }
    }

    private bool _usingGems = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"usingGems", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool usingGems
    {
      get { return _usingGems; }
      set { _usingGems = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"TrainOrUpgradeSpellResponseProto")]
  public partial class TrainOrUpgradeSpellResponseProto : global::ProtoBuf.IExtensible
  {
    public TrainOrUpgradeSpellResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private proto.TrainOrUpgradeSpellResponseProto.TrainOrUpgradeSpellStatus _status = proto.TrainOrUpgradeSpellResponseProto.TrainOrUpgradeSpellStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(proto.TrainOrUpgradeSpellResponseProto.TrainOrUpgradeSpellStatus.SUCCESS)]
    public proto.TrainOrUpgradeSpellResponseProto.TrainOrUpgradeSpellStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"TrainOrUpgradeSpellStatus")]
    public enum TrainOrUpgradeSpellStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_INSUFFICIENT_RESOURCES", Value=1)]
      FAIL_INSUFFICIENT_RESOURCES = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_AT_REQUIRED_LEVEL", Value=2)]
      FAIL_NOT_AT_REQUIRED_LEVEL = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_MAXED_TRAINING", Value=3)]
      FAIL_MAXED_TRAINING = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_SPELL_AT_MAX_LEVEL", Value=4)]
      FAIL_SPELL_AT_MAX_LEVEL = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_CANT_UPGRADE_WHILE_TRAINING", Value=5)]
      FAIL_CANT_UPGRADE_WHILE_TRAINING = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_WRONG_CLASS_TYPE", Value=6)]
      FAIL_WRONG_CLASS_TYPE = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_MISSING_PREREQUISITE_SPELL", Value=7)]
      FAIL_MISSING_PREREQUISITE_SPELL = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_ENOUGH_GEMS", Value=8)]
      FAIL_NOT_ENOUGH_GEMS = 8,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_OTHER", Value=9)]
      FAIL_OTHER = 9
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}