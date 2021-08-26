using System;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGDatabase;

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

[AddComponentMenu("BansheeGz/Generated/ObjData")]
public partial class ObjData : BGEntityGo
{
	public override BGMetaEntity MetaConstraint
	{
		get
		{
			return MetaDefault;
		}
	}
	private static BansheeGz.BGDatabase.BGMetaRow _metaDefault;
	public static BansheeGz.BGDatabase.BGMetaRow MetaDefault
	{
		get
		{
			if(_metaDefault==null || _metaDefault.IsDeleted) _metaDefault=BGRepo.I.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5325377636895802418,15441573546148841095));
			return _metaDefault;
		}
	}
	public static BansheeGz.BGDatabase.BGRepoEvents Events
	{
		get
		{
			return BGRepo.I.Events;
		}
	}
	public new System.String name
	{
		get
		{
			return _name[Entity.Index];
		}
		set
		{
			_name[Entity.Index] = value;
		}
	}
	public System.Int32 ID
	{
		get
		{
			return _ID[Entity.Index];
		}
		set
		{
			_ID[Entity.Index] = value;
		}
	}
	public System.Boolean IsNpc
	{
		get
		{
			return _IsNpc[Entity.Index];
		}
		set
		{
			_IsNpc[Entity.Index] = value;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldEntityName __name;
	public static BansheeGz.BGDatabase.BGFieldEntityName _name
	{
		get
		{
			if(__name==null || __name.IsDeleted) __name=(BansheeGz.BGDatabase.BGFieldEntityName) MetaDefault.GetField(new BGId(5177412833265342228,4611984714222901940));
			return __name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldInt __ID;
	public static BansheeGz.BGDatabase.BGFieldInt _ID
	{
		get
		{
			if(__ID==null || __ID.IsDeleted) __ID=(BansheeGz.BGDatabase.BGFieldInt) MetaDefault.GetField(new BGId(4736580708601719139,16708027799691518382));
			return __ID;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldBool __IsNpc;
	public static BansheeGz.BGDatabase.BGFieldBool _IsNpc
	{
		get
		{
			if(__IsNpc==null || __IsNpc.IsDeleted) __IsNpc=(BansheeGz.BGDatabase.BGFieldBool) MetaDefault.GetField(new BGId(5426706084384915638,1236695535081806498));
			return __IsNpc;
		}
	}
}
