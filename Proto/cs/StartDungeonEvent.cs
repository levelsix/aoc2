//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: StartDungeonEvent.proto
// Note: requires additional types generated from: FullUserProto.proto
// Note: requires additional types generated from: UserEquipmentProto.proto
// Note: requires additional types generated from: UserConsumablesProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StartDungeonRequestProto")]
  public partial class StartDungeonRequestProto : global::ProtoBuf.IExtensible
  {
    public StartDungeonRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }
    private readonly global::System.Collections.Generic.List<proto.UserEquipmentProto> _uerList = new global::System.Collections.Generic.List<proto.UserEquipmentProto>();
    [global::ProtoBuf.ProtoMember(2, Name=@"uerList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<proto.UserEquipmentProto> uerList
    {
      get { return _uerList; }
    }
  
    private readonly global::System.Collections.Generic.List<proto.UserConsumablesProto> _ucpList = new global::System.Collections.Generic.List<proto.UserConsumablesProto>();
    [global::ProtoBuf.ProtoMember(3, Name=@"ucpList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<proto.UserConsumablesProto> ucpList
    {
      get { return _ucpList; }
    }
  

    private string _dungeonName = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"dungeonName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string dungeonName
    {
      get { return _dungeonName; }
      set { _dungeonName = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StartDungeonResponseProto")]
  public partial class StartDungeonResponseProto : global::ProtoBuf.IExtensible
  {
    public StartDungeonResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private proto.StartDungeonResponseProto.StartDungeonStatus _status = proto.StartDungeonResponseProto.StartDungeonStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(proto.StartDungeonResponseProto.StartDungeonStatus.SUCCESS)]
    public proto.StartDungeonResponseProto.StartDungeonStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"StartDungeonStatus")]
    public enum StartDungeonStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NO_HP", Value=1)]
      FAIL_NO_HP = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_AT_REQUIRED_LEVEL", Value=2)]
      FAIL_NOT_AT_REQUIRED_LEVEL = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_EQUIP_STORAGE_FULL", Value=3)]
      FAIL_EQUIP_STORAGE_FULL = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_ZERO_DURABILITY_EQUIP", Value=4)]
      FAIL_ZERO_DURABILITY_EQUIP = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_OTHER", Value=5)]
      FAIL_OTHER = 5
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}