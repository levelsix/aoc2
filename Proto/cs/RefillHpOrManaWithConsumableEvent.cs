//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: RefillHpOrManaWithConsumableEvent.proto
// Note: requires additional types generated from: FullUserProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RefillHpOrManaWithConsumableRequestProto")]
  public partial class RefillHpOrManaWithConsumableRequestProto : global::ProtoBuf.IExtensible
  {
    public RefillHpOrManaWithConsumableRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private string _userConsumableId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"userConsumableId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string userConsumableId
    {
      get { return _userConsumableId; }
      set { _userConsumableId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RefillHpOrManaWithConsumableResponseProto")]
  public partial class RefillHpOrManaWithConsumableResponseProto : global::ProtoBuf.IExtensible
  {
    public RefillHpOrManaWithConsumableResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private proto.RefillHpOrManaWithConsumableResponseProto.RefillHpOrManaWithConsumableStatus _status = proto.RefillHpOrManaWithConsumableResponseProto.RefillHpOrManaWithConsumableStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(proto.RefillHpOrManaWithConsumableResponseProto.RefillHpOrManaWithConsumableStatus.SUCCESS)]
    public proto.RefillHpOrManaWithConsumableResponseProto.RefillHpOrManaWithConsumableStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"RefillHpOrManaWithConsumableStatus")]
    public enum RefillHpOrManaWithConsumableStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_ALREADY_AT_MAX_HP", Value=1)]
      FAIL_ALREADY_AT_MAX_HP = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_ALREADY_AT_MAX_MANA", Value=2)]
      FAIL_ALREADY_AT_MAX_MANA = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NO_POT_EXISTS", Value=3)]
      FAIL_NO_POT_EXISTS = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_ENOUGH_POTS", Value=4)]
      FAIL_NOT_ENOUGH_POTS = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_OTHER", Value=5)]
      FAIL_OTHER = 5
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}