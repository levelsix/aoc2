//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: CollectUserConsumableEvent.proto
// Note: requires additional types generated from: UserConsumableQueueProto.proto
// Note: requires additional types generated from: FullUserProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CollectUserConsumableRequestProto")]
  public partial class CollectUserConsumableRequestProto : global::ProtoBuf.IExtensible
  {
    public CollectUserConsumableRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }
    private readonly global::System.Collections.Generic.List<proto.UserConsumableQueueProto> _ucqpList = new global::System.Collections.Generic.List<proto.UserConsumableQueueProto>();
    [global::ProtoBuf.ProtoMember(2, Name=@"ucqpList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<proto.UserConsumableQueueProto> ucqpList
    {
      get { return _ucqpList; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CollectUserConsumableResponseProto")]
  public partial class CollectUserConsumableResponseProto : global::ProtoBuf.IExtensible
  {
    public CollectUserConsumableResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }
    private readonly global::System.Collections.Generic.List<proto.UserConsumableQueueProto> _ucqpList2 = new global::System.Collections.Generic.List<proto.UserConsumableQueueProto>();
    [global::ProtoBuf.ProtoMember(2, Name=@"ucqpList2", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<proto.UserConsumableQueueProto> ucqpList2
    {
      get { return _ucqpList2; }
    }
  

    private proto.CollectUserConsumableResponseProto.CollectUserConsumableStatus _status = proto.CollectUserConsumableResponseProto.CollectUserConsumableStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(proto.CollectUserConsumableResponseProto.CollectUserConsumableStatus.SUCCESS)]
    public proto.CollectUserConsumableResponseProto.CollectUserConsumableStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"CollectUserConsumableStatus")]
    public enum CollectUserConsumableStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_COMPLETE", Value=1)]
      FAIL_NOT_COMPLETE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_OTHER", Value=2)]
      FAIL_OTHER = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}