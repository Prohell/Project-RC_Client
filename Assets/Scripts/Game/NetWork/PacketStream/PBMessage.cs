//This code create by CodeEngine mrd.cyou.com ,don't modify

using System;
 using scg = global::System.Collections.Generic;
 using pb = global::Google.ProtocolBuffers;
 using pbc = global::Google.ProtocolBuffers.Collections;
 #pragma warning disable



[Serializable]
public class CG_LOGIN : PacketDistributed
{
public enum VALIDATETYPE 
 { 
  TEST = 0,                 //测试登录模式 
  CYOU = 1,                 //聚合SDK登陆模式 
 }
public const int vtypeFieldNumber = 1;
 private bool hasVtype;
 private Int32 vtype_ = 0;
 public bool HasVtype {
 get { return hasVtype; }
 }
 public Int32 Vtype {
 get { return vtype_; }
 set { SetVtype(value); }
 }
 public void SetVtype(Int32 value) { 
 hasVtype = true;
 vtype_ = value;
 }

public const int gameversionFieldNumber = 2;
 private bool hasGameversion;
 private Int32 gameversion_ = 0;
 public bool HasGameversion {
 get { return hasGameversion; }
 }
 public Int32 Gameversion {
 get { return gameversion_; }
 set { SetGameversion(value); }
 }
 public void SetGameversion(Int32 value) { 
 hasGameversion = true;
 gameversion_ = value;
 }

public const int programversionFieldNumber = 3;
 private bool hasProgramversion;
 private Int32 programversion_ = 0;
 public bool HasProgramversion {
 get { return hasProgramversion; }
 }
 public Int32 Programversion {
 get { return programversion_; }
 set { SetProgramversion(value); }
 }
 public void SetProgramversion(Int32 value) { 
 hasProgramversion = true;
 programversion_ = value;
 }

public const int publicresourceversionFieldNumber = 4;
 private bool hasPublicresourceversion;
 private Int32 publicresourceversion_ = 0;
 public bool HasPublicresourceversion {
 get { return hasPublicresourceversion; }
 }
 public Int32 Publicresourceversion {
 get { return publicresourceversion_; }
 set { SetPublicresourceversion(value); }
 }
 public void SetPublicresourceversion(Int32 value) { 
 hasPublicresourceversion = true;
 publicresourceversion_ = value;
 }

public const int maxpacketidFieldNumber = 5;
 private bool hasMaxpacketid;
 private Int32 maxpacketid_ = 0;
 public bool HasMaxpacketid {
 get { return hasMaxpacketid; }
 }
 public Int32 Maxpacketid {
 get { return maxpacketid_; }
 set { SetMaxpacketid(value); }
 }
 public void SetMaxpacketid(Int32 value) { 
 hasMaxpacketid = true;
 maxpacketid_ = value;
 }

public const int forceenterFieldNumber = 6;
 private bool hasForceenter;
 private Int32 forceenter_ = 0;
 public bool HasForceenter {
 get { return hasForceenter; }
 }
 public Int32 Forceenter {
 get { return forceenter_; }
 set { SetForceenter(value); }
 }
 public void SetForceenter(Int32 value) { 
 hasForceenter = true;
 forceenter_ = value;
 }

public const int deviceidFieldNumber = 7;
 private bool hasDeviceid;
 private string deviceid_ = "";
 public bool HasDeviceid {
 get { return hasDeviceid; }
 }
 public string Deviceid {
 get { return deviceid_; }
 set { SetDeviceid(value); }
 }
 public void SetDeviceid(string value) { 
 hasDeviceid = true;
 deviceid_ = value;
 }

public const int devicetypeFieldNumber = 8;
 private bool hasDevicetype;
 private string devicetype_ = "";
 public bool HasDevicetype {
 get { return hasDevicetype; }
 }
 public string Devicetype {
 get { return devicetype_; }
 set { SetDevicetype(value); }
 }
 public void SetDevicetype(string value) { 
 hasDevicetype = true;
 devicetype_ = value;
 }

public const int deviceversionFieldNumber = 9;
 private bool hasDeviceversion;
 private string deviceversion_ = "";
 public bool HasDeviceversion {
 get { return hasDeviceversion; }
 }
 public string Deviceversion {
 get { return deviceversion_; }
 set { SetDeviceversion(value); }
 }
 public void SetDeviceversion(string value) { 
 hasDeviceversion = true;
 deviceversion_ = value;
 }

public const int accountFieldNumber = 10;
 private bool hasAccount;
 private string account_ = "";
 public bool HasAccount {
 get { return hasAccount; }
 }
 public string Account {
 get { return account_; }
 set { SetAccount(value); }
 }
 public void SetAccount(string value) { 
 hasAccount = true;
 account_ = value;
 }

public const int validateinfoFieldNumber = 11;
 private bool hasValidateinfo;
 private string validateinfo_ = "";
 public bool HasValidateinfo {
 get { return hasValidateinfo; }
 }
 public string Validateinfo {
 get { return validateinfo_; }
 set { SetValidateinfo(value); }
 }
 public void SetValidateinfo(string value) { 
 hasValidateinfo = true;
 validateinfo_ = value;
 }

public const int channelidFieldNumber = 12;
 private bool hasChannelid;
 private string channelid_ = "";
 public bool HasChannelid {
 get { return hasChannelid; }
 }
 public string Channelid {
 get { return channelid_; }
 set { SetChannelid(value); }
 }
 public void SetChannelid(string value) { 
 hasChannelid = true;
 channelid_ = value;
 }

public const int mediachannelFieldNumber = 13;
 private bool hasMediachannel;
 private string mediachannel_ = "";
 public bool HasMediachannel {
 get { return hasMediachannel; }
 }
 public string Mediachannel {
 get { return mediachannel_; }
 set { SetMediachannel(value); }
 }
 public void SetMediachannel(string value) { 
 hasMediachannel = true;
 mediachannel_ = value;
 }

public const int rapidvalidatecodeFieldNumber = 14;
 private bool hasRapidvalidatecode;
 private Int32 rapidvalidatecode_ = 0;
 public bool HasRapidvalidatecode {
 get { return hasRapidvalidatecode; }
 }
 public Int32 Rapidvalidatecode {
 get { return rapidvalidatecode_; }
 set { SetRapidvalidatecode(value); }
 }
 public void SetRapidvalidatecode(Int32 value) { 
 hasRapidvalidatecode = true;
 rapidvalidatecode_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasVtype) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Vtype);
}
 if (HasGameversion) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Gameversion);
}
 if (HasProgramversion) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Programversion);
}
 if (HasPublicresourceversion) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Publicresourceversion);
}
 if (HasMaxpacketid) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Maxpacketid);
}
 if (HasForceenter) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Forceenter);
}
 if (HasDeviceid) {
size += pb::CodedOutputStream.ComputeStringSize(7, Deviceid);
}
 if (HasDevicetype) {
size += pb::CodedOutputStream.ComputeStringSize(8, Devicetype);
}
 if (HasDeviceversion) {
size += pb::CodedOutputStream.ComputeStringSize(9, Deviceversion);
}
 if (HasAccount) {
size += pb::CodedOutputStream.ComputeStringSize(10, Account);
}
 if (HasValidateinfo) {
size += pb::CodedOutputStream.ComputeStringSize(11, Validateinfo);
}
 if (HasChannelid) {
size += pb::CodedOutputStream.ComputeStringSize(12, Channelid);
}
 if (HasMediachannel) {
size += pb::CodedOutputStream.ComputeStringSize(13, Mediachannel);
}
 if (HasRapidvalidatecode) {
size += pb::CodedOutputStream.ComputeInt32Size(14, Rapidvalidatecode);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasVtype) {
output.WriteInt32(1, Vtype);
}
 
if (HasGameversion) {
output.WriteInt32(2, Gameversion);
}
 
if (HasProgramversion) {
output.WriteInt32(3, Programversion);
}
 
if (HasPublicresourceversion) {
output.WriteInt32(4, Publicresourceversion);
}
 
if (HasMaxpacketid) {
output.WriteInt32(5, Maxpacketid);
}
 
if (HasForceenter) {
output.WriteInt32(6, Forceenter);
}
 
if (HasDeviceid) {
output.WriteString(7, Deviceid);
}
 
if (HasDevicetype) {
output.WriteString(8, Devicetype);
}
 
if (HasDeviceversion) {
output.WriteString(9, Deviceversion);
}
 
if (HasAccount) {
output.WriteString(10, Account);
}
 
if (HasValidateinfo) {
output.WriteString(11, Validateinfo);
}
 
if (HasChannelid) {
output.WriteString(12, Channelid);
}
 
if (HasMediachannel) {
output.WriteString(13, Mediachannel);
}
 
if (HasRapidvalidatecode) {
output.WriteInt32(14, Rapidvalidatecode);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_LOGIN _inst = (CG_LOGIN) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Vtype = input.ReadInt32();
break;
}
   case  16: {
 _inst.Gameversion = input.ReadInt32();
break;
}
   case  24: {
 _inst.Programversion = input.ReadInt32();
break;
}
   case  32: {
 _inst.Publicresourceversion = input.ReadInt32();
break;
}
   case  40: {
 _inst.Maxpacketid = input.ReadInt32();
break;
}
   case  48: {
 _inst.Forceenter = input.ReadInt32();
break;
}
   case  58: {
 _inst.Deviceid = input.ReadString();
break;
}
   case  66: {
 _inst.Devicetype = input.ReadString();
break;
}
   case  74: {
 _inst.Deviceversion = input.ReadString();
break;
}
   case  82: {
 _inst.Account = input.ReadString();
break;
}
   case  90: {
 _inst.Validateinfo = input.ReadString();
break;
}
   case  98: {
 _inst.Channelid = input.ReadString();
break;
}
   case  106: {
 _inst.Mediachannel = input.ReadString();
break;
}
   case  112: {
 _inst.Rapidvalidatecode = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasAccount) return false;
 return true;
 }

}


[Serializable]
public class GC_LOGIN_RET : PacketDistributed
{
public enum LOGINRESULT 
 { 
  SUCCESS      = 1, 
  ACCOUNTVERIFYFAIL = 2, 
  READROLELISTFAIL = 3, 
  ALREADYLOGIN  = 4, 
  QUEUEFULL  = 5, 
  NEEDFORCEENTER = 6, 
  PACKETNOTMATCH = 7, 
  VERSIONNOTMATCH = 8, 
 }public enum VALIDATERESULT 
 { 
  OK           = 0, 
  FAIL_VALIDATETYPEERROR = 1, 
  FAIL_PERFORM      = 2, 
  FAIL_OPCODE       = 3, 
  FAIL_TAG        = 4, 
  FAIL_STATE       = 5, 
  FAIL_DATA_STATUS    = 6, 
  FAIL_CHANNELID     = 7, 
  FAIL_BLOCKED     = 8, 
 }
public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int validateresultFieldNumber = 2;
 private bool hasValidateresult;
 private Int32 validateresult_ = 0;
 public bool HasValidateresult {
 get { return hasValidateresult; }
 }
 public Int32 Validateresult {
 get { return validateresult_; }
 set { SetValidateresult(value); }
 }
 public void SetValidateresult(Int32 value) { 
 hasValidateresult = true;
 validateresult_ = value;
 }

public const int useridFieldNumber = 3;
 private bool hasUserid;
 private Int64 userid_ = 0;
 public bool HasUserid {
 get { return hasUserid; }
 }
 public Int64 Userid {
 get { return userid_; }
 set { SetUserid(value); }
 }
 public void SetUserid(Int64 value) { 
 hasUserid = true;
 userid_ = value;
 }

public const int oidFieldNumber = 4;
 private bool hasOid;
 private string oid_ = "";
 public bool HasOid {
 get { return hasOid; }
 }
 public string Oid {
 get { return oid_; }
 set { SetOid(value); }
 }
 public void SetOid(string value) { 
 hasOid = true;
 oid_ = value;
 }

public const int accesstokenFieldNumber = 5;
 private bool hasAccesstoken;
 private string accesstoken_ = "";
 public bool HasAccesstoken {
 get { return hasAccesstoken; }
 }
 public string Accesstoken {
 get { return accesstoken_; }
 set { SetAccesstoken(value); }
 }
 public void SetAccesstoken(string value) { 
 hasAccesstoken = true;
 accesstoken_ = value;
 }

public const int cityFieldNumber = 6;
 private bool hasCity;
 private GC_CityData city_ =  new GC_CityData();
 public bool HasCity {
 get { return hasCity; }
 }
 public GC_CityData City {
 get { return city_; }
 set { SetCity(value); }
 }
 public void SetCity(GC_CityData value) { 
 hasCity = true;
 city_ = value;
 }

public const int playernameFieldNumber = 7;
 private bool hasPlayername;
 private string playername_ = "";
 public bool HasPlayername {
 get { return hasPlayername; }
 }
 public string Playername {
 get { return playername_; }
 set { SetPlayername(value); }
 }
 public void SetPlayername(string value) { 
 hasPlayername = true;
 playername_ = value;
 }

public const int levelFieldNumber = 8;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int heroListFieldNumber = 9;
 private bool hasHeroList;
 private GC_HeroList heroList_ =  new GC_HeroList();
 public bool HasHeroList {
 get { return hasHeroList; }
 }
 public GC_HeroList HeroList {
 get { return heroList_; }
 set { SetHeroList(value); }
 }
 public void SetHeroList(GC_HeroList value) { 
 hasHeroList = true;
 heroList_ = value;
 }

public const int marchlistFieldNumber = 10;
 private bool hasMarchlist;
 private GC_MarchList marchlist_ =  new GC_MarchList();
 public bool HasMarchlist {
 get { return hasMarchlist; }
 }
 public GC_MarchList Marchlist {
 get { return marchlist_; }
 set { SetMarchlist(value); }
 }
 public void SetMarchlist(GC_MarchList value) { 
 hasMarchlist = true;
 marchlist_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasValidateresult) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Validateresult);
}
 if (HasUserid) {
size += pb::CodedOutputStream.ComputeInt64Size(3, Userid);
}
 if (HasOid) {
size += pb::CodedOutputStream.ComputeStringSize(4, Oid);
}
 if (HasAccesstoken) {
size += pb::CodedOutputStream.ComputeStringSize(5, Accesstoken);
}
{
int subsize = City.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)6) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 if (HasPlayername) {
size += pb::CodedOutputStream.ComputeStringSize(7, Playername);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(8, Level);
}
{
int subsize = HeroList.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)9) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
int subsize = Marchlist.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)10) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasValidateresult) {
output.WriteInt32(2, Validateresult);
}
 
if (HasUserid) {
output.WriteInt64(3, Userid);
}
 
if (HasOid) {
output.WriteString(4, Oid);
}
 
if (HasAccesstoken) {
output.WriteString(5, Accesstoken);
}
{
output.WriteTag((int)6, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)City.SerializedSize());
City.WriteTo(output);

}
 
if (HasPlayername) {
output.WriteString(7, Playername);
}
 
if (HasLevel) {
output.WriteInt32(8, Level);
}
{
output.WriteTag((int)9, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)HeroList.SerializedSize());
HeroList.WriteTo(output);

}
{
output.WriteTag((int)10, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Marchlist.SerializedSize());
Marchlist.WriteTo(output);

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_LOGIN_RET _inst = (GC_LOGIN_RET) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.Validateresult = input.ReadInt32();
break;
}
   case  24: {
 _inst.Userid = input.ReadInt64();
break;
}
   case  34: {
 _inst.Oid = input.ReadString();
break;
}
   case  42: {
 _inst.Accesstoken = input.ReadString();
break;
}
    case  50: {
GC_CityData subBuilder =  new GC_CityData();
 input.ReadMessage(subBuilder);
 _inst.City = subBuilder;
break;
}
   case  58: {
 _inst.Playername = input.ReadString();
break;
}
   case  64: {
 _inst.Level = input.ReadInt32();
break;
}
    case  74: {
GC_HeroList subBuilder =  new GC_HeroList();
 input.ReadMessage(subBuilder);
 _inst.HeroList = subBuilder;
break;
}
    case  82: {
GC_MarchList subBuilder =  new GC_MarchList();
 input.ReadMessage(subBuilder);
 _inst.Marchlist = subBuilder;
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasValidateresult) return false;
 if (!hasUserid) return false;
 if (!hasOid) return false;
 if (!hasAccesstoken) return false;
  if (HasCity) {
if (!City.IsInitialized()) return false;
}
 if (!hasPlayername) return false;
 if (!hasLevel) return false;
  if (HasHeroList) {
if (!HeroList.IsInitialized()) return false;
}
  if (HasMarchlist) {
if (!Marchlist.IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class CG_CONNECTED_HEARTBEAT : PacketDistributed
{

public const int isresponseFieldNumber = 1;
 private bool hasIsresponse;
 private Int32 isresponse_ = 0;
 public bool HasIsresponse {
 get { return hasIsresponse; }
 }
 public Int32 Isresponse {
 get { return isresponse_; }
 set { SetIsresponse(value); }
 }
 public void SetIsresponse(Int32 value) { 
 hasIsresponse = true;
 isresponse_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasIsresponse) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Isresponse);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasIsresponse) {
output.WriteInt32(1, Isresponse);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_CONNECTED_HEARTBEAT _inst = (CG_CONNECTED_HEARTBEAT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Isresponse = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasIsresponse) return false;
 return true;
 }

}


[Serializable]
public class GC_CONNECTED_HEARTBEAT : PacketDistributed
{

public const int serveransitimeFieldNumber = 1;
 private bool hasServeransitime;
 private Int32 serveransitime_ = 0;
 public bool HasServeransitime {
 get { return hasServeransitime; }
 }
 public Int32 Serveransitime {
 get { return serveransitime_; }
 set { SetServeransitime(value); }
 }
 public void SetServeransitime(Int32 value) { 
 hasServeransitime = true;
 serveransitime_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasServeransitime) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Serveransitime);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasServeransitime) {
output.WriteInt32(1, Serveransitime);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_CONNECTED_HEARTBEAT _inst = (GC_CONNECTED_HEARTBEAT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Serveransitime = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasServeransitime) return false;
 return true;
 }

}


[Serializable]
public class CG_REQ_NEAR_LIST : PacketDistributed
{

public const int marchIdFieldNumber = 1;
 private bool hasMarchId;
 private Int32 marchId_ = 0;
 public bool HasMarchId {
 get { return hasMarchId; }
 }
 public Int32 MarchId {
 get { return marchId_; }
 set { SetMarchId(value); }
 }
 public void SetMarchId(Int32 value) { 
 hasMarchId = true;
 marchId_ = value;
 }

public const int sceneIdFieldNumber = 2;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, MarchId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchId) {
output.WriteInt32(1, MarchId);
}
 
if (HasSceneId) {
output.WriteInt32(2, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_REQ_NEAR_LIST _inst = (CG_REQ_NEAR_LIST) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.MarchId = input.ReadInt32();
break;
}
   case  16: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_NEAR_MARCHLIST : PacketDistributed
{

public const int GuidFieldNumber = 1;
 private pbc::PopsicleList<UInt64> Guid_ = new pbc::PopsicleList<UInt64>();
 public scg::IList<UInt64> GuidList {
 get { return pbc::Lists.AsReadOnly(Guid_); }
 }
 
 public int GuidCount {
 get { return Guid_.Count; }
 }
 
public UInt64 GetGuid(int index) {
 return Guid_[index];
 }
 public void AddGuid(UInt64 value) {
 Guid_.Add(value);
 }

public const int NameFieldNumber = 2;
 private pbc::PopsicleList<string> Name_ = new pbc::PopsicleList<string>();
 public scg::IList<string> NameList {
 get { return pbc::Lists.AsReadOnly(Name_); }
 }
 
 public int NameCount {
 get { return Name_.Count; }
 }
 
public string GetName(int index) {
 return Name_[index];
 }
 public void AddName(string value) {
 Name_.Add(value);
 }

public const int LevelFieldNumber = 3;
 private pbc::PopsicleList<Int32> Level_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> LevelList {
 get { return pbc::Lists.AsReadOnly(Level_); }
 }
 
 public int LevelCount {
 get { return Level_.Count; }
 }
 
public Int32 GetLevel(int index) {
 return Level_[index];
 }
 public void AddLevel(Int32 value) {
 Level_.Add(value);
 }

public const int CombatNumFieldNumber = 4;
 private pbc::PopsicleList<Int32> CombatNum_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> CombatNumList {
 get { return pbc::Lists.AsReadOnly(CombatNum_); }
 }
 
 public int CombatNumCount {
 get { return CombatNum_.Count; }
 }
 
public Int32 GetCombatNum(int index) {
 return CombatNum_[index];
 }
 public void AddCombatNum(Int32 value) {
 CombatNum_.Add(value);
 }

public const int marchIdFieldNumber = 5;
 private bool hasMarchId;
 private Int32 marchId_ = 0;
 public bool HasMarchId {
 get { return hasMarchId; }
 }
 public Int32 MarchId {
 get { return marchId_; }
 set { SetMarchId(value); }
 }
 public void SetMarchId(Int32 value) { 
 hasMarchId = true;
 marchId_ = value;
 }

public const int sceneIdFieldNumber = 6;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
int dataSize = 0;
for(int i=0; i<GuidList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeUInt64SizeNoTag(GuidList[i]);
}
size += dataSize;
size += 1 * Guid_.Count;
}
{
int dataSize = 0;
for(int i=0; i<NameList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeStringSizeNoTag(NameList[i]);
}
size += dataSize;
size += 1 * Name_.Count;
}
{
int dataSize = 0;
for(int i=0; i<LevelList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(LevelList[i]);
}
size += dataSize;
size += 1 * Level_.Count;
}
{
int dataSize = 0;
for(int i=0; i<CombatNumList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(CombatNumList[i]);
}
size += dataSize;
size += 1 * CombatNum_.Count;
}
 if (HasMarchId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, MarchId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 {
if (Guid_.Count > 0) {
for(int i=0; i<Guid_.Count; ++i){
output.WriteUInt64(1,Guid_[i]);
}
}

}
{
if (Name_.Count > 0) {
for(int i=0; i<Name_.Count; ++i){
output.WriteString(2,Name_[i]);
}
}

}
{
if (Level_.Count > 0) {
for(int i=0; i<Level_.Count; ++i){
output.WriteInt32(3,Level_[i]);
}
}

}
{
if (CombatNum_.Count > 0) {
for(int i=0; i<CombatNum_.Count; ++i){
output.WriteInt32(4,CombatNum_[i]);
}
}

}
 
if (HasMarchId) {
output.WriteInt32(5, MarchId);
}
 
if (HasSceneId) {
output.WriteInt32(6, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_NEAR_MARCHLIST _inst = (GC_NEAR_MARCHLIST) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.AddGuid(input.ReadUInt64());
break;
}
   case  18: {
 _inst.AddName(input.ReadString());
break;
}
   case  24: {
 _inst.AddLevel(input.ReadInt32());
break;
}
   case  32: {
 _inst.AddCombatNum(input.ReadInt32());
break;
}
   case  40: {
 _inst.MarchId = input.ReadInt32();
break;
}
   case  48: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class CG_CHAT : PacketDistributed
{
public enum CHATTYPE 
 { 
  CHAT_TYPE_INVALID    = 0, 
  CHAT_TYPE_NORMAL     = 1,  // 附近频道 
  CHAT_TYPE_WORLD     = 2,  // 世界频道 
  CHAT_TYPE_SYSTEM    = 3,  // 系统频道 
  CHAT_TYPE_LOUDSPEAKER  = 4,  // 小喇叭 
  CHAT_TYPE_FRIEND    = 5,  // 好友频道 
 }
public const int chattypeFieldNumber = 1;
 private bool hasChattype;
 private Int32 chattype_ = 0;
 public bool HasChattype {
 get { return hasChattype; }
 }
 public Int32 Chattype {
 get { return chattype_; }
 set { SetChattype(value); }
 }
 public void SetChattype(Int32 value) { 
 hasChattype = true;
 chattype_ = value;
 }

public const int chatinfoFieldNumber = 2;
 private bool hasChatinfo;
 private string chatinfo_ = "";
 public bool HasChatinfo {
 get { return hasChatinfo; }
 }
 public string Chatinfo {
 get { return chatinfo_; }
 set { SetChatinfo(value); }
 }
 public void SetChatinfo(string value) { 
 hasChatinfo = true;
 chatinfo_ = value;
 }

public const int receiverguidFieldNumber = 3;
 private bool hasReceiverguid;
 private UInt64 receiverguid_ = 0;
 public bool HasReceiverguid {
 get { return hasReceiverguid; }
 }
 public UInt64 Receiverguid {
 get { return receiverguid_; }
 set { SetReceiverguid(value); }
 }
 public void SetReceiverguid(UInt64 value) { 
 hasReceiverguid = true;
 receiverguid_ = value;
 }

public const int receivernameFieldNumber = 4;
 private bool hasReceivername;
 private string receivername_ = "";
 public bool HasReceivername {
 get { return hasReceivername; }
 }
 public string Receivername {
 get { return receivername_; }
 set { SetReceivername(value); }
 }
 public void SetReceivername(string value) { 
 hasReceivername = true;
 receivername_ = value;
 }

public const int receiverlevelFieldNumber = 5;
 private bool hasReceiverlevel;
 private Int32 receiverlevel_ = 0;
 public bool HasReceiverlevel {
 get { return hasReceiverlevel; }
 }
 public Int32 Receiverlevel {
 get { return receiverlevel_; }
 set { SetReceiverlevel(value); }
 }
 public void SetReceiverlevel(Int32 value) { 
 hasReceiverlevel = true;
 receiverlevel_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasChattype) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Chattype);
}
 if (HasChatinfo) {
size += pb::CodedOutputStream.ComputeStringSize(2, Chatinfo);
}
 if (HasReceiverguid) {
size += pb::CodedOutputStream.ComputeUInt64Size(3, Receiverguid);
}
 if (HasReceivername) {
size += pb::CodedOutputStream.ComputeStringSize(4, Receivername);
}
 if (HasReceiverlevel) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Receiverlevel);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasChattype) {
output.WriteInt32(1, Chattype);
}
 
if (HasChatinfo) {
output.WriteString(2, Chatinfo);
}
 
if (HasReceiverguid) {
output.WriteUInt64(3, Receiverguid);
}
 
if (HasReceivername) {
output.WriteString(4, Receivername);
}
 
if (HasReceiverlevel) {
output.WriteInt32(5, Receiverlevel);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_CHAT _inst = (CG_CHAT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Chattype = input.ReadInt32();
break;
}
   case  18: {
 _inst.Chatinfo = input.ReadString();
break;
}
   case  24: {
 _inst.Receiverguid = input.ReadUInt64();
break;
}
   case  34: {
 _inst.Receivername = input.ReadString();
break;
}
   case  40: {
 _inst.Receiverlevel = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasChattype) return false;
 if (!hasChatinfo) return false;
 return true;
 }

}


[Serializable]
public class GC_CHAT : PacketDistributed
{
public enum CHATTYPE 
 { 
  CHAT_TYPE_INVALID    = 0, 
  CHAT_TYPE_NORMAL     = 1,  // 附近频道 
  CHAT_TYPE_WORLD     = 2,  // 世界频道 
  CHAT_TYPE_SYSTEM    = 3,  // 系统频道 
  CHAT_TYPE_LOUDSPEAKER  = 4,  // 小喇叭 
  CHAT_TYPE_FRIEND    = 5,  // 好友频道 
 }
public const int chattypeFieldNumber = 1;
 private bool hasChattype;
 private Int32 chattype_ = 0;
 public bool HasChattype {
 get { return hasChattype; }
 }
 public Int32 Chattype {
 get { return chattype_; }
 set { SetChattype(value); }
 }
 public void SetChattype(Int32 value) { 
 hasChattype = true;
 chattype_ = value;
 }

public const int chatinfoFieldNumber = 2;
 private bool hasChatinfo;
 private string chatinfo_ = "";
 public bool HasChatinfo {
 get { return hasChatinfo; }
 }
 public string Chatinfo {
 get { return chatinfo_; }
 set { SetChatinfo(value); }
 }
 public void SetChatinfo(string value) { 
 hasChatinfo = true;
 chatinfo_ = value;
 }

public const int senderguidFieldNumber = 3;
 private bool hasSenderguid;
 private UInt64 senderguid_ = 0;
 public bool HasSenderguid {
 get { return hasSenderguid; }
 }
 public UInt64 Senderguid {
 get { return senderguid_; }
 set { SetSenderguid(value); }
 }
 public void SetSenderguid(UInt64 value) { 
 hasSenderguid = true;
 senderguid_ = value;
 }

public const int sendernameFieldNumber = 4;
 private bool hasSendername;
 private string sendername_ = "";
 public bool HasSendername {
 get { return hasSendername; }
 }
 public string Sendername {
 get { return sendername_; }
 set { SetSendername(value); }
 }
 public void SetSendername(string value) { 
 hasSendername = true;
 sendername_ = value;
 }

public const int senderlevelFieldNumber = 5;
 private bool hasSenderlevel;
 private Int32 senderlevel_ = 0;
 public bool HasSenderlevel {
 get { return hasSenderlevel; }
 }
 public Int32 Senderlevel {
 get { return senderlevel_; }
 set { SetSenderlevel(value); }
 }
 public void SetSenderlevel(Int32 value) { 
 hasSenderlevel = true;
 senderlevel_ = value;
 }

public const int receiverguidFieldNumber = 6;
 private bool hasReceiverguid;
 private UInt64 receiverguid_ = 0;
 public bool HasReceiverguid {
 get { return hasReceiverguid; }
 }
 public UInt64 Receiverguid {
 get { return receiverguid_; }
 set { SetReceiverguid(value); }
 }
 public void SetReceiverguid(UInt64 value) { 
 hasReceiverguid = true;
 receiverguid_ = value;
 }

public const int receivernameFieldNumber = 7;
 private bool hasReceivername;
 private string receivername_ = "";
 public bool HasReceivername {
 get { return hasReceivername; }
 }
 public string Receivername {
 get { return receivername_; }
 set { SetReceivername(value); }
 }
 public void SetReceivername(string value) { 
 hasReceivername = true;
 receivername_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasChattype) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Chattype);
}
 if (HasChatinfo) {
size += pb::CodedOutputStream.ComputeStringSize(2, Chatinfo);
}
 if (HasSenderguid) {
size += pb::CodedOutputStream.ComputeUInt64Size(3, Senderguid);
}
 if (HasSendername) {
size += pb::CodedOutputStream.ComputeStringSize(4, Sendername);
}
 if (HasSenderlevel) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Senderlevel);
}
 if (HasReceiverguid) {
size += pb::CodedOutputStream.ComputeUInt64Size(6, Receiverguid);
}
 if (HasReceivername) {
size += pb::CodedOutputStream.ComputeStringSize(7, Receivername);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasChattype) {
output.WriteInt32(1, Chattype);
}
 
if (HasChatinfo) {
output.WriteString(2, Chatinfo);
}
 
if (HasSenderguid) {
output.WriteUInt64(3, Senderguid);
}
 
if (HasSendername) {
output.WriteString(4, Sendername);
}
 
if (HasSenderlevel) {
output.WriteInt32(5, Senderlevel);
}
 
if (HasReceiverguid) {
output.WriteUInt64(6, Receiverguid);
}
 
if (HasReceivername) {
output.WriteString(7, Receivername);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_CHAT _inst = (GC_CHAT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Chattype = input.ReadInt32();
break;
}
   case  18: {
 _inst.Chatinfo = input.ReadString();
break;
}
   case  24: {
 _inst.Senderguid = input.ReadUInt64();
break;
}
   case  34: {
 _inst.Sendername = input.ReadString();
break;
}
   case  40: {
 _inst.Senderlevel = input.ReadInt32();
break;
}
   case  48: {
 _inst.Receiverguid = input.ReadUInt64();
break;
}
   case  58: {
 _inst.Receivername = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasChattype) return false;
 if (!hasChatinfo) return false;
 return true;
 }

}


[Serializable]
public class GC_NOTICE : PacketDistributed
{

public const int noticeFieldNumber = 1;
 private bool hasNotice;
 private string notice_ = "";
 public bool HasNotice {
 get { return hasNotice; }
 }
 public string Notice {
 get { return notice_; }
 set { SetNotice(value); }
 }
 public void SetNotice(string value) { 
 hasNotice = true;
 notice_ = value;
 }

public const int filterRepeatFieldNumber = 2;
 private bool hasFilterRepeat;
 private Int32 filterRepeat_ = 0;
 public bool HasFilterRepeat {
 get { return hasFilterRepeat; }
 }
 public Int32 FilterRepeat {
 get { return filterRepeat_; }
 set { SetFilterRepeat(value); }
 }
 public void SetFilterRepeat(Int32 value) { 
 hasFilterRepeat = true;
 filterRepeat_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasNotice) {
size += pb::CodedOutputStream.ComputeStringSize(1, Notice);
}
 if (HasFilterRepeat) {
size += pb::CodedOutputStream.ComputeInt32Size(2, FilterRepeat);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasNotice) {
output.WriteString(1, Notice);
}
 
if (HasFilterRepeat) {
output.WriteInt32(2, FilterRepeat);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_NOTICE _inst = (GC_NOTICE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  10: {
 _inst.Notice = input.ReadString();
break;
}
   case  16: {
 _inst.FilterRepeat = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasNotice) return false;
 return true;
 }

}


[Serializable]
public class GC_UPDATE_SCENE_INSTACTIVATION : PacketDistributed
{

public const int sceneclassidFieldNumber = 1;
 private bool hasSceneclassid;
 private Int32 sceneclassid_ = 0;
 public bool HasSceneclassid {
 get { return hasSceneclassid; }
 }
 public Int32 Sceneclassid {
 get { return sceneclassid_; }
 set { SetSceneclassid(value); }
 }
 public void SetSceneclassid(Int32 value) { 
 hasSceneclassid = true;
 sceneclassid_ = value;
 }

public const int sceneinstcountFieldNumber = 2;
 private bool hasSceneinstcount;
 private Int32 sceneinstcount_ = 0;
 public bool HasSceneinstcount {
 get { return hasSceneinstcount; }
 }
 public Int32 Sceneinstcount {
 get { return sceneinstcount_; }
 set { SetSceneinstcount(value); }
 }
 public void SetSceneinstcount(Int32 value) { 
 hasSceneinstcount = true;
 sceneinstcount_ = value;
 }

public const int sceneactivationFieldNumber = 3;
 private pbc::PopsicleList<Int32> sceneactivation_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> sceneactivationList {
 get { return pbc::Lists.AsReadOnly(sceneactivation_); }
 }
 
 public int sceneactivationCount {
 get { return sceneactivation_.Count; }
 }
 
public Int32 GetSceneactivation(int index) {
 return sceneactivation_[index];
 }
 public void AddSceneactivation(Int32 value) {
 sceneactivation_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneclassid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Sceneclassid);
}
 if (HasSceneinstcount) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Sceneinstcount);
}
{
int dataSize = 0;
for(int i=0; i<sceneactivationList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(sceneactivationList[i]);
}
size += dataSize;
size += 1 * sceneactivation_.Count;
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneclassid) {
output.WriteInt32(1, Sceneclassid);
}
 
if (HasSceneinstcount) {
output.WriteInt32(2, Sceneinstcount);
}
{
if (sceneactivation_.Count > 0) {
for(int i=0; i<sceneactivation_.Count; ++i){
output.WriteInt32(3,sceneactivation_[i]);
}
}

}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_UPDATE_SCENE_INSTACTIVATION _inst = (GC_UPDATE_SCENE_INSTACTIVATION) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Sceneclassid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Sceneinstcount = input.ReadInt32();
break;
}
   case  24: {
 _inst.AddSceneactivation(input.ReadInt32());
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneclassid) return false;
 if (!hasSceneinstcount) return false;
 return true;
 }

}


[Serializable]
public class CG_MOVE : PacketDistributed
{

public const int poscountFieldNumber = 1;
 private bool hasPoscount;
 private Int32 poscount_ = 0;
 public bool HasPoscount {
 get { return hasPoscount; }
 }
 public Int32 Poscount {
 get { return poscount_; }
 set { SetPoscount(value); }
 }
 public void SetPoscount(Int32 value) { 
 hasPoscount = true;
 poscount_ = value;
 }

public const int posxFieldNumber = 2;
 private pbc::PopsicleList<Int32> posx_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> posxList {
 get { return pbc::Lists.AsReadOnly(posx_); }
 }
 
 public int posxCount {
 get { return posx_.Count; }
 }
 
public Int32 GetPosx(int index) {
 return posx_[index];
 }
 public void AddPosx(Int32 value) {
 posx_.Add(value);
 }

public const int poszFieldNumber = 3;
 private pbc::PopsicleList<Int32> posz_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> poszList {
 get { return pbc::Lists.AsReadOnly(posz_); }
 }
 
 public int poszCount {
 get { return posz_.Count; }
 }
 
public Int32 GetPosz(int index) {
 return posz_[index];
 }
 public void AddPosz(Int32 value) {
 posz_.Add(value);
 }

public const int ismovingFieldNumber = 4;
 private bool hasIsmoving;
 private Int32 ismoving_ = 0;
 public bool HasIsmoving {
 get { return hasIsmoving; }
 }
 public Int32 Ismoving {
 get { return ismoving_; }
 set { SetIsmoving(value); }
 }
 public void SetIsmoving(Int32 value) { 
 hasIsmoving = true;
 ismoving_ = value;
 }

public const int objidFieldNumber = 5;
 private bool hasObjid;
 private Int32 objid_ = 0;
 public bool HasObjid {
 get { return hasObjid; }
 }
 public Int32 Objid {
 get { return objid_; }
 set { SetObjid(value); }
 }
 public void SetObjid(Int32 value) { 
 hasObjid = true;
 objid_ = value;
 }

public const int sceneIdFieldNumber = 6;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasPoscount) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Poscount);
}
{
int dataSize = 0;
for(int i=0; i<posxList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(posxList[i]);
}
size += dataSize;
size += 1 * posx_.Count;
}
{
int dataSize = 0;
for(int i=0; i<poszList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(poszList[i]);
}
size += dataSize;
size += 1 * posz_.Count;
}
 if (HasIsmoving) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Ismoving);
}
 if (HasObjid) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Objid);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasPoscount) {
output.WriteInt32(1, Poscount);
}
{
if (posx_.Count > 0) {
for(int i=0; i<posx_.Count; ++i){
output.WriteInt32(2,posx_[i]);
}
}

}
{
if (posz_.Count > 0) {
for(int i=0; i<posz_.Count; ++i){
output.WriteInt32(3,posz_[i]);
}
}

}
 
if (HasIsmoving) {
output.WriteInt32(4, Ismoving);
}
 
if (HasObjid) {
output.WriteInt32(5, Objid);
}
 
if (HasSceneId) {
output.WriteInt32(6, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_MOVE _inst = (CG_MOVE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Poscount = input.ReadInt32();
break;
}
   case  16: {
 _inst.AddPosx(input.ReadInt32());
break;
}
   case  24: {
 _inst.AddPosz(input.ReadInt32());
break;
}
   case  32: {
 _inst.Ismoving = input.ReadInt32();
break;
}
   case  40: {
 _inst.Objid = input.ReadInt32();
break;
}
   case  48: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasPoscount) return false;
 if (!hasIsmoving) return false;
 if (!hasObjid) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_MOVE : PacketDistributed
{

public const int objidFieldNumber = 1;
 private bool hasObjid;
 private Int32 objid_ = 0;
 public bool HasObjid {
 get { return hasObjid; }
 }
 public Int32 Objid {
 get { return objid_; }
 set { SetObjid(value); }
 }
 public void SetObjid(Int32 value) { 
 hasObjid = true;
 objid_ = value;
 }

public const int poscountFieldNumber = 2;
 private bool hasPoscount;
 private Int32 poscount_ = 0;
 public bool HasPoscount {
 get { return hasPoscount; }
 }
 public Int32 Poscount {
 get { return poscount_; }
 set { SetPoscount(value); }
 }
 public void SetPoscount(Int32 value) { 
 hasPoscount = true;
 poscount_ = value;
 }

public const int posserialFieldNumber = 3;
 private pbc::PopsicleList<Int32> posserial_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> posserialList {
 get { return pbc::Lists.AsReadOnly(posserial_); }
 }
 
 public int posserialCount {
 get { return posserial_.Count; }
 }
 
public Int32 GetPosserial(int index) {
 return posserial_[index];
 }
 public void AddPosserial(Int32 value) {
 posserial_.Add(value);
 }

public const int posxFieldNumber = 4;
 private pbc::PopsicleList<Int32> posx_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> posxList {
 get { return pbc::Lists.AsReadOnly(posx_); }
 }
 
 public int posxCount {
 get { return posx_.Count; }
 }
 
public Int32 GetPosx(int index) {
 return posx_[index];
 }
 public void AddPosx(Int32 value) {
 posx_.Add(value);
 }

public const int poszFieldNumber = 5;
 private pbc::PopsicleList<Int32> posz_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> poszList {
 get { return pbc::Lists.AsReadOnly(posz_); }
 }
 
 public int poszCount {
 get { return posz_.Count; }
 }
 
public Int32 GetPosz(int index) {
 return posz_[index];
 }
 public void AddPosz(Int32 value) {
 posz_.Add(value);
 }

public const int sceneIdFieldNumber = 6;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Objid);
}
 if (HasPoscount) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Poscount);
}
{
int dataSize = 0;
for(int i=0; i<posserialList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(posserialList[i]);
}
size += dataSize;
size += 1 * posserial_.Count;
}
{
int dataSize = 0;
for(int i=0; i<posxList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(posxList[i]);
}
size += dataSize;
size += 1 * posx_.Count;
}
{
int dataSize = 0;
for(int i=0; i<poszList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(poszList[i]);
}
size += dataSize;
size += 1 * posz_.Count;
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjid) {
output.WriteInt32(1, Objid);
}
 
if (HasPoscount) {
output.WriteInt32(2, Poscount);
}
{
if (posserial_.Count > 0) {
for(int i=0; i<posserial_.Count; ++i){
output.WriteInt32(3,posserial_[i]);
}
}

}
{
if (posx_.Count > 0) {
for(int i=0; i<posx_.Count; ++i){
output.WriteInt32(4,posx_[i]);
}
}

}
{
if (posz_.Count > 0) {
for(int i=0; i<posz_.Count; ++i){
output.WriteInt32(5,posz_[i]);
}
}

}
 
if (HasSceneId) {
output.WriteInt32(6, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_MOVE _inst = (GC_MOVE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Objid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Poscount = input.ReadInt32();
break;
}
   case  24: {
 _inst.AddPosserial(input.ReadInt32());
break;
}
   case  32: {
 _inst.AddPosx(input.ReadInt32());
break;
}
   case  40: {
 _inst.AddPosz(input.ReadInt32());
break;
}
   case  48: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjid) return false;
 if (!hasPoscount) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_STOP : PacketDistributed
{

public const int objidFieldNumber = 1;
 private bool hasObjid;
 private Int32 objid_ = 0;
 public bool HasObjid {
 get { return hasObjid; }
 }
 public Int32 Objid {
 get { return objid_; }
 set { SetObjid(value); }
 }
 public void SetObjid(Int32 value) { 
 hasObjid = true;
 objid_ = value;
 }

public const int posserialFieldNumber = 2;
 private bool hasPosserial;
 private Int32 posserial_ = 0;
 public bool HasPosserial {
 get { return hasPosserial; }
 }
 public Int32 Posserial {
 get { return posserial_; }
 set { SetPosserial(value); }
 }
 public void SetPosserial(Int32 value) { 
 hasPosserial = true;
 posserial_ = value;
 }

public const int posxFieldNumber = 3;
 private bool hasPosx;
 private Int32 posx_ = 0;
 public bool HasPosx {
 get { return hasPosx; }
 }
 public Int32 Posx {
 get { return posx_; }
 set { SetPosx(value); }
 }
 public void SetPosx(Int32 value) { 
 hasPosx = true;
 posx_ = value;
 }

public const int poszFieldNumber = 4;
 private bool hasPosz;
 private Int32 posz_ = 0;
 public bool HasPosz {
 get { return hasPosz; }
 }
 public Int32 Posz {
 get { return posz_; }
 set { SetPosz(value); }
 }
 public void SetPosz(Int32 value) { 
 hasPosz = true;
 posz_ = value;
 }

public const int sceneIdFieldNumber = 5;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjid) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Objid);
}
 if (HasPosserial) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Posserial);
}
 if (HasPosx) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Posx);
}
 if (HasPosz) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Posz);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjid) {
output.WriteInt32(1, Objid);
}
 
if (HasPosserial) {
output.WriteInt32(2, Posserial);
}
 
if (HasPosx) {
output.WriteInt32(3, Posx);
}
 
if (HasPosz) {
output.WriteInt32(4, Posz);
}
 
if (HasSceneId) {
output.WriteInt32(5, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_STOP _inst = (GC_STOP) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Objid = input.ReadInt32();
break;
}
   case  16: {
 _inst.Posserial = input.ReadInt32();
break;
}
   case  24: {
 _inst.Posx = input.ReadInt32();
break;
}
   case  32: {
 _inst.Posz = input.ReadInt32();
break;
}
   case  40: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjid) return false;
 if (!hasPosserial) return false;
 if (!hasPosx) return false;
 if (!hasPosz) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_TELEMOVE : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int targetPosXFieldNumber = 2;
 private bool hasTargetPosX;
 private Int32 targetPosX_ = 0;
 public bool HasTargetPosX {
 get { return hasTargetPosX; }
 }
 public Int32 TargetPosX {
 get { return targetPosX_; }
 set { SetTargetPosX(value); }
 }
 public void SetTargetPosX(Int32 value) { 
 hasTargetPosX = true;
 targetPosX_ = value;
 }

public const int targetPosZFieldNumber = 3;
 private bool hasTargetPosZ;
 private Int32 targetPosZ_ = 0;
 public bool HasTargetPosZ {
 get { return hasTargetPosZ; }
 }
 public Int32 TargetPosZ {
 get { return targetPosZ_; }
 set { SetTargetPosZ(value); }
 }
 public void SetTargetPosZ(Int32 value) { 
 hasTargetPosZ = true;
 targetPosZ_ = value;
 }

public const int needChangeFacetoFieldNumber = 4;
 private bool hasNeedChangeFaceto;
 private Int32 needChangeFaceto_ = 0;
 public bool HasNeedChangeFaceto {
 get { return hasNeedChangeFaceto; }
 }
 public Int32 NeedChangeFaceto {
 get { return needChangeFaceto_; }
 set { SetNeedChangeFaceto(value); }
 }
 public void SetNeedChangeFaceto(Int32 value) { 
 hasNeedChangeFaceto = true;
 needChangeFaceto_ = value;
 }

public const int animaIdFieldNumber = 5;
 private bool hasAnimaId;
 private Int32 animaId_ = 0;
 public bool HasAnimaId {
 get { return hasAnimaId; }
 }
 public Int32 AnimaId {
 get { return animaId_; }
 set { SetAnimaId(value); }
 }
 public void SetAnimaId(Int32 value) { 
 hasAnimaId = true;
 animaId_ = value;
 }

public const int sceneIdFieldNumber = 6;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasTargetPosX) {
size += pb::CodedOutputStream.ComputeInt32Size(2, TargetPosX);
}
 if (HasTargetPosZ) {
size += pb::CodedOutputStream.ComputeInt32Size(3, TargetPosZ);
}
 if (HasNeedChangeFaceto) {
size += pb::CodedOutputStream.ComputeInt32Size(4, NeedChangeFaceto);
}
 if (HasAnimaId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, AnimaId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasTargetPosX) {
output.WriteInt32(2, TargetPosX);
}
 
if (HasTargetPosZ) {
output.WriteInt32(3, TargetPosZ);
}
 
if (HasNeedChangeFaceto) {
output.WriteInt32(4, NeedChangeFaceto);
}
 
if (HasAnimaId) {
output.WriteInt32(5, AnimaId);
}
 
if (HasSceneId) {
output.WriteInt32(6, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_TELEMOVE _inst = (GC_TELEMOVE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.TargetPosX = input.ReadInt32();
break;
}
   case  24: {
 _inst.TargetPosZ = input.ReadInt32();
break;
}
   case  32: {
 _inst.NeedChangeFaceto = input.ReadInt32();
break;
}
   case  40: {
 _inst.AnimaId = input.ReadInt32();
break;
}
   case  48: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasTargetPosX) return false;
 if (!hasTargetPosZ) return false;
 if (!hasNeedChangeFaceto) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class CG_SKILL_USE : PacketDistributed
{

public const int senderIdFieldNumber = 1;
 private bool hasSenderId;
 private Int32 senderId_ = 0;
 public bool HasSenderId {
 get { return hasSenderId; }
 }
 public Int32 SenderId {
 get { return senderId_; }
 set { SetSenderId(value); }
 }
 public void SetSenderId(Int32 value) { 
 hasSenderId = true;
 senderId_ = value;
 }

public const int skillIdFieldNumber = 2;
 private bool hasSkillId;
 private Int32 skillId_ = 0;
 public bool HasSkillId {
 get { return hasSkillId; }
 }
 public Int32 SkillId {
 get { return skillId_; }
 set { SetSkillId(value); }
 }
 public void SetSkillId(Int32 value) { 
 hasSkillId = true;
 skillId_ = value;
 }

public const int targetIdFieldNumber = 3;
 private bool hasTargetId;
 private Int32 targetId_ = 0;
 public bool HasTargetId {
 get { return hasTargetId; }
 }
 public Int32 TargetId {
 get { return targetId_; }
 set { SetTargetId(value); }
 }
 public void SetTargetId(Int32 value) { 
 hasTargetId = true;
 targetId_ = value;
 }

public const int sceneIdFieldNumber = 4;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSenderId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SenderId);
}
 if (HasSkillId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, SkillId);
}
 if (HasTargetId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, TargetId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(4, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSenderId) {
output.WriteInt32(1, SenderId);
}
 
if (HasSkillId) {
output.WriteInt32(2, SkillId);
}
 
if (HasTargetId) {
output.WriteInt32(3, TargetId);
}
 
if (HasSceneId) {
output.WriteInt32(4, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_SKILL_USE _inst = (CG_SKILL_USE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SenderId = input.ReadInt32();
break;
}
   case  16: {
 _inst.SkillId = input.ReadInt32();
break;
}
   case  24: {
 _inst.TargetId = input.ReadInt32();
break;
}
   case  32: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSenderId) return false;
 if (!hasSkillId) return false;
 if (!hasTargetId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_RET_USE_SKILL : PacketDistributed
{

public const int skillIdFieldNumber = 1;
 private bool hasSkillId;
 private Int32 skillId_ = 0;
 public bool HasSkillId {
 get { return hasSkillId; }
 }
 public Int32 SkillId {
 get { return skillId_; }
 set { SetSkillId(value); }
 }
 public void SetSkillId(Int32 value) { 
 hasSkillId = true;
 skillId_ = value;
 }

public const int senderIdFieldNumber = 2;
 private bool hasSenderId;
 private Int32 senderId_ = 0;
 public bool HasSenderId {
 get { return hasSenderId; }
 }
 public Int32 SenderId {
 get { return senderId_; }
 set { SetSenderId(value); }
 }
 public void SetSenderId(Int32 value) { 
 hasSenderId = true;
 senderId_ = value;
 }

public const int TargetIdFieldNumber = 3;
 private bool hasTargetId;
 private Int32 TargetId_ = 0;
 public bool HasTargetId {
 get { return hasTargetId; }
 }
 public Int32 TargetId {
 get { return TargetId_; }
 set { SetTargetId(value); }
 }
 public void SetTargetId(Int32 value) { 
 hasTargetId = true;
 TargetId_ = value;
 }

public const int skillfailTypeFieldNumber = 4;
 private bool hasSkillfailType;
 private Int32 skillfailType_ = 0;
 public bool HasSkillfailType {
 get { return hasSkillfailType; }
 }
 public Int32 SkillfailType {
 get { return skillfailType_; }
 set { SetSkillfailType(value); }
 }
 public void SetSkillfailType(Int32 value) { 
 hasSkillfailType = true;
 skillfailType_ = value;
 }

public const int skillnameFieldNumber = 5;
 private bool hasSkillname;
 private string skillname_ = "";
 public bool HasSkillname {
 get { return hasSkillname; }
 }
 public string Skillname {
 get { return skillname_; }
 set { SetSkillname(value); }
 }
 public void SetSkillname(string value) { 
 hasSkillname = true;
 skillname_ = value;
 }

public const int sceneIdFieldNumber = 6;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSkillId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SkillId);
}
 if (HasSenderId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, SenderId);
}
 if (HasTargetId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, TargetId);
}
 if (HasSkillfailType) {
size += pb::CodedOutputStream.ComputeInt32Size(4, SkillfailType);
}
 if (HasSkillname) {
size += pb::CodedOutputStream.ComputeStringSize(5, Skillname);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(6, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSkillId) {
output.WriteInt32(1, SkillId);
}
 
if (HasSenderId) {
output.WriteInt32(2, SenderId);
}
 
if (HasTargetId) {
output.WriteInt32(3, TargetId);
}
 
if (HasSkillfailType) {
output.WriteInt32(4, SkillfailType);
}
 
if (HasSkillname) {
output.WriteString(5, Skillname);
}
 
if (HasSceneId) {
output.WriteInt32(6, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_RET_USE_SKILL _inst = (GC_RET_USE_SKILL) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SkillId = input.ReadInt32();
break;
}
   case  16: {
 _inst.SenderId = input.ReadInt32();
break;
}
   case  24: {
 _inst.TargetId = input.ReadInt32();
break;
}
   case  32: {
 _inst.SkillfailType = input.ReadInt32();
break;
}
   case  42: {
 _inst.Skillname = input.ReadString();
break;
}
   case  48: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSkillId) return false;
 if (!hasSenderId) return false;
 if (!hasTargetId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_UPDATE_ANIMATION_STATE : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int AnimationStateFieldNumber = 2;
 private bool hasAnimationState;
 private Int32 AnimationState_ = 0;
 public bool HasAnimationState {
 get { return hasAnimationState; }
 }
 public Int32 AnimationState {
 get { return AnimationState_; }
 set { SetAnimationState(value); }
 }
 public void SetAnimationState(Int32 value) { 
 hasAnimationState = true;
 AnimationState_ = value;
 }

public const int verticalOffDisFieldNumber = 3;
 private bool hasVerticalOffDis;
 private Int32 verticalOffDis_ = 0;
 public bool HasVerticalOffDis {
 get { return hasVerticalOffDis; }
 }
 public Int32 VerticalOffDis {
 get { return verticalOffDis_; }
 set { SetVerticalOffDis(value); }
 }
 public void SetVerticalOffDis(Int32 value) { 
 hasVerticalOffDis = true;
 verticalOffDis_ = value;
 }

public const int horizontalOffDisFieldNumber = 4;
 private bool hasHorizontalOffDis;
 private Int32 horizontalOffDis_ = 0;
 public bool HasHorizontalOffDis {
 get { return hasHorizontalOffDis; }
 }
 public Int32 HorizontalOffDis {
 get { return horizontalOffDis_; }
 set { SetHorizontalOffDis(value); }
 }
 public void SetHorizontalOffDis(Int32 value) { 
 hasHorizontalOffDis = true;
 horizontalOffDis_ = value;
 }

public const int stateLastTimeFieldNumber = 5;
 private bool hasStateLastTime;
 private Int32 stateLastTime_ = 0;
 public bool HasStateLastTime {
 get { return hasStateLastTime; }
 }
 public Int32 StateLastTime {
 get { return stateLastTime_; }
 set { SetStateLastTime(value); }
 }
 public void SetStateLastTime(Int32 value) { 
 hasStateLastTime = true;
 stateLastTime_ = value;
 }

public const int stateResortTimeFieldNumber = 6;
 private bool hasStateResortTime;
 private Int32 stateResortTime_ = 0;
 public bool HasStateResortTime {
 get { return hasStateResortTime; }
 }
 public Int32 StateResortTime {
 get { return stateResortTime_; }
 set { SetStateResortTime(value); }
 }
 public void SetStateResortTime(Int32 value) { 
 hasStateResortTime = true;
 stateResortTime_ = value;
 }

public const int hitTimesFieldNumber = 7;
 private bool hasHitTimes;
 private Int32 hitTimes_ = 0;
 public bool HasHitTimes {
 get { return hasHitTimes; }
 }
 public Int32 HitTimes {
 get { return hitTimes_; }
 set { SetHitTimes(value); }
 }
 public void SetHitTimes(Int32 value) { 
 hasHitTimes = true;
 hitTimes_ = value;
 }

public const int hitIntervalFieldNumber = 8;
 private bool hasHitInterval;
 private Int32 hitInterval_ = 0;
 public bool HasHitInterval {
 get { return hasHitInterval; }
 }
 public Int32 HitInterval {
 get { return hitInterval_; }
 set { SetHitInterval(value); }
 }
 public void SetHitInterval(Int32 value) { 
 hasHitInterval = true;
 hitInterval_ = value;
 }

public const int sceneIdFieldNumber = 9;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasAnimationState) {
size += pb::CodedOutputStream.ComputeInt32Size(2, AnimationState);
}
 if (HasVerticalOffDis) {
size += pb::CodedOutputStream.ComputeInt32Size(3, VerticalOffDis);
}
 if (HasHorizontalOffDis) {
size += pb::CodedOutputStream.ComputeInt32Size(4, HorizontalOffDis);
}
 if (HasStateLastTime) {
size += pb::CodedOutputStream.ComputeInt32Size(5, StateLastTime);
}
 if (HasStateResortTime) {
size += pb::CodedOutputStream.ComputeInt32Size(6, StateResortTime);
}
 if (HasHitTimes) {
size += pb::CodedOutputStream.ComputeInt32Size(7, HitTimes);
}
 if (HasHitInterval) {
size += pb::CodedOutputStream.ComputeInt32Size(8, HitInterval);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(9, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasAnimationState) {
output.WriteInt32(2, AnimationState);
}
 
if (HasVerticalOffDis) {
output.WriteInt32(3, VerticalOffDis);
}
 
if (HasHorizontalOffDis) {
output.WriteInt32(4, HorizontalOffDis);
}
 
if (HasStateLastTime) {
output.WriteInt32(5, StateLastTime);
}
 
if (HasStateResortTime) {
output.WriteInt32(6, StateResortTime);
}
 
if (HasHitTimes) {
output.WriteInt32(7, HitTimes);
}
 
if (HasHitInterval) {
output.WriteInt32(8, HitInterval);
}
 
if (HasSceneId) {
output.WriteInt32(9, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_UPDATE_ANIMATION_STATE _inst = (GC_UPDATE_ANIMATION_STATE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.AnimationState = input.ReadInt32();
break;
}
   case  24: {
 _inst.VerticalOffDis = input.ReadInt32();
break;
}
   case  32: {
 _inst.HorizontalOffDis = input.ReadInt32();
break;
}
   case  40: {
 _inst.StateLastTime = input.ReadInt32();
break;
}
   case  48: {
 _inst.StateResortTime = input.ReadInt32();
break;
}
   case  56: {
 _inst.HitTimes = input.ReadInt32();
break;
}
   case  64: {
 _inst.HitInterval = input.ReadInt32();
break;
}
   case  72: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasAnimationState) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_DELETE_OBJ : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int sceneIdFieldNumber = 2;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int marchIdFieldNumber = 3;
 private bool hasMarchId;
 private Int32 marchId_ = 0;
 public bool HasMarchId {
 get { return hasMarchId; }
 }
 public Int32 MarchId {
 get { return marchId_; }
 set { SetMarchId(value); }
 }
 public void SetMarchId(Int32 value) { 
 hasMarchId = true;
 marchId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, SceneId);
}
 if (HasMarchId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, MarchId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasSceneId) {
output.WriteInt32(2, SceneId);
}
 
if (HasMarchId) {
output.WriteInt32(3, MarchId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_DELETE_OBJ _inst = (GC_DELETE_OBJ) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.SceneId = input.ReadInt32();
break;
}
   case  24: {
 _inst.MarchId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasSceneId) return false;
 if (!hasMarchId) return false;
 return true;
 }

}


[Serializable]
public class GC_ATTACKFLY : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int DisFieldNumber = 2;
 private bool hasDis;
 private Int32 Dis_ = 0;
 public bool HasDis {
 get { return hasDis; }
 }
 public Int32 Dis {
 get { return Dis_; }
 set { SetDis(value); }
 }
 public void SetDis(Int32 value) { 
 hasDis = true;
 Dis_ = value;
 }

public const int HightFieldNumber = 3;
 private bool hasHight;
 private Int32 Hight_ = 0;
 public bool HasHight {
 get { return hasHight; }
 }
 public Int32 Hight {
 get { return Hight_; }
 set { SetHight(value); }
 }
 public void SetHight(Int32 value) { 
 hasHight = true;
 Hight_ = value;
 }

public const int FlyTimeFieldNumber = 4;
 private bool hasFlyTime;
 private Int32 FlyTime_ = 0;
 public bool HasFlyTime {
 get { return hasFlyTime; }
 }
 public Int32 FlyTime {
 get { return FlyTime_; }
 set { SetFlyTime(value); }
 }
 public void SetFlyTime(Int32 value) { 
 hasFlyTime = true;
 FlyTime_ = value;
 }

public const int sceneIdFieldNumber = 5;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasDis) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Dis);
}
 if (HasHight) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Hight);
}
 if (HasFlyTime) {
size += pb::CodedOutputStream.ComputeInt32Size(4, FlyTime);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasDis) {
output.WriteInt32(2, Dis);
}
 
if (HasHight) {
output.WriteInt32(3, Hight);
}
 
if (HasFlyTime) {
output.WriteInt32(4, FlyTime);
}
 
if (HasSceneId) {
output.WriteInt32(5, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_ATTACKFLY _inst = (GC_ATTACKFLY) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Dis = input.ReadInt32();
break;
}
   case  24: {
 _inst.Hight = input.ReadInt32();
break;
}
   case  32: {
 _inst.FlyTime = input.ReadInt32();
break;
}
   case  40: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasDis) return false;
 if (!hasHight) return false;
 if (!hasFlyTime) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_FORCE_SETPOS : PacketDistributed
{

public const int objIDFieldNumber = 1;
 private bool hasObjID;
 private Int32 objID_ = 0;
 public bool HasObjID {
 get { return hasObjID; }
 }
 public Int32 ObjID {
 get { return objID_; }
 set { SetObjID(value); }
 }
 public void SetObjID(Int32 value) { 
 hasObjID = true;
 objID_ = value;
 }

public const int PosXFieldNumber = 2;
 private bool hasPosX;
 private Int32 PosX_ = 0;
 public bool HasPosX {
 get { return hasPosX; }
 }
 public Int32 PosX {
 get { return PosX_; }
 set { SetPosX(value); }
 }
 public void SetPosX(Int32 value) { 
 hasPosX = true;
 PosX_ = value;
 }

public const int PosZFieldNumber = 3;
 private bool hasPosZ;
 private Int32 PosZ_ = 0;
 public bool HasPosZ {
 get { return hasPosZ; }
 }
 public Int32 PosZ {
 get { return PosZ_; }
 set { SetPosZ(value); }
 }
 public void SetPosZ(Int32 value) { 
 hasPosZ = true;
 PosZ_ = value;
 }

public const int sceneIdFieldNumber = 4;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjID) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjID);
}
 if (HasPosX) {
size += pb::CodedOutputStream.ComputeInt32Size(2, PosX);
}
 if (HasPosZ) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PosZ);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(4, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjID) {
output.WriteInt32(1, ObjID);
}
 
if (HasPosX) {
output.WriteInt32(2, PosX);
}
 
if (HasPosZ) {
output.WriteInt32(3, PosZ);
}
 
if (HasSceneId) {
output.WriteInt32(4, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_FORCE_SETPOS _inst = (GC_FORCE_SETPOS) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjID = input.ReadInt32();
break;
}
   case  16: {
 _inst.PosX = input.ReadInt32();
break;
}
   case  24: {
 _inst.PosZ = input.ReadInt32();
break;
}
   case  32: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjID) return false;
 if (!hasPosX) return false;
 if (!hasPosZ) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_UPDATE_NEEDIMPACTINFO : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int impactIdFieldNumber = 2;
 private pbc::PopsicleList<Int32> impactId_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> impactIdList {
 get { return pbc::Lists.AsReadOnly(impactId_); }
 }
 
 public int impactIdCount {
 get { return impactId_.Count; }
 }
 
public Int32 GetImpactId(int index) {
 return impactId_[index];
 }
 public void AddImpactId(Int32 value) {
 impactId_.Add(value);
 }

public const int impactLogicIdFieldNumber = 3;
 private pbc::PopsicleList<Int32> impactLogicId_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> impactLogicIdList {
 get { return pbc::Lists.AsReadOnly(impactLogicId_); }
 }
 
 public int impactLogicIdCount {
 get { return impactLogicId_.Count; }
 }
 
public Int32 GetImpactLogicId(int index) {
 return impactLogicId_[index];
 }
 public void AddImpactLogicId(Int32 value) {
 impactLogicId_.Add(value);
 }

public const int isForeverFieldNumber = 4;
 private pbc::PopsicleList<Int32> isForever_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> isForeverList {
 get { return pbc::Lists.AsReadOnly(isForever_); }
 }
 
 public int isForeverCount {
 get { return isForever_.Count; }
 }
 
public Int32 GetIsForever(int index) {
 return isForever_[index];
 }
 public void AddIsForever(Int32 value) {
 isForever_.Add(value);
 }

public const int remainTimeFieldNumber = 5;
 private pbc::PopsicleList<Int32> remainTime_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> remainTimeList {
 get { return pbc::Lists.AsReadOnly(remainTime_); }
 }
 
 public int remainTimeCount {
 get { return remainTime_.Count; }
 }
 
public Int32 GetRemainTime(int index) {
 return remainTime_[index];
 }
 public void AddRemainTime(Int32 value) {
 remainTime_.Add(value);
 }

public const int isAddFieldNumber = 6;
 private pbc::PopsicleList<Int32> isAdd_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> isAddList {
 get { return pbc::Lists.AsReadOnly(isAdd_); }
 }
 
 public int isAddCount {
 get { return isAdd_.Count; }
 }
 
public Int32 GetIsAdd(int index) {
 return isAdd_[index];
 }
 public void AddIsAdd(Int32 value) {
 isAdd_.Add(value);
 }

public const int sceneIdFieldNumber = 7;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
{
int dataSize = 0;
for(int i=0; i<impactIdList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(impactIdList[i]);
}
size += dataSize;
size += 1 * impactId_.Count;
}
{
int dataSize = 0;
for(int i=0; i<impactLogicIdList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(impactLogicIdList[i]);
}
size += dataSize;
size += 1 * impactLogicId_.Count;
}
{
int dataSize = 0;
for(int i=0; i<isForeverList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(isForeverList[i]);
}
size += dataSize;
size += 1 * isForever_.Count;
}
{
int dataSize = 0;
for(int i=0; i<remainTimeList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(remainTimeList[i]);
}
size += dataSize;
size += 1 * remainTime_.Count;
}
{
int dataSize = 0;
for(int i=0; i<isAddList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(isAddList[i]);
}
size += dataSize;
size += 1 * isAdd_.Count;
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(7, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
{
if (impactId_.Count > 0) {
for(int i=0; i<impactId_.Count; ++i){
output.WriteInt32(2,impactId_[i]);
}
}

}
{
if (impactLogicId_.Count > 0) {
for(int i=0; i<impactLogicId_.Count; ++i){
output.WriteInt32(3,impactLogicId_[i]);
}
}

}
{
if (isForever_.Count > 0) {
for(int i=0; i<isForever_.Count; ++i){
output.WriteInt32(4,isForever_[i]);
}
}

}
{
if (remainTime_.Count > 0) {
for(int i=0; i<remainTime_.Count; ++i){
output.WriteInt32(5,remainTime_[i]);
}
}

}
{
if (isAdd_.Count > 0) {
for(int i=0; i<isAdd_.Count; ++i){
output.WriteInt32(6,isAdd_[i]);
}
}

}
 
if (HasSceneId) {
output.WriteInt32(7, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_UPDATE_NEEDIMPACTINFO _inst = (GC_UPDATE_NEEDIMPACTINFO) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.AddImpactId(input.ReadInt32());
break;
}
   case  24: {
 _inst.AddImpactLogicId(input.ReadInt32());
break;
}
   case  32: {
 _inst.AddIsForever(input.ReadInt32());
break;
}
   case  40: {
 _inst.AddRemainTime(input.ReadInt32());
break;
}
   case  48: {
 _inst.AddIsAdd(input.ReadInt32());
break;
}
   case  56: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_PLAY_EFFECT : PacketDistributed
{

public const int ObjIDFieldNumber = 1;
 private bool hasObjID;
 private Int32 ObjID_ = 0;
 public bool HasObjID {
 get { return hasObjID; }
 }
 public Int32 ObjID {
 get { return ObjID_; }
 set { SetObjID(value); }
 }
 public void SetObjID(Int32 value) { 
 hasObjID = true;
 ObjID_ = value;
 }

public const int EffectIDFieldNumber = 2;
 private bool hasEffectID;
 private Int32 EffectID_ = 0;
 public bool HasEffectID {
 get { return hasEffectID; }
 }
 public Int32 EffectID {
 get { return EffectID_; }
 set { SetEffectID(value); }
 }
 public void SetEffectID(Int32 value) { 
 hasEffectID = true;
 EffectID_ = value;
 }

public const int sceneIdFieldNumber = 5;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjID) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjID);
}
 if (HasEffectID) {
size += pb::CodedOutputStream.ComputeInt32Size(2, EffectID);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjID) {
output.WriteInt32(1, ObjID);
}
 
if (HasEffectID) {
output.WriteInt32(2, EffectID);
}
 
if (HasSceneId) {
output.WriteInt32(5, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_PLAY_EFFECT _inst = (GC_PLAY_EFFECT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjID = input.ReadInt32();
break;
}
   case  16: {
 _inst.EffectID = input.ReadInt32();
break;
}
   case  40: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjID) return false;
 if (!hasEffectID) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_REMOVEEFFECT : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int effectIdFieldNumber = 2;
 private bool hasEffectId;
 private Int32 effectId_ = 0;
 public bool HasEffectId {
 get { return hasEffectId; }
 }
 public Int32 EffectId {
 get { return effectId_; }
 set { SetEffectId(value); }
 }
 public void SetEffectId(Int32 value) { 
 hasEffectId = true;
 effectId_ = value;
 }

public const int sceneIdFieldNumber = 3;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasEffectId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, EffectId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasEffectId) {
output.WriteInt32(2, EffectId);
}
 
if (HasSceneId) {
output.WriteInt32(3, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_REMOVEEFFECT _inst = (GC_REMOVEEFFECT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.EffectId = input.ReadInt32();
break;
}
   case  24: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasEffectId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class CG_FIGHT : PacketDistributed
{

public const int TypeFieldNumber = 1;
 private bool hasType;
 private Int32 Type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return Type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 Type_ = value;
 }

public const int AttackIdFieldNumber = 2;
 private bool hasAttackId;
 private Int64 AttackId_ = 0;
 public bool HasAttackId {
 get { return hasAttackId; }
 }
 public Int64 AttackId {
 get { return AttackId_; }
 set { SetAttackId(value); }
 }
 public void SetAttackId(Int64 value) { 
 hasAttackId = true;
 AttackId_ = value;
 }

public const int DefenceIdFieldNumber = 3;
 private bool hasDefenceId;
 private Int64 DefenceId_ = 0;
 public bool HasDefenceId {
 get { return hasDefenceId; }
 }
 public Int64 DefenceId {
 get { return DefenceId_; }
 set { SetDefenceId(value); }
 }
 public void SetDefenceId(Int64 value) { 
 hasDefenceId = true;
 DefenceId_ = value;
 }

public const int sceneIdFieldNumber = 4;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
 if (HasAttackId) {
size += pb::CodedOutputStream.ComputeInt64Size(2, AttackId);
}
 if (HasDefenceId) {
size += pb::CodedOutputStream.ComputeInt64Size(3, DefenceId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(4, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
 
if (HasAttackId) {
output.WriteInt64(2, AttackId);
}
 
if (HasDefenceId) {
output.WriteInt64(3, DefenceId);
}
 
if (HasSceneId) {
output.WriteInt32(4, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_FIGHT _inst = (CG_FIGHT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
   case  16: {
 _inst.AttackId = input.ReadInt64();
break;
}
   case  24: {
 _inst.DefenceId = input.ReadInt64();
break;
}
   case  32: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasType) return false;
 if (!hasAttackId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_FIGHT : PacketDistributed
{

public const int retFieldNumber = 1;
 private bool hasRet;
 private Int32 ret_ = 0;
 public bool HasRet {
 get { return hasRet; }
 }
 public Int32 Ret {
 get { return ret_; }
 set { SetRet(value); }
 }
 public void SetRet(Int32 value) { 
 hasRet = true;
 ret_ = value;
 }

public const int marchIdFieldNumber = 2;
 private bool hasMarchId;
 private Int64 marchId_ = 0;
 public bool HasMarchId {
 get { return hasMarchId; }
 }
 public Int64 MarchId {
 get { return marchId_; }
 set { SetMarchId(value); }
 }
 public void SetMarchId(Int64 value) { 
 hasMarchId = true;
 marchId_ = value;
 }

public const int sceneIdFieldNumber = 3;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasRet) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Ret);
}
 if (HasMarchId) {
size += pb::CodedOutputStream.ComputeInt64Size(2, MarchId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasRet) {
output.WriteInt32(1, Ret);
}
 
if (HasMarchId) {
output.WriteInt64(2, MarchId);
}
 
if (HasSceneId) {
output.WriteInt32(3, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_FIGHT _inst = (GC_FIGHT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Ret = input.ReadInt32();
break;
}
   case  16: {
 _inst.MarchId = input.ReadInt64();
break;
}
   case  24: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasRet) return false;
 return true;
 }

}


[Serializable]
public class CG_LEAVE_COPYSCENE : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int sceneIdFieldNumber = 2;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasSceneId) {
output.WriteInt32(2, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_LEAVE_COPYSCENE _inst = (CG_LEAVE_COPYSCENE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class CG_ROBOT_OPEN : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int openFieldNumber = 2;
 private bool hasOpen;
 private Int32 open_ = 0;
 public bool HasOpen {
 get { return hasOpen; }
 }
 public Int32 Open {
 get { return open_; }
 set { SetOpen(value); }
 }
 public void SetOpen(Int32 value) { 
 hasOpen = true;
 open_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 if (HasOpen) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Open);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 
if (HasOpen) {
output.WriteInt32(2, Open);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_ROBOT_OPEN _inst = (CG_ROBOT_OPEN) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Open = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 if (!hasOpen) return false;
 return true;
 }

}


[Serializable]
public class GC_ROBOT_OPEN : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int retFieldNumber = 2;
 private bool hasRet;
 private Int32 ret_ = 0;
 public bool HasRet {
 get { return hasRet; }
 }
 public Int32 Ret {
 get { return ret_; }
 set { SetRet(value); }
 }
 public void SetRet(Int32 value) { 
 hasRet = true;
 ret_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 if (HasRet) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Ret);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 
if (HasRet) {
output.WriteInt32(2, Ret);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_ROBOT_OPEN _inst = (GC_ROBOT_OPEN) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
   case  16: {
 _inst.Ret = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 if (!hasRet) return false;
 return true;
 }

}


[Serializable]
public class CG_ASSIGN_HERO : PacketDistributed
{

public const int marchidFieldNumber = 1;
 private bool hasMarchid;
 private Int64 marchid_ = 0;
 public bool HasMarchid {
 get { return hasMarchid; }
 }
 public Int64 Marchid {
 get { return marchid_; }
 set { SetMarchid(value); }
 }
 public void SetMarchid(Int64 value) { 
 hasMarchid = true;
 marchid_ = value;
 }

public const int heroidFieldNumber = 2;
 private bool hasHeroid;
 private Int64 heroid_ = 0;
 public bool HasHeroid {
 get { return hasHeroid; }
 }
 public Int64 Heroid {
 get { return heroid_; }
 set { SetHeroid(value); }
 }
 public void SetHeroid(Int64 value) { 
 hasHeroid = true;
 heroid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Marchid);
}
 if (HasHeroid) {
size += pb::CodedOutputStream.ComputeInt64Size(2, Heroid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchid) {
output.WriteInt64(1, Marchid);
}
 
if (HasHeroid) {
output.WriteInt64(2, Heroid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_ASSIGN_HERO _inst = (CG_ASSIGN_HERO) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Marchid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Heroid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchid) return false;
 if (!hasHeroid) return false;
 return true;
 }

}


[Serializable]
public class GC_ASSIGN_HERO : PacketDistributed
{

public const int marchidFieldNumber = 1;
 private bool hasMarchid;
 private Int64 marchid_ = 0;
 public bool HasMarchid {
 get { return hasMarchid; }
 }
 public Int64 Marchid {
 get { return marchid_; }
 set { SetMarchid(value); }
 }
 public void SetMarchid(Int64 value) { 
 hasMarchid = true;
 marchid_ = value;
 }

public const int heroIdFieldNumber = 2;
 private bool hasHeroId;
 private Int64 heroId_ = 0;
 public bool HasHeroId {
 get { return hasHeroId; }
 }
 public Int64 HeroId {
 get { return heroId_; }
 set { SetHeroId(value); }
 }
 public void SetHeroId(Int64 value) { 
 hasHeroId = true;
 heroId_ = value;
 }

public const int retFieldNumber = 3;
 private bool hasRet;
 private Int32 ret_ = 0;
 public bool HasRet {
 get { return hasRet; }
 }
 public Int32 Ret {
 get { return ret_; }
 set { SetRet(value); }
 }
 public void SetRet(Int32 value) { 
 hasRet = true;
 ret_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Marchid);
}
 if (HasHeroId) {
size += pb::CodedOutputStream.ComputeInt64Size(2, HeroId);
}
 if (HasRet) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Ret);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchid) {
output.WriteInt64(1, Marchid);
}
 
if (HasHeroId) {
output.WriteInt64(2, HeroId);
}
 
if (HasRet) {
output.WriteInt32(3, Ret);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_ASSIGN_HERO _inst = (GC_ASSIGN_HERO) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Marchid = input.ReadInt64();
break;
}
   case  16: {
 _inst.HeroId = input.ReadInt64();
break;
}
   case  24: {
 _inst.Ret = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchid) return false;
 if (!hasHeroId) return false;
 if (!hasRet) return false;
 return true;
 }

}


[Serializable]
public class CG_SEND_MARCH : PacketDistributed
{

public const int marchidFieldNumber = 1;
 private bool hasMarchid;
 private Int64 marchid_ = 0;
 public bool HasMarchid {
 get { return hasMarchid; }
 }
 public Int64 Marchid {
 get { return marchid_; }
 set { SetMarchid(value); }
 }
 public void SetMarchid(Int64 value) { 
 hasMarchid = true;
 marchid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Marchid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchid) {
output.WriteInt64(1, Marchid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_SEND_MARCH _inst = (CG_SEND_MARCH) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Marchid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchid) return false;
 return true;
 }

}


[Serializable]
public class GC_SEND_MARCH : PacketDistributed
{

public const int marchIdFieldNumber = 1;
 private bool hasMarchId;
 private Int64 marchId_ = 0;
 public bool HasMarchId {
 get { return hasMarchId; }
 }
 public Int64 MarchId {
 get { return marchId_; }
 set { SetMarchId(value); }
 }
 public void SetMarchId(Int64 value) { 
 hasMarchId = true;
 marchId_ = value;
 }

public const int retFieldNumber = 2;
 private bool hasRet;
 private Int32 ret_ = 0;
 public bool HasRet {
 get { return hasRet; }
 }
 public Int32 Ret {
 get { return ret_; }
 set { SetRet(value); }
 }
 public void SetRet(Int32 value) { 
 hasRet = true;
 ret_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchId) {
size += pb::CodedOutputStream.ComputeInt64Size(1, MarchId);
}
 if (HasRet) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Ret);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchId) {
output.WriteInt64(1, MarchId);
}
 
if (HasRet) {
output.WriteInt32(2, Ret);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_SEND_MARCH _inst = (GC_SEND_MARCH) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.MarchId = input.ReadInt64();
break;
}
   case  16: {
 _inst.Ret = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchId) return false;
 if (!hasRet) return false;
 return true;
 }

}


[Serializable]
public class CG_CREATEROLE : PacketDistributed
{

public const int genderFieldNumber = 1;
 private bool hasGender;
 private Int32 gender_ = 0;
 public bool HasGender {
 get { return hasGender; }
 }
 public Int32 Gender {
 get { return gender_; }
 set { SetGender(value); }
 }
 public void SetGender(Int32 value) { 
 hasGender = true;
 gender_ = value;
 }

public const int nameFieldNumber = 2;
 private bool hasName;
 private string name_ = "";
 public bool HasName {
 get { return hasName; }
 }
 public string Name {
 get { return name_; }
 set { SetName(value); }
 }
 public void SetName(string value) { 
 hasName = true;
 name_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGender) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Gender);
}
 if (HasName) {
size += pb::CodedOutputStream.ComputeStringSize(2, Name);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGender) {
output.WriteInt32(1, Gender);
}
 
if (HasName) {
output.WriteString(2, Name);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_CREATEROLE _inst = (CG_CREATEROLE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Gender = input.ReadInt32();
break;
}
   case  18: {
 _inst.Name = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGender) return false;
 if (!hasName) return false;
 return true;
 }

}


[Serializable]
public class GC_CREATEROLE_RET : PacketDistributed
{
public enum CREATEROLE_RESULT 
 { 
  CREATEROLE_SUCCESS    = 0,  // 创建人物成功 
  CREATEROLE_FAIL    = 1,  // 服务器内部错误 
  CREATEROLE_FAIL_NAMEEXIST  = 2,  // 名字已经存在 
  CREATEROLE_FAIL_NAMESCREENING  = 3,  // 名字含有屏蔽字 
  CREATEROLE_FAIL_LONGNAME  = 4,  // 名字太长 
 
 
 }
public const int resultFieldNumber = 1;
 private bool hasResult;
 private Int32 result_ = 0;
 public bool HasResult {
 get { return hasResult; }
 }
 public Int32 Result {
 get { return result_; }
 set { SetResult(value); }
 }
 public void SetResult(Int32 value) { 
 hasResult = true;
 result_ = value;
 }

public const int playerGuidFieldNumber = 2;
 private bool hasPlayerGuid;
 private UInt64 playerGuid_ = 0;
 public bool HasPlayerGuid {
 get { return hasPlayerGuid; }
 }
 public UInt64 PlayerGuid {
 get { return playerGuid_; }
 set { SetPlayerGuid(value); }
 }
 public void SetPlayerGuid(UInt64 value) { 
 hasPlayerGuid = true;
 playerGuid_ = value;
 }

public const int playerNameFieldNumber = 3;
 private bool hasPlayerName;
 private string playerName_ = "";
 public bool HasPlayerName {
 get { return hasPlayerName; }
 }
 public string PlayerName {
 get { return playerName_; }
 set { SetPlayerName(value); }
 }
 public void SetPlayerName(string value) { 
 hasPlayerName = true;
 playerName_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasResult) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Result);
}
 if (HasPlayerGuid) {
size += pb::CodedOutputStream.ComputeUInt64Size(2, PlayerGuid);
}
 if (HasPlayerName) {
size += pb::CodedOutputStream.ComputeStringSize(3, PlayerName);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasResult) {
output.WriteInt32(1, Result);
}
 
if (HasPlayerGuid) {
output.WriteUInt64(2, PlayerGuid);
}
 
if (HasPlayerName) {
output.WriteString(3, PlayerName);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_CREATEROLE_RET _inst = (GC_CREATEROLE_RET) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Result = input.ReadInt32();
break;
}
   case  16: {
 _inst.PlayerGuid = input.ReadUInt64();
break;
}
   case  26: {
 _inst.PlayerName = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasResult) return false;
 if (!hasPlayerGuid) return false;
 if (!hasPlayerName) return false;
 return true;
 }

}


[Serializable]
public class GC_LOGIN_QUEUE_STATUS : PacketDistributed
{
public enum QUEUESTATUS 
 { 
  BEGINQUEUE = 0, 
  QUEUING = 1, 
  ENDQUEUE = 2, 
 }
public const int statusFieldNumber = 1;
 private bool hasStatus;
 private Int32 status_ = 0;
 public bool HasStatus {
 get { return hasStatus; }
 }
 public Int32 Status {
 get { return status_; }
 set { SetStatus(value); }
 }
 public void SetStatus(Int32 value) { 
 hasStatus = true;
 status_ = value;
 }

public const int indexFieldNumber = 2;
 private bool hasIndex;
 private Int32 index_ = 0;
 public bool HasIndex {
 get { return hasIndex; }
 }
 public Int32 Index {
 get { return index_; }
 set { SetIndex(value); }
 }
 public void SetIndex(Int32 value) { 
 hasIndex = true;
 index_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasStatus) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Status);
}
 if (HasIndex) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Index);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasStatus) {
output.WriteInt32(1, Status);
}
 
if (HasIndex) {
output.WriteInt32(2, Index);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_LOGIN_QUEUE_STATUS _inst = (GC_LOGIN_QUEUE_STATUS) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Status = input.ReadInt32();
break;
}
   case  16: {
 _inst.Index = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasStatus) return false;
 if (!hasIndex) return false;
 return true;
 }

}


[Serializable]
public class GC_RoleData : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int hpFieldNumber = 2;
 private bool hasHp;
 private Int32 hp_ = 0;
 public bool HasHp {
 get { return hasHp; }
 }
 public Int32 Hp {
 get { return hp_; }
 set { SetHp(value); }
 }
 public void SetHp(Int32 value) { 
 hasHp = true;
 hp_ = value;
 }

public const int accountNameFieldNumber = 3;
 private bool hasAccountName;
 private string accountName_ = "";
 public bool HasAccountName {
 get { return hasAccountName; }
 }
 public string AccountName {
 get { return accountName_; }
 set { SetAccountName(value); }
 }
 public void SetAccountName(string value) { 
 hasAccountName = true;
 accountName_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasHp) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Hp);
}
 if (HasAccountName) {
size += pb::CodedOutputStream.ComputeStringSize(3, AccountName);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasHp) {
output.WriteInt32(2, Hp);
}
 
if (HasAccountName) {
output.WriteString(3, AccountName);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_RoleData _inst = (GC_RoleData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Hp = input.ReadInt32();
break;
}
   case  26: {
 _inst.AccountName = input.ReadString();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasAccountName) return false;
 return true;
 }

}


[Serializable]
public class GC_BuildingData : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int typeFieldNumber = 2;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int slotFieldNumber = 3;
 private bool hasSlot;
 private Int32 slot_ = 0;
 public bool HasSlot {
 get { return hasSlot; }
 }
 public Int32 Slot {
 get { return slot_; }
 set { SetSlot(value); }
 }
 public void SetSlot(Int32 value) { 
 hasSlot = true;
 slot_ = value;
 }

public const int levelFieldNumber = 4;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Type);
}
 if (HasSlot) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Slot);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Level);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasType) {
output.WriteInt32(2, Type);
}
 
if (HasSlot) {
output.WriteInt32(3, Slot);
}
 
if (HasLevel) {
output.WriteInt32(4, Level);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_BuildingData _inst = (GC_BuildingData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Type = input.ReadInt32();
break;
}
   case  24: {
 _inst.Slot = input.ReadInt32();
break;
}
   case  32: {
 _inst.Level = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasType) return false;
 if (!hasSlot) return false;
 if (!hasLevel) return false;
 return true;
 }

}


[Serializable]
public class GC_CoolDownInfo : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int64 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int64 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int64 value) { 
 hasId = true;
 id_ = value;
 }

public const int cdtimeFieldNumber = 2;
 private bool hasCdtime;
 private Int32 cdtime_ = 0;
 public bool HasCdtime {
 get { return hasCdtime; }
 }
 public Int32 Cdtime {
 get { return cdtime_; }
 set { SetCdtime(value); }
 }
 public void SetCdtime(Int32 value) { 
 hasCdtime = true;
 cdtime_ = value;
 }

public const int elapsedFieldNumber = 3;
 private bool hasElapsed;
 private Int32 elapsed_ = 0;
 public bool HasElapsed {
 get { return hasElapsed; }
 }
 public Int32 Elapsed {
 get { return elapsed_; }
 set { SetElapsed(value); }
 }
 public void SetElapsed(Int32 value) { 
 hasElapsed = true;
 elapsed_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Id);
}
 if (HasCdtime) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Cdtime);
}
 if (HasElapsed) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Elapsed);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt64(1, Id);
}
 
if (HasCdtime) {
output.WriteInt32(2, Cdtime);
}
 
if (HasElapsed) {
output.WriteInt32(3, Elapsed);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_CoolDownInfo _inst = (GC_CoolDownInfo) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt64();
break;
}
   case  16: {
 _inst.Cdtime = input.ReadInt32();
break;
}
   case  24: {
 _inst.Elapsed = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 if (!hasCdtime) return false;
 if (!hasElapsed) return false;
 return true;
 }

}


[Serializable]
public class GC_HeroData : PacketDistributed
{

public const int guidFieldNumber = 1;
 private bool hasGuid;
 private Int64 guid_ = 0;
 public bool HasGuid {
 get { return hasGuid; }
 }
 public Int64 Guid {
 get { return guid_; }
 set { SetGuid(value); }
 }
 public void SetGuid(Int64 value) { 
 hasGuid = true;
 guid_ = value;
 }

public const int typeFieldNumber = 2;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int levelFieldNumber = 3;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int stateFieldNumber = 4;
 private bool hasState;
 private Int32 state_ = 0;
 public bool HasState {
 get { return hasState; }
 }
 public Int32 State {
 get { return state_; }
 set { SetState(value); }
 }
 public void SetState(Int32 value) { 
 hasState = true;
 state_ = value;
 }

public const int hpFieldNumber = 5;
 private bool hasHp;
 private Int32 hp_ = 0;
 public bool HasHp {
 get { return hasHp; }
 }
 public Int32 Hp {
 get { return hp_; }
 set { SetHp(value); }
 }
 public void SetHp(Int32 value) { 
 hasHp = true;
 hp_ = value;
 }

public const int mpFieldNumber = 6;
 private bool hasMp;
 private Int32 mp_ = 0;
 public bool HasMp {
 get { return hasMp; }
 }
 public Int32 Mp {
 get { return mp_; }
 set { SetMp(value); }
 }
 public void SetMp(Int32 value) { 
 hasMp = true;
 mp_ = value;
 }

public const int skillFieldNumber = 7;
 private pbc::PopsicleList<Int32> skill_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> skillList {
 get { return pbc::Lists.AsReadOnly(skill_); }
 }
 
 public int skillCount {
 get { return skill_.Count; }
 }
 
public Int32 GetSkill(int index) {
 return skill_[index];
 }
 public void AddSkill(Int32 value) {
 skill_.Add(value);
 }

public const int cooldownFieldNumber = 8;
 private pbc::PopsicleList<GC_CoolDownInfo> cooldown_ = new pbc::PopsicleList<GC_CoolDownInfo>();
 public scg::IList<GC_CoolDownInfo> cooldownList {
 get { return pbc::Lists.AsReadOnly(cooldown_); }
 }
 
 public int cooldownCount {
 get { return cooldown_.Count; }
 }
 
public GC_CoolDownInfo GetCooldown(int index) {
 return cooldown_[index];
 }
 public void AddCooldown(GC_CoolDownInfo value) {
 cooldown_.Add(value);
 }

public const int arrangeindexFieldNumber = 9;
 private bool hasArrangeindex;
 private Int32 arrangeindex_ = 0;
 public bool HasArrangeindex {
 get { return hasArrangeindex; }
 }
 public Int32 Arrangeindex {
 get { return arrangeindex_; }
 set { SetArrangeindex(value); }
 }
 public void SetArrangeindex(Int32 value) { 
 hasArrangeindex = true;
 arrangeindex_ = value;
 }

public const int marchIdFieldNumber = 10;
 private bool hasMarchId;
 private Int64 marchId_ = 0;
 public bool HasMarchId {
 get { return hasMarchId; }
 }
 public Int64 MarchId {
 get { return marchId_; }
 set { SetMarchId(value); }
 }
 public void SetMarchId(Int64 value) { 
 hasMarchId = true;
 marchId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasGuid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Guid);
}
 if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Type);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Level);
}
 if (HasState) {
size += pb::CodedOutputStream.ComputeInt32Size(4, State);
}
 if (HasHp) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Hp);
}
 if (HasMp) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Mp);
}
{
int dataSize = 0;
for(int i=0; i<skillList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(skillList[i]);
}
size += dataSize;
size += 1 * skill_.Count;
}
{
for(int i=0; i<cooldownList.Count; ++i){
int subsize = cooldownList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)8) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasArrangeindex) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Arrangeindex);
}
 if (HasMarchId) {
size += pb::CodedOutputStream.ComputeInt64Size(10, MarchId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasGuid) {
output.WriteInt64(1, Guid);
}
 
if (HasType) {
output.WriteInt32(2, Type);
}
 
if (HasLevel) {
output.WriteInt32(3, Level);
}
 
if (HasState) {
output.WriteInt32(4, State);
}
 
if (HasHp) {
output.WriteInt32(5, Hp);
}
 
if (HasMp) {
output.WriteInt32(6, Mp);
}
{
if (skill_.Count > 0) {
for(int i=0; i<skill_.Count; ++i){
output.WriteInt32(7,skill_[i]);
}
}

}

do{
for(int i=0; i<cooldownList.Count; ++i){
output.WriteTag((int)8, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)cooldownList[i].SerializedSize());
cooldownList[i].WriteTo(output);

}
}while(false);
 
if (HasArrangeindex) {
output.WriteInt32(9, Arrangeindex);
}
 
if (HasMarchId) {
output.WriteInt64(10, MarchId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_HeroData _inst = (GC_HeroData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Guid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Type = input.ReadInt32();
break;
}
   case  24: {
 _inst.Level = input.ReadInt32();
break;
}
   case  32: {
 _inst.State = input.ReadInt32();
break;
}
   case  40: {
 _inst.Hp = input.ReadInt32();
break;
}
   case  48: {
 _inst.Mp = input.ReadInt32();
break;
}
   case  56: {
 _inst.AddSkill(input.ReadInt32());
break;
}
    case  66: {
GC_CoolDownInfo subBuilder =  new GC_CoolDownInfo();
input.ReadMessage(subBuilder);
 _inst.AddCooldown(subBuilder);
break;
}
   case  72: {
 _inst.Arrangeindex = input.ReadInt32();
break;
}
   case  80: {
 _inst.MarchId = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasGuid) return false;
 if (!hasType) return false;
 if (!hasLevel) return false;
 if (!hasState) return false;
 if (!hasHp) return false;
 if (!hasMp) return false;
for(int i=0; i<cooldownList.Count; ++i) {
if (!cooldownList[i].IsInitialized()) return false;
}
 if (!hasArrangeindex) return false;
 if (!hasMarchId) return false;
 return true;
 }

}


[Serializable]
public class GC_TroopData : PacketDistributed
{

public const int typeFieldNumber = 1;
 private bool hasType;
 private Int32 type_ = 0;
 public bool HasType {
 get { return hasType; }
 }
 public Int32 Type {
 get { return type_; }
 set { SetType(value); }
 }
 public void SetType(Int32 value) { 
 hasType = true;
 type_ = value;
 }

public const int levelFieldNumber = 2;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int hpFieldNumber = 3;
 private bool hasHp;
 private Int32 hp_ = 0;
 public bool HasHp {
 get { return hasHp; }
 }
 public Int32 Hp {
 get { return hp_; }
 set { SetHp(value); }
 }
 public void SetHp(Int32 value) { 
 hasHp = true;
 hp_ = value;
 }

public const int mpFieldNumber = 4;
 private bool hasMp;
 private Int32 mp_ = 0;
 public bool HasMp {
 get { return hasMp; }
 }
 public Int32 Mp {
 get { return mp_; }
 set { SetMp(value); }
 }
 public void SetMp(Int32 value) { 
 hasMp = true;
 mp_ = value;
 }

public const int skillFieldNumber = 5;
 private pbc::PopsicleList<Int32> skill_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> skillList {
 get { return pbc::Lists.AsReadOnly(skill_); }
 }
 
 public int skillCount {
 get { return skill_.Count; }
 }
 
public Int32 GetSkill(int index) {
 return skill_[index];
 }
 public void AddSkill(Int32 value) {
 skill_.Add(value);
 }

public const int cooldownFieldNumber = 6;
 private pbc::PopsicleList<GC_CoolDownInfo> cooldown_ = new pbc::PopsicleList<GC_CoolDownInfo>();
 public scg::IList<GC_CoolDownInfo> cooldownList {
 get { return pbc::Lists.AsReadOnly(cooldown_); }
 }
 
 public int cooldownCount {
 get { return cooldown_.Count; }
 }
 
public GC_CoolDownInfo GetCooldown(int index) {
 return cooldown_[index];
 }
 public void AddCooldown(GC_CoolDownInfo value) {
 cooldown_.Add(value);
 }

public const int arrangeindexFieldNumber = 7;
 private bool hasArrangeindex;
 private Int32 arrangeindex_ = 0;
 public bool HasArrangeindex {
 get { return hasArrangeindex; }
 }
 public Int32 Arrangeindex {
 get { return arrangeindex_; }
 set { SetArrangeindex(value); }
 }
 public void SetArrangeindex(Int32 value) { 
 hasArrangeindex = true;
 arrangeindex_ = value;
 }

public const int marchidFieldNumber = 8;
 private bool hasMarchid;
 private Int64 marchid_ = 0;
 public bool HasMarchid {
 get { return hasMarchid; }
 }
 public Int64 Marchid {
 get { return marchid_; }
 set { SetMarchid(value); }
 }
 public void SetMarchid(Int64 value) { 
 hasMarchid = true;
 marchid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasType) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Type);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Level);
}
 if (HasHp) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Hp);
}
 if (HasMp) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Mp);
}
{
int dataSize = 0;
for(int i=0; i<skillList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(skillList[i]);
}
size += dataSize;
size += 1 * skill_.Count;
}
{
for(int i=0; i<cooldownList.Count; ++i){
int subsize = cooldownList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)6) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 if (HasArrangeindex) {
size += pb::CodedOutputStream.ComputeInt32Size(7, Arrangeindex);
}
 if (HasMarchid) {
size += pb::CodedOutputStream.ComputeInt64Size(8, Marchid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasType) {
output.WriteInt32(1, Type);
}
 
if (HasLevel) {
output.WriteInt32(2, Level);
}
 
if (HasHp) {
output.WriteInt32(3, Hp);
}
 
if (HasMp) {
output.WriteInt32(4, Mp);
}
{
if (skill_.Count > 0) {
for(int i=0; i<skill_.Count; ++i){
output.WriteInt32(5,skill_[i]);
}
}

}

do{
for(int i=0; i<cooldownList.Count; ++i){
output.WriteTag((int)6, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)cooldownList[i].SerializedSize());
cooldownList[i].WriteTo(output);

}
}while(false);
 
if (HasArrangeindex) {
output.WriteInt32(7, Arrangeindex);
}
 
if (HasMarchid) {
output.WriteInt64(8, Marchid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_TroopData _inst = (GC_TroopData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Type = input.ReadInt32();
break;
}
   case  16: {
 _inst.Level = input.ReadInt32();
break;
}
   case  24: {
 _inst.Hp = input.ReadInt32();
break;
}
   case  32: {
 _inst.Mp = input.ReadInt32();
break;
}
   case  40: {
 _inst.AddSkill(input.ReadInt32());
break;
}
    case  50: {
GC_CoolDownInfo subBuilder =  new GC_CoolDownInfo();
input.ReadMessage(subBuilder);
 _inst.AddCooldown(subBuilder);
break;
}
   case  56: {
 _inst.Arrangeindex = input.ReadInt32();
break;
}
   case  64: {
 _inst.Marchid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasType) return false;
 if (!hasLevel) return false;
 if (!hasHp) return false;
 if (!hasMp) return false;
for(int i=0; i<cooldownList.Count; ++i) {
if (!cooldownList[i].IsInitialized()) return false;
}
 if (!hasArrangeindex) return false;
 if (!hasMarchid) return false;
 return true;
 }

}


[Serializable]
public class GC_MarchData : PacketDistributed
{

public const int marchidFieldNumber = 1;
 private bool hasMarchid;
 private Int64 marchid_ = 0;
 public bool HasMarchid {
 get { return hasMarchid; }
 }
 public Int64 Marchid {
 get { return marchid_; }
 set { SetMarchid(value); }
 }
 public void SetMarchid(Int64 value) { 
 hasMarchid = true;
 marchid_ = value;
 }

public const int begintimeFieldNumber = 2;
 private bool hasBegintime;
 private Int32 begintime_ = 0;
 public bool HasBegintime {
 get { return hasBegintime; }
 }
 public Int32 Begintime {
 get { return begintime_; }
 set { SetBegintime(value); }
 }
 public void SetBegintime(Int32 value) { 
 hasBegintime = true;
 begintime_ = value;
 }

public const int playeridFieldNumber = 3;
 private bool hasPlayerid;
 private Int64 playerid_ = 0;
 public bool HasPlayerid {
 get { return hasPlayerid; }
 }
 public Int64 Playerid {
 get { return playerid_; }
 set { SetPlayerid(value); }
 }
 public void SetPlayerid(Int64 value) { 
 hasPlayerid = true;
 playerid_ = value;
 }

public const int cityIdFieldNumber = 4;
 private bool hasCityId;
 private Int64 cityId_ = 0;
 public bool HasCityId {
 get { return hasCityId; }
 }
 public Int64 CityId {
 get { return cityId_; }
 set { SetCityId(value); }
 }
 public void SetCityId(Int64 value) { 
 hasCityId = true;
 cityId_ = value;
 }

public const int statusFieldNumber = 5;
 private bool hasStatus;
 private Int32 status_ = 0;
 public bool HasStatus {
 get { return hasStatus; }
 }
 public Int32 Status {
 get { return status_; }
 set { SetStatus(value); }
 }
 public void SetStatus(Int32 value) { 
 hasStatus = true;
 status_ = value;
 }

public const int speedFieldNumber = 6;
 private bool hasSpeed;
 private Int32 speed_ = 0;
 public bool HasSpeed {
 get { return hasSpeed; }
 }
 public Int32 Speed {
 get { return speed_; }
 set { SetSpeed(value); }
 }
 public void SetSpeed(Int32 value) { 
 hasSpeed = true;
 speed_ = value;
 }

public const int fightidFieldNumber = 7;
 private bool hasFightid;
 private Int64 fightid_ = 0;
 public bool HasFightid {
 get { return hasFightid; }
 }
 public Int64 Fightid {
 get { return fightid_; }
 set { SetFightid(value); }
 }
 public void SetFightid(Int64 value) { 
 hasFightid = true;
 fightid_ = value;
 }

public const int buildidFieldNumber = 8;
 private bool hasBuildid;
 private Int64 buildid_ = 0;
 public bool HasBuildid {
 get { return hasBuildid; }
 }
 public Int64 Buildid {
 get { return buildid_; }
 set { SetBuildid(value); }
 }
 public void SetBuildid(Int64 value) { 
 hasBuildid = true;
 buildid_ = value;
 }

public const int heroFieldNumber = 9;
 private bool hasHero;
 private GC_HeroData hero_ =  new GC_HeroData();
 public bool HasHero {
 get { return hasHero; }
 }
 public GC_HeroData Hero {
 get { return hero_; }
 set { SetHero(value); }
 }
 public void SetHero(GC_HeroData value) { 
 hasHero = true;
 hero_ = value;
 }

public const int troopFieldNumber = 10;
 private pbc::PopsicleList<GC_TroopData> troop_ = new pbc::PopsicleList<GC_TroopData>();
 public scg::IList<GC_TroopData> troopList {
 get { return pbc::Lists.AsReadOnly(troop_); }
 }
 
 public int troopCount {
 get { return troop_.Count; }
 }
 
public GC_TroopData GetTroop(int index) {
 return troop_[index];
 }
 public void AddTroop(GC_TroopData value) {
 troop_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Marchid);
}
 if (HasBegintime) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Begintime);
}
 if (HasPlayerid) {
size += pb::CodedOutputStream.ComputeInt64Size(3, Playerid);
}
 if (HasCityId) {
size += pb::CodedOutputStream.ComputeInt64Size(4, CityId);
}
 if (HasStatus) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Status);
}
 if (HasSpeed) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Speed);
}
 if (HasFightid) {
size += pb::CodedOutputStream.ComputeInt64Size(7, Fightid);
}
 if (HasBuildid) {
size += pb::CodedOutputStream.ComputeInt64Size(8, Buildid);
}
{
int subsize = Hero.SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)9) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
{
for(int i=0; i<troopList.Count; ++i){
int subsize = troopList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)10) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchid) {
output.WriteInt64(1, Marchid);
}
 
if (HasBegintime) {
output.WriteInt32(2, Begintime);
}
 
if (HasPlayerid) {
output.WriteInt64(3, Playerid);
}
 
if (HasCityId) {
output.WriteInt64(4, CityId);
}
 
if (HasStatus) {
output.WriteInt32(5, Status);
}
 
if (HasSpeed) {
output.WriteInt32(6, Speed);
}
 
if (HasFightid) {
output.WriteInt64(7, Fightid);
}
 
if (HasBuildid) {
output.WriteInt64(8, Buildid);
}
{
output.WriteTag((int)9, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)Hero.SerializedSize());
Hero.WriteTo(output);

}

do{
for(int i=0; i<troopList.Count; ++i){
output.WriteTag((int)10, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)troopList[i].SerializedSize());
troopList[i].WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_MarchData _inst = (GC_MarchData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Marchid = input.ReadInt64();
break;
}
   case  16: {
 _inst.Begintime = input.ReadInt32();
break;
}
   case  24: {
 _inst.Playerid = input.ReadInt64();
break;
}
   case  32: {
 _inst.CityId = input.ReadInt64();
break;
}
   case  40: {
 _inst.Status = input.ReadInt32();
break;
}
   case  48: {
 _inst.Speed = input.ReadInt32();
break;
}
   case  56: {
 _inst.Fightid = input.ReadInt64();
break;
}
   case  64: {
 _inst.Buildid = input.ReadInt64();
break;
}
    case  74: {
GC_HeroData subBuilder =  new GC_HeroData();
 input.ReadMessage(subBuilder);
 _inst.Hero = subBuilder;
break;
}
    case  82: {
GC_TroopData subBuilder =  new GC_TroopData();
input.ReadMessage(subBuilder);
 _inst.AddTroop(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchid) return false;
 if (!hasBegintime) return false;
 if (!hasPlayerid) return false;
 if (!hasCityId) return false;
 if (!hasStatus) return false;
 if (!hasSpeed) return false;
  if (HasHero) {
if (!Hero.IsInitialized()) return false;
}
for(int i=0; i<troopList.Count; ++i) {
if (!troopList[i].IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class GC_TargetMarchData : PacketDistributed
{

public const int marchidFieldNumber = 1;
 private bool hasMarchid;
 private Int64 marchid_ = 0;
 public bool HasMarchid {
 get { return hasMarchid; }
 }
 public Int64 Marchid {
 get { return marchid_; }
 set { SetMarchid(value); }
 }
 public void SetMarchid(Int64 value) { 
 hasMarchid = true;
 marchid_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasMarchid) {
size += pb::CodedOutputStream.ComputeInt64Size(1, Marchid);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasMarchid) {
output.WriteInt64(1, Marchid);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_TargetMarchData _inst = (GC_TargetMarchData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Marchid = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasMarchid) return false;
 return true;
 }

}


[Serializable]
public class GC_CityData : PacketDistributed
{

public const int tileIdFieldNumber = 1;
 private bool hasTileId;
 private Int32 tileId_ = 0;
 public bool HasTileId {
 get { return hasTileId; }
 }
 public Int32 TileId {
 get { return tileId_; }
 set { SetTileId(value); }
 }
 public void SetTileId(Int32 value) { 
 hasTileId = true;
 tileId_ = value;
 }

public const int cityIdFieldNumber = 2;
 private bool hasCityId;
 private Int64 cityId_ = 0;
 public bool HasCityId {
 get { return hasCityId; }
 }
 public Int64 CityId {
 get { return cityId_; }
 set { SetCityId(value); }
 }
 public void SetCityId(Int64 value) { 
 hasCityId = true;
 cityId_ = value;
 }

public const int levelFieldNumber = 3;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int foodFieldNumber = 4;
 private bool hasFood;
 private Int64 food_ = 0;
 public bool HasFood {
 get { return hasFood; }
 }
 public Int64 Food {
 get { return food_; }
 set { SetFood(value); }
 }
 public void SetFood(Int64 value) { 
 hasFood = true;
 food_ = value;
 }

public const int stoneFieldNumber = 5;
 private bool hasStone;
 private Int64 stone_ = 0;
 public bool HasStone {
 get { return hasStone; }
 }
 public Int64 Stone {
 get { return stone_; }
 set { SetStone(value); }
 }
 public void SetStone(Int64 value) { 
 hasStone = true;
 stone_ = value;
 }

public const int ironFieldNumber = 6;
 private bool hasIron;
 private Int64 iron_ = 0;
 public bool HasIron {
 get { return hasIron; }
 }
 public Int64 Iron {
 get { return iron_; }
 set { SetIron(value); }
 }
 public void SetIron(Int64 value) { 
 hasIron = true;
 iron_ = value;
 }

public const int buildlistFieldNumber = 7;
 private pbc::PopsicleList<GC_BuildingData> buildlist_ = new pbc::PopsicleList<GC_BuildingData>();
 public scg::IList<GC_BuildingData> buildlistList {
 get { return pbc::Lists.AsReadOnly(buildlist_); }
 }
 
 public int buildlistCount {
 get { return buildlist_.Count; }
 }
 
public GC_BuildingData GetBuildlist(int index) {
 return buildlist_[index];
 }
 public void AddBuildlist(GC_BuildingData value) {
 buildlist_.Add(value);
 }

public const int trooplistFieldNumber = 8;
 private pbc::PopsicleList<GC_TroopData> trooplist_ = new pbc::PopsicleList<GC_TroopData>();
 public scg::IList<GC_TroopData> trooplistList {
 get { return pbc::Lists.AsReadOnly(trooplist_); }
 }
 
 public int trooplistCount {
 get { return trooplist_.Count; }
 }
 
public GC_TroopData GetTrooplist(int index) {
 return trooplist_[index];
 }
 public void AddTrooplist(GC_TroopData value) {
 trooplist_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasTileId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, TileId);
}
 if (HasCityId) {
size += pb::CodedOutputStream.ComputeInt64Size(2, CityId);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(3, Level);
}
 if (HasFood) {
size += pb::CodedOutputStream.ComputeInt64Size(4, Food);
}
 if (HasStone) {
size += pb::CodedOutputStream.ComputeInt64Size(5, Stone);
}
 if (HasIron) {
size += pb::CodedOutputStream.ComputeInt64Size(6, Iron);
}
{
for(int i=0; i<buildlistList.Count; ++i){
int subsize = buildlistList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)7) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
{
for(int i=0; i<trooplistList.Count; ++i){
int subsize = trooplistList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)8) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasTileId) {
output.WriteInt32(1, TileId);
}
 
if (HasCityId) {
output.WriteInt64(2, CityId);
}
 
if (HasLevel) {
output.WriteInt32(3, Level);
}
 
if (HasFood) {
output.WriteInt64(4, Food);
}
 
if (HasStone) {
output.WriteInt64(5, Stone);
}
 
if (HasIron) {
output.WriteInt64(6, Iron);
}

do{
for(int i=0; i<buildlistList.Count; ++i){
output.WriteTag((int)7, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)buildlistList[i].SerializedSize());
buildlistList[i].WriteTo(output);

}
}while(false);

do{
for(int i=0; i<trooplistList.Count; ++i){
output.WriteTag((int)8, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)trooplistList[i].SerializedSize());
trooplistList[i].WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_CityData _inst = (GC_CityData) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.TileId = input.ReadInt32();
break;
}
   case  16: {
 _inst.CityId = input.ReadInt64();
break;
}
   case  24: {
 _inst.Level = input.ReadInt32();
break;
}
   case  32: {
 _inst.Food = input.ReadInt64();
break;
}
   case  40: {
 _inst.Stone = input.ReadInt64();
break;
}
   case  48: {
 _inst.Iron = input.ReadInt64();
break;
}
    case  58: {
GC_BuildingData subBuilder =  new GC_BuildingData();
input.ReadMessage(subBuilder);
 _inst.AddBuildlist(subBuilder);
break;
}
    case  66: {
GC_TroopData subBuilder =  new GC_TroopData();
input.ReadMessage(subBuilder);
 _inst.AddTrooplist(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasTileId) return false;
 if (!hasCityId) return false;
 if (!hasLevel) return false;
 if (!hasFood) return false;
 if (!hasStone) return false;
 if (!hasIron) return false;
for(int i=0; i<buildlistList.Count; ++i) {
if (!buildlistList[i].IsInitialized()) return false;
}
for(int i=0; i<trooplistList.Count; ++i) {
if (!trooplistList[i].IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class GC_HeroList : PacketDistributed
{

public const int heroListFieldNumber = 1;
 private pbc::PopsicleList<GC_HeroData> heroList_ = new pbc::PopsicleList<GC_HeroData>();
 public scg::IList<GC_HeroData> heroListList {
 get { return pbc::Lists.AsReadOnly(heroList_); }
 }
 
 public int heroListCount {
 get { return heroList_.Count; }
 }
 
public GC_HeroData GetHeroList(int index) {
 return heroList_[index];
 }
 public void AddHeroList(GC_HeroData value) {
 heroList_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
for(int i=0; i<heroListList.Count; ++i){
int subsize = heroListList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
for(int i=0; i<heroListList.Count; ++i){
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)heroListList[i].SerializedSize());
heroListList[i].WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_HeroList _inst = (GC_HeroList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
GC_HeroData subBuilder =  new GC_HeroData();
input.ReadMessage(subBuilder);
 _inst.AddHeroList(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 for(int i=0; i<heroListList.Count; ++i) {
if (!heroListList[i].IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class GC_MarchList : PacketDistributed
{

public const int marchlistFieldNumber = 1;
 private pbc::PopsicleList<GC_MarchData> marchlist_ = new pbc::PopsicleList<GC_MarchData>();
 public scg::IList<GC_MarchData> marchlistList {
 get { return pbc::Lists.AsReadOnly(marchlist_); }
 }
 
 public int marchlistCount {
 get { return marchlist_.Count; }
 }
 
public GC_MarchData GetMarchlist(int index) {
 return marchlist_[index];
 }
 public void AddMarchlist(GC_MarchData value) {
 marchlist_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
 {
for(int i=0; i<marchlistList.Count; ++i){
int subsize = marchlistList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)1) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
 
do{
for(int i=0; i<marchlistList.Count; ++i){
output.WriteTag((int)1, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)marchlistList[i].SerializedSize());
marchlistList[i].WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_MarchList _inst = (GC_MarchList) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
     case  10: {
GC_MarchData subBuilder =  new GC_MarchData();
input.ReadMessage(subBuilder);
 _inst.AddMarchlist(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
 for(int i=0; i<marchlistList.Count; ++i) {
if (!marchlistList[i].IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class CG_BATTLEINFOR : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_BATTLEINFOR _inst = (CG_BATTLEINFOR) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_OBJINFOR : PacketDistributed
{

public const int idFieldNumber = 1;
 private bool hasId;
 private Int32 id_ = 0;
 public bool HasId {
 get { return hasId; }
 }
 public Int32 Id {
 get { return id_; }
 set { SetId(value); }
 }
 public void SetId(Int32 value) { 
 hasId = true;
 id_ = value;
 }

public const int unitDataIdFieldNumber = 2;
 private bool hasUnitDataId;
 private Int32 unitDataId_ = 0;
 public bool HasUnitDataId {
 get { return hasUnitDataId; }
 }
 public Int32 UnitDataId {
 get { return unitDataId_; }
 set { SetUnitDataId(value); }
 }
 public void SetUnitDataId(Int32 value) { 
 hasUnitDataId = true;
 unitDataId_ = value;
 }

public const int skilldataidFieldNumber = 3;
 private pbc::PopsicleList<Int32> skilldataid_ = new pbc::PopsicleList<Int32>();
 public scg::IList<Int32> skilldataidList {
 get { return pbc::Lists.AsReadOnly(skilldataid_); }
 }
 
 public int skilldataidCount {
 get { return skilldataid_.Count; }
 }
 
public Int32 GetSkilldataid(int index) {
 return skilldataid_[index];
 }
 public void AddSkilldataid(Int32 value) {
 skilldataid_.Add(value);
 }

public const int campFieldNumber = 4;
 private bool hasCamp;
 private Int32 camp_ = 0;
 public bool HasCamp {
 get { return hasCamp; }
 }
 public Int32 Camp {
 get { return camp_; }
 set { SetCamp(value); }
 }
 public void SetCamp(Int32 value) { 
 hasCamp = true;
 camp_ = value;
 }

public const int unitcountFieldNumber = 5;
 private bool hasUnitcount;
 private Int32 unitcount_ = 0;
 public bool HasUnitcount {
 get { return hasUnitcount; }
 }
 public Int32 Unitcount {
 get { return unitcount_; }
 set { SetUnitcount(value); }
 }
 public void SetUnitcount(Int32 value) { 
 hasUnitcount = true;
 unitcount_ = value;
 }

public const int hpFieldNumber = 6;
 private bool hasHp;
 private Int32 hp_ = 0;
 public bool HasHp {
 get { return hasHp; }
 }
 public Int32 Hp {
 get { return hp_; }
 set { SetHp(value); }
 }
 public void SetHp(Int32 value) { 
 hasHp = true;
 hp_ = value;
 }

public const int maxhpFieldNumber = 7;
 private bool hasMaxhp;
 private Int32 maxhp_ = 0;
 public bool HasMaxhp {
 get { return hasMaxhp; }
 }
 public Int32 Maxhp {
 get { return maxhp_; }
 set { SetMaxhp(value); }
 }
 public void SetMaxhp(Int32 value) { 
 hasMaxhp = true;
 maxhp_ = value;
 }

public const int attackFieldNumber = 8;
 private bool hasAttack;
 private Int32 attack_ = 0;
 public bool HasAttack {
 get { return hasAttack; }
 }
 public Int32 Attack {
 get { return attack_; }
 set { SetAttack(value); }
 }
 public void SetAttack(Int32 value) { 
 hasAttack = true;
 attack_ = value;
 }

public const int defenceFieldNumber = 9;
 private bool hasDefence;
 private Int32 defence_ = 0;
 public bool HasDefence {
 get { return hasDefence; }
 }
 public Int32 Defence {
 get { return defence_; }
 set { SetDefence(value); }
 }
 public void SetDefence(Int32 value) { 
 hasDefence = true;
 defence_ = value;
 }

public const int spFieldNumber = 10;
 private bool hasSp;
 private Int32 sp_ = 0;
 public bool HasSp {
 get { return hasSp; }
 }
 public Int32 Sp {
 get { return sp_; }
 set { SetSp(value); }
 }
 public void SetSp(Int32 value) { 
 hasSp = true;
 sp_ = value;
 }

public const int levelFieldNumber = 11;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

public const int posxFieldNumber = 12;
 private bool hasPosx;
 private Int32 posx_ = 0;
 public bool HasPosx {
 get { return hasPosx; }
 }
 public Int32 Posx {
 get { return posx_; }
 set { SetPosx(value); }
 }
 public void SetPosx(Int32 value) { 
 hasPosx = true;
 posx_ = value;
 }

public const int poszFieldNumber = 13;
 private bool hasPosz;
 private Int32 posz_ = 0;
 public bool HasPosz {
 get { return hasPosz; }
 }
 public Int32 Posz {
 get { return posz_; }
 set { SetPosz(value); }
 }
 public void SetPosz(Int32 value) { 
 hasPosz = true;
 posz_ = value;
 }

public const int arrangeindexFieldNumber = 14;
 private bool hasArrangeindex;
 private Int32 arrangeindex_ = 0;
 public bool HasArrangeindex {
 get { return hasArrangeindex; }
 }
 public Int32 Arrangeindex {
 get { return arrangeindex_; }
 set { SetArrangeindex(value); }
 }
 public void SetArrangeindex(Int32 value) { 
 hasArrangeindex = true;
 arrangeindex_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, Id);
}
 if (HasUnitDataId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, UnitDataId);
}
{
int dataSize = 0;
for(int i=0; i<skilldataidList.Count; ++i){
dataSize += pb::CodedOutputStream.ComputeInt32SizeNoTag(skilldataidList[i]);
}
size += dataSize;
size += 1 * skilldataid_.Count;
}
 if (HasCamp) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Camp);
}
 if (HasUnitcount) {
size += pb::CodedOutputStream.ComputeInt32Size(5, Unitcount);
}
 if (HasHp) {
size += pb::CodedOutputStream.ComputeInt32Size(6, Hp);
}
 if (HasMaxhp) {
size += pb::CodedOutputStream.ComputeInt32Size(7, Maxhp);
}
 if (HasAttack) {
size += pb::CodedOutputStream.ComputeInt32Size(8, Attack);
}
 if (HasDefence) {
size += pb::CodedOutputStream.ComputeInt32Size(9, Defence);
}
 if (HasSp) {
size += pb::CodedOutputStream.ComputeInt32Size(10, Sp);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(11, Level);
}
 if (HasPosx) {
size += pb::CodedOutputStream.ComputeInt32Size(12, Posx);
}
 if (HasPosz) {
size += pb::CodedOutputStream.ComputeInt32Size(13, Posz);
}
 if (HasArrangeindex) {
size += pb::CodedOutputStream.ComputeInt32Size(14, Arrangeindex);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasId) {
output.WriteInt32(1, Id);
}
 
if (HasUnitDataId) {
output.WriteInt32(2, UnitDataId);
}
{
if (skilldataid_.Count > 0) {
for(int i=0; i<skilldataid_.Count; ++i){
output.WriteInt32(3,skilldataid_[i]);
}
}

}
 
if (HasCamp) {
output.WriteInt32(4, Camp);
}
 
if (HasUnitcount) {
output.WriteInt32(5, Unitcount);
}
 
if (HasHp) {
output.WriteInt32(6, Hp);
}
 
if (HasMaxhp) {
output.WriteInt32(7, Maxhp);
}
 
if (HasAttack) {
output.WriteInt32(8, Attack);
}
 
if (HasDefence) {
output.WriteInt32(9, Defence);
}
 
if (HasSp) {
output.WriteInt32(10, Sp);
}
 
if (HasLevel) {
output.WriteInt32(11, Level);
}
 
if (HasPosx) {
output.WriteInt32(12, Posx);
}
 
if (HasPosz) {
output.WriteInt32(13, Posz);
}
 
if (HasArrangeindex) {
output.WriteInt32(14, Arrangeindex);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_OBJINFOR _inst = (GC_OBJINFOR) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.Id = input.ReadInt32();
break;
}
   case  16: {
 _inst.UnitDataId = input.ReadInt32();
break;
}
   case  24: {
 _inst.AddSkilldataid(input.ReadInt32());
break;
}
   case  32: {
 _inst.Camp = input.ReadInt32();
break;
}
   case  40: {
 _inst.Unitcount = input.ReadInt32();
break;
}
   case  48: {
 _inst.Hp = input.ReadInt32();
break;
}
   case  56: {
 _inst.Maxhp = input.ReadInt32();
break;
}
   case  64: {
 _inst.Attack = input.ReadInt32();
break;
}
   case  72: {
 _inst.Defence = input.ReadInt32();
break;
}
   case  80: {
 _inst.Sp = input.ReadInt32();
break;
}
   case  88: {
 _inst.Level = input.ReadInt32();
break;
}
   case  96: {
 _inst.Posx = input.ReadInt32();
break;
}
   case  104: {
 _inst.Posz = input.ReadInt32();
break;
}
   case  112: {
 _inst.Arrangeindex = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasId) return false;
 if (!hasUnitDataId) return false;
 if (!hasCamp) return false;
 if (!hasUnitcount) return false;
 if (!hasHp) return false;
 if (!hasMaxhp) return false;
 if (!hasAttack) return false;
 if (!hasDefence) return false;
 if (!hasSp) return false;
 if (!hasLevel) return false;
 if (!hasPosx) return false;
 if (!hasPosz) return false;
 if (!hasArrangeindex) return false;
 return true;
 }

}


[Serializable]
public class GC_BATTLEINFOR : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int objListFieldNumber = 2;
 private pbc::PopsicleList<GC_OBJINFOR> objList_ = new pbc::PopsicleList<GC_OBJINFOR>();
 public scg::IList<GC_OBJINFOR> objListList {
 get { return pbc::Lists.AsReadOnly(objList_); }
 }
 
 public int objListCount {
 get { return objList_.Count; }
 }
 
public GC_OBJINFOR GetObjList(int index) {
 return objList_[index];
 }
 public void AddObjList(GC_OBJINFOR value) {
 objList_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
{
for(int i=0; i<objListList.Count; ++i){
int subsize = objListList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}

do{
for(int i=0; i<objListList.Count; ++i){
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)objListList[i].SerializedSize());
objListList[i].WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_BATTLEINFOR _inst = (GC_BATTLEINFOR) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
    case  18: {
GC_OBJINFOR subBuilder =  new GC_OBJINFOR();
input.ReadMessage(subBuilder);
 _inst.AddObjList(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
for(int i=0; i<objListList.Count; ++i) {
if (!objListList[i].IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class GC_OBJPOS : PacketDistributed
{

public const int objIdFieldNumber = 1;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int posXFieldNumber = 2;
 private bool hasPosX;
 private Int32 posX_ = 0;
 public bool HasPosX {
 get { return hasPosX; }
 }
 public Int32 PosX {
 get { return posX_; }
 set { SetPosX(value); }
 }
 public void SetPosX(Int32 value) { 
 hasPosX = true;
 posX_ = value;
 }

public const int posZFieldNumber = 3;
 private bool hasPosZ;
 private Int32 posZ_ = 0;
 public bool HasPosZ {
 get { return hasPosZ; }
 }
 public Int32 PosZ {
 get { return posZ_; }
 set { SetPosZ(value); }
 }
 public void SetPosZ(Int32 value) { 
 hasPosZ = true;
 posZ_ = value;
 }

public const int hpFieldNumber = 4;
 private bool hasHp;
 private Int32 hp_ = 0;
 public bool HasHp {
 get { return hasHp; }
 }
 public Int32 Hp {
 get { return hp_; }
 set { SetHp(value); }
 }
 public void SetHp(Int32 value) { 
 hasHp = true;
 hp_ = value;
 }

public const int targetIdFieldNumber = 5;
 private bool hasTargetId;
 private Int32 targetId_ = 0;
 public bool HasTargetId {
 get { return hasTargetId; }
 }
 public Int32 TargetId {
 get { return targetId_; }
 set { SetTargetId(value); }
 }
 public void SetTargetId(Int32 value) { 
 hasTargetId = true;
 targetId_ = value;
 }

public const int objStateFieldNumber = 6;
 private bool hasObjState;
 private Int32 objState_ = 0;
 public bool HasObjState {
 get { return hasObjState; }
 }
 public Int32 ObjState {
 get { return objState_; }
 set { SetObjState(value); }
 }
 public void SetObjState(Int32 value) { 
 hasObjState = true;
 objState_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, ObjId);
}
 if (HasPosX) {
size += pb::CodedOutputStream.ComputeInt32Size(2, PosX);
}
 if (HasPosZ) {
size += pb::CodedOutputStream.ComputeInt32Size(3, PosZ);
}
 if (HasHp) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Hp);
}
 if (HasTargetId) {
size += pb::CodedOutputStream.ComputeInt32Size(5, TargetId);
}
 if (HasObjState) {
size += pb::CodedOutputStream.ComputeInt32Size(6, ObjState);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasObjId) {
output.WriteInt32(1, ObjId);
}
 
if (HasPosX) {
output.WriteInt32(2, PosX);
}
 
if (HasPosZ) {
output.WriteInt32(3, PosZ);
}
 
if (HasHp) {
output.WriteInt32(4, Hp);
}
 
if (HasTargetId) {
output.WriteInt32(5, TargetId);
}
 
if (HasObjState) {
output.WriteInt32(6, ObjState);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_OBJPOS _inst = (GC_OBJPOS) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  16: {
 _inst.PosX = input.ReadInt32();
break;
}
   case  24: {
 _inst.PosZ = input.ReadInt32();
break;
}
   case  32: {
 _inst.Hp = input.ReadInt32();
break;
}
   case  40: {
 _inst.TargetId = input.ReadInt32();
break;
}
   case  48: {
 _inst.ObjState = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasObjId) return false;
 if (!hasPosX) return false;
 if (!hasPosZ) return false;
 if (!hasHp) return false;
 if (!hasTargetId) return false;
 if (!hasObjState) return false;
 return true;
 }

}


[Serializable]
public class GC_OBJPOSLIST : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int objPosListFieldNumber = 2;
 private pbc::PopsicleList<GC_OBJPOS> objPosList_ = new pbc::PopsicleList<GC_OBJPOS>();
 public scg::IList<GC_OBJPOS> objPosListList {
 get { return pbc::Lists.AsReadOnly(objPosList_); }
 }
 
 public int objPosListCount {
 get { return objPosList_.Count; }
 }
 
public GC_OBJPOS GetObjPosList(int index) {
 return objPosList_[index];
 }
 public void AddObjPosList(GC_OBJPOS value) {
 objPosList_.Add(value);
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
{
for(int i=0; i<objPosListList.Count; ++i){
int subsize = objPosListList[i].SerializedSize();	
size += pb::CodedOutputStream.ComputeTagSize((int)2) + pb::CodedOutputStream.ComputeRawVarint32Size((uint)subsize) + subsize;
}
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}

do{
for(int i=0; i<objPosListList.Count; ++i){
output.WriteTag((int)2, pb::WireFormat.WireType.LengthDelimited);
output.WriteRawVarint32((uint)objPosListList[i].SerializedSize());
objPosListList[i].WriteTo(output);

}
}while(false);
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_OBJPOSLIST _inst = (GC_OBJPOSLIST) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
    case  18: {
GC_OBJPOS subBuilder =  new GC_OBJPOS();
input.ReadMessage(subBuilder);
 _inst.AddObjPosList(subBuilder);
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
for(int i=0; i<objPosListList.Count; ++i) {
if (!objPosListList[i].IsInitialized()) return false;
}
 return true;
 }

}


[Serializable]
public class CG_OBJPOSLIST : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_OBJPOSLIST _inst = (CG_OBJPOSLIST) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 return true;
 }

}


[Serializable]
public class GC_OBJCOMMANDPURSUE : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int objIdFieldNumber = 2;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int aimObjIdFieldNumber = 3;
 private bool hasAimObjId;
 private Int32 aimObjId_ = 0;
 public bool HasAimObjId {
 get { return hasAimObjId; }
 }
 public Int32 AimObjId {
 get { return aimObjId_; }
 set { SetAimObjId(value); }
 }
 public void SetAimObjId(Int32 value) { 
 hasAimObjId = true;
 aimObjId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, ObjId);
}
 if (HasAimObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, AimObjId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 
if (HasObjId) {
output.WriteInt32(2, ObjId);
}
 
if (HasAimObjId) {
output.WriteInt32(3, AimObjId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_OBJCOMMANDPURSUE _inst = (GC_OBJCOMMANDPURSUE) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
   case  16: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  24: {
 _inst.AimObjId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 if (!hasObjId) return false;
 if (!hasAimObjId) return false;
 return true;
 }

}


[Serializable]
public class GC_OBJPREPAREFORATTACK : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int objIdFieldNumber = 2;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int aimObjIdFieldNumber = 3;
 private bool hasAimObjId;
 private Int32 aimObjId_ = 0;
 public bool HasAimObjId {
 get { return hasAimObjId; }
 }
 public Int32 AimObjId {
 get { return aimObjId_; }
 set { SetAimObjId(value); }
 }
 public void SetAimObjId(Int32 value) { 
 hasAimObjId = true;
 aimObjId_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, ObjId);
}
 if (HasAimObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, AimObjId);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 
if (HasObjId) {
output.WriteInt32(2, ObjId);
}
 
if (HasAimObjId) {
output.WriteInt32(3, AimObjId);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_OBJPREPAREFORATTACK _inst = (GC_OBJPREPAREFORATTACK) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
   case  16: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  24: {
 _inst.AimObjId = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 if (!hasObjId) return false;
 if (!hasAimObjId) return false;
 return true;
 }

}


[Serializable]
public class GC_OBJGETHURT : PacketDistributed
{

public const int sceneIdFieldNumber = 1;
 private bool hasSceneId;
 private Int32 sceneId_ = 0;
 public bool HasSceneId {
 get { return hasSceneId; }
 }
 public Int32 SceneId {
 get { return sceneId_; }
 set { SetSceneId(value); }
 }
 public void SetSceneId(Int32 value) { 
 hasSceneId = true;
 sceneId_ = value;
 }

public const int objIdFieldNumber = 2;
 private bool hasObjId;
 private Int32 objId_ = 0;
 public bool HasObjId {
 get { return hasObjId; }
 }
 public Int32 ObjId {
 get { return objId_; }
 set { SetObjId(value); }
 }
 public void SetObjId(Int32 value) { 
 hasObjId = true;
 objId_ = value;
 }

public const int attackObjIdFieldNumber = 3;
 private bool hasAttackObjId;
 private Int32 attackObjId_ = 0;
 public bool HasAttackObjId {
 get { return hasAttackObjId; }
 }
 public Int32 AttackObjId {
 get { return attackObjId_; }
 set { SetAttackObjId(value); }
 }
 public void SetAttackObjId(Int32 value) { 
 hasAttackObjId = true;
 attackObjId_ = value;
 }

public const int damageFieldNumber = 4;
 private bool hasDamage;
 private Int32 damage_ = 0;
 public bool HasDamage {
 get { return hasDamage; }
 }
 public Int32 Damage {
 get { return damage_; }
 set { SetDamage(value); }
 }
 public void SetDamage(Int32 value) { 
 hasDamage = true;
 damage_ = value;
 }

public const int objDeadFieldNumber = 5;
 private bool hasObjDead;
 private Int32 objDead_ = 0;
 public bool HasObjDead {
 get { return hasObjDead; }
 }
 public Int32 ObjDead {
 get { return objDead_; }
 set { SetObjDead(value); }
 }
 public void SetObjDead(Int32 value) { 
 hasObjDead = true;
 objDead_ = value;
 }

public const int deathNumberFieldNumber = 6;
 private bool hasDeathNumber;
 private Int32 deathNumber_ = 0;
 public bool HasDeathNumber {
 get { return hasDeathNumber; }
 }
 public Int32 DeathNumber {
 get { return deathNumber_; }
 set { SetDeathNumber(value); }
 }
 public void SetDeathNumber(Int32 value) { 
 hasDeathNumber = true;
 deathNumber_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasSceneId) {
size += pb::CodedOutputStream.ComputeInt32Size(1, SceneId);
}
 if (HasObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(2, ObjId);
}
 if (HasAttackObjId) {
size += pb::CodedOutputStream.ComputeInt32Size(3, AttackObjId);
}
 if (HasDamage) {
size += pb::CodedOutputStream.ComputeInt32Size(4, Damage);
}
 if (HasObjDead) {
size += pb::CodedOutputStream.ComputeInt32Size(5, ObjDead);
}
 if (HasDeathNumber) {
size += pb::CodedOutputStream.ComputeInt32Size(6, DeathNumber);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasSceneId) {
output.WriteInt32(1, SceneId);
}
 
if (HasObjId) {
output.WriteInt32(2, ObjId);
}
 
if (HasAttackObjId) {
output.WriteInt32(3, AttackObjId);
}
 
if (HasDamage) {
output.WriteInt32(4, Damage);
}
 
if (HasObjDead) {
output.WriteInt32(5, ObjDead);
}
 
if (HasDeathNumber) {
output.WriteInt32(6, DeathNumber);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_OBJGETHURT _inst = (GC_OBJGETHURT) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.SceneId = input.ReadInt32();
break;
}
   case  16: {
 _inst.ObjId = input.ReadInt32();
break;
}
   case  24: {
 _inst.AttackObjId = input.ReadInt32();
break;
}
   case  32: {
 _inst.Damage = input.ReadInt32();
break;
}
   case  40: {
 _inst.ObjDead = input.ReadInt32();
break;
}
   case  48: {
 _inst.DeathNumber = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasSceneId) return false;
 if (!hasObjId) return false;
 if (!hasAttackObjId) return false;
 if (!hasDamage) return false;
 if (!hasObjDead) return false;
 if (!hasDeathNumber) return false;
 return true;
 }

}


[Serializable]
public class CG_Building_LevelUp : PacketDistributed
{

public const int buildingIDFieldNumber = 1;
 private bool hasBuildingID;
 private Int64 buildingID_ = 0;
 public bool HasBuildingID {
 get { return hasBuildingID; }
 }
 public Int64 BuildingID {
 get { return buildingID_; }
 set { SetBuildingID(value); }
 }
 public void SetBuildingID(Int64 value) { 
 hasBuildingID = true;
 buildingID_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBuildingID) {
size += pb::CodedOutputStream.ComputeInt64Size(1, BuildingID);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBuildingID) {
output.WriteInt64(1, BuildingID);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 CG_Building_LevelUp _inst = (CG_Building_LevelUp) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.BuildingID = input.ReadInt64();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBuildingID) return false;
 return true;
 }

}


[Serializable]
public class GC_Building_LevelUp : PacketDistributed
{

public const int buildingIDFieldNumber = 1;
 private bool hasBuildingID;
 private Int64 buildingID_ = 0;
 public bool HasBuildingID {
 get { return hasBuildingID; }
 }
 public Int64 BuildingID {
 get { return buildingID_; }
 set { SetBuildingID(value); }
 }
 public void SetBuildingID(Int64 value) { 
 hasBuildingID = true;
 buildingID_ = value;
 }

public const int levelFieldNumber = 2;
 private bool hasLevel;
 private Int32 level_ = 0;
 public bool HasLevel {
 get { return hasLevel; }
 }
 public Int32 Level {
 get { return level_; }
 set { SetLevel(value); }
 }
 public void SetLevel(Int32 value) { 
 hasLevel = true;
 level_ = value;
 }

 private int memoizedSerializedSize = -1;
 public override int SerializedSize()
 {
 int size = memoizedSerializedSize;
 if (size != -1) return size;
 size = 0;
  if (HasBuildingID) {
size += pb::CodedOutputStream.ComputeInt64Size(1, BuildingID);
}
 if (HasLevel) {
size += pb::CodedOutputStream.ComputeInt32Size(2, Level);
}
 memoizedSerializedSize = size;
 return size;
 }

public override void WriteTo(pb::CodedOutputStream output)
 {
 int size = SerializedSize();
  
if (HasBuildingID) {
output.WriteInt64(1, BuildingID);
}
 
if (HasLevel) {
output.WriteInt32(2, Level);
}
 }
public override PacketDistributed MergeFrom(pb::CodedInputStream input,PacketDistributed _base) {
 GC_Building_LevelUp _inst = (GC_Building_LevelUp) _base;
 while (true) {
 uint tag = input.ReadTag();
 switch (tag) {
 case 0:
 {
 return _inst;
 }
    case  8: {
 _inst.BuildingID = input.ReadInt64();
break;
}
   case  16: {
 _inst.Level = input.ReadInt32();
break;
}

 }
 }
 return _inst;
 }
//end merged
public override bool IsInitialized() {
  if (!hasBuildingID) return false;
 if (!hasLevel) return false;
 return true;
 }

}

