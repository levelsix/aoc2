//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: UserSpellProto.proto
namespace proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserSpellProto")]
  public partial class UserSpellProto : global::ProtoBuf.IExtensible
  {
    public UserSpellProto() {}
    

    private int _userID = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"userID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userID
    {
      get { return _userID; }
      set { _userID = value; }
    }

    private int _spellID = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"spellID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int spellID
    {
      get { return _spellID; }
      set { _spellID = value; }
    }

    private int _level = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}