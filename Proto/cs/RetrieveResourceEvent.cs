//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: RetrieveResourceEvent.proto
// Note: requires additional types generated from: ResourceType.proto
// Note: requires additional types generated from: FullUserProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RetrieveResourceRequestProto")]
  public partial class RetrieveResourceRequestProto : global::ProtoBuf.IExtensible
  {
    public RetrieveResourceRequestProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private string _structureId = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"structureId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string structureId
    {
      get { return _structureId; }
      set { _structureId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RetrieveResourceResponseProto")]
  public partial class RetrieveResourceResponseProto : global::ProtoBuf.IExtensible
  {
    public RetrieveResourceResponseProto() {}
    

    private proto.MinimumUserProto _mup = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"mup", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public proto.MinimumUserProto mup
    {
      get { return _mup; }
      set { _mup = value; }
    }

    private proto.RetrieveResourceResponseProto.RetrieveResourceStatus _status = proto.RetrieveResourceResponseProto.RetrieveResourceStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(proto.RetrieveResourceResponseProto.RetrieveResourceStatus.SUCCESS)]
    public proto.RetrieveResourceResponseProto.RetrieveResourceStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"RetrieveResourceStatus")]
    public enum RetrieveResourceStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NO_STRUCTURE_OR_USER_EXISTS", Value=1)]
      FAIL_NO_STRUCTURE_OR_USER_EXISTS = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_NOT_LONG_ENOUGH", Value=2)]
      FAIL_NOT_LONG_ENOUGH = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FAIL_OTHER", Value=3)]
      FAIL_OTHER = 3
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}