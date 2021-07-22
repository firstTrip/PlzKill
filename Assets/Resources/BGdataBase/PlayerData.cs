using System;
using System.Collections.Generic;
using UnityEngine;
using BansheeGz.BGDatabase;

//=============================================================
//||                   Generated by BansheeGz Code Generator ||
//=============================================================

[AddComponentMenu("BansheeGz/Generated/PlayerData")]
public partial class PlayerData : BGEntityGo
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
			if(_metaDefault==null || _metaDefault.IsDeleted) _metaDefault=BGRepo.I.GetMeta<BansheeGz.BGDatabase.BGMetaRow>(new BGId(5256914230074536749,13187996397603669638));
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
	public System.Single HP
	{
		get
		{
			return _HP[Entity.Index];
		}
		set
		{
			_HP[Entity.Index] = value;
		}
	}
	public System.Single MaxBlood
	{
		get
		{
			return _MaxBlood[Entity.Index];
		}
		set
		{
			_MaxBlood[Entity.Index] = value;
		}
	}
	public System.Single att
	{
		get
		{
			return _att[Entity.Index];
		}
		set
		{
			_att[Entity.Index] = value;
		}
	}
	public System.Single speed
	{
		get
		{
			return _speed[Entity.Index];
		}
		set
		{
			_speed[Entity.Index] = value;
		}
	}
	public System.Single dashPower
	{
		get
		{
			return _dashPower[Entity.Index];
		}
		set
		{
			_dashPower[Entity.Index] = value;
		}
	}
	public System.Single jumpPower
	{
		get
		{
			return _jumpPower[Entity.Index];
		}
		set
		{
			_jumpPower[Entity.Index] = value;
		}
	}
	public System.Single attSpeed
	{
		get
		{
			return _attSpeed[Entity.Index];
		}
		set
		{
			_attSpeed[Entity.Index] = value;
		}
	}
	public System.Int32 dashCnt
	{
		get
		{
			return _dashCnt[Entity.Index];
		}
		set
		{
			_dashCnt[Entity.Index] = value;
		}
	}
	public System.Single dashCoolTime
	{
		get
		{
			return _dashCoolTime[Entity.Index];
		}
		set
		{
			_dashCoolTime[Entity.Index] = value;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldEntityName __name;
	public static BansheeGz.BGDatabase.BGFieldEntityName _name
	{
		get
		{
			if(__name==null || __name.IsDeleted) __name=(BansheeGz.BGDatabase.BGFieldEntityName) MetaDefault.GetField(new BGId(5131886411663381547,17874811727491851399));
			return __name;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __HP;
	public static BansheeGz.BGDatabase.BGFieldFloat _HP
	{
		get
		{
			if(__HP==null || __HP.IsDeleted) __HP=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(5261247971559687039,6381648331135180934));
			return __HP;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __MaxBlood;
	public static BansheeGz.BGDatabase.BGFieldFloat _MaxBlood
	{
		get
		{
			if(__MaxBlood==null || __MaxBlood.IsDeleted) __MaxBlood=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(5167706758831567330,16319482871070299819));
			return __MaxBlood;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __att;
	public static BansheeGz.BGDatabase.BGFieldFloat _att
	{
		get
		{
			if(__att==null || __att.IsDeleted) __att=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(5013235073814893058,12683349894572286081));
			return __att;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __speed;
	public static BansheeGz.BGDatabase.BGFieldFloat _speed
	{
		get
		{
			if(__speed==null || __speed.IsDeleted) __speed=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(4616277905970282001,2353523970049356953));
			return __speed;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __dashPower;
	public static BansheeGz.BGDatabase.BGFieldFloat _dashPower
	{
		get
		{
			if(__dashPower==null || __dashPower.IsDeleted) __dashPower=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(4662344933355631685,4250120464863478157));
			return __dashPower;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __jumpPower;
	public static BansheeGz.BGDatabase.BGFieldFloat _jumpPower
	{
		get
		{
			if(__jumpPower==null || __jumpPower.IsDeleted) __jumpPower=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(5190816218476410183,9141953717292721290));
			return __jumpPower;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __attSpeed;
	public static BansheeGz.BGDatabase.BGFieldFloat _attSpeed
	{
		get
		{
			if(__attSpeed==null || __attSpeed.IsDeleted) __attSpeed=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(4957189377571619166,3665004102090819982));
			return __attSpeed;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldInt __dashCnt;
	public static BansheeGz.BGDatabase.BGFieldInt _dashCnt
	{
		get
		{
			if(__dashCnt==null || __dashCnt.IsDeleted) __dashCnt=(BansheeGz.BGDatabase.BGFieldInt) MetaDefault.GetField(new BGId(4718578035614315806,4473164736814075830));
			return __dashCnt;
		}
	}
	private static BansheeGz.BGDatabase.BGFieldFloat __dashCoolTime;
	public static BansheeGz.BGDatabase.BGFieldFloat _dashCoolTime
	{
		get
		{
			if(__dashCoolTime==null || __dashCoolTime.IsDeleted) __dashCoolTime=(BansheeGz.BGDatabase.BGFieldFloat) MetaDefault.GetField(new BGId(5392769588600640680,8110546161694491553));
			return __dashCoolTime;
		}
	}
}