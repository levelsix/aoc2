//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: ConsumableProto.proto
// Note: requires additional types generated from: ResourceType.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ConsumableProto")]
  public partial class ConsumableProto : global::ProtoBuf.IExtensible
  {
    public ConsumableProto() {}
    

    private int _consumeID = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"consumeID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int consumeID
    {
      get { return _consumeID; }
      set { _consumeID = value; }
    }

    private int _health = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"health", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int health
    {
      get { return _health; }
      set { _health = value; }
    }

    private int _mana = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"mana", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int mana
    {
      get { return _mana; }
      set { _mana = value; }
    }

    private int _cost = default(int);
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"cost", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int cost
    {
      get { return _cost; }
      set { _cost = value; }
    }

    private int _limit = default(int);
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"limit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int limit
    {
      get { return _limit; }
      set { _limit = value; }
    }

    private int _timeToCreate = default(int);
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"timeToCreate", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int timeToCreate
    {
      get { return _timeToCreate; }
      set { _timeToCreate = value; }
    }
    private proto.ResourceType _resourceToCreate;
    [global::ProtoBuf.ProtoMember(19, IsRequired = true, Name=@"resourceToCreate", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public proto.ResourceType resourceToCreate
    {
      get { return _resourceToCreate; }
      set { _resourceToCreate = value; }
    }

    private int _baseSpeedUpBuildCost = default(int);
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"baseSpeedUpBuildCost", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int baseSpeedUpBuildCost
    {
      get { return _baseSpeedUpBuildCost; }
      set { _baseSpeedUpBuildCost = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"ConsumableType")]
    public enum ConsumableType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"HEALTH", Value=0)]
      HEALTH = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MANA", Value=1)]
      MANA = 1
    }
  
}