//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: DungeonHistoryProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"DungeonHistoryProto")]
  public partial class DungeonHistoryProto : global::ProtoBuf.IExtensible
  {
    public DungeonHistoryProto() {}
    

    private int _dungeonID = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"dungeonID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int dungeonID
    {
      get { return _dungeonID; }
      set { _dungeonID = value; }
    }

    private int _roomID = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"roomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int roomID
    {
      get { return _roomID; }
      set { _roomID = value; }
    }

    private int _timeTaken = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"timeTaken", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int timeTaken
    {
      get { return _timeTaken; }
      set { _timeTaken = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}