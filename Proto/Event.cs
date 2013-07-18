//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Event.proto
// Note: requires additional types generated from: Info.proto
namespace com.lvl6.proto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StartupRequestProto")]
  public partial class StartupRequestProto : global::ProtoBuf.IExtensible
  {
    public StartupRequestProto() {}
    

    private string _udid = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"udid", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string udid
    {
      get { return _udid; }
      set { _udid = value; }
    }

    private float _versionNum = default(float);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"versionNum", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue(default(float))]
    public float versionNum
    {
      get { return _versionNum; }
      set { _versionNum = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StartupResponseProto")]
  public partial class StartupResponseProto : global::ProtoBuf.IExtensible
  {
    public StartupResponseProto() {}
    

    private com.lvl6.proto.FullUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.FullUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.StartupResponseProto.StartupStatus _startupStatus = com.lvl6.proto.StartupResponseProto.StartupStatus.USER_IN_DB;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"startupStatus", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.StartupResponseProto.StartupStatus.USER_IN_DB)]
    public com.lvl6.proto.StartupResponseProto.StartupStatus startupStatus
    {
      get { return _startupStatus; }
      set { _startupStatus = value; }
    }

    private com.lvl6.proto.StartupResponseProto.UpdateStatus _updateStauts = com.lvl6.proto.StartupResponseProto.UpdateStatus.NO_UPDATE;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"updateStauts", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.StartupResponseProto.UpdateStatus.NO_UPDATE)]
    public com.lvl6.proto.StartupResponseProto.UpdateStatus updateStauts
    {
      get { return _updateStauts; }
      set { _updateStauts = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"UpdateStatus")]
    public enum UpdateStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"NO_UPDATE", Value=0)]
      NO_UPDATE = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MINOR_UPDATE", Value=1)]
      MINOR_UPDATE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MAJOR_UPDATE", Value=2)]
      MAJOR_UPDATE = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"StartupStatus")]
    public enum StartupStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"USER_IN_DB", Value=0)]
      USER_IN_DB = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"USER_NOT_IN_DB", Value=1)]
      USER_NOT_IN_DB = 1
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserCreateRequestProto")]
  public partial class UserCreateRequestProto : global::ProtoBuf.IExtensible
  {
    public UserCreateRequestProto() {}
    

    private string _udid = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"udid", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string udid
    {
      get { return _udid; }
      set { _udid = value; }
    }

    private string _name = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }

    private com.lvl6.proto.PlayerClass _class = com.lvl6.proto.PlayerClass.WARRIOR;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"class", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.PlayerClass.WARRIOR)]
    public com.lvl6.proto.PlayerClass @class
    {
      get { return _class; }
      set { _class = value; }
    }

    private string _referrerCode = "";
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"referrerCode", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string referrerCode
    {
      get { return _referrerCode; }
      set { _referrerCode = value; }
    }

    private string _deviceToken = "";
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"deviceToken", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string deviceToken
    {
      get { return _deviceToken; }
      set { _deviceToken = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserCreateResponseProto")]
  public partial class UserCreateResponseProto : global::ProtoBuf.IExtensible
  {
    public UserCreateResponseProto() {}
    

    private com.lvl6.proto.FullUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.FullUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.UserCreateResponseProto.UserCreateStatus _status = com.lvl6.proto.UserCreateResponseProto.UserCreateStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.UserCreateResponseProto.UserCreateStatus.SUCCESS)]
    public com.lvl6.proto.UserCreateResponseProto.UserCreateStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"UserCreateStatus")]
    public enum UserCreateStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INVALID_NAME", Value=1)]
      INVALID_NAME = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INVALID_LOCATION", Value=2)]
      INVALID_LOCATION = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"USER_WITH_UDID_ALREADY_EXISTS", Value=3)]
      USER_WITH_UDID_ALREADY_EXISTS = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TIME_ISSUE", Value=4)]
      TIME_ISSUE = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INVALID_SKILL_POINT_ALLOCATION", Value=5)]
      INVALID_SKILL_POINT_ALLOCATION = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INVALID_REFER_CODE", Value=6)]
      INVALID_REFER_CODE = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=7)]
      OTHER_FAIL = 7
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PurchaseNormStructureRequestProto")]
  public partial class PurchaseNormStructureRequestProto : global::ProtoBuf.IExtensible
  {
    public PurchaseNormStructureRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.CoordinateProto _structCoordinates = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"structCoordinates", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.CoordinateProto structCoordinates
    {
      get { return _structCoordinates; }
      set { _structCoordinates = value; }
    }

    private com.lvl6.proto.StructType _type = com.lvl6.proto.StructType.TOWN_HALL;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.StructType.TOWN_HALL)]
    public com.lvl6.proto.StructType type
    {
      get { return _type; }
      set { _type = value; }
    }

    private long _timeOfPurchase = default(long);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"timeOfPurchase", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long timeOfPurchase
    {
      get { return _timeOfPurchase; }
      set { _timeOfPurchase = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"PurchaseNormStructureResponseProto")]
  public partial class PurchaseNormStructureResponseProto : global::ProtoBuf.IExtensible
  {
    public PurchaseNormStructureResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.PurchaseNormStructureResponseProto.PurchaseNormStructureStatus _status = com.lvl6.proto.PurchaseNormStructureResponseProto.PurchaseNormStructureStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.PurchaseNormStructureResponseProto.PurchaseNormStructureStatus.SUCCESS)]
    public com.lvl6.proto.PurchaseNormStructureResponseProto.PurchaseNormStructureStatus status
    {
      get { return _status; }
      set { _status = value; }
    }

    private int _userStructId = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"userStructId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userStructId
    {
      get { return _userStructId; }
      set { _userStructId = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"PurchaseNormStructureStatus")]
    public enum PurchaseNormStructureStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_ENOUGH_MATERIALS", Value=1)]
      NOT_ENOUGH_MATERIALS = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LEVEL_TOO_LOW", Value=2)]
      LEVEL_TOO_LOW = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ANOTHER_STRUCT_STILL_BUILDING", Value=3)]
      ANOTHER_STRUCT_STILL_BUILDING = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ALREADY_HAVE_MAX_OF_THIS_STRUCT", Value=4)]
      ALREADY_HAVE_MAX_OF_THIS_STRUCT = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=5)]
      OTHER_FAIL = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLIENT_TOO_APART_FROM_SERVER_TIME", Value=6)]
      CLIENT_TOO_APART_FROM_SERVER_TIME = 6
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MoveOrRotateNormStructureRequestProto")]
  public partial class MoveOrRotateNormStructureRequestProto : global::ProtoBuf.IExtensible
  {
    public MoveOrRotateNormStructureRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private int _userStructId = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"userStructId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userStructId
    {
      get { return _userStructId; }
      set { _userStructId = value; }
    }

    private com.lvl6.proto.CoordinateProto _newStructCoordinates = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"newStructCoordinates", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.CoordinateProto newStructCoordinates
    {
      get { return _newStructCoordinates; }
      set { _newStructCoordinates = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MoveOrRotateNormStructureResponseProto")]
  public partial class MoveOrRotateNormStructureResponseProto : global::ProtoBuf.IExtensible
  {
    public MoveOrRotateNormStructureResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.MoveOrRotateNormStructureResponseProto.MoveOrRotateNormStructureStatus _status = com.lvl6.proto.MoveOrRotateNormStructureResponseProto.MoveOrRotateNormStructureStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.MoveOrRotateNormStructureResponseProto.MoveOrRotateNormStructureStatus.SUCCESS)]
    public com.lvl6.proto.MoveOrRotateNormStructureResponseProto.MoveOrRotateNormStructureStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"MoveOrRotateNormStructureStatus")]
    public enum MoveOrRotateNormStructureStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=2)]
      OTHER_FAIL = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UpgradeNormStructureRequestProto")]
  public partial class UpgradeNormStructureRequestProto : global::ProtoBuf.IExtensible
  {
    public UpgradeNormStructureRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private int _userStructId = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"userStructId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userStructId
    {
      get { return _userStructId; }
      set { _userStructId = value; }
    }

    private long _timeOfUpgrade = default(long);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"timeOfUpgrade", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long timeOfUpgrade
    {
      get { return _timeOfUpgrade; }
      set { _timeOfUpgrade = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UpgradeNormStructureResponseProto")]
  public partial class UpgradeNormStructureResponseProto : global::ProtoBuf.IExtensible
  {
    public UpgradeNormStructureResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.UpgradeNormStructureResponseProto.UpgradeNormStructureStatus _status = com.lvl6.proto.UpgradeNormStructureResponseProto.UpgradeNormStructureStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.UpgradeNormStructureResponseProto.UpgradeNormStructureStatus.SUCCESS)]
    public com.lvl6.proto.UpgradeNormStructureResponseProto.UpgradeNormStructureStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"UpgradeNormStructureStatus")]
    public enum UpgradeNormStructureStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_ENOUGH_MATERIALS", Value=1)]
      NOT_ENOUGH_MATERIALS = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_BUILT_YET", Value=2)]
      NOT_BUILT_YET = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_USERS_STRUCT", Value=3)]
      NOT_USERS_STRUCT = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ANOTHER_STRUCT_STILL_UPGRADING", Value=4)]
      ANOTHER_STRUCT_STILL_UPGRADING = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=5)]
      OTHER_FAIL = 5,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLIENT_TOO_APART_FROM_SERVER_TIME", Value=6)]
      CLIENT_TOO_APART_FROM_SERVER_TIME = 6,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AT_MAX_LEVEL_ALREADY", Value=7)]
      AT_MAX_LEVEL_ALREADY = 7
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RetrieveCurrencyFromNormStructureRequestProto")]
  public partial class RetrieveCurrencyFromNormStructureRequestProto : global::ProtoBuf.IExtensible
  {
    public RetrieveCurrencyFromNormStructureRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }
    private readonly global::System.Collections.Generic.List<com.lvl6.proto.RetrieveCurrencyFromNormStructureRequestProto.StructRetrieval> _structRetrievals = new global::System.Collections.Generic.List<com.lvl6.proto.RetrieveCurrencyFromNormStructureRequestProto.StructRetrieval>();
    [global::ProtoBuf.ProtoMember(2, Name=@"structRetrievals", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.lvl6.proto.RetrieveCurrencyFromNormStructureRequestProto.StructRetrieval> structRetrievals
    {
      get { return _structRetrievals; }
    }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StructRetrieval")]
  public partial class StructRetrieval : global::ProtoBuf.IExtensible
  {
    public StructRetrieval() {}
    

    private int _userStructId = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"userStructId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userStructId
    {
      get { return _userStructId; }
      set { _userStructId = value; }
    }

    private long _timeOfRetrieval = default(long);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"timeOfRetrieval", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long timeOfRetrieval
    {
      get { return _timeOfRetrieval; }
      set { _timeOfRetrieval = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RetrieveCurrencyFromNormStructureResponseProto")]
  public partial class RetrieveCurrencyFromNormStructureResponseProto : global::ProtoBuf.IExtensible
  {
    public RetrieveCurrencyFromNormStructureResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.RetrieveCurrencyFromNormStructureResponseProto.RetrieveCurrencyFromNormStructureStatus _status = com.lvl6.proto.RetrieveCurrencyFromNormStructureResponseProto.RetrieveCurrencyFromNormStructureStatus.OTHER_FAIL;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.RetrieveCurrencyFromNormStructureResponseProto.RetrieveCurrencyFromNormStructureStatus.OTHER_FAIL)]
    public com.lvl6.proto.RetrieveCurrencyFromNormStructureResponseProto.RetrieveCurrencyFromNormStructureStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"RetrieveCurrencyFromNormStructureStatus")]
    public enum RetrieveCurrencyFromNormStructureStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=1)]
      OTHER_FAIL = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=2)]
      SUCCESS = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLIENT_TOO_APART_FROM_SERVER_TIME", Value=3)]
      CLIENT_TOO_APART_FROM_SERVER_TIME = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_LONG_ENOUGH", Value=4)]
      NOT_LONG_ENOUGH = 4
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FinishNormStructWaittimeWithDiamondsRequestProto")]
  public partial class FinishNormStructWaittimeWithDiamondsRequestProto : global::ProtoBuf.IExtensible
  {
    public FinishNormStructWaittimeWithDiamondsRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private int _userStructId = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"userStructId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int userStructId
    {
      get { return _userStructId; }
      set { _userStructId = value; }
    }

    private long _timeOfSpeedup = default(long);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"timeOfSpeedup", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long timeOfSpeedup
    {
      get { return _timeOfSpeedup; }
      set { _timeOfSpeedup = value; }
    }

    private com.lvl6.proto.FinishNormStructWaittimeWithDiamondsRequestProto.NormStructWaitTimeType _waitTimeType = com.lvl6.proto.FinishNormStructWaittimeWithDiamondsRequestProto.NormStructWaitTimeType.FINISH_CONSTRUCTION;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"waitTimeType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.FinishNormStructWaittimeWithDiamondsRequestProto.NormStructWaitTimeType.FINISH_CONSTRUCTION)]
    public com.lvl6.proto.FinishNormStructWaittimeWithDiamondsRequestProto.NormStructWaitTimeType waitTimeType
    {
      get { return _waitTimeType; }
      set { _waitTimeType = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"NormStructWaitTimeType")]
    public enum NormStructWaitTimeType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"FINISH_CONSTRUCTION", Value=0)]
      FINISH_CONSTRUCTION = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FINISH_INCOME_WAITTIME", Value=1)]
      FINISH_INCOME_WAITTIME = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FINISH_UPGRADE", Value=2)]
      FINISH_UPGRADE = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"FinishNormStructWaittimeWithDiamondsResponseProto")]
  public partial class FinishNormStructWaittimeWithDiamondsResponseProto : global::ProtoBuf.IExtensible
  {
    public FinishNormStructWaittimeWithDiamondsResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.FinishNormStructWaittimeWithDiamondsResponseProto.FinishNormStructWaittimeStatus _status = com.lvl6.proto.FinishNormStructWaittimeWithDiamondsResponseProto.FinishNormStructWaittimeStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.FinishNormStructWaittimeWithDiamondsResponseProto.FinishNormStructWaittimeStatus.SUCCESS)]
    public com.lvl6.proto.FinishNormStructWaittimeWithDiamondsResponseProto.FinishNormStructWaittimeStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"FinishNormStructWaittimeStatus")]
    public enum FinishNormStructWaittimeStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_ENOUGH_DIAMONDS", Value=1)]
      NOT_ENOUGH_DIAMONDS = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=2)]
      OTHER_FAIL = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLIENT_TOO_APART_FROM_SERVER_TIME", Value=3)]
      CLIENT_TOO_APART_FROM_SERVER_TIME = 3
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NormStructWaitCompleteRequestProto")]
  public partial class NormStructWaitCompleteRequestProto : global::ProtoBuf.IExtensible
  {
    public NormStructWaitCompleteRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }
    private readonly global::System.Collections.Generic.List<int> _userStructId = new global::System.Collections.Generic.List<int>();
    [global::ProtoBuf.ProtoMember(2, Name=@"userStructId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<int> userStructId
    {
      get { return _userStructId; }
    }
  

    private long _curTime = default(long);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"curTime", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long curTime
    {
      get { return _curTime; }
      set { _curTime = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"NormStructWaitCompleteResponseProto")]
  public partial class NormStructWaitCompleteResponseProto : global::ProtoBuf.IExtensible
  {
    public NormStructWaitCompleteResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.NormStructWaitCompleteResponseProto.NormStructWaitCompleteStatus _status = com.lvl6.proto.NormStructWaitCompleteResponseProto.NormStructWaitCompleteStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.NormStructWaitCompleteResponseProto.NormStructWaitCompleteStatus.SUCCESS)]
    public com.lvl6.proto.NormStructWaitCompleteResponseProto.NormStructWaitCompleteStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    private readonly global::System.Collections.Generic.List<com.lvl6.proto.FullUserStructProto> _userStruct = new global::System.Collections.Generic.List<com.lvl6.proto.FullUserStructProto>();
    [global::ProtoBuf.ProtoMember(3, Name=@"userStruct", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.lvl6.proto.FullUserStructProto> userStruct
    {
      get { return _userStruct; }
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"NormStructWaitCompleteStatus")]
    public enum NormStructWaitCompleteStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NOT_DONE_YET", Value=1)]
      NOT_DONE_YET = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=2)]
      OTHER_FAIL = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLIENT_TOO_APART_FROM_SERVER_TIME", Value=3)]
      CLIENT_TOO_APART_FROM_SERVER_TIME = 3
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadPlayerCityRequestProto")]
  public partial class LoadPlayerCityRequestProto : global::ProtoBuf.IExtensible
  {
    public LoadPlayerCityRequestProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private int _cityOwnerId = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"cityOwnerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int cityOwnerId
    {
      get { return _cityOwnerId; }
      set { _cityOwnerId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoadPlayerCityResponseProto")]
  public partial class LoadPlayerCityResponseProto : global::ProtoBuf.IExtensible
  {
    public LoadPlayerCityResponseProto() {}
    

    private com.lvl6.proto.MinimumUserProto _sender = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sender", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto sender
    {
      get { return _sender; }
      set { _sender = value; }
    }

    private com.lvl6.proto.MinimumUserProto _cityOwner = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"cityOwner", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public com.lvl6.proto.MinimumUserProto cityOwner
    {
      get { return _cityOwner; }
      set { _cityOwner = value; }
    }

    private com.lvl6.proto.LoadPlayerCityResponseProto.LoadPlayerCityStatus _status = com.lvl6.proto.LoadPlayerCityResponseProto.LoadPlayerCityStatus.SUCCESS;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(com.lvl6.proto.LoadPlayerCityResponseProto.LoadPlayerCityStatus.SUCCESS)]
    public com.lvl6.proto.LoadPlayerCityResponseProto.LoadPlayerCityStatus status
    {
      get { return _status; }
      set { _status = value; }
    }
    private readonly global::System.Collections.Generic.List<com.lvl6.proto.FullUserStructProto> _ownerNormStructs = new global::System.Collections.Generic.List<com.lvl6.proto.FullUserStructProto>();
    [global::ProtoBuf.ProtoMember(4, Name=@"ownerNormStructs", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<com.lvl6.proto.FullUserStructProto> ownerNormStructs
    {
      get { return _ownerNormStructs; }
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"LoadPlayerCityStatus")]
    public enum LoadPlayerCityStatus
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SUCCESS", Value=0)]
      SUCCESS = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NO_SUCH_PLAYER", Value=1)]
      NO_SUCH_PLAYER = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OTHER_FAIL", Value=2)]
      OTHER_FAIL = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}