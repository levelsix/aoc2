//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: BuildOrUpgradeStructureEvent.proto
// Note: requires additional types generated from: FullUserProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BuildOrUpgradeStructureRequestProto")]
  public partial class BuildOrUpgradeStructureRequestProto : global::ProtoBuf.IExtensible
  {
    public BuildOrUpgradeStructureRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private string _userStructureId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"userStructureId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string userStructureId
    {
      get { return _userStructureId; }
      set { _userStructureId = value; }
    }

    private bool _isBuild = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"isBuild", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool isBuild
    {
      get { return _isBuild; }
      set { _isBuild = value; }
    }

    private bool _usingGems = default(bool);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"usingGems", DataFormat = global::ProtoBuf.DataFormat.Default)]
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
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"BuildOrUpgradeStructureResponseProto")]
  public partial class BuildOrUpgradeStructureResponseProto : global::ProtoBuf.IExtensible
  {
    public BuildOrUpgradeStructureResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private proto.BuildOrUpgradeStructureResponseProto.BuildOrUpgradeStructureStatus _status = proto.BuildOrUpgradeStructureResponseProto.BuildOrUpgradeStructureStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(proto.BuildOrUpgradeStructureResponseProto.BuildOrUpgradeStructureStatus.SUCCESS)]
    public proto.BuildOrUpgradeStructureResponseProto.BuildOrUpgradeStructureStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"BuildOrUpgradeStructureStatus")]
    public enum BuildOrUpgradeStructureStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_INSUFFICIENT_RESOURCES", Value=1)]
      FAIL_INSUFFICIENT_RESOURCES = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_AT_REQUIRED_LEVEL", Value=2)]
      FAIL_NOT_AT_REQUIRED_LEVEL = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_MAXED_CONSTRUCTION", Value=3)]
      FAIL_MAXED_CONSTRUCTION = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NO_STRUCTURE_EXISTS", Value=4)]
      FAIL_NO_STRUCTURE_EXISTS = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_RESTRICTION_ON_NUMBER_OF_STRUCTURES", Value=5)]
      FAIL_RESTRICTION_ON_NUMBER_OF_STRUCTURES = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_STRUCTURE_AT_MAX_LEVEL", Value=6)]
      FAIL_STRUCTURE_AT_MAX_LEVEL = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_CANT_UPGRADE_WHILE_BUILDING", Value=7)]
      FAIL_CANT_UPGRADE_WHILE_BUILDING = 7,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_OTHER", Value=8)]
      FAIL_OTHER = 8
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"ResourceCostType")]
    public enum ResourceCostType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"GOLD", Value=0)]
      GOLD = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TONIC", Value=1)]
      TONIC = 1
    }
  
}