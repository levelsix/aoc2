// Generated by ProtoGen, Version=2.4.1.473, Culture=neutral, PublicKeyToken=17b3b1f090c3ea48.  DO NOT EDIT!
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.lvl6.proto {
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class Info {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    internal static pbd::MessageDescriptor internal__static_com_lvl6_proto_FullUserStructureProto__Descriptor;
    internal static pb::FieldAccess.FieldAccessorTable<global::com.lvl6.proto.FullUserStructureProto, global::com.lvl6.proto.FullUserStructureProto.Builder> internal__static_com_lvl6_proto_FullUserStructureProto__FieldAccessorTable;
    #endregion
    #region Descriptor
    public static pbd::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbd::FileDescriptor descriptor;
    
    static Info() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          "CgpJbmZvLnByb3RvEg5jb20ubHZsNi5wcm90byKAAQoWRnVsbFVzZXJTdHJ1" + 
          "Y3R1cmVQcm90bxIUCgx1c2VyU3RydWN0SWQYASABKAUSDgoGdXNlcklkGAIg" + 
          "ASgFEhAKCHN0cnVjdElkGAMgASgFEi4KCnN0cnVjdFR5cGUYBCABKA4yGi5j" + 
          "b20ubHZsNi5wcm90by5TdHJ1Y3RUeXBlKmkKClN0cnVjdFR5cGUSDQoJVE9X" + 
          "Tl9IQUxMEAASEgoOR09MRF9DT0xMRUNUT1IQARIQCgxHT0xEX1NUT1JBR0UQ" + 
          "AhITCg9UT05JQ19DT0xMRUNUT1IQAxIRCg1UT05JQ19TVE9SQUdFEARCC0IJ" + 
          "SW5mb1Byb3Rv");
      pbd::FileDescriptor.InternalDescriptorAssigner assigner = delegate(pbd::FileDescriptor root) {
        descriptor = root;
        internal__static_com_lvl6_proto_FullUserStructureProto__Descriptor = Descriptor.MessageTypes[0];
        internal__static_com_lvl6_proto_FullUserStructureProto__FieldAccessorTable = 
            new pb::FieldAccess.FieldAccessorTable<global::com.lvl6.proto.FullUserStructureProto, global::com.lvl6.proto.FullUserStructureProto.Builder>(internal__static_com_lvl6_proto_FullUserStructureProto__Descriptor,
                new string[] { "UserStructId", "UserId", "StructId", "StructType", });
        return null;
      };
      pbd::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
          new pbd::FileDescriptor[] {
          }, assigner);
    }
    #endregion
    
  }
  #region Enums
  public enum StructType {
    TOWN_HALL = 0,
    GOLD_COLLECTOR = 1,
    GOLD_STORAGE = 2,
    TONIC_COLLECTOR = 3,
    TONIC_STORAGE = 4,
  }
  
  #endregion
  
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class FullUserStructureProto : pb::GeneratedMessage<FullUserStructureProto, FullUserStructureProto.Builder> {
    private FullUserStructureProto() { }
    private static readonly FullUserStructureProto defaultInstance = new FullUserStructureProto().MakeReadOnly();
    private static readonly string[] _fullUserStructureProtoFieldNames = new string[] { "structId", "structType", "userId", "userStructId" };
    private static readonly uint[] _fullUserStructureProtoFieldTags = new uint[] { 24, 32, 16, 8 };
    public static FullUserStructureProto DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override FullUserStructureProto DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override FullUserStructureProto ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::com.lvl6.proto.Info.internal__static_com_lvl6_proto_FullUserStructureProto__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<FullUserStructureProto, FullUserStructureProto.Builder> InternalFieldAccessors {
      get { return global::com.lvl6.proto.Info.internal__static_com_lvl6_proto_FullUserStructureProto__FieldAccessorTable; }
    }
    
    public const int UserStructIdFieldNumber = 1;
    private bool hasUserStructId;
    private int userStructId_;
    public bool HasUserStructId {
      get { return hasUserStructId; }
    }
    public int UserStructId {
      get { return userStructId_; }
    }
    
    public const int UserIdFieldNumber = 2;
    private bool hasUserId;
    private int userId_;
    public bool HasUserId {
      get { return hasUserId; }
    }
    public int UserId {
      get { return userId_; }
    }
    
    public const int StructIdFieldNumber = 3;
    private bool hasStructId;
    private int structId_;
    public bool HasStructId {
      get { return hasStructId; }
    }
    public int StructId {
      get { return structId_; }
    }
    
    public const int StructTypeFieldNumber = 4;
    private bool hasStructType;
    private global::com.lvl6.proto.StructType structType_ = global::com.lvl6.proto.StructType.TOWN_HALL;
    public bool HasStructType {
      get { return hasStructType; }
    }
    public global::com.lvl6.proto.StructType StructType {
      get { return structType_; }
    }
    
    public override bool IsInitialized {
      get {
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      int size = SerializedSize;
      string[] field_names = _fullUserStructureProtoFieldNames;
      if (hasUserStructId) {
        output.WriteInt32(1, field_names[3], UserStructId);
      }
      if (hasUserId) {
        output.WriteInt32(2, field_names[2], UserId);
      }
      if (hasStructId) {
        output.WriteInt32(3, field_names[0], StructId);
      }
      if (hasStructType) {
        output.WriteEnum(4, field_names[1], (int) StructType, StructType);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        
        size = 0;
        if (hasUserStructId) {
          size += pb::CodedOutputStream.ComputeInt32Size(1, UserStructId);
        }
        if (hasUserId) {
          size += pb::CodedOutputStream.ComputeInt32Size(2, UserId);
        }
        if (hasStructId) {
          size += pb::CodedOutputStream.ComputeInt32Size(3, StructId);
        }
        if (hasStructType) {
          size += pb::CodedOutputStream.ComputeEnumSize(4, (int) StructType);
        }
        size += UnknownFields.SerializedSize;
        memoizedSerializedSize = size;
        return size;
      }
    }
    
    public static FullUserStructureProto ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static FullUserStructureProto ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static FullUserStructureProto ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static FullUserStructureProto ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private FullUserStructureProto MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(FullUserStructureProto prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<FullUserStructureProto, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(FullUserStructureProto cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private FullUserStructureProto result;
      
      private FullUserStructureProto PrepareBuilder() {
        if (resultIsReadOnly) {
          FullUserStructureProto original = result;
          result = new FullUserStructureProto();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override FullUserStructureProto MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::com.lvl6.proto.FullUserStructureProto.Descriptor; }
      }
      
      public override FullUserStructureProto DefaultInstanceForType {
        get { return global::com.lvl6.proto.FullUserStructureProto.DefaultInstance; }
      }
      
      public override FullUserStructureProto BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is FullUserStructureProto) {
          return MergeFrom((FullUserStructureProto) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(FullUserStructureProto other) {
        if (other == global::com.lvl6.proto.FullUserStructureProto.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasUserStructId) {
          UserStructId = other.UserStructId;
        }
        if (other.HasUserId) {
          UserId = other.UserId;
        }
        if (other.HasStructId) {
          StructId = other.StructId;
        }
        if (other.HasStructType) {
          StructType = other.StructType;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_fullUserStructureProtoFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _fullUserStructureProtoFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 8: {
              result.hasUserStructId = input.ReadInt32(ref result.userStructId_);
              break;
            }
            case 16: {
              result.hasUserId = input.ReadInt32(ref result.userId_);
              break;
            }
            case 24: {
              result.hasStructId = input.ReadInt32(ref result.structId_);
              break;
            }
            case 32: {
              object unknown;
              if(input.ReadEnum(ref result.structType_, out unknown)) {
                result.hasStructType = true;
              } else if(unknown is int) {
                if (unknownFields == null) {
                  unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
                }
                unknownFields.MergeVarintField(4, (ulong)(int)unknown);
              }
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasUserStructId {
        get { return result.hasUserStructId; }
      }
      public int UserStructId {
        get { return result.UserStructId; }
        set { SetUserStructId(value); }
      }
      public Builder SetUserStructId(int value) {
        PrepareBuilder();
        result.hasUserStructId = true;
        result.userStructId_ = value;
        return this;
      }
      public Builder ClearUserStructId() {
        PrepareBuilder();
        result.hasUserStructId = false;
        result.userStructId_ = 0;
        return this;
      }
      
      public bool HasUserId {
        get { return result.hasUserId; }
      }
      public int UserId {
        get { return result.UserId; }
        set { SetUserId(value); }
      }
      public Builder SetUserId(int value) {
        PrepareBuilder();
        result.hasUserId = true;
        result.userId_ = value;
        return this;
      }
      public Builder ClearUserId() {
        PrepareBuilder();
        result.hasUserId = false;
        result.userId_ = 0;
        return this;
      }
      
      public bool HasStructId {
        get { return result.hasStructId; }
      }
      public int StructId {
        get { return result.StructId; }
        set { SetStructId(value); }
      }
      public Builder SetStructId(int value) {
        PrepareBuilder();
        result.hasStructId = true;
        result.structId_ = value;
        return this;
      }
      public Builder ClearStructId() {
        PrepareBuilder();
        result.hasStructId = false;
        result.structId_ = 0;
        return this;
      }
      
      public bool HasStructType {
       get { return result.hasStructType; }
      }
      public global::com.lvl6.proto.StructType StructType {
        get { return result.StructType; }
        set { SetStructType(value); }
      }
      public Builder SetStructType(global::com.lvl6.proto.StructType value) {
        PrepareBuilder();
        result.hasStructType = true;
        result.structType_ = value;
        return this;
      }
      public Builder ClearStructType() {
        PrepareBuilder();
        result.hasStructType = false;
        result.structType_ = global::com.lvl6.proto.StructType.TOWN_HALL;
        return this;
      }
    }
    static FullUserStructureProto() {
      object.ReferenceEquals(global::com.lvl6.proto.Info.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code
